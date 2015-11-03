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

namespace MainWindowTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UserControl1 page1 = new UserControl1() { Height = 400, Background = new SolidColorBrush(Colors.PaleVioletRed )};
        UserControl2 page2 = new UserControl2() { Height = 400, Background = new SolidColorBrush(Colors.HotPink) };
        UserControl3 page3 = new UserControl3() { Height = 400, Background = new SolidColorBrush(Colors.Ivory) };


        public MainWindow()
        {
            InitializeComponent();
            Stackbob.Children.Add(page1);
            Stackbob.Children.Add(page2);
            Stackbob.Children.Add(page3);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Stackbob.Children.Clear();
            Stackbob.Children.Add(page1);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Stackbob.Children.Clear();
            Stackbob.Children.Add(page2);
       
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Stackbob.Children.Clear();
            Stackbob.Children.Add(page3);
       
        }
    }
}
