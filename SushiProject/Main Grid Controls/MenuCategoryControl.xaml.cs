﻿using System;
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
    /// Interaction logic for MenuCategoryControl.xaml
    /// </summary>
    public partial class MenuCategoryControl : UserControl
    {
        public MenuCategoryControl()
        {
            InitializeComponent();
        }

        private void MenuItemControl_Loaded(object sender, RoutedEventArgs e)
        {
            Console.WriteLine((sender as UserControl).DataContext);
        }

    }
}