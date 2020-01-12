using System.Collections.Generic;
using System.Linq;
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
            FindRelation = new Command(FindRelation_Command, CanFindRelation_Command);

            Deep = 5;

            using (var db = new ReferenceBookContext())
            {
                Original = db.Products.GroupBy(p => new { p.VendorCode, p.Brand })
                    .Select(g => new ProductNode()
                    {
                        VendorCode = g.Key.VendorCode,
                        Brand = g.Key.Brand
                    })
                    .OrderBy(p => p.Brand)
                    .ToList();

                Desired = db.Products.GroupBy(p => new { p.AnalogueVendorCode, p.AnalogueBrand })
                    .Select(g => new ProductNode()
                    {
                        VendorCode = g.Key.AnalogueVendorCode,
                        Brand = g.Key.AnalogueBrand
                    })
                    .OrderBy(n => n.Brand)
                    .ToList();
            }
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

        /// <summary>
        /// Оригиналы
        /// </summary>
        public IEnumerable<ProductNode> Original { get; set; }

        /// <summary>
        /// Аналоги
        /// </summary>
        public IEnumerable<ProductNode> Desired { get; set; }

        private ProductNode originalNode;
        /// <summary>
        /// Выбранный исходный товар
        /// </summary>
        public ProductNode OriginalNode
        {
            get => originalNode;
            set
            {
                originalNode = value;
                OnPropertyChanged();
            }
        }

        private ProductNode desiredNode;
        /// <summary>
        /// Выбранный искомый товар
        /// </summary>
        public ProductNode DesiredNode
        {
            get => desiredNode;
            set
            {
                desiredNode = value;
                OnPropertyChanged();
            }
        }

        #region Command

        public Command FindRelation { get; set; }

        #endregion

        #region Private

        private void FindRelation_Command(object parameter)
        {
            var search = new RouteSearch<ProductNode>(Deep, new ProductRouteSearch());
            var result = search.FindRoutes(OriginalNode, DesiredNode);

            if (result.Count != 0)
            {
                var view = new RelationView();
                var vm = new RelationVM(result);
                view.DataContext = vm;
                view.Show();
            }
            else
            {
                var message = string.Format("Искомый товар \"{0} / {1}\" не найден за {2} шагов", DesiredNode.VendorCode, DesiredNode.Brand, Deep);
                MessageBox.Show(message);
            }
            var window = parameter as Window;
            window.Close();
        }

        private bool CanFindRelation_Command(object parameter)
        {
            return Deep > 0 && OriginalNode != null && DesiredNode != null;
        }

        #endregion
    }
}
