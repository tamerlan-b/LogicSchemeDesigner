using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySchemes.Models
{
    /// <summary>
    /// Логическое "И"
    /// </summary>
    public class LogicAnd : LogicElement
    {
        /// <summary>
        /// Конструктор с входным параметром
        /// </summary>
        /// <param name="inputsNum">Количество входов у элемента</param>
        public LogicAnd(int inputsNum):base(inputsNum){
            //Evaluate(); 
        }

        public override void Evaluate()
        {
            Output = true;
            //Проходимся в цикле по всем входам
            for (int i = 0; i < Inputs.Length; i++)
            {
                //Если хотя бы 1 вход равен false
                if (!Inputs[i])
                {
                    //Присваиваем Output значение false
                    Output = false;
                    //Выходим из цикла
                    break;
                }
            }
        }

        public override string ToString()
        {
            return Inputs.Length + " - AND";
        }
    }
}
