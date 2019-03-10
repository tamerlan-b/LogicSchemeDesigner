using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MySchemes.Models
{
    /// <summary>
    /// Перечисление со значениями областей panel
    /// </summary>
    public enum PanelArea
    {
        Inputs,
        Middle,
        Outputs
    };

    /// <summary>
    /// Панель, представляющая собой отображение графического элемента
    /// </summary>
    public class LogicPanel:Panel
    {
        /// <summary>
        /// Области панели, где располагаются входы, выходы и центральная часть
        /// </summary>
        float inputArea, outputArea, middleArea;

        /// <summary>
        /// Логический элемент панели
        /// </summary>
        public LogicElement logicElement;

        /// <summary>
        /// Положение указателя мыши относительно панели
        /// </summary>
        private Point mouseRelativeToPanel;

        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        public LogicPanel()
        {
            inputArea = 0.2f;
            outputArea = inputArea;
            middleArea = 1 - 2 * inputArea;
        }

        /// <summary>
        /// Конструктор с параметром
        /// </summary>
        /// <param name="logicElement">Логический элемент, представленный на панели</param>
        public LogicPanel(LogicElement logicElement):this()
        {
            this.logicElement = logicElement;
            logicElement.Panel = this;
        }

        /// <summary>
        /// Обработчик события движения мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnMouseUp(object sender, MouseEventArgs e)
        {
            var panel = (Panel)sender;
            panel.Tag = null;
        }

        /// <summary>
        /// Обработчик события движения мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnMouseMove(object sender, MouseEventArgs e)
        {

            var panel = (Panel)sender;
            if (panel.Tag != null)
            {
                //Создаем новое положение панели
                var panelLocation = new System.Drawing.Point()
                {
                    //Вычисляем положение панели относительно формы
                    X = e.Location.X - mouseRelativeToPanel.X + panel.Location.X,
                    Y = e.Location.Y - mouseRelativeToPanel.Y + panel.Location.Y
                };
                //Обновляем положение панели с элементом
                panel.Location = panelLocation;
            }
        }

        /// <summary>
        /// Обработчик события нажатия мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnMouseDown(object sender, MouseEventArgs e)
        {
            //Приводим объект sender к типу Panel
            var panel = (Panel)sender;
            //Создаем объект Tag у панели
            panel.Tag = new object();
            //Запоминаем положение курсора относительно панели
            mouseRelativeToPanel = e.Location;
        }

        /// <summary>
        /// Узнать в какой части panel находится курсор
        /// </summary>
        /// <param name="cursorPos"></param>
        /// <returns></returns>
        public PanelArea GetWhereCursor(Point cursorPos)
        {
            if (cursorPos.X <= this.Width * inputArea)
                return PanelArea.Inputs;
            else if (cursorPos.X <= this.Width * middleArea)
                return PanelArea.Middle;
            else
                return PanelArea.Outputs;

        }

        /// <summary>
        /// Переводит координаты точки из системы формы в систему панели
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public Point ToPanelCoordinates(Point p)
        {
            Point p1 = new Point()
            {
                X = p.X - this.Location.X,
                Y = p.Y - this.Location.Y
            };
            return p1;
        }

        /// <summary>
        /// Добавляем картинку логического элемента на панель
        /// </summary>
        public void DrawLogicElement(bool colorizeElementPins = false)
        {
            this.BackgroundImage = DrawImage(colorizeElementPins);
        }

        /// <summary>
        /// Рисуем картинку с логическим элементом
        /// </summary>
        /// <returns></returns>
        public Bitmap DrawImage(bool colorizeElementPins)
        {
            //Соотношение частей логического элемента
            float leftArea = 0.2f, rightArea = leftArea;
            float middleArea = 1 - 2 * leftArea;
            Bitmap bmp = new Bitmap(this.Width, this.Height);
            //Создаем перо черного цвета и толщиной 1 пиксель
            using (Pen pen = new Pen(Color.Black, 1))
            {
                //Создаем объект класса Grathics и даем ему возможность рисовать н панели
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    //Закрашиваем панель в белый цвет
                    g.FillRectangle(new SolidBrush(Color.White), 0, 0, this.Width, this.Height);
                    //Рисуем прямоугольник в middle части панели
                    g.DrawRectangle(pen, leftArea * this.Width, 0, middleArea * this.Width, this.Height - pen.Width);
                    //Создаем шрифт для надписи на элементе
                    var font = new Font(FontFamily.GenericSansSerif, 20);

                    //Символ логического элемента
                    string symbol = logicElement is LogicAnd ? "&" : "1";

                    //Добавляем надпись в углу нарисованного прямоугольника
                    g.DrawString(symbol, font, pen.Brush, new PointF(leftArea * this.Width, 0));

                    //Расстояние между входами
                    int inputDist = this.Height / (logicElement.Inputs.Length + 1);
                    //Рисуем прямые линии для всех входов
                    for (int i = 0; i < logicElement.Inputs.Length; i++)
                    {
                        //Высота расположения линии
                        int lineHeight = (i + 1) * this.Height / (logicElement.Inputs.Length + 1);
                        //Если на входе элемента 1, красим в красный
                        if (colorizeElementPins && logicElement[i])
                            pen.Color = Color.Red;
                        else
                            pen.Color = Color.Black;
                        g.DrawLine(pen, 0, lineHeight, leftArea * this.Width, lineHeight);
                    }
                    //Если на выходе микросхемы 1, то красим выход в красный
                    if (colorizeElementPins && logicElement.Output)
                        pen.Color = Color.Red;
                    else
                        pen.Color = Color.Black;

                    //Рисуем линию выхода элемента
                    g.DrawLine(pen, this.Width * (1 - rightArea), this.Height / 2, this.Width, this.Height / 2);

                    //Возвращаем исходный цвет
                    pen.Color = Color.Black;

                    //Если данный элемент является логическим НЕ
                    if (logicElement is LogicNot)
                    {
                        //Задаем радиус круга
                        int radius = (int)(rightArea * this.Width) / 3;
                        //Создаем прямоугольник, ограничивающий круг
                        Rectangle circle = new Rectangle((int)(this.Width * (1 - rightArea)) - radius, this.Height / 2 - radius, 2 * radius, 2 * radius);
                        //Закрашиваем круг в белый цвет
                        g.FillEllipse(Brushes.White, circle);
                        //Рисуем окружность
                        g.DrawEllipse(pen, circle);
                    }

                    //Сохраняем нарисованный элемент
                    //string path = "../../Data/";
                    //bmp.Save(path + "LogicElement.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
                }

            }
            return bmp;
        }

        /// <summary>
        /// Вычислить координаты входа, по которому кликнули
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public int GetClickedInput(Point pt)
        {
            int index = -1;
            //Определяем в какой области находится точка
            var pos = GetWhereCursor(pt);
            if(pos == PanelArea.Inputs)
            {
                //Определяем число областей, в которых находятся входы
                int numberOfAreas = logicElement.Inputs.Length;
                //Определяем ширину одной области
                int areaHeight = this.Height / numberOfAreas;
                //Вычисляем индекс входа по высоте расположения точки и расстоянию между входами
                index = pt.Y / areaHeight; 
            }
            return index;
        }

        /// <summary>
        /// Вычислить координаты входа по индексу
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Point GetInputCoordinates(int index)
        {
            Point pt = new Point() { X = 0 };
            //Расстояние между входами
            int inputDist = this.Height / (this.logicElement.Inputs.Length + 1);
            //Вычисляем ординату входа
            pt.Y = (index + 1) * inputDist;
            return pt;
        }

        /// <summary>
        /// Вычислить координаты выхода элемента
        /// </summary>
        /// <returns></returns>
        public Point GetOutputCoordinates()
        {
            return new Point()
            {
                X = this.Width,
                Y = this.Height / 2
            };
        }

        /// <summary>
        /// Перевести координаты точки из СК панели в СК формы
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public Point ToParentCoordinates(Point pt)
        {
            return new Point()
            {
                X = this.Location.X + pt.X,
                Y = this.Location.Y + pt.Y
            };
        }
    }
}
