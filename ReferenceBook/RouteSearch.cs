using System.Collections.Generic;
using System.Linq;

namespace ReferenceBook
{
    /// <summary>
    /// Поиск путей между узлами графа
    /// </summary>
    /// <typeparam name="T">Тип узлов графа</typeparam>
    public class RouteSearch<T>
    {
        private IDictionary<int, ICollection<T>> _routes = new Dictionary<int, ICollection<T>>();
        private IRouteSearchParameter<T> _search;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="deep">Глубина поиска</param>
        /// <param name="search">Параметры поиска</param>
        public RouteSearch(int deep, IRouteSearchParameter<T> search)
        {
            _search = search;
            Deep = deep;
        }

        /// <summary>
        /// Глубина поиска
        /// </summary>
        public int Deep { get; set; }

        /// <summary>
        /// Найти пути между узлами
        /// </summary>
        /// <param name="original">Исходный узел</param>
        /// <param name="desired">Искомый узел</param>
        /// <returns></returns>
        public IDictionary<int, ICollection<T>> FindRoutes(T original, T desired)
        {
            var route = new List<T>();
            FindPathsInternal(original, desired, route, 0);

            return _routes;
        }

        #region Private

        private void FindPathsInternal(T a, T b, ICollection<T> route, int deep)
        {
            if (_search.Equals(a, b))
            {
                route.Add(b);
                _routes.Add(_routes.Count, route);
                return;
            }

            deep += 1;
            if (deep > Deep)
            {
                return;
            }

            var newRoute = new List<T>(route);
            newRoute.Add(a);

            if (!_search.IsSearchConitue(a, b))
            {
                return;
            }

            var nodes = _search.GetNodes(a);
            foreach (var node in nodes.Where(n => !route.Any(i => _search.Equals(i, n))))
            {
                FindPathsInternal(node, b, newRoute, deep);
            }
        }

        #endregion
    }
}
