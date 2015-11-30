﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiSushi
{
    public class MenuCategory
    {
        public List<MenuItemData> AssociatedMenuItems { set; get ;  }
        public string CategoryName { set; get; }

        public MenuCategory(string name, List<MenuItemData> setMenuItems)
        {
            CategoryName = name;
            AssociatedMenuItems = setMenuItems;
        }
   
  
    }
}