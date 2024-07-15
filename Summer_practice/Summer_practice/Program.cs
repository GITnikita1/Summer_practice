using Dapper;
using Npgsql;
using System;
using System.Diagnostics;
using System.IO;
using DDPlanet;

namespace DDPlanet
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "DD_Planet summer practice";
            string database_login = "Host=localhost;Port=5432;Username=postgres;Password=pg123;Database=DD_Planet";
            ManagerDB db = new ManagerDB(database_login);

            using (var link = db.GetLink())
            {
                Console.WriteLine("СПИСОК ЗАДАЧ");
                Console.WriteLine("1. Вывести всех пользователей и роли, в которых они состоят.");
                Console.WriteLine("2. По каждой роли определить количество пользователей, которые входят в эту роль.");
                Console.WriteLine("3. Завершить работу.");

                var flag = true;
                while (flag)
                {
                    Console.Write("\nВыберите задачу для решения: ");
                    var button = Console.ReadLine();
                    if (button == "1")
                    {
                        ManagerDB.FirstTask(link);
                    }
                    else if (button == "2")
                    {
                        ManagerDB.SecondTask(link);
                    }
                    else if (button == "3")
                    {
                        Console.WriteLine("Вы завершили программу.");
                        flag = false;
                    }
                    else
                    {
                        Console.WriteLine("ОШИБКА!!! Выбирать можно лишь из \"СПИОК ЗАДАЧ\".");
                        Console.WriteLine("Попробуй снова.");
                    }
                }
            }
        }
    }
}