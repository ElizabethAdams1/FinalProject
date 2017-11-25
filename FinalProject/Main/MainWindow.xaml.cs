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

namespace FinalProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Search.wndSearch wndSearchForm;
        
        public MainWindow()
        {
            InitializeComponent();
            wndSearchForm = new Search.wndSearch();
        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            wndSearchForm.ShowDialog();
            this.Show();

        }

        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            EditPage ep = new EditPage();
            ep.Show();
            this.Close();
        }
    }
}
