using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ReferenceBook
{
    internal class MainWindowVM
    {
        private ReferenceBookContext db;

        public MainWindowVM()
        {
            db = new ReferenceBookContext();

            db.Products.Load();
            Products = db.Products.Local;
        }

        public IEnumerable<Product> Products { get; set; }

        #region Commands

        public RoutedCommand AddItem { get; private set; } = new RoutedCommand();
        public RoutedCommand EditItem { get; private set; } = new RoutedCommand();
        public RoutedCommand RemoveItem { get; private set; } = new RoutedCommand();
        public RoutedCommand FindRelation { get; private set; } = new RoutedCommand();

        #endregion

        public void Add(object parameter, IInputElement target)
        {
            var dialog = new DialogService<Product, ProductView>("Добавление элемента", typeof(ProductVM));

            dialog.ShowDialog();
            if (!dialog.DialogResult)
            {
                return;
            }

            var product = dialog.Product;

            db.Products.Add(product);
            db.SaveChanges();
        }

        //public void Edit(object parameter, IInputElement target)
        //{
        //    var product = db.Products.Find(id);

        //    var dialog = new ProductView();
        //    dialog.Title = "Редактирование элемента";

        //    dialog.VendorCode.Text = product.VendorCode;
        //    dialog.Brand.Text = product.Brand;
        //    dialog.AnalogueVendorCode.Text = product.AnalogueVendorCode;
        //    dialog.AnalogueBrand.Text = product.AnalogueBrand;
        //    dialog.Trust.Text = product.Trust.ToString();


        //    if (!dialog.ShowDialog().Value)
        //    {
        //        return;
        //    }

        //    product.VendorCode = dialog.VendorCode.Text;
        //    product.Brand = dialog.Brand.Text;
        //    product.AnalogueVendorCode = dialog.AnalogueVendorCode.Text;
        //    product.AnalogueBrand = dialog.AnalogueBrand.Text;
        //    product.Trust = Convert.ToInt32(dialog.Trust.Text);

        //    db.SaveChanges();
        //}

        //public void Remove(object parameter, IInputElement target)
        //{
        //    var product = db.Products.Find(id);
        //    db.Products.Remove(product);
        //    db.SaveChanges();
        //}
    }

    public class DialogService<TResult, TForm> 
        where TForm : Window
    {
        public DialogService(string title, Type vm)
        {
            Title = title;
            VM = vm;
        }

        public string Title { get; set; }

        public Type VM { get; set; }

        public bool DialogResult { get; private set; }

        public TResult Product { get; set; }

        public void ShowDialog()
        {
            var dialog = Activator.CreateInstance<TForm>();
            dialog.Title = Title;
            dialog.DataContext = Activator.CreateInstance(VM, new object[] { Product });
            
            DialogResult = dialog.ShowDialog().Value;
        }
    }
}
