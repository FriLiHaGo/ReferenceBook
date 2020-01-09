using System.Windows;

namespace ReferenceBook
{
    internal class FindRelationVM : NotifyPropertyChange
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public FindRelationVM()
        {
            FindRelation = new Command(FindRelation_Command);
            Deep = 5;
        }

        private int deep;
        /// <summary>
        /// Глубина поиска
        /// </summary>
        public int Deep
        {
            get => deep;
            set
            {
                deep = value;
                OnPropertyChanged();
            }
        }

        private string vendorCode;
        /// <summary>
        /// Артикул 1
        /// </summary>
        public string VendorCode
        {
            get => vendorCode;
            set
            {
                vendorCode = value;
                OnPropertyChanged();
            }
        }

        private string brand;
        /// <summary>
        /// Производитель 1
        /// </summary>
        public string Brand
        {
            get => brand;
            set
            {
                brand = value;
                OnPropertyChanged();
            }
        }

        private string analogueVendorCode;
        /// <summary>
        /// Артикул 2
        /// </summary>
        public string AnalogueVendorCode
        {
            get => analogueVendorCode;
            set
            {
                analogueVendorCode = value;
                OnPropertyChanged();
            }
        }

        private string analogueBrand;
        /// <summary>
        /// Производитель 2
        /// </summary>
        public string AnalogueBrand
        {
            get => analogueBrand;
            set
            {
                analogueBrand = value;
                OnPropertyChanged();
            }
        }

        #region Command

        public Command FindRelation { get; set; }

        #endregion

        #region Private

        private void FindRelation_Command(object parameter)
        {
            ProductNode original, desired;
            using (var db = new ReferenceBookContext())
            {
                original = new ProductNode(VendorCode, Brand);
                desired = new ProductNode(AnalogueVendorCode, AnalogueBrand);
            }
            var search = new RouteSearch<ProductNode>(Deep, new ProductRouteSearch());
            var result = search.FindRoutes(original, desired);

            if (result.Count != 0)
            {
                var view = new RelationView();
                var vm = new RelationVM(result);
                view.DataContext = vm;
                view.Show();
            }
            else
            {
                var message = string.Format("Искомый товар \"{0} / {1}\" не найден за {2} шагов", desired.VendorCode, desired.Brand, Deep);
                MessageBox.Show(message);
            }
            var window = parameter as Window;
            window.Close();
        }

        #endregion
    }
}
