using CollectionsAndLinq.BL.Interfaces;
using CollectionsAndLinq.BL.Models.Projects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColletionsAndLinq.Console.Display
{
    public class Menu
    {
        public string Title { get => _title; set => _title = value ; }

        private readonly IDataProcessingService _service;
        private string _title;

        public Menu(IDataProcessingService service, string title)
        {
            _service = service;
            _title = title;
        }

        public void Start()
        {
            while (true)
            {
                System.Console.WriteLine($"----------------> {Title} <----------------\n\n");
                System.Console.WriteLine(
                    "\t1. Отримати кiлькiсть таскiв у проектах конкретного користувача по id.\n" +
                    "\t2. Отримати список таскiв, призначених для конкретного користувача по id.\n" +
                    "\t3. Отримати список кортежiв (id, name) з колекцiї проектiв, у яких команда проекту мiстить бiльше заданої кiлькостi людей.\n" +
                    "\t4. Отримати список команд, вiдсортованих за назвою за зростанням, всi учасники яких народились ранiше заданого року, вiдсортованих за датою реєстрацiї користувача за спаданням.\n" +
                    "\t5. Отримати список користувачiв за алфавiтом firstName по зростанню з вiдсортованими задачами по довжинi name (за спаданням).\n" +
                    "\t6. Отримати iнформацiю про користувача по id.\n" +
                    "\t7. Отримати iнформацiю про проекти.\n" +
                    "\t8. Отримати посортовану, профiльтровану сторiнку з проектами.\n\n\n");

                System.Console.Write("ВIДКРИТИ ОПЦIЮ номер ");
                int tab = Convert.ToInt32(System.Console.ReadLine());

                System.Console.WriteLine("<---------------->  <---------------->  <---------------->\n\n");

                switch (tab)
                {
                    case -1:
                        System.Console.WriteLine("Має бути цифрою.");
                        break;
                    case 0:
                        break;
                    case 1:
                        System.Console.WriteLine("ID : ");
                        int number = Convert.ToInt32(System.Console.ReadLine());
                        
                        if(number < 0)
                        {
                            System.Console.WriteLine("Має бути додатнiм числом.");
                        }

                        Display.Show(_service.GetTasksCountInProjectsByUserIdAsync(number).Result);

                        break;
                    case 2:
                        System.Console.WriteLine("ID : ");
                        number = Convert.ToInt32(System.Console.ReadLine());

                        if (number < 0)
                        {
                            System.Console.WriteLine("Має бути додатнiм числом.");
                        }

                        Display.Show(_service.GetCapitalTasksByUserIdAsync(number).Result);

                        break;
                    case 3:
                        System.Console.WriteLine("КiЛЬКiСТЬ ЛЮДЕЙ : ");
                        number = Convert.ToInt32(System.Console.ReadLine());

                        if (number < 0)
                        {
                            System.Console.WriteLine("Має бути додатнiм числом.");
                        }

                        Display.Show(_service.GetProjectsByTeamSizeAsync(number).Result);

                        break;
                    case 4:
                        System.Console.WriteLine("РiК : ");
                        number = Convert.ToInt32(System.Console.ReadLine());

                        if (number < 0 || number > DateTime.Now.Year )
                        {
                            System.Console.WriteLine("Має бути додатнiм числом.");
                        }

                        Display.Show(_service.GetSortedTeamByMembersWithYearAsync(number).Result);

                        break;
                    case 5:

                        Display.Show(_service.GetSortedUsersWithSortedTasksAsync().Result);

                        break;
                    case 6:
                        System.Console.WriteLine("ID : ");
                        number = Convert.ToInt32(System.Console.ReadLine());

                        if (number < 0)
                        {
                            System.Console.WriteLine("Має бути додатнiм числом.");
                        }

                        Display.Show(_service.GetUserInfoAsync(number).Result);

                        break;
                    case 7:

                        Display.Show(_service.GetProjectsInfoAsync().Result);

                        break;
                    case 8:
                        /*try
                        {
                            System.Console.Write("РОЗМIР СТОРIНКИ (в елементах): ");
                            int size = Convert.ToInt32(System.Console.ReadLine());
                            System.Console.Write("НОМЕР СТОРIНКИ : ");
                            int num = Convert.ToInt32(System.Console.ReadLine());
                            
                            if(num < 1 || size < 1)
                            {
                                throw new InvalidDataException();
                            }
                        }
                        catch 
                        { 
                            System.Console.WriteLine("Має бути 1 чи більше."); 
                        }
                        */

                        Display.Show(_service.GetSortedFilteredPageOfProjectsAsync(null, null, null).Result); //add menu options for filtering, sorting, paging

                        break;
                    default:
                        System.Console.WriteLine("Має бути елемент з меню.");
                        break;


                }
                System.Console.WriteLine("Натиснiсть будь-яку клавiшу, щоб продовжити.");
                System.Console.ReadKey();
                System.Console.Clear();
            }
        }

    }
}
