using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Applications.Navigation
{
    /// <summary>
    /// MenuDefinition用来区分导航的分类，如"SidebarMenu", "HeaderMenu"等。
    /// </summary>
    public class MenuDefinition : MenuDefinition<MenuItemDefinition>
    {
        public MenuDefinition([NotNull] string name) : base(name)
        {
        }
    }

    public class MenuDefinition<TMenuItemDefinition> where TMenuItemDefinition : MenuItemDefinition
    {
        public string Name { get; set; }
        public List<TMenuItemDefinition> Items { get; set; }

        public MenuDefinition([NotNull]string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Name = name;
            Items = new List<TMenuItemDefinition>();
        }

        public MenuDefinition<TMenuItemDefinition> AddItem([NotNull]TMenuItemDefinition menuItem)
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
