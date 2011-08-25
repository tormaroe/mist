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
            var loopIter = new LoopIteration(loopSpec, loopScope);
            return loopIter.Execute();
        }

        class LoopIteration
        {
            private readonly Bindings _scope;
            private readonly LoopSpecification _spec;
            
            public LoopIteration(LoopSpecification spec, Bindings scope)
            {
                _spec = spec;
                _scope = scope;
            }

            public int Var(string symbol)
            {
                return (int)_scope.Resolve(symbol).Value;
            }

            public Expression Execute()
            {
                while (true)
                {
                    if (_spec.TerminationPointReached(this))
                        break;

                    _spec.Step(this);
                }
                return new IntExpression(new Token(Tokens.INT, "10"));
            }

        }

        class LoopSpecification // TODO: split out parser from Specification
        {
            private int _index;
            private readonly List<Expression> _args;
            private readonly Bindings _scope;
            private string _latestLoopSymbol;

            public LoopSpecification(List<Expression> args, Bindings scope)
            {
                _scope = scope;
                _args = args;
                ParseCommandArgs();
            }
            
            private void ParseCommandArgs()
            {
                for (_index = 0; 
                     _index < _args.Count - 1; 
                     _index = _index + 1)
                {
                    if (FOR) AddFor();
                    else if (UPTO) AddUpto();
                }
            }

            private void AddFor()
            {
                _index++;
                _latestLoopSymbol = _args[_index].Token.Text;
                _scope.AddBinding(_latestLoopSymbol, 0.ToExpression());
                var temp = _latestLoopSymbol;
                AddStep(() =>
                {
                    var val = _scope.Resolve(_latestLoopSymbol);
                    int newval = ((int)val.Value) + 1;
                    _scope.UpdateBinding(new SymbolExpression(temp), newval.ToExpression());
                });
            }

            private void AddUpto()
            {
                var temp = _latestLoopSymbol;
                var limit = (int)_args[_index + 1].Value;
                AddLoopTermination(loop => loop.Var(temp) >= limit);
            }

            private Predicate<LoopIteration> _loopTerm;
            private void AddLoopTermination(Predicate<LoopIteration> t)
            {
                _loopTerm = t;
            }
            public bool TerminationPointReached(LoopIteration iter)
            {
                return _loopTerm(iter);
            }
            
            private Action _step;
            private void AddStep(Action s)
            {
                _step = s;
            }
            public void Step(LoopIteration iter)
            {
                _step();
            }

            private bool UPTO { get { return CurrentIs("upto"); } }
            private bool FOR { get { return CurrentIs("for"); } }

            private bool CurrentIs(string symbol)
            {
                return _args[_index].Token.Text == symbol;
            }
        }
    }
}
