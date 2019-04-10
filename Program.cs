using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace distribution
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int total = 0, sum = 0, index = 0, did = 0;
            int parts = 3; //Инициализация количества ячеек для распределения массива
            int[] result = new int[parts]; //Инициализируем массив для хранения результатов
            
            do
            {
                Console.Write("Введите числа : ");
                string input_str = Console.ReadLine();
                var input_no_sort = input_str.Split(',').Select(str => byte.Parse(str)).ToArray();//Из строки преобразовываем в массив чисел

                var input = from i in input_no_sort//Сортируем массив по возрастанию
                            orderby i
                            select i;                
                
                foreach (int i in input) //Подсчитать сумму значений входного массива
                {
                    total += i;                                       
                }

                var length = input.AsQueryable().Count();//Подсчитываем количество элементов в массиве                
                int threshold = (total / parts) - (total / length) / 2; //Вычисляем  пороговое значение для записи суммы последующих элементов
                
                foreach (int i in input) //Подсчитать сумму значений входного массива
                {
                    if (length < 2)//Если в массиве всего один элемент, то его прибавляем к минимальному значению из результата 
                    {   
                        int min_index = result.ToList().IndexOf(result.Min());               
                        result[min_index]+= i;
                    }

                    else
                        sum += i; //Добавляем значения массива в сумму постепенно
                        if (sum >= threshold) //Если полученная сумма достигает порогового значения, то полученное значение записывается в результат и сумма сбрасывается 
                        {               
                            result[index] += sum;
                            index += 1;
                            sum = 0;
                            continue;
                        }                                       
                }

                if (index < parts) //Если во входном массиве остаются числа, которые не прошли пороговое значение, то они прибавляються к последнему результату
                {
                    result[index] += sum;
                }

                foreach (int k in result) //Вывод результата распределения
                {                    
                    Console.WriteLine(k);
                }

                total = 0;
                index = 0;
                did += 1;
            }   while (did != 5);

            Console.Write("Цикл завершился. Нажмите для выхода");
            Console.ReadKey();
       }
    }
}
