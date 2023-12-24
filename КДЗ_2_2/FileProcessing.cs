using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace КДЗ_2_2
{
    /// <summary>
    /// Класс для работы с файлами.
    /// </summary>
   static class FileProcessing
   {
        /// <summary>
        /// Метод проверки файла на корректное имя.
        /// </summary>
        /// <param name="fileName"> Имя файла. </param>
        /// <exception cref="ArgumentNullException"> Имя файла пустое. </exception>
        /// <exception cref="CustomAttributeFormatException"> Имя файла содержит некорректные символы. </exception>
        public static void CorrectName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException();
            }
            foreach (char v in fileName)
            {
                if (Path.GetInvalidFileNameChars().Contains(v))
                {
                    throw new CustomAttributeFormatException();
                }
            }
        }

        /// <summary>
        /// Метод проверки существования файла в директории исполняемого файла.
        /// </summary>
        /// <param name="fileName"> Имя файла . </param>
        /// <exception cref="FileNotFoundException"> Файла с таикм именем нет в директории. </exception>
        /// <exception cref="ArgumentNullException"> Имя файла пустое. </exception>
        /// <exception cref="CustomAttributeFormatException"> Имя файла содержит некорректные символы. </exception>
        static void Exist(string fileName)
        {
            try
            {
                string path = GetPathByName(fileName);
                if (!File.Exists(path))
                {
                    throw new FileNotFoundException();
                }
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException();
            }
            catch (CustomAttributeFormatException)
            {
                throw new CustomAttributeFormatException();
            }
        }

        /// <summary>
        /// Метод генерации пути к фалйу, лежащего в директории исполняемого файла.
        /// </summary>
        /// <param name="fileName"> Имя файла. </param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"> Имя файла пустое .</exception>
        /// <exception cref="CustomAttributeFormatException"> Имя файла содержит некорректные символы. </exception>
        public static string GetPathByName(string fileName)
        {
            try
            {
                FileProcessing.CorrectName(fileName);
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException();
            }
            catch (CustomAttributeFormatException)
            {
                throw new CustomAttributeFormatException();
            }
            return Directory.GetCurrentDirectory() + '\\' + fileName + ".txt"; 
        }

        /// <summary>
        /// Метод считывания строки.
        /// </summary>
        /// <param name="fileName"> Имя файла. </param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"> Имя файла пустое .</exception>
        /// <exception cref="CustomAttributeFormatException"> Имя файла содержит некорректные символы. </exception>
        /// <exception cref="FileNotFoundException"> Файла с таким именем не существует. </exception>
        /// <exception cref="FileLoadException"> Файл некорректный. </exception>
        public static int Read(string fileName)
        {
            try
            {
                FileProcessing.Exist(fileName);
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException();
            }
            catch (CustomAttributeFormatException)
            {
                throw new CustomAttributeFormatException    ();
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException();
            }
            int k;
            StreamReader sr = new StreamReader(FileProcessing.GetPathByName(fileName));
            try
            {
                k = int.Parse(sr.ReadLine());
            }
            catch (FormatException)
            {
                throw new FileLoadException();
            }
            return k;
        }

        /// <summary>
        /// Метод вывод массива строк в файл по имени.
        /// </summary>
        /// <param name="fileName"> Имя файла. </param>
        /// <param name="output"> Массив выводимых строк. </param>
        /// <exception cref="ArgumentNullException"> Имя фалйа или выводимые строки пустые. </exception>
        /// <exception cref="CustomAttributeFormatException"> Имя файла содержит некорректные символы. </exception>
        public static void Write(string fileName, string[] output)
        {
            try
            {
                FileProcessing.CorrectName(fileName);
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException();
            }
            catch (CustomAttributeFormatException)
            {
                throw new CustomAttributeFormatException();
            }

            string path = FileProcessing.GetPathByName(fileName);
            File.AppendAllLines(path, output); 

        }
    }
}
