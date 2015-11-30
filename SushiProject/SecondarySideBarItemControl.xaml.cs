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
    /// Interaction logic for SecondarySideBarItemControl.xaml
    /// </summary>
    public partial class SecondarySideBarItemControl : UserControl
    {
        #region Dependency Properties
        public static readonly DependencyProperty MainBackgroundProperty = DependencyProperty.Register(
        "MainBackgroundSecond",
        typeof(Color),
        typeof(SidebarItemControl),
        new UIPropertyMetadata(BackgroundPropertyChanged));

        // .NET Property wrapper necessary for setting the actual property
        public Color MainBackgroundSecond
        {
            get { return (Color)GetValue(MainBackgroundProperty); }
            set { SetValue(MainBackgroundProperty, value); }
        }

        //An event that fires because the setter does not fire at xaml instantation
        public static void BackgroundPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            (sender as SidebarItemControl).MainGrid.Background = new BrushConverter().ConvertFromString(e.NewValue.ToString()) as SolidColorBrush;
        }


        #endregion

        public static EventHandler<MenuItemData> OnOrderAgainClicked;

        public SecondarySideBarItemControl()
        {
            InitializeComponent();
        }

        private void OrderAgainButton_Click(object sender, RoutedEventArgs e)
        {
            OnOrderAgainClicked(sender, ((sender as Button).DataContext as MenuItemData));
        }


    }
}
