using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySchemes.Models
{
    /// <summary>
    /// Рисовальщик логических элементов и схем
    /// </summary>
    public static class LogicPainter
    {
        /// <summary>
        /// Нарисовать ломаную линию между двумя точками
        /// </summary>
        /// <param name="pt1">Первая точка</param>
        /// <param name="pt2">Вторая точка</param>
        /// <param name="color">Цвет линии связи</param>
        /// <param name="form">Форма, на которой рисуется линия</param>
        public static void DrawBrokenLine(Point pt1, Point pt2, Color color, Form form)
        {
            //Промежуточные точки для задания ломаной
            Point ptMiddle1 = new Point()
            {
                X = (pt1.X + pt2.X) / 2,
                Y = pt1.Y
            };
            Point ptMiddle2 = new Point()
            {
                X = (pt1.X + pt2.X) / 2,
                Y = pt2.Y
            };
            //Рисуем прямую линию между элементами
            using (var g = form.CreateGraphics())
            using (var pen = new Pen(color, 1))
                g.DrawLines(pen, new Point[] { pt1, ptMiddle1, ptMiddle2, pt2 });
        }

        /// <summary>
        /// Нарисовать связь между элементами
        /// </summary>
        /// <param name="panelSecond"></param>
        /// <param name="index"></param>
        public static void DrawConnection(LogicPanel panelFrom, LogicPanel panelTo, int index)
        {
            //Получаем точки точного соединения с выходом и входом
            Point ptFrom = panelFrom.GetOutputCoordinates();
            Point ptTo = panelTo.GetInputCoordinates(index);
            //Переводим координаты точек в СК формы
            Point pt1 = panelFrom.ToParentCoordinates(ptFrom);
            Point pt2 = panelTo.ToParentCoordinates(ptTo);

            //Зададим цвет линии в зависимости от сигнала, который по ней идет
            Color color = Color.Black;
            if (panelFrom.logicElement.Output)
                color = Color.Red;
            //Соединяем точки ломаной
            LogicPainter.DrawBrokenLine(pt1, pt2, color, panelFrom.FindForm());
        } 

        /// <summary>
        /// Отрисовываем все связи элементов
        /// </summary>
        public static void DrawSchemeConnections(Scheme scheme)
        {
            //Отрисовываем все связи элементов
            foreach (var element in scheme.logicElements)
            {
                //Отрисовываем связи данного элемента с дочерними
                foreach (var item in element.Children)
                    LogicPainter.DrawConnection(element.Panel, item.Element.Panel, item.Index);
            }
        }
    }
}
