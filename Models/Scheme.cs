using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySchemes.Models
{
    /// <summary>
    /// Хранит состояние текущей схемы и все элементы на ней
    /// </summary>
    public class Scheme
    {
        /// <summary>
        /// Логические элементы схемы
        /// </summary>
        public List<LogicElement> logicElements;

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Scheme()
        {
            logicElements = new List<LogicElement>();
        }

        /// <summary>
        /// Добавить логический элемент в схему
        /// </summary>
        /// <param name="elem"></param>
        public void AddElement(LogicElement elem)
        {
            if (elem != null)
                logicElements.Add(elem);
        }

    }
}
