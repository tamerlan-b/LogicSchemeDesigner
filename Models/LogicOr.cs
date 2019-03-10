using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySchemes.Models
{
    /// <summary>
    /// Логическое "ИЛИ"
    /// </summary>
    public class LogicOr : LogicElement
    {
        /// <summary>
        /// Конструктор с параметром
        /// </summary>
        /// <param name="inputsNum">Количество входов у элемента</param>
        public LogicOr(int inputsNum):base(inputsNum) {
            //Evaluate(); 
        }


        public override void Evaluate()
        {
            Output = false;
            //Проходимся в цикле по всем входам
            for (int i = 0; i < Inputs.Length; i++)
            {
                //Если хотя бы 1 вход равен true
                if (Inputs[i])
                {
                    //Присваиваем Output значение true
                    Output = true;
                    //Выходим из цикла
                    break;
                }
            }
        }

        public override string ToString()
        {
            return Inputs.Length + " - OR";
        }
    }
}
