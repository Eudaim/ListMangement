using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListManagement;
using ListManagement.models;
using ListManagement.helpers;
using ListManagement.services;


namespace ListManagement
{
    public class Program
    {
        static void Main()
        {
            int userInput = -1;
            int userNum = -1;
            int userInput_01;
            DateTime currentTime = DateTime.Now;
            Console.WriteLine("Welcome to List to the List Management App");
            Console.WriteLine("Please Choose an availible Optioin \n \n");
            PrintMenu();
            var Tasks = new List<Task>();
            var itemService = ItemService.Current;
            while (userInput != 0)
            {
                if (int.TryParse(Console.ReadLine(), out userInput))
                    switch (userInput)
                    {
                        case 1:
                            Task nextTask = new Task();
                            Console.Write("Please Enter Name of Task: ");
                            nextTask.Name = Console.ReadLine();
                            Console.Write("Please Enter Task Description: ");
                            nextTask.Description = Console.ReadLine();
                            Console.Write("Please Enter Task Deadline: ");
                            while(true)
                            {
                                try
                                {
                                    nextTask.DeadLine = DateTime.Parse(Console.ReadLine());
                                    if (nextTask.DeadLine < currentTime)
                                    {
                                        throw new Exception();
                                    }
                                    break;
                                }

                                catch (System.FormatException)
                                {
                                    Console.WriteLine("\n Invalid Format. Try Again.\n");
                                    continue;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("\n Error. Please Enter a Valid Date\n");
                                }
                            }
                            Console.WriteLine("\n\n Task have been added \n \n");
                            itemService.Add(nextTask);
                            PrintMenu();
                            break;
                        case 3:
                            Console.WriteLine("Choose Task You Want to Delete \n");
                            for (int i = 0; i < Tasks.Count(); i++)
                            {
                                Console.WriteLine($"[{i}] {Tasks[i].Name} : {Tasks[i].Description} Due Date: {Tasks[i].DeadLine}");
                            }
                            if (int.TryParse(Console.ReadLine(), out userNum))
                            {
                                if (userNum > Tasks.Count() || userNum < 0)
                                {
                                    Console.WriteLine("\n Task Does Not Exist\n");
                                }
                                else
                                {
                                    var selectedTask = itemService.Items[userNum];
                                    itemService.Remove(selectedTask);
                                    Console.WriteLine("\n Task Sucessfully Deleted\n");
                                }
                            }
                            break;
                        case 2:
                            Appointment nextAppointment = new Appointment();
                            Console.Write("Please Enter Name of Appointment: ");
                            nextAppointment.Name = Console.ReadLine();
                            Console.Write("Please Enter Appointment Description: ");
                            nextAppointment.Description = Console.ReadLine();
                            Console.Write("Please Enter Appointment start date: ");
                            while (true)
                            {
                                try
                                {
                                    nextAppointment.Start = DateTime.Parse(Console.ReadLine());
                                    if (nextAppointment.Start < currentTime)
                                    {
                                        throw new Exception();
                                    }
                                    break;
                                }

                                catch (System.FormatException)
                                {
                                    Console.WriteLine("\n Invalid Format. Try Again.\n");
                                    continue;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("\n Error. Please Enter a Valid Date\n");
                                }
                            }
                            Console.Write("Please Enter Appointment stop date: ");
                            while (true)
                            {
                                try
                                {
                                    nextAppointment.End = DateTime.Parse(Console.ReadLine());
                                    if (nextAppointment.End < currentTime && nextAppointment.Start > nextAppointment.End)
                                    {
                                        throw new Exception();
                                    }
                                    break;
                                }

                                catch (System.FormatException)
                                {
                                    Console.WriteLine("\n Invalid Format. Try Again.\n");
                                    continue;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("\n Error. Please Enter a Valid Date\n");
                                }
                            }
                            Console.WriteLine("\n\n Appointment has been added \n \n");
                            itemService.Add(nextAppointment);
                            PrintMenu();
                            break;
                        case 4:
                            Console.WriteLine("Choose Task You Want to Edit \n");
                            for (int i = 0; i < itemService.Items.Count(); i++)
                            {
                                Console.WriteLine($"[{i}] {itemService.Items[i].Name} : {itemService.Items[i].Description} ");
                            }
                            if (int.TryParse(Console.ReadLine(), out userNum))
                            {
                                if (userNum > Tasks.Count() || userNum < 0)
                                {
                                    Console.WriteLine("\n Task Does Not Exist\n");
                                }
                                else
                                {
                                    Console.WriteLine("What do you want to update \n \n");
                                    Console.WriteLine("1. Task Name");
                                    Console.WriteLine("2. Task Description");
                                    Console.WriteLine("3. Task DeadLine");
                                    if (int.TryParse(Console.ReadLine(), out userInput_01))
                                    {
                                        switch (userInput_01)
                                        {
                                            case 1:
                                                Console.Write("New Task Name: ");
                                                Tasks[userNum].Name = Console.ReadLine();
                                                Console.WriteLine("\n Task Name Updated\n");
                                                PrintMenu();
                                                break;
                                            case 2:
                                                Console.Write("New Task Description: ");
                                                Tasks[userNum].Name = Console.ReadLine();
                                                Console.WriteLine("\n Task Descritopion Updated\n");
                                                PrintMenu();
                                                break;
                                            case 3:
                                                Console.Write("New Task DeadLine");
                                                Tasks[userNum].Name = Console.ReadLine();
                                                Console.WriteLine("\n Task DeadLine Updated\n");
                                                PrintMenu();
                                                break;
                                            default:
                                                Console.WriteLine("\n Error. Try Again.\n");
                                                break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("\n Invalid Input\n");
                            }
                            break;
                        case 5:
                            {
                                //Complete Task
                                Console.WriteLine("Which item should I complete?");
                                if (int.TryParse(Console.ReadLine(), out int selection))
                                {
                                    var selectedItem = itemService.Items[selection - 1] as Task;

                                    if (selectedItem != null)
                                    {
                                        selectedItem.IsCompleted = true;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Sorry, I can't find that item!");
                                }
                            }
                            break;
                        case 6:
                            itemService.ShowComplete = false;
                            var userSelection = string.Empty;
                            while (userSelection != "e")
                            {
                                foreach (var item in itemService.GetPage())
                                {
                                    Console.WriteLine(item);
                                }
                                userSelection = Console.ReadLine();

                                if (userSelection == "N")
                                {
                                    itemService.NextPage();
                                }
                                else if (userSelection == "P")
                                {
                                    itemService.PreviousPage();
                                }
                                
                            }
                            break;
                        case 7:
                            var userPageInput = string.Empty;
                            while (userPageInput != "e")
                            {
                                foreach (var item in itemService.GetPage())
                                {
                                    Console.WriteLine(item);
                                }
                                foreach (var ito in itemService.Items)
                                    Console.WriteLine(ito);
                                userPageInput = Console.ReadLine();

                                if (userPageInput == "N")
                                {
                                    itemService.NextPage();
                                }
                                else if (userPageInput == "P")
                                {
                                    itemService.PreviousPage();
                                }
                            }
                            break;
                        case 8:
                            var userSearchString = string.Empty;
                            Console.WriteLine("Enter A Search String"); 
                            userSearchString = Console.ReadLine();
                            var results = itemService.Items
                            .Where(x => x.Name.Contains(userSearchString) || x.Description.Contains(userSearchString));
                            foreach(var item in results)
                                Console.WriteLine(item); 
                            break;
                        case 9:
                            itemService.Save();
                            break;
                        case 0:
                            Console.WriteLine("\n Exiting....\n");
                            break;
                        default:
                            Console.WriteLine("\n Error. Try Again\n");
                            break; 
                    };
            }

        }
        public static void PrintMenu()
        {
            Console.WriteLine("1. Create a new task.");
            Console.WriteLine("2. Create a new Appointment");
            Console.WriteLine("3. Delete an existing task.");
            Console.WriteLine("4. Edit an existing task.");
            Console.WriteLine("5. Complete a task.");
            Console.WriteLine("6. List all outstading (not complete) task.");
            Console.WriteLine("7. List all task.");
            Console.WriteLine("8. Search Task");
            Console.WriteLine("9. Save");
            Console.WriteLine("10. List Appoinments");
            Console.WriteLine("0. Exit.");
            Console.WriteLine("\n \n");

        }
    }
}