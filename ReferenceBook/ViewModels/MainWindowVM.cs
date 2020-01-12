using System.Collections.Generic;
using System.Data.Entity;
using System.Windows;

namespace ReferenceBook
{
    internal class MainWindowVM : NotifyPropertyChange
    {
        private readonly ReferenceBookContext db;

        /// <summary>
        /// Ctor
        /// </summary>
        public MainWindowVM()
        {
            AddCommand = new Command(Add);
            EditСommand = new Command(Edit, o => SelectedProduct != null);
            RemoveCommand = new Command(Remove, o => SelectedProduct != null);
            FindRelationCommand = new Command(FindRelation);
            AcceptCommand = new Command(Accept);

            db = new ReferenceBookContext();

            db.Products.Load();
            Products = db.Products.Local;
        }

        /// <summary>
        /// Справочник аналогов
        /// </summary>
        public IEnumerable<Product> Products { get; set; }

        private Product selectedProduct;
        /// <summary>
        /// Выбранный элемент
        /// </summary>
        public Product SelectedProduct
        {
            get => selectedProduct;
            set
            {
                selectedProduct = value;
                OnPropertyChanged();
            }
        }

        #region Commands

        public Command AddCommand { get; private set; }
        public Command EditСommand { get; private set; }
        public Command RemoveCommand { get; private set; }
        public Command FindRelationCommand { get; private set; }
        public Command AcceptCommand { get; private set; }

        #endregion

        #region Private

        private void Add(object parameter)
        {
            var dialog = new ProductView();
            dialog.Title = "Добавить элемент";
            dialog.DataContext = this;

            SelectedProduct = new Product();

            dialog.ShowDialog();
            if (!dialog.DialogResult.Value)
            {
                return;
            }

            var product = SelectedProduct;
            db.Products.Add(product);
            db.SaveChanges();

            SelectedProduct = null;
        }

        private void Edit(object parameter)
        {
            if (SelectedProduct == null)
            {
                return;
            }

            var dialog = new ProductView();
            dialog.Title = "Редактировать элемент";
            dialog.DataContext = this;

            dialog.ShowDialog();
            if (!dialog.DialogResult.Value)
            {
                return;
            }

            var product = SelectedProduct;
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();

            SelectedProduct = null;
        }

        private void Remove(object parameter)
        {
            if (SelectedProduct == null)
            {
                return;
            }
            var product = db.Products.Find(SelectedProduct.Id);
            db.Products.Remove(product);
            db.SaveChanges();

            SelectedProduct = null;
        }

        private void Accept(object parameter)
        {
            var window = parameter as Window;
            window.DialogResult = true;
            window.Close();
        }

        private void FindRelation(object parameter)
        {
            var view = new FindRelationView();
            var vm = new FindRelationVM();
            view.DataContext = vm;
            view.Show();
        }

        #endregion
    }
}
