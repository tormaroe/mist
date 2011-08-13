using System;

namespace Marosoft.Mist.Evaluation
{
    public class SpecialFormAttribute : Attribute
    {
        public SpecialFormAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}
