using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp82
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool isRunning = true;

            string[] fullNames = new string[0];
            string[] jobs = new string[0];

            while (isRunning)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Добро пожаловать в управление кадровым учетом");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"\nВыберите действие:\n[1] - Добавить запись;\n[2] - Показать все записи;\n[3] - Удалить запись;\n[4] - Поиск по фамилии;\n[5] - Выход;\n");
                Console.Write($"Введите команду: ");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        AddEntry(ref fullNames, ref jobs);
                        break;
                    case "2":
                        ShowEntries(in fullNames, in jobs);
                        break;
                    case "3":
                        DeleteEntry(ref fullNames, ref jobs);
                        break;
                    case "4":
                        SearchEntries(ref fullNames, ref jobs);
                        break;
                    case "5":
                        Console.WriteLine($"\nЗакрытие приложения.");
                        Console.WriteLine();
                        isRunning = false;
                        break;
                }
                Console.Clear();
            }
        }

        static void AddEntry(ref string[] fullNames, ref string[] jobs)
        {
            Console.Write($"\nВведите фамилию нового участника: ");
            string personFullName = Console.ReadLine();
            Console.WriteLine($"Фамилия добавлена.");

            Console.Write($"\nВведите позицию в досье нового участника: ");
            string personJob = Console.ReadLine();
            Console.WriteLine($"Позиция добавлена.");

            fullNames = ExpandArray(fullNames);
            fullNames[fullNames.Length - 1] = personFullName;

            jobs = ExpandArray(jobs);
            jobs[fullNames.Length - 1] = personJob;

            Console.WriteLine($"\nНовая запись успешно добавлена!\n");
            ClearConsole();
        }

        static string[] ExpandArray(string[] array, int index = 1)
        {
            string[] tempArray = new string[array.Length + index];

            for (int i = 0; i < array.Length; i++)
            {
                tempArray[i] = array[i];
            }

            return tempArray;
        }

        static void ShowEntries(in string[] fullNames, in string[] jobs)
        {
            Console.WriteLine($"\nПоказ всех записей: ");

            int entryIndex = 1;

            for (int i = 0; i < fullNames.Length; i++)
            {
                Console.WriteLine("Запись #" + (entryIndex + i) + ": " + fullNames[i] + " - " + jobs[i]);
            }
            Console.WriteLine($"\n:::::::::::::::::::::::\n");
            ClearConsole();
        }

        static void ReduceArray(ref string[] array, ref int userIndex)
        {
            if (array.Length - 1 > 0)
            {
                string[] tempArray = new string[array.Length - 1];

                for (int i = 0; i < userIndex - 1; i++)
                {
                    tempArray[i] = array[i];
                }
                for (int i = userIndex; i < array.Length; i++)
                {
                    tempArray[i - 1] = array[i];
                }
                array = tempArray;
            }
            else
            {
                Console.WriteLine($"\nНет записей для удаления.Возврат в главное меню.");
                ClearConsole();
            }
        }

        static void DeleteEntry(ref string[] fullNames, ref string[] jobs)
        {
            if (fullNames == null || fullNames.Length == 0)
            {
                Console.WriteLine($"\nНет записей для удаления.Возврат в главное меню.");
                ClearConsole();
            }
            else
            {
                Console.WriteLine($"\nИнициализация процесса удаления....");
                Console.Write($"\nВведите номер удаляемой записи:  ");
                int entryIndex = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine($"\nВы уверены, что хотите удалить последнюю запись?");
                Console.WriteLine($"[Да] или [Нет]\n");
                Console.Write($"Введите: ");
                string userInput = Console.ReadLine().ToLower();

                switch (userInput)
                {
                    case "да":
                        ReduceArray(ref fullNames, ref entryIndex);
                        ReduceArray(ref jobs, ref entryIndex);
                        Console.WriteLine($"\nЗапись удалена.\n");
                        break;
                    case "нет":
                        Console.WriteLine($"\nОтмена процесса удаления.\n");
                        break;
                    default:
                        Console.WriteLine($"\nВведите правильную команду.Возврат в главное меню\n");
                        break;
                }
                ClearConsole();
            }
        }

        static void SearchEntries(ref string[] fullNames, ref string[] jobs)
        {
            string searchedSurname;

            int entryIndex = 1;

            Console.Write($"\nПоиск записи по фамилии: ");
            searchedSurname = Console.ReadLine();

            for (int i = 0; i < fullNames.Length; i++)
            {
                string[] wordedEntries = fullNames[i].Split();

                foreach (var word in wordedEntries)
                {
                    if (searchedSurname.ToLower() == word.ToLower())
                    {
                        Console.WriteLine("Запись #" + (entryIndex + i) + ": " + fullNames[i] + " - " + jobs[i]);
                        Console.WriteLine("\n");
                        ClearConsole();
                        break;
                    }
                }
            }
        }

        static void ClearConsole()
        {
            Console.Write($"Нажмите 'Enter'...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
