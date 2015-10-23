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
             List<provingPointObj> list = new List<provingPointObj>() { 
                 new provingPointObj("inari"), 
                 new provingPointObj("sushi"), 
                 new provingPointObj("roe") ,  
                 new provingPointObj("california roll"), 
                 new provingPointObj("wrap"), 
                 new provingPointObj("tempura"),
                 new provingPointObj("shrimp"), 
                 new provingPointObj("icecream"), 
                 new provingPointObj("bob")};
             ItemsListBox.ItemsSource = list;   
        }
        public class provingPointObj
        {
            public string name { get ; set; }
            public provingPointObj(string aaaaa)
            {
                name = aaaaa;
            }
        }

    }
}
