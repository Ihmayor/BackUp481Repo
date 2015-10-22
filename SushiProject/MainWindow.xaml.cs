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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
    {
        public MainWindow()
        {
            InitializeComponent();
        }



        public void PopulateMenu()
        {
            var x = 0;
            var y = 0;
            MenuItemsGrid.RowDefinitions.Add(new RowDefinition());
            foreach (var section in MenuData.MenuSections)
            {
                foreach (var item in section.MenuItems.Select(item => new MenuItemControl(item)))
                {
                    item.CompleteClicked += MenuItemFeedback;
                    Grid.SetColumn(item, x);
                    Grid.SetRow(item, y);
                    MenuItemsGrid.Children.Add(item);
                    x++;
                    if (x != 3) continue;
                    MenuItemsGrid.RowDefinitions.Add(new RowDefinition());
                    y++;
                    x = 0;
                }
            }
        }

        private void MenuItemFeedback(object sender, EventArgs e)
        {
            MenuItemControl item = (MenuItemControl)sender;
        }
    }

    public class MenuData
    {
        public static List<MenuSection> MenuSections;
    }

    public class MenuSection
    {
        public List<MenuItemInfo> MenuItems;
    }

    public class MenuItemInfo
    {
        public int id;
        public string name;
        public float price;
        public string description;
        public List<string> mods;
        public bool gluten;
        public bool veg;
        public string picture;
    }
}
