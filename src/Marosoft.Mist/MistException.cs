using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Marosoft.Mist.Evaluation;
using Marosoft.Mist.Parsing;

namespace Marosoft.Mist
{
    /**
     * 
     * KEEP ALL EXCEPTIONS IN THIS FILE
     * 
     ***/

    public class ParseException : MistException
    {
        public ParseException(string format, params object[] args)
            : base(string.Format(format, args))
        {
            
        }
    }

    public class SymbolResolveException : MistException
    {
        public SymbolResolveException(string symbol)
            : base(string.Format("Unable to resolve '{0}' in current scope", symbol))
        {
        }
    }

    public class FunctionEvaluationPreconditionException : MistException
    {
        public FunctionEvaluationPreconditionException(string symbol, IEnumerable<Expression> args)
            : base(string.Format("Arguments to '{0}' does not comply with preconditions.\nArguments: {1}", symbol, args.Aggregate("", (acc, n) => acc + " " + n.ToString()).Substring(1)))
        {

        }
    }

    public class MistApplicationException : MistException
    {
        public MistApplicationException(IEnumerable<string> message)
            : base(message.Aggregate((a, b) => a + " " + b))
        {

        }
    }

    public class MistException : Exception
    {
        public MistException(string message)
            : base(message)
        {

        }        
    }
}
