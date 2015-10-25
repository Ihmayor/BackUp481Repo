using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SushiSushi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
    {
        public MainWindow()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            try 
            {
                generateMenuItems();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        public void generateMenuItems()
        {
            MenuCategory SpecialCategory = new MenuCategory("Special", generateMenuCategoryType(0, "Special", new BitmapImage(), false, false, "These are special Items", null));
            MenuCategory SushiCategory = new MenuCategory("Sushi", generateMenuCategoryType(0, "Sushi", new BitmapImage(), true, true, "These are sushi Items", null));
            MenuCategory AppetizerCategory = new MenuCategory("Appetizer", generateMenuCategoryType(0, "Appetizer", new BitmapImage(), true, false, "These are appetizer Items", null));
            MenuCategory FriedCategory = new MenuCategory("Fried", generateMenuCategoryType(0, "Fried", new BitmapImage(), false, false, "These are fried Items", null));
            MenuCategory DrinksCategory = new MenuCategory("Drinks", generateMenuCategoryType(0, "Drinks", new BitmapImage(), false, false, "These are drink Items", null));
            MenuCategory DessertCategory = new MenuCategory("Dessert", generateMenuCategoryType(0, "Desserts", new BitmapImage(), true, false, "These are dessert Items", null));
            List<MenuCategory> TotalItems = new List<MenuCategory>() { SpecialCategory, SushiCategory, AppetizerCategory, FriedCategory, DrinksCategory, DessertCategory };
            
            MainListView.ItemsSource = TotalItems;
        }

        public List<MenuItemObject> generateMenuCategoryType(int ID, string nameOfItem, BitmapImage imageSource, bool isVegan, bool isGlutenFree, string Description, List<string> optionsList)
        {
            List<MenuItemObject> associatedItems = new List<MenuItemObject>();
            for (int i = 0; i < 20; i++)
            {
                associatedItems.Add(new MenuItemObject(ID, nameOfItem, imageSource, isVegan, isGlutenFree, Description, optionsList));
            }
            return associatedItems;
        }


    }
}
