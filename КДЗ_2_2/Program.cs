using System.Reflection;
using System.Reflection.Metadata;

namespace КДЗ_2_2;
class Program 
{
    /// <summary>
    /// Считываемый файл должен быть формата .txt
    /// Программа считывает только первую строчку файла.
    /// Проверка корректности файла, заключается в том, что на его первой строке стоит целое число и ничего больше.
    /// Вывод массива в файл происходит разделяе его символы с помощью пробела.
    /// Далее в выходном файле появляестя n-строк, где i-тая строка отображает сколько треугольников можно составить и i-того массива.
    /// </summary>
    static void Main() 
    {
        do 
        {
            NumbJagged numbJagged=null ;
            string filename="";
            int n=-1;
            do
            {
                try
                {
                    Console.WriteLine("Введите имя файла.");
                    filename = Console.ReadLine();
                    string path = FileProcessing.GetPathByName(filename);
                    n = FileProcessing.Read(filename);
                    numbJagged = new NumbJagged(n);
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("Введите непустую строку.");
                }
                catch (CustomAttributeFormatException)
                {
                    Console.WriteLine("Строка содержит некорреткные символы.");
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("Файл с таким именем нет в папке.");
                }
                catch (FileLoadException)
                {
                    Console.WriteLine("Неправильный формат входного файла.");
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("N - целое число большее нуля.");
                }
            }
            while (n < 0);

            Console.WriteLine("------------------------------");
            foreach (var v in numbJagged.AsString()) 
            {
                Console.WriteLine(v);
            }
            Console.WriteLine("------------------------------");

            string newpath =null;
            do
            {
                try
                {
                    Console.WriteLine("Введите имя файла.");
                    filename = Console.ReadLine();
                    newpath = FileProcessing.GetPathByName(filename);
                }
                catch (ArgumentNullException)   
                {
                    Console.WriteLine("Введите непустую строку.");
                }
                catch (CustomAttributeFormatException)
                {
                    Console.WriteLine("Строка содержит некорреткные символы.");
                }
            }
            while (newpath is null);
            File.WriteAllLines(newpath, new string[] { "Исходный массив :" });
            FileProcessing.Write(filename,numbJagged.AsString());
            File.AppendAllLines(newpath, new string[] { "Кол-во треугольников :" });
            for (int i = 0; i < n; i++) 
            {
                FileProcessing.Write(filename, new string[] { numbJagged.TriangleNumber(i).ToString() });
            }

            Console.WriteLine("Нажмите Enter , чтобы продолжить .");
        }
        while (Console.ReadKey().Key == ConsoleKey.Enter);
    }
}