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
        private string name { get; set; }
        private BitmapImage source { get; set; }
        private bool isVegan { get; set; }
        private bool isGlutenFree { get; set; }
        private bool hasOptions { get; set; }
        private List<string> options { get; set; }
        public int countOfItem { get; set; }
        public int id;
        public string description { get; set; }
        
        public MenuItemObject(int ID, string nameOfItem , BitmapImage imageSource, bool Vegan, bool GlutenFree, string Description, List<string> optionsList)
        {
            id = ID;
            name = nameOfItem;
            source = imageSource;
            description = Description;
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
