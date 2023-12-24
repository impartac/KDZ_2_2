using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace КДЗ_2_2
{
    class NumbJagged
    {
        int[][] jagArr;
        int _n;

        /// <summary>
        /// Индексатор, позволяющий получить i-тый массив класса.
        /// </summary>
        /// <param name="x"></param>
        /// <returns> Значение массива. </returns>
        /// <exception cref="ArgumentOutOfRangeException"> Индекс за гранницами класса. </exception>
        public int[] this[int x] 
        {
            get 
            {
                return (x >= 0 && x<_n)? jagArr[x] : throw new ArgumentOutOfRangeException();
            }
            set 
            {
                jagArr[x] = value;
            }
        }

        public int Length => _n;

        /// <summary>
        /// Генератор зубчатого массива.
        /// Следующий элемент генерируется, а затем массив расширяется, 
        /// после чего последнему элементу задается значение сгенерированного.
        /// l - отображает текущую длину i-того массива.
        /// </summary>
        /// <param name="N"> Кол-во строк ступенчатого массива. </param>
        /// <exception cref="ArgumentOutOfRangeException"> Длина, желаемого массива <=0 .</exception>
        public NumbJagged(int N) 
        {
            if (N <= 0) 
            {
                throw new ArgumentOutOfRangeException();
            }
            _n = N;
            jagArr = new int[N][];
            for (int i = 0; i < N; i++) 
            {
                Random rnd = new Random();
                jagArr[i] = new int[0];
                int temp = rnd.Next(0, 6);
                int l = 0;
                while ( temp!= 0)
                {
                    Array.Resize(ref jagArr[i], l + 1);
                    jagArr[i][l] = temp;
                    temp = rnd.Next(0, 6);
                    l++;
                }
                Array.Resize(ref jagArr[i], l + 1);
                jagArr[i][l] = 0;
            }
        }
        
        public NumbJagged() 
        { 
            jagArr = null;
            _n = 0;
        }



        /// <summary>
        /// Метод преобразования объекта класса в массив строк.
        /// </summary>
        /// <returns> Массив строк, где i-тая строка получена из i-той строки массива путем склеивания через пробел. </returns>
        /// <exception cref="ArgumentNullException"> Если объект класса был null .</exception>
        public string[] AsString()  
        {
            if (this is null) 
            {
                throw new ArgumentNullException();
            }
            string[] ans = new string[_n];
            for (int i = 0; i < _n; i++) 
            {
                ans[i] = String.Join(" ", this[i]) ; // Объединение массива в троку с помощью пробела.
            }
            return ans;
        }

        /// <summary>
        /// Метод определяющий кол-во треугольников, который можно составить из элементов массива.
        /// Алгоритм перебирает все возможны тройки чисел ( сохрнаяя их порядок из изначального массива).
        /// Проверка возможности составиить треугольник осуществляется проверкой Ннеравенства треугольник для сторон.
        /// </summary>
        /// <param name="i"> Номер исходной строки массива. </param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"> Массив пуст. </exception>
        /// <exception cref="ArgumentOutOfRangeException"> Индекс за границей массива. </exception>
        public int TriangleNumber(int i ) 
        {
            if (this is null)
            {
                throw new ArgumentNullException();
            }
            if (0>i || i>=_n) 
            {
                throw new ArgumentOutOfRangeException();
            }

            int ans = 0;
            for (int a = 0; a < jagArr[i].Length-1; a++) 
            {
                for( int b = a+1; b < jagArr[i].Length-1; b++) 
                {
                    for (int c = b + 1; c < jagArr[i].Length - 1; c++) 
                    {
                        int mx = Math.Max(Math.Max(jagArr[i][a], jagArr[i][b]), jagArr[i][c]);
                        if (mx< jagArr[i][a] + jagArr[i][b] + jagArr[i][c] - mx) 
                        {
                            ans++;
                        }
                    }
                }
            }
            return ans;
        }

    }

}
