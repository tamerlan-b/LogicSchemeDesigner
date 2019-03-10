using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySchemes.Models
{
    /// <summary>
    /// Логическое "НЕ"
    /// </summary>
    public class LogicNot:LogicElement
    {
        /// <summary>
        /// Конструктор с входным параметром
        /// </summary>
        /// <param name="inputNum"></param>
        public LogicNot(int inputNum):base(1)
        {
            //Evaluate();
        }

        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        public LogicNot():this(1){}

        public override void Evaluate()
        {
            //Инвертируем входное значение
            Output = !Inputs[0];
        }

        /// <summary>
        /// Переопределение индексатора
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public override bool this[int index]
        {
            get
            {
                if (index == 0)
                    return base[index];
                else
                    throw new IndexOutOfRangeException();
            }
            set
            {
                if (index == 0)
                    base[index] = value;
                else
                    throw new IndexOutOfRangeException();
            }
        }

        public override string ToString()
        {
            return "NOT";
        }
    }
}
