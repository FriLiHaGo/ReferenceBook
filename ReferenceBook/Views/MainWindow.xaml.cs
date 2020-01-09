using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace ReferenceBook
{
    public partial class MainWindow : Window
    {
        private ReferenceBookContext db;

        public MainWindow()
        {
            //var code = new Code()
            //{
            //    Deep = 7
            //};
            //code.FindPaths(new Code.Graph(), new Code.Node(1), new Code.Node(8));

            InitializeComponent();

            db = new ReferenceBookContext();

            db.Products.Load();
            Products = db.Products.Local;

            DataContext = this;
        }

        public IEnumerable<Product> Products { get; set; }



        public void Add()
        {
            var dialog = new ProductView();
            dialog.Title = "Добавление нового элемента";
            if (!dialog.ShowDialog().Value)
            {
                return;
            }

            var product = new Product()
            {
                VendorCode = dialog.VendorCode.Text,
                Brand = dialog.Brand.Text,
                AnalogueVendorCode = dialog.AnalogueVendorCode.Text,
                AnalogueBrand = dialog.AnalogueBrand.Text,
                Trust = Convert.ToInt32(dialog.Trust.Text)
            };

            db.Products.Add(product);
            db.SaveChanges();
        }

        public void Edit(int id)
        {
            var product = db.Products.Find(id);

            var dialog = new ProductView();
            dialog.Title = "Редактирование элемента";

            dialog.VendorCode.Text = product.VendorCode;
            dialog.Brand.Text = product.Brand;
            dialog.AnalogueVendorCode.Text = product.AnalogueVendorCode;
            dialog.AnalogueBrand.Text = product.AnalogueBrand;
            dialog.Trust.Text = product.Trust.ToString();


            if (!dialog.ShowDialog().Value)
            {
                return;
            }

            product.VendorCode = dialog.VendorCode.Text;
            product.Brand = dialog.Brand.Text;
            product.AnalogueVendorCode = dialog.AnalogueVendorCode.Text;
            product.AnalogueBrand = dialog.AnalogueBrand.Text;
            product.Trust = Convert.ToInt32(dialog.Trust.Text);

            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Add();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var product = dataGrid.SelectedItem as Product;
            if(product != null)
            {
                Edit(product.Id);
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var product = dataGrid.SelectedItem as Product;
            if (product != null)
            {
                Delete(product.Id);
            }
        }

        private void BtnFindRelation_Click(object sender, RoutedEventArgs e)
        {
            var view = new FindRelationView();
            view.Show();
        }
    }
}
