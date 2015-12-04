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
        public static readonly DependencyProperty MainBackgroundSecondProperty = DependencyProperty.Register(
        "MainBackgroundSecond",
        typeof(Color),
        typeof(SecondarySideBarItemControl),
        new UIPropertyMetadata(BackgroundPropertyChanged));

        // .NET Property wrapper necessary for setting the actual property
        public Color MainBackgroundSecond
        {
            get { return (Color)GetValue(MainBackgroundSecondProperty); }
            set { SetValue(MainBackgroundSecondProperty, value); }
        }

        //An event that fires because the setter does not fire at xaml instantation
        public static void BackgroundPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            (sender as SecondarySideBarItemControl).MainGrid.Background = new BrushConverter().ConvertFromString(e.NewValue.ToString()) as SolidColorBrush;
        }
        public static readonly DependencyProperty ForegroundTextProperty = DependencyProperty.Register(
        "ForegroundText",
        typeof(Color),
        typeof(SecondarySideBarItemControl),
        new UIPropertyMetadata(ForegroundTextPropertyChanged));

        // .NET Property wrapper necessary for setting the actual property
        public Color ForegroundText
        {
            get { return (Color)GetValue(ForegroundTextProperty); }
            set { SetValue(ForegroundTextProperty, value); }
        }

        //An event that fires because the setter does not fire at xaml instantation
        public static void ForegroundTextPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            (sender as SecondarySideBarItemControl).CountOfItemLabel.Foreground = new BrushConverter().ConvertFromString(e.NewValue.ToString()) as SolidColorBrush;
            (sender as SecondarySideBarItemControl).NameLabel.Foreground = new BrushConverter().ConvertFromString(e.NewValue.ToString()) as SolidColorBrush;
            (sender as SecondarySideBarItemControl).OrderLabel.Foreground = new BrushConverter().ConvertFromString(e.NewValue.ToString()) as SolidColorBrush;
            (sender as SecondarySideBarItemControl).SelectedOptionLabel.Foreground = new BrushConverter().ConvertFromString(e.NewValue.ToString()) as SolidColorBrush;
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
