using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Application.Dtos.Inputs
{
    public class CheckboxRole
    {
        public CheckboxRole() { }

        public CheckboxRole(Guid id, string name, bool selected)
        {
            Id = id;
            Name = name;
            Selected = selected;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; }
    }
}
