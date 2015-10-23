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
    /// Interaction logic for toproveapoint.xaml
    /// </summary>
    public partial class toproveapoint : UserControl
    {
        public toproveapoint()
        {
            InitializeComponent();
             List<provingPointObj> list = new List<provingPointObj>() 
             { 
                 new provingPointObj("inari", new BitmapImage(new Uri("../../Images/sushi.png",UriKind.Relative))), 
                 new provingPointObj("sushi", new BitmapImage(new Uri("../../Images/sushi.png",UriKind.Relative))), 
                 new provingPointObj("roe", new BitmapImage(new Uri("../../Images/sushi.png",UriKind.Relative))) ,  
                 new provingPointObj("california roll", new BitmapImage(new Uri("../../Images/sushi.png",UriKind.Relative))), 
                 new provingPointObj("wrap", new BitmapImage(new Uri("../../Images/sushi.png",UriKind.Relative))), 
                 new provingPointObj("tempura", new BitmapImage(new Uri("../../Images/sushi.png",UriKind.Relative))),
                 new provingPointObj("shrimp", new BitmapImage(new Uri("../../Images/sushi.png",UriKind.Relative))), 
                 new provingPointObj("icecream", new BitmapImage(new Uri("../../Images/sushi.png",UriKind.Relative))), 
                 new provingPointObj("bob", new BitmapImage(new Uri("../../Images/sushi.png",UriKind.Relative)))
             };
             ItemsListBox.ItemsSource = list;   
        }
        public class provingPointObj
        {
            public string name { get ; set; }
            public BitmapImage source { get; set; }
            public provingPointObj(string aaaaa,BitmapImage imageSource)
            {
                name = aaaaa;
                source = imageSource;
            }
        }

    }
}
