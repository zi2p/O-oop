#nullable enable
using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Reports.DAL.Entities;
using Reports.Server.Database;
using Reports.Server.Services;

namespace Reports.Clients
{
    internal static class Program
    {
        private static readonly ReportsDatabaseContext _context;
        private static EmployeeService _service = new EmployeeService(_context);
        internal static void Main(string[] args)
        {
            Console.WriteLine("В настоящее время " + DateTime.Now + ".\n");
            Console.WriteLine("Вы можете воспользоваться слудующими функциями:\n 1-посмотреть список задач\n" +
                              " 2-посмотреть список сотрудников\n 3-посмотреть какие задачи выполяет данный " +
                              "сотрудник(по его личному номеру)\n 4-посмотреть очет по прошедшей неделе\n 5-добавить " +
                              "комментарий к задаче\n 6-добавить задачу в отчет\n");
            int ans = Convert.ToInt32(Console.ReadLine());
            switch (ans)
            {
                case 1:
                    Task1();
                    break;
                case 2:
                    Task2();
                    break;
                case 3:
                    Task3();
                    break;
                case 4:
                    Task4();
                    break;
                case 5:
                    Task5();
                    break;
                case 6:
                    Task6();
                    break;
                default:
                    break;
            }
        }

        private static void Task1()
        {
            Console.WriteLine(_service.GetList());
        }

        private static void Task2()
        {
            Console.WriteLine(_service.Employees());
        }

        private static void Task3()
        {
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(_service.GetTaskById(id));
        }

        private static void Task4()
        {
            Console.WriteLine(_service.WeekReport());
        }

        private static void Task5()
        {
            Console.WriteLine("Введите номер пользователя: \n");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите имя полльзователя: \n");
            string name = Console.ReadLine() ?? throw new InvalidOperationException();
            Employee employee = new Employee(id, name);
            Console.WriteLine("Введите номер задачи для комментирования: \n");
            int idTask = Convert.ToInt32(Console.ReadLine());
            TaskModel tm = _service.GetTaskById(idTask);
            if (tm == null)
            {
                Console.WriteLine("Запись не найдена.\n");
                throw new Exception("error..");
            }
            
            Console.WriteLine("Введите комментарий: \n");
            string comment = Console.ReadLine();
            Console.WriteLine(comment);
        }

        private static void Task6()
        {
            Console.WriteLine("Введите номер сотрудника, затем, введите номер задачи: \n");
            int idEmployee = Convert.ToInt32(Console.ReadLine());
            int idTask = Convert.ToInt32(Console.ReadLine());
            Employee employee = _service.FindById(idEmployee);
            if (employee==null) Console.WriteLine("Сотрудник не найден.\n");
            Console.WriteLine(_service.AddTaskToReport(employee.Report(),_service.GetTaskById(idTask)));
        }
        
        internal static void Main1(string[] args)
        {
            CreateEmployee("Aboba");
            //FindEmployeeById("ac8ac3ce-f738-4cd6-b131-1aa0e16eaadc");
            FindEmployeeByName("Aboba");
            FindEmployeeByName("kek");
            Console.WriteLine("В настоящее время " + DateTime.Now);
        }
        
        private static void CreateEmployee(string name)
        {
            // Запрос к серверу
            var request = WebRequest.Create($"https://localhost:5001/employees/?name={name}");
            request.Method = WebRequestMethods.Http.Post;
            WebResponse response = request.GetResponse();
        
            // Чтение ответа
            Stream responseStream = response.GetResponseStream();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
            string responseString = readStream.ReadToEnd();
        
            // Десериализация (перевод JSON'a к C# классу)
            Employee? employee = JsonConvert.DeserializeObject<Employee>(responseString);
        
            Console.WriteLine("Created employee:");
            if (employee == null) return;
            Console.WriteLine($"Id: {employee.Id}");
            Console.WriteLine($"Name: {employee.Name}");
        }
        
        private static void FindEmployeeById(string id)
        {
            // Запрос к серверу
            var request = WebRequest.Create($"https://localhost:5001/employees/?id={id}");
            request.Method = WebRequestMethods.Http.Get;
        
            try
            {
                WebResponse? response = request.GetResponse();
        
                // Чтение ответа
                Stream? responseStream = response.GetResponseStream();
                using var readStream = new StreamReader(responseStream, Encoding.UTF8);
                string? responseString = readStream.ReadToEnd();
        
                // Десериализация (перевод JSON'a к C# классу)
                Employee? employee = JsonConvert.DeserializeObject<Employee>(responseString);
        
                Console.WriteLine("Found employee by id:");
                if (employee == null) return;
                Console.WriteLine($"Id: {employee.Id}");
                Console.WriteLine($"Name: {employee.Name}");
            }
            catch (WebException e)
            {
                Console.WriteLine("Employee was not found");
                Console.Error.WriteLine(e.Message);
            }
        }
        
        private static void FindEmployeeByName(string name)
        {
            // Запрос к серверу
            var request = WebRequest.Create($"https://localhost:5001/employees/?name={name}");
            request.Method = WebRequestMethods.Http.Get;
            try
            {
                WebResponse? response = request.GetResponse();
        
                // Чтение ответа
                Stream? responseStream = response.GetResponseStream();
                using var readStream = new StreamReader(responseStream, Encoding.UTF8);
                string? responseString = readStream.ReadToEnd();
        
                // Десериализация (перевод JSON'a к C# классу)
                Employee? employee = JsonConvert.DeserializeObject<Employee>(responseString);
        
                Console.WriteLine("Found employee by name:");
                if (employee == null) return;
                Console.WriteLine($"Id: {employee.Id}");
                Console.WriteLine($"Name: {employee.Name}");
            }
            catch (WebException e)
            {
                Console.WriteLine("Employee was not found");
                Console.Error.WriteLine(e.Message);
            }
        }
    }
}