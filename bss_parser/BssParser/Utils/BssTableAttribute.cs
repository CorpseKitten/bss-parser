using System;

namespace BssParser.Utils
{
    public class BssTableAttribute : Attribute
    {
        public string Name { get; set; }

        public BssTableAttribute(string name)
        {
            Name = name;
        }
    }
}
