using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Applications.Navigation
{
    /// <summary>
    /// MenuGroup，如"SidebarMenu", "HeaderMenu"等。
    /// </summary>
    public class MenuGroup : MenuGroup<MenuItem>
    {
        public MenuGroup([NotNull] string name) : base(name)
        {
        }
    }

    public class MenuGroup<TMenuItem> where TMenuItem : MenuItem
    {
        public string Name { get; set; }
        public List<TMenuItem> Items { get; set; }

        public MenuGroup([NotNull]string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Name = name;
            Items = new List<TMenuItem>();
        }

        public MenuGroup<TMenuItem> AddItem([NotNull]TMenuItem menuItem)
        {
            Check.NotNull(menuItem, nameof(menuItem));
            Items.Add(menuItem);
            return this;
        }

        public void RemoveItem(string name)
        {
            Items.RemoveAll(m => m.Name == name);
        }
    }
}
