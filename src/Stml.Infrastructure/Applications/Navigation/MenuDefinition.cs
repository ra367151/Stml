using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Applications.Navigation
{
    /// <summary>
    /// 菜单类别，如SidebarMenu, TopbarMenu等
    /// </summary>
    public class MenuDefinition
    {
        public string Name { get; set; }
        public List<MenuItemDefinition> Items { get; set; }

        public MenuDefinition([NotNull]string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Name = name;
            Items = new List<MenuItemDefinition>();
        }

        public MenuDefinition AddItem([NotNull]MenuItemDefinition menuItem)
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
