using System.Collections.Generic;
using System.Linq;

namespace ReferenceBook
{
    internal class RelationVM : NotifyPropertyChange
    {
        private IDictionary<int, ICollection<ProductNode>> _routes;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="routes">Словарь доступных маршрутов</param>
        public RelationVM(IDictionary<int, ICollection<ProductNode>> routes)
        {
            _routes = routes;

            RouteNumber = new Route(0);
            Nodes = GetRouteContent();
        }

        private Route routeNumber;
        /// <summary>
        /// Выбранный номер маршрута
        /// </summary>
        public Route RouteNumber
        {
            get => routeNumber;
            set
            {
                routeNumber = value;
                OnPropertyChanged();
                OnRoutedNumberchange();
            }
        }

        /// <summary>
        /// Коллекция номеров маршрутов
        /// </summary>
        public IEnumerable<Route> Routes => _routes.Keys.Select(k => new Route() { RouteNumber = k }).ToList();

        private IEnumerable<RouteContent> nodes;
        /// <summary>
        /// Коллекцция переходов
        /// </summary>
        public IEnumerable<RouteContent> Nodes
        {
            get => nodes;
            set
            {
                nodes = value;
                OnPropertyChanged();
            }
        }

        #region Private

        private void OnRoutedNumberchange()
        {
            Nodes = GetRouteContent();
        }

        private IEnumerable<RouteContent> GetRouteContent()
        {
            var items = new List<RouteContent>();
            for (int i = 1; i < _routes[RouteNumber.RouteNumber].Count; i++)
            {
                var item = new RouteContent()
                {
                    A = _routes[RouteNumber.RouteNumber].ElementAt(i - 1),
                    B = _routes[RouteNumber.RouteNumber].ElementAt(i)
                };
                items.Add(item);
            }
            return items;
        }

        #endregion

        #region Internal classes

        /// <summary>
        /// Номер маршрута
        /// </summary>
        internal class Route
        {
            public Route()
            {

            }
            public Route(int routeNumber)
            {
                RouteNumber = routeNumber;
            }
            public int RouteNumber { get; set; }
        }

        /// <summary>
        /// Переход между узлами
        /// </summary>
        internal class RouteContent
        {
            public ProductNode A { get; set; }

            public ProductNode B { get; set; }

            public override string ToString()
            {
                return string.Format("{0}/{1} -> {2}/{3}", A.VendorCode, A.Brand, B.VendorCode, B.Brand);
            }
        }

        #endregion
    }
}
