﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OnlineStoreWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<Product> TempProducts { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Products = new ObservableCollection<Product>
            {
                new Product()
                {
                    Name = "Bread",
                    Price = 0.6,
                    ImagePath = @"Images\Bread.png",
                },
                new Product()
                {
                    Name = "Cola",
                    Price = 1.5,
                    ImagePath = @"Images\Cola.png",
                },
                new Product()
                {
                    Name = "Lays",
                    Price = 2.99,
                    ImagePath = @"Images\Lays.png",
                },
                new Product()
                {
                    Name = "Pepsi",
                    Price = 3,
                    ImagePath = @"Images\Pepsi.png",
                },
                new Product()
                {
                    Name = "Potato",
                    Price = 1.24,
                    ImagePath = @"Images\Potato.png",
                },
                new Product()
                {
                    Name = "Snickers",
                    Price = 2.4,
                    ImagePath = @"Images\Snickers.png",
                },
                new Product()
                {
                    Name = "Sprite-0.5",
                    Price = 1,
                    ImagePath = @"Images\sprite-0.5.png",
                },
                new Product()
                {
                    Name = "Sprite-0.3",
                    Price = 0.7,
                    ImagePath = @"Images\sprite-0.3.png",
                }
            };
            TempProducts = Products;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => DragMove();

        private void Image_Exit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => Close();

        private void Image_FullScreen_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => WindowState = WindowState.Maximized;

        private void TextBoxSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txbSearch.Text == "Search")
            {
                txbSearch.Text = "";
                txbSearch.Foreground = Brushes.Black;
                txbSearch.FontWeight = FontWeights.Normal;
            }
        }

        private void TextBoxSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txbSearch.Text))
            {
                txbSearch.Text = "Search";
                txbSearch.Foreground = Brushes.Silver;
                txbSearch.FontWeight = FontWeights.SemiBold;
                Products = TempProducts;
            }
        }

        private void LbProduct_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ProductEditWindow editWindow = new ProductEditWindow();
            editWindow.Product = lbProduct.SelectedItem as Product;
            editWindow.ShowDialog();

        }

        private void Image_Add_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ProductEditWindow editWindow = new ProductEditWindow();
            editWindow.btnImgChnOrAdd.Content = "Image add";
            editWindow.btnAddOrDeleteProduct.Background = Brushes.LightSkyBlue;
            editWindow.btnAddOrDeleteProduct.Content = "Add";
            editWindow.ShowDialog();
        }

        private void TxbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txbSearch.Text.Length != 0 && txbSearch.Text != "Search")
            {
                var searchText = txbSearch.Text.ToLower();
                List<Product> productsList = new List<Product>();
                productsList = TempProducts.Where(p => p.Name.ToLower().StartsWith(searchText)).ToList();
                ObservableCollection<Product> newList = new ObservableCollection<Product>(productsList);
                Products = newList;
                lbProduct.ItemsSource = newList;

            }
            else if (txbSearch.Text.Length == 0)
            {
                Products = TempProducts;
                lbProduct.ItemsSource = Products;
            }
        }
    }
}
