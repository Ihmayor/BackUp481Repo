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
    /// Interaction logic for SidebarItemControl.xaml
    /// </summary>
    public partial class SidebarItemControl
    {
        #region Dependency Properties
        //This is used to set the inner grid property of this suer control
        //This registers this as property to be accessed inside xaml
        public static readonly DependencyProperty MainBackgroundProperty = DependencyProperty.Register(
            "MainBackground", 
            typeof(Color),   
            typeof(SidebarItemControl),
            new UIPropertyMetadata(BackgroundPropertyChanged));

        // .NET Property wrapper necessary for setting the actual property
        public Color MainBackground
        {
            get { return (Color)GetValue(MainBackgroundProperty); }
            set {SetValue(MainBackgroundProperty, value); }
        }

        //An event that fires because the setter does not fire at xaml instantation
        public static void BackgroundPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            (sender as SidebarItemControl).MainGrid.Background = new BrushConverter().ConvertFromString(e.NewValue.ToString()) as SolidColorBrush;
        }

        #endregion

        public static EventHandler<MenuItemData> OnMinusButtonPressed;
        public static EventHandler<MenuItemData> OnPlusButtonPressed;
        



        public SidebarItemControl()
        {
            InitializeComponent();


        }

        private void Minus_Click(object sender, RoutedEventArgs e)
        {
            OnMinusButtonPressed(sender, ((sender as Button).DataContext as MenuItemData));
        }

        private void Plus_Click(object sender, RoutedEventArgs e)
        {
            OnPlusButtonPressed(sender, ((sender as Button).DataContext as MenuItemData));
        }

        private void PriceLabel_Loaded(object sender, RoutedEventArgs e)
        {

            PriceLabel.Content = ((sender as Label).DataContext as MenuItemData).Price;
        }
    }
}
