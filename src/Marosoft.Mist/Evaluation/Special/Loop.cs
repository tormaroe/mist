using Marosoft.Mist.Parsing;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Marosoft.Mist.Evaluation.Special
{
    [SpecialForm("loop")]
    public class Loop : SpecialForm
    {
        public override Expression Call(Expression expr)
        {
            var loopScope = new Bindings() { ParentScope = Environment.CurrentScope };
            var loopSpec = new LoopSpecification(expr.Elements.Skip(1).ToList(), loopScope);
            var loopIter = new LoopIteration(loopSpec);
            return loopIter.Execute();
        }

        /// <summary>
        /// Represents the actual loop,
        /// delegates what happens in each iteration to LoopSpecification
        /// </summary>
        class LoopIteration
        {
            private readonly LoopSpecification _spec;
            
            public LoopIteration(LoopSpecification spec)
            {
                _spec = spec;
            }

            public Expression Execute()
            {
                while (!_spec.TerminationPointReached())
                {
                    if (_spec.ShouldAccumulate())
                    {
                        _spec.AccumulateResults();
                        _spec.SideEffects.Invoke();
                    }
                    _spec.Step.Invoke();
                }
                return _spec.Result;
            }
        }

        /// <summary>
        /// Parses the loop expression and builds a LoopSpecification
        /// to be used when looping
        /// </summary>
        class LoopParser
        {
            private readonly LoopSpecification _spec;
            private readonly List<Expression> _args;
            private readonly Bindings _scope;
            private int _index;
            private string _latestLoopSymbol;
            
            public LoopParser(LoopSpecification spec, List<Expression> args, Bindings scope)
            {
                _scope = scope;
                _args = args;            
                _spec = spec;
            }

            public void ParseCommandArgs()
            {
                for (_index = 0;
                     _index < _args.Count - 1;
                     _index++)
                {
                    switch (_args[_index].Token.Text)
                    {
                        case "for": For(); break;
                        case "upto": Upto(); break;
                        case "below": Below(); break;
                        case "sum": Sum(); break;
                        case "count": Count(); break;
                        case "in": In(); break;
                        case "until": Until(); break;
                        case "while": While(); break;
                        case "from": From(); break;
                        case "to": To(); break;
                        case "collect": Collect(); break;
                        case "when": When(); break;
                        case "do": Do(); break;
                        case "repeat": Repeat(); break;
                        default:
                            throw new Exception("Unexpected expression " + _args[_index].Token + " in loop specification");
                    }

                }
            }

            private Expression ConsumeNextExpression()
            {
                return _args[++_index];
            }

            private bool NextTokenIsOneOf(params string[] symbols)
            {
                string next = _args[_index + 1].Token.Text;
                return symbols.Any(s => s.Equals(next));
            }

            #region ADD AND INITIALIZE LOOP VARIABLES
            private void For()
            {
                _latestLoopSymbol = ConsumeNextExpression().Token.Text;

                if(!NextTokenIsOneOf("from"))
                    _scope.AddBinding(_latestLoopSymbol, 0.ToExpression());

                if (!NextTokenIsOneOf("in"))
                {
                    var temp = _latestLoopSymbol;
                    _spec.Step.Add(() =>
                    {
                        var val = _scope.Resolve(temp);
                        int newval = ((int)val.Value) + 1;
                        _scope.UpdateBinding(new SymbolExpression(temp), newval.ToExpression());
                    });
                }
            }

            private void In()
            {
                Expression list = ConsumeNextExpression().Evaluate(_scope);

                // Add list iterator step
                IEnumerable<Expression> listElementsCopyForIteration = list.Elements;
                var tempSymbol = new SymbolExpression(_latestLoopSymbol);
                Action step = () =>
                {
                    var nextElement = listElementsCopyForIteration.FirstOrDefault();
                    if (nextElement != null)
                    {
                        _scope.UpdateBinding(tempSymbol, nextElement);
                        listElementsCopyForIteration = listElementsCopyForIteration.Skip(1);
                    }
                    else
                    {
                        // Add terminsation step when list is empty
                        _spec.AddLoopTermination(() => true);
                    }
                };
                step.Invoke(); // Invoke one time to get the first value
                _spec.Step.Add(step);                                
            }

            private void From()
            {
                var fromValue = ConsumeNextExpression().Evaluate(_scope);
                _scope.AddBinding(_latestLoopSymbol, fromValue);
            }
            #endregion

            #region LIMITS: To / While / Until / Upto / Below
            private void To()
            {
                AddLimitForLatestLoopVariable((loopvar, to) => _scope.GetFunction(">").Call(loopvar, to).IsTrue);
            }
            
            private void While()
            {
                AddLimit(limitExpr => !limitExpr.IsTrue);
            }

            private void Until()
            {
                AddLimit(limitExpr => limitExpr.IsTrue);
            }

            private void Upto()
            {
                AddLimitForLatestLoopVariable((a, b) => _scope.GetFunction(">").Call(a, b).IsTrue);
            }

            private void Below()
            {
                AddLimitForLatestLoopVariable((a, b) => _scope.GetFunction(">=").Call(a, b).IsTrue);
            }

            private void Repeat()
            {
                int counter = 0;
                AddLimit(limitExpr => counter++ >= (int)limitExpr.Value);
            }

            private void AddLimitForLatestLoopVariable(Func<Expression, Expression, bool> test) 
            {
                var variable = _latestLoopSymbol;
                AddLimit(expr => test(_scope.Resolve(variable), expr));
            }

            private void AddLimit(Func<Expression, bool> test)
            {
                var limit = ConsumeNextExpression();
                _spec.AddLoopTermination(() => test(limit.Evaluate(_scope)));
            }
            #endregion

            #region ACCUMULATORS
            private void When()
            {
                var condition = ConsumeNextExpression();
                _spec.AddAccumulationCondition(() => condition.Evaluate(_scope).IsTrue);
            }

            private void Sum()
            {
                var temp = ConsumeNextExpression();
                _spec.AddAccumulation(0, acc => acc + (int)temp.Evaluate(_scope).Value);
            }

            private void Count()
            {
                var temp = ConsumeNextExpression();
                _spec.AddAccumulation(0, acc => acc + (temp.Evaluate(_scope).IsNil ? 0 : 1));
            }
            private void Collect()
            {
                var exprToAdd = ConsumeNextExpression();
                _spec.AddAccumulation(new ListExpression(), acc =>
                {
                    acc.Elements.Add(exprToAdd.Evaluate(_scope));
                    return acc;
                });
            }
            #endregion
            
            private void Do()
            {
                var sideEffect = ConsumeNextExpression();
                _spec.SideEffects.Add(() => sideEffect.Evaluate(_scope));
            }
        }

        /// <summary>
        /// Specifies what will happen in the loop, and know when to terminate.
        /// Also carries any accumulated value.
        /// </summary>
        class LoopSpecification
        {
            public readonly ActionPack Step = new ActionPack();
            public readonly ActionPack SideEffects = new ActionPack();

            public LoopSpecification(List<Expression> args, Bindings scope)
            {
                try
                {
                    new LoopParser(this, args, scope).ParseCommandArgs();
                }
                catch (Exception ex)
                {
                    throw new MistException("Error in loop specification (" + ex.Message + ")");
                }
            }
            
            private Func<bool> _loopTerm;
            public void AddLoopTermination(Func<bool> t)
            {
                _loopTerm = t;
            }
            public bool TerminationPointReached()
            {
                if(_loopTerm != null)
                    return _loopTerm();
                return false; // loop forever if you have to :-)
            }            

            private Func<bool> _accumulationCondition;
            public void AddAccumulationCondition(Func<bool> condition)
            {
                if (_accumulationCondition != null)
                    throw new MistException("Support for multiple accumulation conditions in loop not implemented");
                _accumulationCondition = condition;
            }
            public bool ShouldAccumulate()
            {
                if(_accumulationCondition != null)
                    return _accumulationCondition.Invoke();
                return true;
            }

            private object _accumulatedValue;            
            private Action _stepAccumulator;
            public void AddAccumulation<T>(T startValue, Func<T, T> f)
            {
                if (_stepAccumulator == null)
                {
                    _accumulatedValue = startValue;
                    _stepAccumulator = () => _accumulatedValue = f((T)_accumulatedValue);                    
                }
                else
                    throw new NotImplementedException("Composing of multiple accumulator steps not implemented");
            }
            public void AccumulateResults()
            {
                if (_stepAccumulator != null)
                    _stepAccumulator();
            }


            public Expression Result
            {
                get
                {
                    if (_accumulatedValue == null)
                        return NIL.Instance;
                    return _accumulatedValue.ToExpression();
                }
            }
        }

        /// <summary>
        /// Holds zero, one or more Actions, 
        /// which can all be run in sequence
        /// by calling Invoke.
        /// </summary>
        class ActionPack
        {
            private Action a;
            public void Add(Action toAdd)
            {
                if (a == null)
                    a = toAdd;
                else
                {
                    var temp = a;
                    a = () => { temp(); toAdd(); };
                }
            }
            public void Invoke()
            {
                if (a != null)
                    a.Invoke();
            }
        }
    }
}
