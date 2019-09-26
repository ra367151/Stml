using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.EPPlus
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SheetAttribute : Attribute
    {
        private string name;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public SheetAttribute(string name)
        {
            this.name = name;
        }
    }
}
