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
    /// Interaction logic for MenuItem.xaml
    /// </summary>
    public partial class MenuItemControl 
    {

        public MenuItemControl()
        {
            InitializeComponent();
        }
        public static event EventHandler<MenuItemData> CompleteClicked;


        // Passes the information associated with the item. Item itself contains the count. NOT THE CONTROL.
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MenuItemData MenuItemInfo = ((MenuItemData)(sender as Button).DataContext).clone();
            if (OptionsAvailable.HasItems && OptionsAvailable.SelectedIndex != 0)
                MenuItemInfo.setSelectedOption(OptionsAvailable.SelectedIndex);
            try
            {
                CompleteClicked(sender, MenuItemInfo);

            }
            catch (Exception ex)
            {
            }               

            
        }

        private void OptionsAvailable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
        }

        private void Gluten_Free_Loaded(object sender, RoutedEventArgs e)
        {
            MenuItemData MenuItem = (sender as Label).DataContext as MenuItemData;
            if (MenuItem != null)
            {
                if (!MenuItem.isGlutenFree)
                    (sender as Label).Visibility = System.Windows.Visibility.Hidden;

            }
        }

        private void Vegan_Loaded(object sender, RoutedEventArgs e)
        {
            MenuItemData MenuItem = (sender as Label).DataContext as MenuItemData;
            if (MenuItem != null)
            {
                if (!MenuItem.isVegan)
                    (sender as Label).Visibility = System.Windows.Visibility.Hidden;

            }
        
        }

        private void OptionsAvailable_Loaded(object sender, RoutedEventArgs e)
        {
            MenuItemData MenuItem = (sender as ComboBox).DataContext as MenuItemData;
            if (MenuItem != null)
            {
                if (MenuItem.hasOptions)
                {

                }
                else
                {
                        (sender as ComboBox).Visibility = System.Windows.Visibility.Hidden;
                }
            }
       
        }

    
    }
}
