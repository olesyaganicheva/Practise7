using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Практика_Задание_7_12
{
    class Практика_Задание_7_12
    {
        static List<string> AllDefines = new List<string>();
        static void Main(string[] args)
        {
            Console.WriteLine("Введите кол-во переменных в функции:");
            var size = ReadNaturalNum();
            Regex reg = new Regex(@"^\s*[01*]{" + Math.Pow(2, size).ToString() + @"}\s*$");
            Console.WriteLine("Введите вектор значений булевой функции:");
            var function = InputVector(size, reg);
            if (!CheckS(function) && !function.Contains('*'))
            {
                Console.WriteLine("Функция: {0} полностью определена и несамодвойственна", function);
                return;
            }
            GenerateAll(function, "");
            if (AllDefines.Count == 0)
                Console.WriteLine("Функцию: {0} невозможно доопределить до несамодвойственной", function);
            else
            {
                Console.WriteLine("Все возможные доопределения функции: {0} до несамодвойственной:", function);
                Array.Sort(AllDefines.ToArray());
                foreach (var curDefine in AllDefines)
                    Console.WriteLine(curDefine);
            }
        }
        static string InputVector(int size, Regex reg)
        {
            string inpVec;
            do
            {
                inpVec = Console.ReadLine();
                if (!reg.IsMatch(inpVec))
                    Console.WriteLine("Вектор введён некорректно, попробуйте ещё раз\nРазмер должен быть {0}\n" +
    "Используемые символы: \"0\", \"1\", \"*\"", Math.Pow(2, size));
                else
                    break;

            } while (true);
            return inpVec;
        }
        static int ReadNum()
        {
            int inpNum;
            while (true)
            {
                try
                {
                    inpNum = Convert.ToInt32(Console.ReadLine());
                    return inpNum;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Внимание: {0} Попробуйте ещё раз", e.Message);
                }
            }
        }
        static int ReadNaturalNum()
        {
            int inpNum = 0;
            while (inpNum < 1)
            {
                inpNum = ReadNum();
                if (inpNum < 1)
                    Console.WriteLine("Необходимо ввести натуральное число");
            }
            return inpNum;

        }
        static bool CheckS(string func)
        {
            for (int i = 0; i < func.Length / 2; ++i)
                if (func[i] != '*' && func[i] == func[func.Length - 1 - i])
                    return false;
            return true;
        }
        static void GenerateAll(string func, string cur)
        {
            if (cur.Length == func.Length)
            {
                if (!CheckS(cur))
                    AllDefines.Add(cur);
                return;
            }
            if (func[cur.Length] == '*')
            {
                GenerateAll(func, cur + '0');
                GenerateAll(func, cur + '1');
            }
            else
                GenerateAll(func, cur + func[cur.Length]);
        }
    }
}