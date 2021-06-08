using System;

namespace Zen.CLI.Commands.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    sealed class CommandAttribute : Attribute
    {
        public string Name { get; }
        public string Description { get; set; }
        public CommandAttribute()
        {

        }
        
        
        public CommandAttribute(string name)
        {
            
        }
    }
}