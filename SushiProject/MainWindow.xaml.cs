using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
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
        public ObservableCollection<MenuItemObject> OrderedItems { get { return orderedItems; } }
        private ObservableCollection<MenuItemObject> orderedItems = new ObservableCollection<MenuItemObject>();
        
        public ObservableCollection<MenuItemObject> DeliveredItems { get { return deliveredItems; } }
        private ObservableCollection<MenuItemObject> deliveredItems = new ObservableCollection<MenuItemObject>();

        public ObservableCollection<MenuItemObject> SelectedItems { get { return selectedItems; } }
        private ObservableCollection<MenuItemObject> selectedItems = new ObservableCollection<MenuItemObject>();
        public MainWindow()
        {
                InitializeComponent();
                MenuItemControl.CompleteClicked += MenuItemControl_CompleteClicked;
                generateMenuItems();
        }

        
        public void generateMenuItems()
        {
          
            MenuCategory SpecialCategory = new MenuCategory("Special", generateMenuCategoryType(0, "Special",       new BitmapImage(new Uri(@"pack://application:,,,/Resources/SpecialSushi.png")), false, false, "These are special Items", null));
            MenuCategory SushiCategory = new MenuCategory("Sushi", generateMenuCategoryType2(0, "Sushi",             new BitmapImage(new Uri(@"pack://application:,,,/Resources/SalmonSushi.png")), true, true, "These are sushi Items", null));
            MenuCategory AppetizerCategory = new MenuCategory("Appetizers", generateMenuCategoryType(0, "Appetizer", new BitmapImage(new Uri(@"pack://application:,,,/Resources/Gyoza.png")), true, false, "These are appetizer Items", null));
            MenuCategory FriedCategory = new MenuCategory("Fried", generateMenuCategoryType2(0, "Fried",             new BitmapImage(new Uri(@"pack://application:,,,/Resources/ShrimpTempura.png")), false, false, "These are fried Items", null));
            MenuCategory DrinksCategory = new MenuCategory("Drinks", generateMenuCategoryType3(0, "Drinks",          new BitmapImage(new Uri(@"pack://application:,,,/Resources/CocaCola.png")), false, false, "These are drink Items", null));
            MenuCategory DessertCategory = new MenuCategory("Dessert", generateMenuCategoryType(0, "Desserts",      new BitmapImage(new Uri(@"pack://application:,,,/Resources/Mochi.png")), true, false, "These are dessert Items", null));
            List<MenuCategory> TotalItems = new List<MenuCategory>() { SpecialCategory, SushiCategory, AppetizerCategory, FriedCategory, DrinksCategory, DessertCategory };
            
            MainListView.ItemsSource = TotalItems;
        }

        public List<MenuItemObject> generateMenuCategoryType(int ID, string nameOfItem, BitmapImage imageSource, bool isVegan, bool isGlutenFree, string Description, List<string> optionsList)
        {
            List<MenuItemObject> associatedItems = new List<MenuItemObject>();
            for (int i = 0; i < 9; i++)
            {
                associatedItems.Add(new MenuItemObject(ID, 5, nameOfItem, imageSource, isVegan, isGlutenFree, Description, optionsList));
            }
            return associatedItems;
        }
        public List<MenuItemObject> generateMenuCategoryType2(int ID, string nameOfItem, BitmapImage imageSource, bool isVegan, bool isGlutenFree, string Description, List<string> optionsList)
        {
            List<MenuItemObject> associatedItems = new List<MenuItemObject>();
            for (int i = 0; i < 20; i++)
            {
                associatedItems.Add(new MenuItemObject(ID, 4.20, nameOfItem, imageSource, isVegan, isGlutenFree, Description, optionsList));
            }
            return associatedItems;
        }
        public List<MenuItemObject> generateMenuCategoryType3(int ID, string nameOfItem, BitmapImage imageSource, bool isVegan, bool isGlutenFree, string Description, List<string> optionsList)
        {
            List<MenuItemObject> associatedItems = new List<MenuItemObject>();
            for (int i = 0; i < 6; i++)
            {
                associatedItems.Add(new MenuItemObject(ID, 4.20, nameOfItem, imageSource, isVegan, isGlutenFree, Description, optionsList));
            }
            return associatedItems;
        }



        void MenuItemControl_CompleteClicked(object sender, MenuItemObject addItem)
        {
            selectedItems.Add(addItem);
        }

        private void StackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            Console.WriteLine((sender as Grid).DataContext);
        }



    }
}
