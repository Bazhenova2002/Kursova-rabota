using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kurse_work
{
    public partial class Form1 : Form
    {
        OpenFileDialog openFileDialog;
        SaveFileDialog saveFileDialog;
        List<Investor> investors = new List<Investor>();

        public Form1()
        {
            openFileDialog = new OpenFileDialog();
            saveFileDialog = new SaveFileDialog();
            InitializeComponent();
        }

        void UpdateNotes()
        {

            dataGridView1.Rows.Clear();
            foreach (var investor in investors)
            {
                dataGridView1.Rows.Add(investor.InvestorName, investor.ContractNumber, investor.HomeAdress, investor.DepositAmount, investor.ContractTerm);
            }

        }


        bool IsPositiveDigit(string st)
        {
            bool check = false;
            int temp;
            if (int.TryParse(st, out temp) == true && temp > 0)
                check = true;

            return check;
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            if (IsPositiveDigit(textBox5.Text) && IsPositiveDigit(textBox7.Text) && IsPositiveDigit(textBox8.Text))
            {
                investors.Add(new Investor()
                {
                    InvestorName = textBox4.Text,
                    ContractNumber = Convert.ToInt32(textBox5.Text),
                    HomeAdress = textBox6.Text,
                    DepositAmount = Convert.ToInt32(textBox7.Text),
                    ContractTerm = Convert.ToInt32(textBox8.Text)
                });
                UpdateNotes();
            }
            else
            {
                MessageBox.Show("Вы должны вводить положительные числа!");
            }


        }

        private void ВывестиВсехВкладчиковToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateNotes();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            UpdateNotes();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0 && this.dataGridView1.SelectedRows[0].Index != this.dataGridView1.Rows.Count - 1)
            {
                this.dataGridView1.Rows.RemoveAt(this.dataGridView1.SelectedRows[0].Index);
                int index = this.dataGridView1.SelectedRows[0].Index;
                investors.RemoveAt(index);
            }
            UpdateNotes();
        }

        private void Button5_Click(object sender, EventArgs e)
        {

            if (this.dataGridView1.SelectedRows.Count > 0 && this.dataGridView1.SelectedRows[0].Index != this.dataGridView1.Rows.Count - 1)
            {

                int index = this.dataGridView1.SelectedRows[0].Index;

                investors[index].InvestorName = textBox4.Text;
                investors[index].ContractNumber = Convert.ToInt32(textBox5.Text);
                investors[index].HomeAdress = textBox6.Text;
                investors[index].DepositAmount = Convert.ToInt32(textBox7.Text);
                investors[index].ContractTerm = Convert.ToInt32(textBox8.Text);
            }
            UpdateNotes();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            string name = textBox1.Text;
            bool check = false;
            foreach (var investor in investors)
            {
                if (investor.InvestorName == name)
                {
                    dataGridView1.Rows.Add(investor.InvestorName, investor.ContractNumber, investor.HomeAdress, investor.DepositAmount, investor.ContractTerm);
                    check = true;
                }
            }
            if (check == false)
            {
                MessageBox.Show("Не удалось найти записей с таким именем!");
                dataGridView1.Rows.Clear();
            }

        }

        private void Button2_Click(object sender, EventArgs e)
        {

            if (IsPositiveDigit(textBox2.Text))
            {
                dataGridView1.Rows.Clear();
                int deposit = Convert.ToInt32(textBox2.Text);
                bool check = false;
                foreach (var investor in investors)
                {
                    if (investor.DepositAmount > deposit)
                    {
                        dataGridView1.Rows.Add(investor.InvestorName, investor.ContractNumber, investor.HomeAdress, investor.DepositAmount, investor.ContractTerm);
                        check = true;
                    }
                }
                if (check == false)
                {
                    MessageBox.Show("Не удалось найти записей!");
                    dataGridView1.Rows.Clear();
                }
            }
            else
            {
                MessageBox.Show("Вы должны вводить положительные числа!");
            }

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            string name = textBox3.Text;
            bool check = false;

            investors = investors.Where(x => x.InvestorName != name).ToList();

            MessageBox.Show("Записи удалены!");



        }

        private void ВкладчикиДоговорКоторыхСвыше12МесToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();

            bool check = false;
            foreach (var investor in investors)
            {
                if (investor.ContractTerm > 12)
                {
                    dataGridView1.Rows.Add(investor.InvestorName, investor.ContractNumber, investor.HomeAdress, investor.DepositAmount, investor.ContractTerm);
                    check = true;
                }
            }
            if (check == false)
            {
                MessageBox.Show("Не удалось найти записей с таким именем!");
                dataGridView1.Rows.Clear();
            }
        }

        private void ОткрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                return;

            string filename = openFileDialog.FileName;

            FileStream fsR = new FileStream(filename, FileMode.Open, FileAccess.Read);

            BinaryReader br = new BinaryReader(fsR, Encoding.UTF8);


            int length = br.ReadInt32();


            for (int i = 0; i < length; i++)
            {
                investors.Add(Investor.Read(br));
            }

            dataGridView1.Rows.Clear();

            UpdateNotes();

            // Закрываем потоки
            br.Close();
            fsR.Close();
        }

        private void СохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = saveFileDialog.FileName;



            FileStream fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs, Encoding.UTF8);

            int length = investors.Count;
            bw.Write(length);

            //Пишем данные
            foreach (var investor in investors)
                investor.Write(bw);


            // Закрываем потоки
            bw.Close();
            fs.Close();
            MessageBox.Show("Файл сохранен");
        }

        private void УдалитьВсеЗаписиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            investors.Clear();
            dataGridView1.Rows.Clear();

        }

        private void Button8_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            bool check = false;
            string keyWord = textBox9.Text;
            switch (comboBox1.Text)
            {
                case "ФИО":

                    foreach (var investor in investors)
                    {
                        if (investor.InvestorName == keyWord)
                        {
                            dataGridView1.Rows.Add(investor.InvestorName, investor.ContractNumber, investor.HomeAdress, investor.DepositAmount, investor.ContractTerm);
                            check = true;
                        }
                    }
                    break;
                case "Номер договора":

                    foreach (var investor in investors)
                    {
                        if (investor.ContractNumber == Convert.ToInt32(keyWord))
                        {
                            dataGridView1.Rows.Add(investor.InvestorName, investor.ContractNumber, investor.HomeAdress, investor.DepositAmount, investor.ContractTerm);
                            check = true;
                        }
                    }
                    break;
                case "Домашний адрес":
                    foreach (var investor in investors)
                    {
                        if (investor.HomeAdress == keyWord)
                        {
                            dataGridView1.Rows.Add(investor.InvestorName, investor.ContractNumber, investor.HomeAdress, investor.DepositAmount, investor.ContractTerm);
                            check = true;
                        }
                    }
                    break;
                case "Сумма вклада":
                    foreach (var investor in investors)
                    {
                        if (investor.DepositAmount == Convert.ToInt32(keyWord))
                        {
                            dataGridView1.Rows.Add(investor.InvestorName, investor.ContractNumber, investor.HomeAdress, investor.DepositAmount, investor.ContractTerm);
                            check = true;
                        }
                    }
                    break;
                case "Срок договора":
                    foreach (var investor in investors)
                    {
                        if (investor.ContractTerm == Convert.ToInt32(keyWord))
                        {
                            dataGridView1.Rows.Add(investor.InvestorName, investor.ContractNumber, investor.HomeAdress, investor.DepositAmount, investor.ContractTerm);
                            check = true;
                        }
                    }
                    break;
                default:
                    MessageBox.Show("Выберите поле поиска!");
                    break;

            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
