using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySchemes.Models
{
    /// <summary>
    /// Класс реализующий логический элемент
    /// </summary>
    public class LogicElement
    {
        /// <summary>
        /// Входы логической схемы
        /// </summary>
        /// ToDo: изменить модификатор доступа
        public bool[] Inputs;

        /// <summary>
        /// Выход логической схемы
        /// </summary>
        public bool Output;

        /// <summary>
        /// Дочерние логические элементы, к которым подключен выход данного элемента
        /// </summary>
        public List<Connection<LogicElement>> Children = new List<Connection<LogicElement>>();

        /// <summary>
        /// Родительские логические элементы, подключенные ко входам данного
        /// </summary>
        public List<Connection<LogicElement>> Parents = new List<Connection<LogicElement>>();

        /// <summary>
        /// Графическое представление элемента
        /// </summary>
        public LogicPanel Panel;

        /// <summary>
        /// Конструктор с параметром
        /// </summary>
        /// <param name="inputNum"></param>
        public LogicElement(int inputNum)
        {
            Inputs = new bool[inputNum];
        }

        /// <summary>
        /// Вычислить значение выхода логического элемента при заданных входах
        /// </summary>
        public virtual void Evaluate()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Послать значение на элементы, которые к подключены к данному
        /// </summary>
        public void SendValue()
        {
            //Вычислляем значение текущего элемента
            Evaluate();
            foreach (var item in Children)
            {
                //Отправляем это значения на входы всех последующих элементов
                item.Element[item.Index] = this.Output;
            }
        }

        /// <summary>
        /// Подключить дочерний логический элемент
        /// </summary>
        /// <param name="element"></param>
        /// <param name="index"></param>
        public void AddChild(LogicElement element, int index)
        {
            //Добавляем текущему элементу дочерний
            Children.Add(new Connection<LogicElement>(element, index));
            //Добавляем дочернему элементу родительский
            element.Parents.Add(new Connection<LogicElement>(this, index));
        }

        /// <summary>
        /// Подключить родительский логический элемент
        /// </summary>
        /// <param name="element"></param>
        /// <param name="index"></param>
        public void AddParent(LogicElement element, int index)
        {
            //Добавляем текущему элементу родительский
            Parents.Add(new Connection<LogicElement>(element, index));
            //Добавляем родительскому элементу текущий в качестве дочернего
            element.Children.Add(new Connection<LogicElement>(this, index));
        }

        /// <summary>
        /// Индексатор логического элемента
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public virtual bool this[int index]
        {
            get => Inputs[index];
            set
            {
                Inputs[index] = value;
                //При изменении входа обновляем выходное значение и отправляем его дальше
                SendValue();
            }
        }
    }
}
