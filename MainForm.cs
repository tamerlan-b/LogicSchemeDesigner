using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySchemes.Models;

namespace MySchemes
{
    /// <summary>
    /// Состояния схемы
    /// </summary>
    public enum SchemeState
    {
        WaitAction,
        DrawConnection
    }

    public partial class MainForm : Form
    {
        /// <summary>
        /// Форма для выбора числа входов
        /// </summary>
        private DialogForm inputDialog;

        /// <summary>
        /// Первая точка линии
        /// </summary>
        private Point ptFirst;

        /// <summary>
        /// Первый элемент, который надо связать
        /// </summary>
        private LogicElement elemFirst;

        public MainForm()
        {
            InitializeComponent();
            //Создаем форму для выбора количества входов
            inputDialog = new DialogForm();
            SetupForm();
        }

        /// <summary>
        /// Настроить форму для работы с логическими элементами
        /// </summary>
        private void SetupForm()
        {
            //Переходим в начальное состояние, если кликнули по форме
            //в режиме рисования связи
            this.MouseClick += (s, e) =>
            {
                if (schemeState == SchemeState.DrawConnection)
                    schemeState = SchemeState.WaitAction;
            };

            this.MouseMove += OnFormMouseMove;

            //Подписываемся на нажатия клавиш создания логических элементов
            this.toolBtnAnd.Click += (s, e) =>
            {
                //Отображаем диалоговое окно для выбора количества входов
                inputDialog.ShowDialog();
                if (inputDialog.DialogResult == DialogResult.OK)
                {
                    //Задаем тип логического элемента
                    (s as ToolStripButton).Tag = new LogicAnd(0);
                    //Создаем логический элемент
                    CreateLogicElement(s, inputDialog.InpuNum);
                }
            };
            this.toolBtnOr.Click += (s, e) =>
            {
                inputDialog.ShowDialog();
                if (inputDialog.DialogResult == DialogResult.OK)
                {
                    (s as ToolStripButton).Tag = new LogicOr(0);
                    CreateLogicElement(s, inputDialog.InpuNum);
                }
            };
            this.toolBtnNot.Click += (s, e) =>
            {
                (s as ToolStripButton).Tag = new LogicNot();
                CreateLogicElement(s, 1);
            };

            //Закрашиваем форму в белый цвет
            this.BackColor = Color.White;
        }

        /// <summary>
        /// Состояние схемы
        /// </summary>
        private SchemeState schemeState;

        /// <summary>
        /// Текущая схема
        /// </summary>
        private Scheme scheme = new Scheme();

        /// <summary>
        /// Создание логического элемента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateLogicElement(object sender, int inputNum)
        {
            //Создаем логический элемент
            LogicElement logicElement = null;
            var elemType = (sender as ToolStripButton).Tag;
            //Определяем тип логического элемента
            if(elemType is LogicAnd)
                logicElement = new LogicAnd(inputNum);
            else if(elemType is LogicOr)
                logicElement = new LogicOr(inputNum);
            else if(elemType is LogicNot)
                logicElement = new LogicNot(inputNum);

            //Создаем панель с логическим элементом
            LogicPanel panel = new LogicPanel(logicElement)
            {
                //Задаем размер панели
                Size = new Size(new Point(100, 100)),
                //Позиционируем панель
                Location = new Point(this.Width / 2, this.Height / 2),
            };

            //Подписываемся на события для возможности перетаскивания элемента
            panel.MouseDown += panel.OnMouseDown;
            panel.MouseMove += panel.OnMouseMove;
            panel.MouseUp += panel.OnMouseUp;
            //Подписываемся на событие перемещения элемента
            panel.LocationChanged += PanelLocationChanged;
            //Добавляем панель на форму
            this.Controls.Add(panel);

            //Добавляем обработчик двойного щелчка
            panel.MouseDoubleClick += OnPanelMouseDoubleClick;
            
            //Рисуем логический элемент
            panel.DrawLogicElement();

            //Добавляем элемент в схему
            scheme.AddElement(logicElement);

            //Добавляем элемент в дерево
            treeView.Nodes.Add(logicElement.ToString());
        }

        /// <summary>
        /// Обработчик события перемещения элемента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PanelLocationChanged(object sender, EventArgs e)
        {
            //ToDo: Найти метод получше
            this.Refresh();
            LogicPainter.DrawSchemeConnections(scheme);
        }

        /// <summary>
        /// Обработчик события движения мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnFormMouseMove(object sender, MouseEventArgs e)
        {
            //Если сейчас рисуем линию, то
            if (schemeState == SchemeState.DrawConnection)
            {
                this.Refresh();
                LogicPainter.DrawBrokenLine(ptFirst, e.Location, Color.Black, this);
                //Отрисовываем заново все связи
                LogicPainter.DrawSchemeConnections(scheme);
            }
        }

        /// <summary>
        /// Обработчик двойного щелчка мыши по панели
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPanelMouseDoubleClick(object sender, MouseEventArgs e)
        {
            var panel = sender as LogicPanel;
            //Определяем в какой области панели мышь
            var clickedArea = (panel).GetWhereCursor(e.Location);
            //Если кликнули по выходу схемы
            if(clickedArea == PanelArea.Outputs)
            {
                //Если схема безействует
                if (schemeState == SchemeState.WaitAction)
                {
                    //Переводим схему в состояние рисования связи
                    schemeState = SchemeState.DrawConnection;

                    //Вычисляем координаты выхода
                    Point first = panel.GetOutputCoordinates();

                    //Запоминаем начальную точку
                    ptFirst = new Point()
                    {
                        X = panel.Location.X + first.X,
                        Y = panel.Location.Y + first.Y
                    };
                    //Запоминаем логический элемент по которому кликнули
                    elemFirst = panel.logicElement;
                }
            }
            //Если кликнули по входу схемы
            else if(clickedArea == PanelArea.Inputs)
            {
                //Если схема безействует
                if (schemeState == SchemeState.WaitAction)
                {
                    //Получаем элемент, по которому дважды кликнули
                    var clickedElement = panel.logicElement;
                    //Вычисляем индекс входа, по которому кликнули
                    int index = panel.GetClickedInput(e.Location);

                    //Если нет родительского элемента, подключенного к данному входу
                    if(!clickedElement.Parents.Exists(con => con.Index == index))
                    {
                        //Инвертируем значение кликнутого входа
                        clickedElement[index] = !clickedElement[index];
                        panel.Parent.Refresh();
                        //Отрисовываем все связи
                        LogicPainter.DrawSchemeConnections(scheme);
                        //Красим входы и выходы всех элементов
                        foreach (var item in scheme.logicElements)
                            item.Panel.DrawLogicElement(true);
                    }
                }
                //Если схема в режиме рисования
                else if (schemeState == SchemeState.DrawConnection)
                {
                    panel.Parent.Refresh();
                    //Получаем второй элемент
                    var elemSecond = panel.logicElement;
                    //Вычисляем индекс входа, к которому подключаемся
                    int index = panel.GetClickedInput(e.Location);
                    //Проверяем, свободен ли данный вход
                    if (!elemSecond.Parents.Exists(con => con.Index == index))
                    {
                        //Проверяем, не совпадает ли первая схема со второй
                        if (elemFirst != elemSecond)
                        {
                            //Соединяем элементы программно
                            elemFirst.AddChild(elemSecond, index);
                        }
                    }
                    //Вычисляем значения всех логических элементов
                    foreach (var elem in scheme.logicElements)
                        if (elem.Parents.Count == 0 && elem.Children.Count != 0)
                            elem.SendValue();
                    //Отрисовываем все связи
                    LogicPainter.DrawSchemeConnections(scheme);
                    //Красим входы и выходы всех элементов
                    foreach (var item in scheme.logicElements)
                        item.Panel.DrawLogicElement(true);
                    //Переводим схему в состояние бездействия
                    schemeState = SchemeState.WaitAction;

                    //Найти индекс elemFirst в списке scheme
                    int indexFirst = scheme.logicElements.IndexOf(elemFirst);
                    if(indexFirst >= 0)
                    {
                        //Нарисовать ту связь, которая к нему добавилась
                        treeView.Nodes[indexFirst].Nodes.Add("["+ (index + 1) + "] " + elemSecond.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// Закрыть форму
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
