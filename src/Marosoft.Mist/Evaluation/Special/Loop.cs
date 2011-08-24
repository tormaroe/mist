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
                }
                return new IntExpression(new Token(Tokens.INT, "10"));
            }

        }

        class LoopSpecification
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
                    if (FOR)
                    {
                        _latestLoopSymbol = _args[_index + 1].Token.Text;
                        _scope.AddBinding(_latestLoopSymbol, 0.ToExpression());
                        AddStep(() => 
                        {
                            var val = _scope.Resolve(_latestLoopSymbol);
                            int newval = ((int)val.Value) + 1;
                            _scope.UpdateBinding(new SymbolExpression(_latestLoopSymbol), newval.ToExpression());
                        });
                    }
                    else if (UPTO)
                    {
                        var temp = _latestLoopSymbol;
                        int limit = (int)((IntExpression)_args[_index + 1]).Value;
                        AddLoopTermination(loop => loop.Var(temp) >= limit);
                    }
                }
            }

            private Func<LoopIteration, bool> _loopTerm;
            private void AddLoopTermination(Func<LoopIteration, bool> t)
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

            private bool UPTO
            {
                get
                {
                    return _args[_index].Token.Text == "upto";
                }
            }

            private bool FOR
            {
                get
                {
                    return _args[_index].Token.Text == "for";
                }
            }
        }
    }
}
