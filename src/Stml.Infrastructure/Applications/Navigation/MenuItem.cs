using JetBrains.Annotations;
using Stml.Infrastructure;
using Stml.Infrastructure.Collection.Extensions;
using Stml.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stml.Infrastructure.Applications.Navigation
{
    public class MenuItem
    {
        /// <summary>
        /// Unique name of the menu item in the application. Can be used to find this menu item later.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The URL to navigate when this menu item is selected. Optional.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Icon of the menu item if exists. Optional.
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Target of menu item. Can be "_blank", "_self", "_parent", "_top" or a frame.
        /// </summary>
        public string Target { get; set; }

        public string RequiredPermission { get; set; }

        /// <summary>
        /// Returns true if this menu has no child MenuItemDefinitions.
        /// </summary>
        public bool IsLeaf => Items.IsNullOrEmpty();

        public List<MenuItem> Items { get; private set; }

        public MenuItem([NotNull]string name, string url = null, string icon = null, string requiredPermission = null, string target = null)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Name = name;
            Url = url;
            Icon = icon;
            RequiredPermission = requiredPermission;
            Target = target;
            Items = new List<MenuItem>();
        }

        public MenuItem AddItem([NotNull]MenuItem menuItem)
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
