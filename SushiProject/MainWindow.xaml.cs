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
using System.Windows.Media.Animation;
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
        #region List Associated with SideBar
        public static ObservableCollection<MenuItemObject> OrderedItems { get { return orderedItems; } }
        private static ObservableCollection<MenuItemObject> orderedItems = new ObservableCollection<MenuItemObject>();

        public static ObservableCollection<MenuItemObject> DeliveredItems { get { return deliveredItems; } }
        private static ObservableCollection<MenuItemObject> deliveredItems = new ObservableCollection<MenuItemObject>();

        public static ObservableCollection<MenuItemObject> SelectedItems { get { return selectedItems; } }
        private static ObservableCollection<MenuItemObject> selectedItems = new ObservableCollection<MenuItemObject>();
        #endregion

        #region Events/Methods Involved with Loaded Items
        public MainWindow()
        {
                InitializeComponent();
                MenuItemControl.CompleteClicked += MenuItemControl_CompleteClicked;
                SidebarItemControl.OnMinusButtonPressed += SidebarItemControl_MinusButton;
                SidebarItemControl.OnPlusButtonPressed += SidebarItemControl_PlusButton;
                generateMenuItems();
        }


        private void OrderDialogWindow_Loaded(object sender, RoutedEventArgs e)
        {
            OrderDialog.onDialogButtonClick += OrderDialog_DialogButtonClick;
            OrderDialogWindow.SelectedList.ItemsSource = selectedItems;
     
        }

        #endregion

        #region Control Click Events Both inside Window and Outside
        
        private void OrderDialog_DialogButtonClick(object sender, bool confirmed)
        {
           if (confirmed)
           {
               foreach (MenuItemObject item in selectedItems)
               {
                    MenuItemObject foundItem = orderedItems.FirstOrDefault(x => x.isSameMenuItem(item));
                    if (foundItem == null)
                    {
                        item.countOfItem++;
                        orderedItems.Add(item);
                    }
                    else
                    {
                        foundItem.countOfItem++;
                        updateMenuItem(orderedItems,foundItem);
                    }
               }
               EmptySelected();
               OrderDialogWindow.Visibility = System.Windows.Visibility.Hidden;
               GrayOutWindow.Visibility = System.Windows.Visibility.Hidden;
      
           }

           else
           {
               OrderDialogWindow.Visibility = System.Windows.Visibility.Hidden;
               GrayOutWindow.Visibility = System.Windows.Visibility.Hidden;
      
           }
        
        }

        void MenuItemControl_CompleteClicked(object sender, MenuItemObject addItem)
        {
            MenuItemObject foundItem = selectedItems.FirstOrDefault(x => x.isSameMenuItem(addItem));
            if (foundItem == null)
            {
                addItem.countOfItem++;
                selectedItems.Add(addItem);
            }
            else
            {
                foundItem.countOfItem++;
                updateMenuItem(selectedItems, foundItem);
            }
        }


        private void SidebarItemControl_PlusButton(object sender, MenuItemObject chosenItem)
        {
            chosenItem.countOfItem++;
            MenuItemObject foundItem = selectedItems.FirstOrDefault(x => x.isSameMenuItem(chosenItem));
            updateMenuItem(selectedItems,foundItem);
        }


        private void SidebarItemControl_MinusButton(object sender, MenuItemObject chosenItem)
        {
            MenuItemObject foundItem = selectedItems.FirstOrDefault(x => x.isSameMenuItem(chosenItem));
            foundItem.countOfItem--;
            if (foundItem.countOfItem == 0)
            {
                selectedItems.Remove(foundItem);
            }
            else
            {
                updateMenuItem(selectedItems,foundItem);
            }

        }

        private void ConfirmOrder_Click(object sender, RoutedEventArgs e)
        {
            OrderDialogWindow.Visibility = System.Windows.Visibility.Visible;
            GrayOutWindow.Visibility = System.Windows.Visibility.Visible;
        }

        
     

        private void SpecialButton_Click(object sender, RoutedEventArgs e)
        {
            var scrollViewer = GetDescendantByType(MainListView, typeof(ScrollViewer)) as ScrollViewer;
            scrollViewer.ScrollToTop();
        }

        private void AppetizersButton_Click(object sender, RoutedEventArgs e)
        {
            jumpToCategory("Appetizers");
        }

      
        private void SushiButton_Click(object sender, RoutedEventArgs e)
        {
            jumpToCategory("Sushi");
        }

        private void FriedButton_Click(object sender, RoutedEventArgs e)
        {
            jumpToCategory("Fried");

        }

        private void DrinksButton_Click(object sender, RoutedEventArgs e)
        {

            jumpToCategory("Drinks");
        }


        private void DessertsButton_Click(object sender, RoutedEventArgs e)
        {
            jumpToCategory("Desserts");
        }
        #endregion

        #region Data Generation Methods
        public void generateMenuItems()
        {
            MenuCategory SpecialCategory = new MenuCategory("Specials", generateMenuCategoryType(0, "Special",       new BitmapImage(new Uri(@"pack://application:,,,/Resources/SpecialSushi.png")), false, false, "These are special Items", null));
            MenuCategory SushiCategory = new MenuCategory("Sushi", generateMenuCategoryType2(0, "Sushi",             new BitmapImage(new Uri(@"pack://application:,,,/Resources/SalmonSushi.png")), true, true, "These are sushi Items", null));
            MenuCategory AppetizerCategory = new MenuCategory("Appetizers", generateMenuCategoryType(0, "Appetizer", new BitmapImage(new Uri(@"pack://application:,,,/Resources/Gyoza.png")), true, false, "These are appetizer Items", null));
            MenuCategory FriedCategory = new MenuCategory("Fried", generateMenuCategoryType2(0, "Fried",             new BitmapImage(new Uri(@"pack://application:,,,/Resources/ShrimpTempura.png")), false, false, "These are fried Items", null));
            MenuCategory DrinksCategory = new MenuCategory("Drinks", generateMenuCategoryType3(0, "Drinks",          new BitmapImage(new Uri(@"pack://application:,,,/Resources/CocaCola.png")), false, false, "These are drink Items", null));
            MenuCategory DessertCategory = new MenuCategory("Desserts", generateMenuCategoryType(0, "Desserts",      new BitmapImage(new Uri(@"pack://application:,,,/Resources/Mochi.png")), true, false, "These are dessert Items", null));
            List<MenuCategory> TotalItems = new List<MenuCategory>() { SpecialCategory, AppetizerCategory,SushiCategory, FriedCategory, DrinksCategory, DessertCategory };
            
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

        #endregion

        #region Misc Helper Methods

        //Triggers the item to reload.
        private void updateMenuItem(ObservableCollection<MenuItemObject> ListInvolved, MenuItemObject itemToUpdate)
        {
            int indexMaintain = ListInvolved.IndexOf(itemToUpdate);
            ListInvolved.Remove(itemToUpdate);
            ListInvolved.Insert(indexMaintain, itemToUpdate);
        }


        private void ExpandCollapseElement(FrameworkElement inputElem, double from, double to, double inDurationInMilli = 250) //All duration are set to 1/4 second
        {
            DoubleAnimation anim = new DoubleAnimation(from, to, TimeSpan.FromMilliseconds(inDurationInMilli));
            inputElem.BeginAnimation(FrameworkElement.HeightProperty, anim);
        }

        private void EmptySelected()
        {
            ObservableCollection<MenuItemObject> toRemove = new ObservableCollection<MenuItemObject>();
            foreach (MenuItemObject item in selectedItems)
            {
                item.countOfItem = 0;
                toRemove.Add(item);
            }
            
            foreach(MenuItemObject item in toRemove)
            {
                selectedItems.Remove(item);
            }
        }

        private double calculateOffset(int indexOfCategory)
        {
            double verticalOffset = 0;
            for (int i = 0; i < indexOfCategory; i++)
            {
                MenuCategory currItem = (MenuCategory)MainListView.Items[i];
                ListBoxItem lbi = MainListView.ItemContainerGenerator.ContainerFromItem(currItem) as ListBoxItem;
                verticalOffset += lbi.ActualHeight;
            }
            return verticalOffset;
        }

        private void jumpToCategory(string categoryName)
        {
            MenuCategory foundItem = FindMenuCategoryControl(categoryName);
            if (foundItem != null)
            {
                var scrollViewer = GetDescendantByType(MainListView, typeof(ScrollViewer)) as ScrollViewer;
                scrollViewer.ScrollToVerticalOffset(calculateOffset(MainListView.Items.IndexOf(foundItem)));
            }

        }

        private MenuCategory FindMenuCategoryControl(string CategoryToFind)
        {
            MenuCategory foundItem = null;
            foreach (MenuCategory item in MainListView.Items)
            {
                if (item.CategoryName == CategoryToFind)
                    foundItem = item;
            }
            return foundItem;
        }

        //http://stackoverflow.com/questions/10293236/accessing-the-scrollviewer-of-a-listbox-from-c-sharp Author: punker76
        private static Visual GetDescendantByType(Visual element, Type type)
        {
            if (element == null)
            {
                return null;
            }
            if (element.GetType() == type)
            {
                return element;
            }
            Visual foundElement = null;
            if (element is FrameworkElement)
            {
                (element as FrameworkElement).ApplyTemplate();
            }
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
            {
                Visual visual = VisualTreeHelper.GetChild(element, i) as Visual;
                foundElement = GetDescendantByType(visual, type);
                if (foundElement != null)
                {
                    break;
                }
            }
            return foundElement;
        }


        #endregion

        
    }
}
