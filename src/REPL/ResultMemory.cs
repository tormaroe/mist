using Marosoft.Mist.Parsing;
using Marosoft.Mist.Evaluation;

namespace Marosoft.Mist.Repl
{
    class ResultMemory
    {
        private readonly Bindings _global;

        private SymbolExpression m1 = new SymbolExpression("*m*");
        private SymbolExpression m2 = new SymbolExpression("*m2*");
        private SymbolExpression m3 = new SymbolExpression("*m3*");

        public ResultMemory(Bindings global)
        {
            _global = global;

            global.AddBinding(m1);
            global.AddBinding(m2);
            global.AddBinding(m3);
        }

        public void UpdateReplMemory(Expression expr)
        {
            _global.UpdateBinding(m3, _global.Resolve(m2.Token.Text));
            _global.UpdateBinding(m2, _global.Resolve(m1.Token.Text));
            _global.UpdateBinding(m1, expr);
        }
    }
}
