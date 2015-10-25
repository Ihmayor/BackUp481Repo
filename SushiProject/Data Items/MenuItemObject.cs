using System;                  
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SushiSushi
{
    public class MenuItemObject
    {
        public string Name { get; set; }
        public BitmapImage Image { get; set; }
        public bool isVegan { get; set; }
        public bool isGlutenFree { get; set; }
        public bool hasOptions { get; set; }
        public List<string> options { get; set; }
        public string selectedOption = "";
        public int countOfItem { get; set; }
        public int id;
        public string Price { get; set; }
        public string Description { get; set; }
        
        public MenuItemObject(int ID, double priceOfItem, string nameOfItem , BitmapImage imageSource, bool Vegan, bool GlutenFree, string description, List<string> optionsList)
        {
            id = ID;
            Price = "Price: "+priceOfItem.ToString("0.00")+"$";
            Name = nameOfItem;
            Image = imageSource;
            Description = description;
            isVegan = Vegan;
            isGlutenFree = GlutenFree;
            if (options == null || options.Count == 0)
                hasOptions = false;
            else
            {
                hasOptions = true;
                options = optionsList;
            }
        }

    }
}
