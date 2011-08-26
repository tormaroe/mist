using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;
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
                while (!_spec.TerminationPointReached(this))
                {
                    _spec.AccumulateResults(this);
                    _spec.Step(this);
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
                     _index = _index + 1)
                {
                    switch (_args[_index].Token.Text)
                    {
                        case "for": AddFor(); break;
                        case "upto": AddUpto(); break;
                        case "below": AddBelow(); break;
                        case "sum": AddSum(); break;
                        case "count": AddCount(); break;
                        case "in": AddIn(); break;
                        case "until": AddUntil(); break;
                        case "while": AddWhile(); break;
                        case "from": AddFrom(); break;
                        case "to": AddTo(); break;
                        default:
                            throw new Exception("Unexpected expression " + _args[_index].Token + " in loop specification");
                    }

                }
            }

            private bool NextTokenIsOneOf(params string[] symbols)
            {
                string next = _args[_index + 1].Token.Text;
                return symbols.Any(s => s.Equals(next));
            }

            #region ADD AND INITIALIZE LOOP VARIABLES
            private void AddFor()
            {
                _index++;
                _latestLoopSymbol = _args[_index].Token.Text;

                if(!NextTokenIsOneOf("from"))
                    _scope.AddBinding(_latestLoopSymbol, 0.ToExpression());

                if (!NextTokenIsOneOf("in"))
                {
                    var temp = _latestLoopSymbol;
                    _spec.AddStep(() =>
                    {
                        var val = _scope.Resolve(temp);
                        int newval = ((int)val.Value) + 1;
                        _scope.UpdateBinding(new SymbolExpression(temp), newval.ToExpression());
                    });
                }
            }

            private void AddIn()
            {
                _index++;
                Expression list = _args[_index].Evaluate(_scope);

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
                        _spec.AddLoopTermination(loop => true);
                    }
                };
                step.Invoke(); // Invoke one time to get the first value
                _spec.AddStep(step);                                
            }

            private void AddFrom()
            {
                _index++;
                var fromValue = _args[_index].Evaluate(_scope);
                _scope.AddBinding(_latestLoopSymbol, fromValue);
            }
            #endregion

            #region LIMITS: To / While / Until / Upto / Below
            private void AddTo()
            {
                AddLimit((loopvar, to) => _scope.GetFunction(">").Call(loopvar, to).IsTrue);
            }
            
            private void AddWhile()
            {
                AddLimit((loopvar, limitExpr) => !limitExpr.IsTrue);
            }

            private void AddUntil()
            {
                AddLimit((loopvar, limitExpr) => limitExpr.IsTrue);
            }

            private void AddUpto()
            {
                AddLimit((a, b) => _scope.GetFunction(">").Call(a, b).IsTrue);
            }

            private void AddBelow()
            {
                AddLimit((a, b) => _scope.GetFunction(">=").Call(a, b).IsTrue);
            }

            private void AddLimit(Func<Expression, Expression, bool> test) 
            {
                _index++;
                var variable = _latestLoopSymbol;
                var limit = _args[_index];
                _spec.AddLoopTermination(loop => test(_scope.Resolve(variable), limit.Evaluate(_scope)));
            }
            #endregion

            #region ACCUMULATORS
            private void AddSum()
            {
                _index++;
                var temp = _latestLoopSymbol;
                _spec.AddAccumulation(0, (int acc, LoopIteration loop) => acc + (int)_scope.Resolve(temp).Value);
            }

            private void AddCount()
            {
                _index++;
                var temp = _latestLoopSymbol;
                _spec.AddAccumulation(0, (int acc, LoopIteration loop) => acc + (_scope.Resolve(temp).IsNil ? 0 : 1));
            }
            #endregion
        }

        /// <summary>
        /// Specifies what will happen in the loop, and know when to terminate.
        /// Also carries any accumulated value.
        /// </summary>
        class LoopSpecification
        {            
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
            
            private Predicate<LoopIteration> _loopTerm;
            public void AddLoopTermination(Predicate<LoopIteration> t)
            {
                _loopTerm = t;
            }
            public bool TerminationPointReached(LoopIteration iter)
            {
                if(_loopTerm != null)
                    return _loopTerm(iter);
                return false; // loop forever if you have to :-)
            }
            
            private Action _step;
            public void AddStep(Action s)
            {
                _step = s;
            }
            public void Step(LoopIteration iter)  // argument not needed?
            {
                _step();
            }

            private object _accumulatedValue;
            private Action<LoopIteration> _stepAccumulator;
            public void AddAccumulation<T>(T startValue, Func<T, LoopIteration, T> f)
            {
                if (_stepAccumulator == null)
                {
                    _accumulatedValue = startValue;
                    _stepAccumulator = loop => _accumulatedValue = f((T)_accumulatedValue, loop);                    
                }
                else
                    throw new NotImplementedException("Composing of multiple accumulator steps not implemented");
            }
            public void AccumulateResults(LoopIteration iter)
            {
                if (_stepAccumulator != null)
                    _stepAccumulator(iter);
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
    }
}