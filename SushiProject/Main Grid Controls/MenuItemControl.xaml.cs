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
        public static event EventHandler<MenuItemObject> CompleteClicked;


        // Passes the information associated with the item. Item itself contains the count. NOT THE CONTROL.
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dynamic MenuItemInfo = (sender as Button).DataContext;
            if (OptionsAvailable.HasItems)
                MenuItemInfo.selectedOption = OptionsAvailable.SelectedItem;
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

    
    }
}
