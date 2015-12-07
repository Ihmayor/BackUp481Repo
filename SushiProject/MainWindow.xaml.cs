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
        #region Variables Directly  Associated With Controls
        public static ObservableCollection<MenuItemData> OrderedItems { get { return orderedItems; } }
        private static ObservableCollection<MenuItemData> orderedItems = new ObservableCollection<MenuItemData>();

        public static ObservableCollection<MenuItemData> DeliveredItems { get { return deliveredItems; } }
        private static ObservableCollection<MenuItemData> deliveredItems = new ObservableCollection<MenuItemData>();

        public static ObservableCollection<MenuItemData> SelectedItems { get { return selectedItems; } }
        private static ObservableCollection<MenuItemData> selectedItems = new ObservableCollection<MenuItemData>();

        private static double totalCost = 0;
        public static string costString = "Total Price: " + totalCost;

        
 
        #endregion

        #region Events/Methods Involved with Loaded Items
        public MainWindow()
        {
            InitializeComponent();
            MenuItemControl.CompleteClicked += MenuItemControl_CompleteClicked;
            SidebarItemControl.OnMinusButtonPressed += SidebarItemControl_MinusButton;
            SidebarItemControl.OnPlusButtonPressed += SidebarItemControl_PlusButton;
            SecondarySideBarItemControl.OnOrderAgainClicked += SecondarySideBarItemControl_OrderAgainClicked;
            generateMenuItems();

            var scrollViewer = GetDescendantByType(MainListView, typeof(ScrollViewer)) as ScrollViewer;
            scrollViewer.ScrollChanged += scrollViewer_ScrollChanged;
        }

        private void SecondarySideBarItemControl_OrderAgainClicked(object sender, MenuItemData chosenItem)
        {
       
            MenuItemData foundItem = selectedItems.FirstOrDefault(x => x.isSameMenuItem(chosenItem));
            if (foundItem == null)
            {
                MenuItemData newItem = chosenItem.clone();
                newItem.countOfItem++;
                if (chosenItem.SelectedOption != "")
                {
                    newItem.setSelectedOption(chosenItem.options.FindIndex(x => x == chosenItem.SelectedOption));
                }
                selectedItems.Add(newItem);
            }
            else
            {
                foundItem.countOfItem++;
                updateMenuItem(selectedItems, foundItem);
            }
            Selected.IsSelected = true;
            updateCost(chosenItem.NumPrice);
 
        
        }

        void scrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            double check = e.VerticalOffset;
         
            if (check >= calculateOffset(0) && check < calculateOffset(1))
            {
                changeCategoryButtonColor(SpecialsButton);
            }
            else if (check >= calculateOffset(1) && check < calculateOffset(2))
            {
                changeCategoryButtonColor(AppetizersButton);

            }
            else if (check >= calculateOffset(2) && check < calculateOffset(3))
            {
                changeCategoryButtonColor(SushiButton);

            }
            else if (check >= calculateOffset(3) && check < calculateOffset(4))
            {
                changeCategoryButtonColor(FriedButton);

            }

            else if (check >= calculateOffset(4) && check < calculateOffset(5)-10)
            {
                changeCategoryButtonColor(DrinksButton);

            }
            else if (check >= calculateOffset(5)-10)
            {
                changeCategoryButtonColor(DessertsButton);
            }

        }

        public void changeCategoryButtonColor(Button applyOnlyToThis)
        {
            List<Button> buttons = new List<Button> { SpecialsButton, AppetizersButton, SushiButton, FriedButton, DrinksButton, DessertsButton };
            foreach (Button button in buttons)
            {
                if (applyOnlyToThis != button)
                {
                    button.Background = new SolidColorBrush(Colors.White);
                    button.Foreground = new SolidColorBrush(Colors.Black);
                }
                else
                {
                    button.Background = new SolidColorBrush(Colors.Green);
                    button.Foreground = new SolidColorBrush(Colors.White);
                }

            }

        }

        private void OrderDialogWindow_Loaded(object sender, RoutedEventArgs e)
        {
            OrderDialog.onDialogButtonClick += OrderDialog_ConfirmedOrderButtonClick;
            OrderDialogWindow.SelectedList.ItemsSource = selectedItems;
        }

        #endregion

        #region Control Click Events Both inside Window and Outside

        #region Menu Item Control
        void MenuItemControl_CompleteClicked(object sender, MenuItemData addItem)
        {
            bool limitReached = false;
            MenuItemData foundItem = selectedItems.FirstOrDefault(x => x.isSameMenuItem(addItem));
            if (foundItem == null)
            {
                addItem.countOfItem = 1;
                selectedItems.Add(addItem);
            }
            else
            {
                if (foundItem.countOfItem == 25)
                    limitReached = true;
                else
                {
                    foundItem.countOfItem++;
                    updateMenuItem(selectedItems, foundItem);
                }
            }
            Selected.IsSelected = true;
            if (!limitReached)
                updateCost(addItem.NumPrice);

        }


        private void SidebarItemControl_PlusButton(object sender, MenuItemData chosenItem)
        {
            if (chosenItem.countOfItem < 25)
            {
                chosenItem.countOfItem++;
                MenuItemData foundItem = selectedItems.FirstOrDefault(x => x.isSameMenuItem(chosenItem));
                updateMenuItem(selectedItems, foundItem);
                updateCost(chosenItem.NumPrice);
            }
        }


        private void SidebarItemControl_MinusButton(object sender, MenuItemData chosenItem)
        {
            MenuItemData foundItem = selectedItems.FirstOrDefault(x => x.isSameMenuItem(chosenItem));
            foundItem.countOfItem--;
            if (foundItem.countOfItem == 0)
            {
                selectedItems.Remove(foundItem);
            }
            else
            {
                updateMenuItem(selectedItems, foundItem);
            }
            updateCost(-chosenItem.NumPrice);
            
        }

        #endregion

        #region Dialog Related
        private void ConfirmOrder_Click(object sender, RoutedEventArgs e)
        {
            if (selectedItems.Count==0)return;
            OrderDialogWindow.Visibility = System.Windows.Visibility.Visible;
            GrayOutWindow.Visibility = System.Windows.Visibility.Visible;
            updateCost(0);

        }


        private void OrderDialog_ConfirmedOrderButtonClick(object sender, bool confirmed)
        {
            if (confirmed)
            {
                foreach (MenuItemData item in selectedItems)
                {
                   
                        MenuItemData newItem = item.clone();
                        newItem.countOfItem = item.countOfItem;
                        if(item.SelectedOption != "")
                        {
                            newItem.setSelectedOption( item.options.FindIndex(x => x == item.SelectedOption));
                        }
                        orderedItems.Add(newItem);
                }
                EmptyObservableCollection(selectedItems);
                OrderDialogWindow.Visibility = System.Windows.Visibility.Hidden;
                GrayOutWindow.Visibility = System.Windows.Visibility.Hidden;
                Ordered.IsSelected = true;
            }

            else
            {
                OrderDialogWindow.Visibility = System.Windows.Visibility.Hidden;
                GrayOutWindow.Visibility = System.Windows.Visibility.Hidden;

            }

        }
        #endregion


        #region Category Buttons

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


        #endregion

        #region Data Generation Methods
        public void generateMenuItems()
        {
            var names = new List<string> {"Inari","Twinari","Double Happiness Roll" };
            var desc =  new List<string> { "Fried beancurd","Double fried beancurd","This sushi will make you smile."};
            var options = new List<List<string>> {new List<string>{"Select Option","Gluten Free"},null,null };
            List<BitmapImage> images = new List<BitmapImage> 
                {new BitmapImage(new Uri(@"pack://application:,,,/Resources/Inari.png")),
                 new BitmapImage(new Uri(@"pack://application:,,,/Resources/Twinari.jpg")),
                 new BitmapImage(new Uri(@"pack://application:,,,/Resources/HappinessRoll.png"))
                };
            MenuCategory SpecialCategory = new MenuCategory("Specials", generateMenuCategoryCust(0, names, images, desc, options));
            MenuCategory SushiCategory = new MenuCategory("Sushi", generateMenuCategoryType2(20, "Sushi", new BitmapImage(new Uri(@"pack://application:,,,/Resources/SalmonSushi.png")), true, true, "These are sushi Items", null));
            MenuCategory AppetizerCategory = new MenuCategory("Appetizers", generateMenuCategoryType(40, "Gyoza Set", new BitmapImage(new Uri(@"pack://application:,,,/Resources/Gyoza.png")), true, false, "These are appetizer Items", null));
            MenuCategory FriedCategory = new MenuCategory("Fried", generateMenuCategoryType2(60, "Fried", new BitmapImage(new Uri(@"pack://application:,,,/Resources/ShrimpTempura.png")), false, false, "These are fried Items", null));
            MenuCategory DrinksCategory = new MenuCategory("Drinks", generateMenuCategoryType3(80, "Drinks", new BitmapImage(new Uri(@"pack://application:,,,/Resources/CocaCola.png")), false, false, "These are drink Items", null));
            MenuCategory DessertCategory = new MenuCategory("Desserts", generateMenuCategoryType4(100, "Desserts", new BitmapImage(new Uri(@"pack://application:,,,/Resources/Mochi.png")), true, false, "These are dessert Items", null));
            List<MenuCategory> TotalItems = new List<MenuCategory>() { SpecialCategory, AppetizerCategory, SushiCategory, FriedCategory, DrinksCategory, DessertCategory };

            MainListView.ItemsSource = TotalItems;
        }

        public List<MenuItemData> generateMenuCategoryCust(int ID, List<string> namesOfItems, List<BitmapImage> imageSource, List<string> Description, List<List<string>> optionsList)
        {
            List<MenuItemData> associatedItems = new List<MenuItemData>();
            int i = 0;
            foreach (var name in namesOfItems)
            {
                bool isVegan = (i % 2 == 1);
                if (i == 0)
                    isVegan = true;
                bool isGlutenFree = (i % 3 == 2);
                associatedItems.Add(new MenuItemData(ID + i, ((i+1)*2+((i*.7)%1)), name, imageSource[i], isVegan, isGlutenFree, Description[i], optionsList[i]));
                i++;
            }
            return associatedItems;
        }

        public List<MenuItemData> generateMenuCategoryType(int ID, string nameOfItem, BitmapImage imageSource, bool isVegan, bool isGlutenFree, string Description, List<string> optionsList)
        {
            List<MenuItemData> associatedItems = new List<MenuItemData>();
            for (int i = 0; i < 9; i++)
            {
                associatedItems.Add(new MenuItemData(ID + i, 5, nameOfItem + "" + i, imageSource, isVegan, isGlutenFree, Description, optionsList));
            }
            return associatedItems;
        }

        public List<MenuItemData> generateMenuCategoryType2(int ID, string nameOfItem, BitmapImage imageSource, bool isVegan, bool isGlutenFree, string Description, List<string> optionsList)
        {
            List<MenuItemData> associatedItems = new List<MenuItemData>();
            for (int i = 0; i < 20; i++)
            {
                associatedItems.Add(new MenuItemData(ID + i, 4.20, nameOfItem + "" + i, imageSource, isVegan, isGlutenFree, Description, optionsList));
            }
            return associatedItems;
        }
        public List<MenuItemData> generateMenuCategoryType3(int ID, string nameOfItem, BitmapImage imageSource, bool isVegan, bool isGlutenFree, string Description, List<string> optionsList)
        {
            List<MenuItemData> associatedItems = new List<MenuItemData>();
            for (int i = 0; i < 6; i++)
            {
                associatedItems.Add(new MenuItemData(ID + i, 4.20, nameOfItem + "" + i, imageSource, isVegan, isGlutenFree, Description, optionsList));
            }
            return associatedItems;
        }



        public List<MenuItemData> generateMenuCategoryType4(int ID, string nameOfItem, BitmapImage imageSource, bool isVegan, bool isGlutenFree, string Description, List<string> optionsList)
        {
            List<MenuItemData> associatedItems = new List<MenuItemData>();
            for (int i = 0; i < 12; i++)
            {
                associatedItems.Add(new MenuItemData(ID + i, 5, nameOfItem + "" + i, imageSource, isVegan, isGlutenFree, Description, optionsList));
            }
            return associatedItems;
        }
        #endregion

        #region Misc Helper Methods

        private void updateCost(double change)
        {
            totalCost += change;
            TotalCostField.Content = "Total Price: " +"$"+ totalCost.ToString("0.00");
            OrderDialogWindow.updateTotalCost("$"+totalCost.ToString("0.00"));
        }



        //Triggers the item to reload.
        private void updateMenuItem(ObservableCollection<MenuItemData> ListInvolved, MenuItemData itemToUpdate)
        {
            int indexMaintain = ListInvolved.IndexOf(itemToUpdate);
            ListInvolved.Remove(itemToUpdate);
            ListInvolved.Insert(indexMaintain, itemToUpdate);
        }


        private void EmptyObservableCollection(ObservableCollection<MenuItemData> listToClear)
        {
            ObservableCollection<MenuItemData> toRemove = new ObservableCollection<MenuItemData>();
            foreach (MenuItemData item in listToClear)
            {
                item.countOfItem = 0;
                toRemove.Add(item);
            }

            foreach (MenuItemData item in toRemove)
            {
                listToClear.Remove(item);
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


        //Method used to mock up the delivery
        private void mainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            if (OrderDialogWindow.Visibility == System.Windows.Visibility.Hidden && e.Key == Key.D && orderedItems.Count > 0)
            {
               
                MenuItemData item = orderedItems[0];
                MenuItemData newItem = item.clone();
                if (item.SelectedOption != "")
                {
                    newItem.setSelectedOption(item.options.FindIndex(x => x == item.SelectedOption));
                }                    
            
                newItem.countOfItem = item.countOfItem;
                deliveredItems.Add(newItem);
                orderedItems.Remove(item); 
                Delivered.IsSelected = true;
            }
        
        }

        bool called = false;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement LabelToAffect = (AssistanceLabel as FrameworkElement);
            if (!called)
            {
                DoubleAnimation anim = new DoubleAnimation(1.0, 0.1, TimeSpan.FromMilliseconds(750)) { RepeatBehavior = RepeatBehavior.Forever,AutoReverse=true };
                Storyboard sb = new Storyboard();
                //buttonToaffect.BeginStoryboard = anim;
                LabelToAffect.BeginAnimation(FrameworkElement.OpacityProperty, anim);
                called = true;
                (sender as Button).Content = "Cancel Assistance Call"; 
       
            }
            else
            {
                called = false;
                LabelToAffect.BeginAnimation(FrameworkElement.OpacityProperty, null);
                (sender as Button).Opacity = 1;
                (sender as Button).Content = "Call for Assistance"; 
              
            }
            
        }


    }
}