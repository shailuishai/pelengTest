using System;
using Eventer.Services;

namespace Eventer
{
    class Program
    {
        static void Main(string[] args)
        {
            var eventManager = new EventManager();
            bool exit = false;

            Console.WriteLine("Добро пожаловать в Eventer!");

            while (!exit)
            {
                Console.WriteLine("\nВыберите действие:");
                Console.WriteLine("1. Добавить событие");
                Console.WriteLine("2. Показать все события");
                Console.WriteLine("3. Показать предстоящие события");
                Console.WriteLine("4. Отметить событие как выполненное");
                Console.WriteLine("5. Удалить событие");
                Console.WriteLine("0. Выход");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            AddNewEvent(eventManager);
                            break;
                        case 2:
                            ShowAllEvents(eventManager);
                            break;
                        case 3:
                            ShowUpcomingEvents(eventManager);
                            break;
                        case 4:
                            MarkEventAsCompleted(eventManager);
                            break;
                        case 5:
                            RemoveEvent(eventManager);
                            break;
                        case 0:
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Неверный выбор. Попробуйте снова.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Неверный ввод. Пожалуйста, введите число.");
                }
            }
        }

        private static void AddNewEvent(EventManager manager)
        {
            Console.WriteLine("Введите название события:");
            string title = Console.ReadLine();

            Console.WriteLine("Введите описание события:");
            string description = Console.ReadLine();

            Console.WriteLine("Введите дату и время события (в формате dd.MM.yyyy HH:mm):");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime dateTime))
            {
                Console.WriteLine("Введите место проведения:");
                string location = Console.ReadLine();

                var newEvent = manager.AddEvent(title, description, dateTime, location);
                Console.WriteLine($"Событие успешно добавлено (ID: {newEvent.Id})");
            }
            else
            {
                Console.WriteLine("Неверный формат даты и времени.");
            }
        }

        private static void ShowAllEvents(EventManager manager)
        {
            var events = manager.GetAllEvents();
            DisplayEvents("Все события:", events);
        }

        private static void ShowUpcomingEvents(EventManager manager)
        {
            var events = manager.GetUpcomingEvents();
            DisplayEvents("Предстоящие события:", events);
        }

        private static void DisplayEvents(string header, System.Collections.Generic.IEnumerable<Models.Event> events)
        {
            Console.WriteLine(header);
            bool hasEvents = false;
            foreach (var evt in events)
            {
                hasEvents = true;
                Console.WriteLine($"ID: {evt.Id}");
                Console.WriteLine(evt.ToString());
                Console.WriteLine();
            }
            if (!hasEvents)
            {
                Console.WriteLine("Нет событий для отображения.");
            }
        }

        private static void MarkEventAsCompleted(EventManager manager)
        {
            Console.WriteLine("Введите ID события:");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                if (manager.MarkAsCompleted(id))
                {
                    Console.WriteLine("Событие отмечено как выполненное.");
                }
                else
                {
                    Console.WriteLine("Событие не найдено.");
                }
            }
            else
            {
                Console.WriteLine("Неверный формат ID.");
            }
        }

        private static void RemoveEvent(EventManager manager)
        {
            Console.WriteLine("Введите ID события для удаления:");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                if (manager.RemoveEvent(id))
                {
                    Console.WriteLine("Событие успешно удалено.");
                }
                else
                {
                    Console.WriteLine("Событие не найдено.");
                }
            }
            else
            {
                Console.WriteLine("Неверный формат ID.");
            }
        }
    }
}
