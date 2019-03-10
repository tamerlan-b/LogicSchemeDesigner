using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySchemes.Models
{
    /// <summary>
    /// Форма для выбора количества входных сигналов элемента
    /// </summary>
    public class DialogForm:Form
    {
        private ComboBox comboBox;
        private Button btnOk;
        private Button btnCancel;
        private Label lbTitle;

        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        public DialogForm()
        {
            InitializeComponent();
            //Добавляем название форме
            this.Text = "Number of inputs";
            //Заполняем comboBox
            for (int i = 1; i < 8; i++)
                comboBox.Items.Add(i);
            //Располагаем диалоговое окно по центр родительской формы
            this.StartPosition = FormStartPosition.CenterParent;    
        }

        /// <summary>
        /// Количество входов логического элемента
        /// </summary>
        public int InpuNum;

        private void InitializeComponent()
        {
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.comboBox = new System.Windows.Forms.ComboBox();
            this.lbTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(16, 99);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(130, 99);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // comboBox
            // 
            this.comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox.FormattingEnabled = true;
            this.comboBox.Location = new System.Drawing.Point(16, 46);
            this.comboBox.Name = "comboBox";
            this.comboBox.Size = new System.Drawing.Size(189, 21);
            this.comboBox.TabIndex = 0;
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Location = new System.Drawing.Point(13, 20);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(118, 13);
            this.lbTitle.TabIndex = 3;
            this.lbTitle.Text = "Select number of inputs";
            // 
            // DialogForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoSize = true;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(223, 152);
            this.Controls.Add(this.lbTitle);
            this.Controls.Add(this.comboBox);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "DialogForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        /// <summary>
        /// Обработчик нажатия на кнопку "ОК"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            //Считываем выбранное значение
            int inputNum = comboBox.SelectedIndex + 1;
            if (inputNum > 0)
            {
                //Сохраняем его
                InpuNum = inputNum;
                //Выставляем статус диалогового окна
                DialogResult = DialogResult.OK;
                //Закрываем форму
            }
            else
                DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Обработчик нажатия на кнопку "Отмена"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
