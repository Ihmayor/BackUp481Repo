using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for OrderDialog.xaml
    /// </summary>
    public partial class OrderDialog : UserControl
    {

        public static ObservableCollection<MenuItemData> Selected { get { return selected; } }
        private static ObservableCollection<MenuItemData> selected;

                                              
        public static EventHandler<bool> onDialogButtonClick;



                                           

        public OrderDialog()
        {
            InitializeComponent();
            SelectedList.Items.CurrentChanged += Items_CurrentChanged;
        }

        void Items_CurrentChanged(object sender, EventArgs e)
        {
            updateCurrOrderCost();
        }


        public void updateTotalCost(string newCost)
        {
            TotalMealCost.Content = newCost;
            updateCurrOrderCost();
        
        }

        private void updateCurrOrderCost()
        {

            double calcCost = 0;
            foreach (MenuItemData item in SelectedList.Items)
            {
                    double itemPrice = item.NumPrice * item.countOfItem;
                    calcCost += itemPrice;
            }


            SelectedCost.Content = "$"+calcCost.ToString("0.00"); 

        }


        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            onDialogButtonClick(sender, true);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            onDialogButtonClick(sender, false);
        }

        public static void AddSelected(ObservableCollection<MenuItemData> currSelected)
        {
            selected = currSelected;
        }


        










    }
}
