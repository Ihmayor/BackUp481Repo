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
        public string SelectedOption { get { return selectedOption; } }
        private string selectedOption = "Modified Selection/Blank";
        public int countOfItem { get; set; }
        public int id;
        public double NumPrice { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        
        public MenuItemObject(int ID, double priceOfItem, string nameOfItem , BitmapImage imageSource, bool Vegan, bool GlutenFree, string description, List<string> optionsList)
        {
            id = ID;
            NumPrice = priceOfItem;
            Price = "Price: "+priceOfItem.ToString("0.00")+"$";
            Name = nameOfItem;
            Image = imageSource;
            Description = description;
            isVegan = Vegan;
            isGlutenFree = GlutenFree;
            options = optionsList;

            if (options == null)
                hasOptions = false;
            else
            {
                hasOptions = true;
            }
        }
        public void setSelectedOption (int optionIndex)
        {
            selectedOption = options.ToArray()[optionIndex];
        }

        public bool isSameMenuItem (MenuItemObject MenuItemObjectToCompare)
        {
            bool isSame = false;
            if (Name == MenuItemObjectToCompare.Name && selectedOption == MenuItemObjectToCompare.selectedOption)
                isSame = true;
            return isSame;
        }

    }
}
