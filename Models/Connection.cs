using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySchemes.Models
{
    //ToDo: Заменить на словарь

    /// <summary>
    /// Связь между логическими элементами
    /// </summary>
    public class Connection<T>
    {
        /// <summary>
        /// Элемент
        /// </summary>
        public T Element;

        /// <summary>
        /// Номер входа элемента
        /// </summary>
        public int Index;


        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="element"></param>
        /// <param name="index"></param>
        public Connection(T element, int index)
        {
            Element = element;
            Index = index;
        }
    }
}
