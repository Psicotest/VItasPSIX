﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using SqlConn;
using word = Microsoft.Office.Interop.Word;
using InsertWord;
using System.Threading;

namespace Psico
{
    public partial class Fenom1 : Form
    {
        SqlConnection con = DBUtils.GetDBConnection(); // Класс подключения к БД
        int kolvoRb; // Количество radiobutton
        DataGridView datagr = new DataGridView(); // Создание datagridview
        WordInsert wordinsert = new WordInsert(); // Класс записи данных в ворд документ

        public Fenom1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Запись данных в ворд документ
            try
            {
                //Отключение счётчика времени на форме
                timer1.Enabled = false;

                // Присвоение переменных
                Program.AllT = Program.AllT + Program.Fenom1T; // Общее время выполнения задачи
                Program.fenomenologiya = richTextBox2.Text; // Данные о резюме по феноменологии
                Program.glavsved = richTextBox3.Text; // Общие сведения по феноменологии

                if (Program.glavsved != "")
                {
                    // Данные, которые нужно записать в ворд документ
                    Program.Insert = "Главные сведения по феноменологии: " + Program.glavsved + "";
                    wordinsert.Ins();
                }

                if (Program.fenomenologiya !="")
                {
                    // Данные, которые нужно записать в ворд документ
                    Program.Insert = "Резюме по феноменологии: " + Program.fenomenologiya + "";
                    wordinsert.Ins();
                }

                // Данные, которые нужно записать в ворд документ
                Program.Insert = "Время на феноменологии 1:" + Program.Fenom1T + " сек";
                wordinsert.Ins();

                // Переход на главную форму задачи
                Zadacha zadacha = new Zadacha();
                zadacha.Show();
                Close();
            }

            // Если возникла ошибка при записи данных в ворд документ
            catch
            {
                MessageBox.Show("Отсутствует шаблон протокола! Обратитесь в службу поддержки.", "Внимание!",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning); // Вывод сообщения
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Program.diagnoz == 3) // Если диагноз верный
            {
                DialogResult result = MessageBox.Show("Если вы закроете программу, у вас не будет возможности вернутся к этой задаче!", "Внимание!",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning); // Вывод сообщения

                if (result == DialogResult.OK) // Если пользователь нажал ОК
                {
                    // Запись данных о решении задачи
                    SqlCommand StrPrc1 = new SqlCommand("resh_add", con);
                    StrPrc1.CommandType = CommandType.StoredProcedure;
                    StrPrc1.Parameters.AddWithValue("@Users_id", Program.user);
                    StrPrc1.Parameters.AddWithValue("@Zadacha_id", Program.NomerZadachi);
                    StrPrc1.ExecuteNonQuery();

                    // Запись данных в ворд документ
                    try
                    {

                        timer1.Enabled = false;

                        Program.AllT = Program.AllT + Program.Fenom1T;
                        Program.fenomenologiya = richTextBox2.Text;
                        Program.glavsved = richTextBox3.Text;

                        if (Program.glavsved != "")
                        {
                            // Данные, которые нужно записать в ворд документ
                            Program.Insert = "Главные сведения по феноменологии: " + Program.glavsved + "";                  
                            wordinsert.Ins();
                        }

                        if (Program.fenomenologiya != "")
                        {
                            // Данные, которые нужно записать в ворд документ
                            Program.Insert = "Резюме по феноменологии: " + Program.fenomenologiya + "";
                            wordinsert.Ins();
                        }

                        Program.Insert = "Время на феноменологии 1:" + Program.Fenom1T + " сек";
                        wordinsert.Ins();

                        // Выход из программы
                        Application.Exit();
                    }

                    // Если возникла ошибка при записи данных в ворд документ
                    catch
                    {
                        MessageBox.Show("Отсутствует шаблон протокола! Обратитесь в службу поддержки.", "Внимание!",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning); // Вывод сообщения
                    }
                }
            }

            // Если задача не решена или диагноз неверный
            else
            {
                DialogResult result = MessageBox.Show("Если вы закроете программу, ваши данные не сохранятся!", "Внимание!",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning); // Вывод сообщение

                if (result == DialogResult.OK) // Если пользователь нажал ОК
                {

                    // Запись данных
                    try
                    {

                        timer1.Enabled = false;

                        Program.AllT = Program.AllT + Program.Fenom1T;
                        Program.fenomenologiya = richTextBox2.Text;
                        Program.glavsved = richTextBox3.Text;

                        if (Program.glavsved != "")
                        {
                            // Данные, которые нужно записать в ворд документ
                            Program.Insert = "Главные сведения по феноменологии: " + Program.glavsved + "";
                            wordinsert.Ins();
                        }

                        if (Program.fenomenologiya != "")
                        {
                            // Данные, которые нужно записать в ворд документ
                            Program.Insert = "Резюме по феноменологии: " + Program.fenomenologiya + "";
                            wordinsert.Ins();
                        }

                        Program.Insert = "Время на феноменологии 1:" + Program.Fenom1T + " сек";
                        wordinsert.Ins();

                        // Выход из программы
                        Application.Exit();
                    }

                    // Если возникла ошибка при записи данных в ворд документ
                    catch
                    {
                        MessageBox.Show("Отсутствует шаблон протокола! Обратитесь в службу поддержки.", "Внимание!",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning); // Вывод сообщения
                    }
                }
            }
        }

        private void Fenom1_Load(object sender, EventArgs e)
        {
            Program.Fenom1T = 0; // Переменная времени на фореме
            timer1.Enabled = true; // Счётчик времени на форме

            richTextBox2.Text = Program.fenomenologiya; // Запись данных о резюме по феноменологии
            richTextBox3.Text = Program.glavsved; // Запись данных о главных сведения по феноменологии

            con.Open(); // подключение к БД

            // Выбор данных из БД
            SqlCommand Zaprosi = new SqlCommand("select Zapros, sved from zadacha where id_zadacha = " + Program.NomerZadachi + "", con);
            SqlDataReader dr = Zaprosi.ExecuteReader();
            dr.Read();
            label3.Text = dr["Zapros"].ToString();
            label1.Text = "Задача №" + Convert.ToString(Program.NomerZadachi) + "   " + dr["sved"].ToString() + "";
            dr.Close();

            // Подсчёт количества radiobutton, необходимых для заполнения формы
            SqlCommand kolvo = new SqlCommand("select count(*) as 'kolvo' from fenom1 where zadacha_id = " + Program.NomerZadachi + "", con);
            SqlDataReader dr0 = kolvo.ExecuteReader();
            dr0.Read();
            kolvoRb = Convert.ToInt32(dr0["kolvo"].ToString());
            dr0.Close();
            kolvoRb = kolvoRb + 1;

            // Создание таблицы с данными из БД
            datagr.Name = "datagrview";
            datagr.Location = new Point(300,300);
            SqlDataAdapter da1 = new SqlDataAdapter("select rb, rbtext from fenom1 where zadacha_id = " + Program.NomerZadachi + "", con);
            SqlCommandBuilder cb1 = new SqlCommandBuilder(da1);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1, "fenom1");
            datagr.DataSource = ds1.Tables[0];
            datagr.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            panel1.Controls.Add(datagr);
            datagr.Visible = false;

            // Динамическое создание radiobutton на форме
            for (int y = 150, i = 1; i < kolvoRb; i++)
            {
                RadioButton radioButton = new RadioButton();
                radioButton.Name = "radiobutton" + i + "";
                radioButton.Text = Convert.ToString(datagr.Rows[i-1].Cells[0].Value);
                radioButton.Location = new Point(17, y);
                radioButton.AutoSize = true;
                radioButton.CheckedChanged += radiobutton_checkedchanged;
                panel1.Controls.Add(radioButton);
                y = y + 30;
            }

            // Адаптация разрешения экрана пользователя
            Rectangle screen = Screen.PrimaryScreen.Bounds;
            if (Convert.ToInt32(screen.Size.Width) < 1300)
            {
                Width = 1024;
                Height = 768;

                panel2.Width = 1024;
                panel2.Height = 768;

                panel1.Width = 1003;
                panel1.Height = 747;

                richTextBox1.Width = 450;
                richTextBox2.Width = 450;
                richTextBox3.Width = 450;

                label3.MaximumSize = new Size(950, 64);
                label3.AutoSize = true;
                label5.Width = 450;

                button3.Left = button3.Left - 350;
                button1.Left = button1.Left - 340;
                label4.Left = label4.Left - 170;
                richTextBox1.Left = richTextBox1.Left - 170;
                richTextBox2.Left = richTextBox2.Left - 170;

                foreach (Control ctrl in panel1.Controls)
                {
                    int newFontSize = 12; //размер
                    ctrl.Font = new Font(ctrl.Font.FontFamily, newFontSize);
                }
            }

            // Позиционирование элементов формы на экране пользователя
            panel1.Left = Width / 2 - panel1.Width / 2;
            Left = Convert.ToInt32(screen.Size.Width) / 2 - Width / 2;
            label1.Left = panel1.Width / 2 - label1.Width / 2;
            label3.Left = panel1.Width / 2 - label3.Width / 2;
        }

        public void radiobutton_checkedchanged(object sender, EventArgs e)
        {
            // Выбор всех radiobutton на форме
            RadioButton radiobtn = (RadioButton)sender;

            // Переборка radiobutton по их количеству
            for (int x = 1; x < kolvoRb; x++)
            {
                // При выборе определённого radiobutton
                if (radiobtn.Name == "radiobutton" + x + "")
                {
                    //Запись данных в richtextbox о выбранном radiobutton
                    richTextBox1.Text = Convert.ToString(datagr.Rows[x-1].Cells[1].Value);

                    if (radiobtn.Checked == true)
                    {
                        // Запись данных в ворд документ
                        try
                        {
                            // Запись данных о нажатии на radiobutton
                            Program.Insert = "Просмотрено: " + radiobtn.Text + "";

                            wordinsert.CBIns();
                        }

                        // При возникновении ошибки при записи данных в ворд документ
                        catch
                        {
                            MessageBox.Show("Отсутствует шаблон протокола! Обратитесь в службу поддержки.", "Внимание!",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning); // Вывод сообщения
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Запись данных в ворд документ
            try
            {

                timer1.Enabled = false;
                Program.AllT = Program.AllT + Program.Fenom1T;
                Program.fenomenologiya = richTextBox2.Text;
                Program.glavsved = richTextBox3.Text;

                if (Program.glavsved != "")
                {
                    // Данные, которые нужно записать в ворд документ
                    Program.Insert = "Главные сведения по феноменологии: " + Program.glavsved + "";
                    wordinsert.Ins();
                }

                if (Program.fenomenologiya != "")
                {
                    // Данные, которые нужно записать в ворд документ
                    Program.Insert = "Резюме по феноменологии: " + Program.fenomenologiya + "";
                    wordinsert.Ins();
                }

                Program.Insert = "Время на феноменологии 1: " + Program.Fenom1T + " сек";
                wordinsert.Ins();

                // Переход на следующую форму
                Fenom2 fenom2 = new Fenom2();
                fenom2.Show();
                Close();
            }

            // При возникновении ошибки при записи данных в ворд документ
            catch
            {
                MessageBox.Show("Отсутствует шаблон протокола! Обратитесь в службу поддержки.", "Внимание!",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning); // Вывод сообщения
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Program.Fenom1T = Program.Fenom1T + 1; // Счётчик времени на форме
        }
    }
}
