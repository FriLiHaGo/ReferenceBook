using System.Collections.Generic;

namespace ReferenceBook
{
    /// <summary>
    /// Параметры поиска
    /// </summary>
    /// <typeparam name="T">Тип узлов графа</typeparam>
    public interface IRouteSearchParameter<T>
    {
        /// <summary>
        /// Проверить равенство двух узлов
        /// </summary>
        /// <param name="nodeA"></param>
        /// <param name="nodeB"></param>
        /// <returns></returns>
        bool Equals(T nodeA, T nodeB);

        /// <summary>
        /// Дополнительное условие продолжения поиска
        /// </summary>
        /// <param name="nodeA">Текущий узел</param>
        /// <param name="nodeB">Искомый узел</param>
        /// <returns></returns>
        bool IsSearchConitue(T nodeA, T nodeB);

        /// <summary>
        /// Получить соседние узлы
        /// </summary>
        /// <param name="node">Текущий узел</param>
        /// <returns>Сосендние узлы</returns>
        IEnumerable<T> GetNodes(T node);
    }
}
