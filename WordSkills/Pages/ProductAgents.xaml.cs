
using Blagodat.Pages;
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
using WordSkills.Models;

namespace Blagodat
{
    /// <summary>
    /// Логика взаимодействия для ProductAgents.xaml
    /// </summary>
    public partial class ProductAgents : Page
    {
        int _currentPage = 1, _countInPage = 10, _maxPages;
        public ProductAgents()
        {
            InitializeComponent();
          /* DGridProducts.ItemsSource = BlagodatAlibekov195Entities.GetContext().productsale.ToList();*/
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditPage());
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                BlagodatAlibekov195Entities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridProducts.ItemsSource = BlagodatAlibekov195Entities.GetContext().productsale.ToList();
            }
        }


        private void RefreshData()
        {
            var data = BlagodatAlibekov195Entities.GetContext().productsale.ToList();

            // List<Models.Ingredient> listIngredients = _context.Ingredient.ToList();            

            _maxPages = (int)Math.Ceiling(data.Count * 1.0 / _countInPage);
            data = data.Skip((_currentPage - 1) * _countInPage).Take(_countInPage).ToList();

            LblPages.Content = $"{_currentPage}/{_maxPages}";

            DGridProducts.ItemsSource = data;

            ManageButtonsEnable();
            GeneratePageNumbers();
        }
        private void GeneratePageNumbers()
        {
            SPanelPages.Children.Clear();

            for (int i = 1; i <= _maxPages; i++)
            {
                Button btn = new Button();
                btn.Content = i.ToString();
                btn.Width = 28;
                btn.Click += NavigateToSelectedPage;
                SPanelPages.Children.Add(btn);
            }
        }

        private void NavigateToSelectedPage(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string pageStr = btn.Content.ToString();
            int page = int.Parse(pageStr);
            _currentPage = page;
            RefreshData();
        }






        private void ScanBtn_Click2(object sender, RoutedEventArgs e)
        {

        }

        private void BtnFirstPage_Click(object sender, RoutedEventArgs e)
        {
            _currentPage = 1;
            RefreshData();
        }

        private void BtnPreviousPage_Click(object sender, RoutedEventArgs e)
        {
            _currentPage--;
            RefreshData();
        }

        private void BtnNextPage_Click(object sender, RoutedEventArgs e)
        {
            _currentPage++;
            RefreshData();
        }

        private void BtnLastPage_Click(object sender, RoutedEventArgs e)
        {
            _currentPage = _maxPages;
            RefreshData();
        }

        private void ManageButtonsEnable()
        {
            BtnLastPage.IsEnabled = BtnNextPage.IsEnabled = true;
            BtnFirstPage.IsEnabled = BtnPreviousPage.IsEnabled = true;

            if (_currentPage == 1)
            {
                BtnFirstPage.IsEnabled = BtnPreviousPage.IsEnabled = false;
            }

            if (_currentPage == _maxPages)
            {
                BtnLastPage.IsEnabled = BtnNextPage.IsEnabled = false;
            }
        }



    }
}
