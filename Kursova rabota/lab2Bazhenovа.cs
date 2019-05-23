using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursova_rabota
{
    class lab2Bazhenovа
    {
        public partial class Form1 : Form
        {
            Random ran = new Random();

            public Form1()
            {
                InitializeComponent();
            }

            private void CalculateButton_Click(object sender, EventArgs e)
            {
                int n = Convert.ToInt32(textBoxN.Text);
                int m = Convert.ToInt32(textBoxM.Text);
                Square[] squares = new Square[n];  // объявляем массив квадратов 
                RegularSquarePrism[] prisms = new RegularSquarePrism[m];  // и призм

                for (int i = 0; i < n; i++)  // инициализируем массив квадратов 
                {
                    squares[i] = new Square(ran.NextDouble() * 10);
                }
                for (int i = 0; i < m; i++)  // и призм
                {
                    prisms[i] = new RegularSquarePrism(ran.NextDouble() * 10, ran.NextDouble() * 10);
                }

                double[] squaresArea = new double[n];  // создаем массивы, в которые будем записывать диагонали призм
                double[] prismsDiagonal = new double[m];  // и площади квадратов
                for (int i = 0; i < n; i++)  // инициализируем эти массивы
                {
                    squaresArea[i] = squares[i].GetArea();
                }
                for (int i = 0; i < m; i++)
                {
                    prismsDiagonal[i] = prisms[i].GetPrismDiagonal();
                }

                int indexMax1 = 0; double maxN = 0;  // ищем наибольшие элементы этих массивов
                int indexMax2 = 0; double maxM = 0;  // и их индексы
                for (int i = 0; i < n - 1; i++)
                {
                    if (maxN < squaresArea[i])
                    {
                        maxN = squaresArea[i];
                        indexMax1 = i;
                    }
                }
                for (int i = 0; i < m - 1; i++)
                {
                    if (maxM < prismsDiagonal[i])
                    {
                        maxM = prismsDiagonal[i];
                        indexMax2 = i;
                    }
                }

                ansLabel1.Text = squares[indexMax1].GetInformationAboutTheFigure(squares[indexMax1]);  // потом просто выводим информацию
                ansLabel2.Text = prisms[indexMax2].GetInformationAboutTheFigure(prisms[indexMax2]);    // о фигуре с нужным индекос
            }
        }
    }
}
