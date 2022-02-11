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
                        case 2:
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
                        case 3:
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
                        case 4:
                            Console.WriteLine("What Task Would you like to Complete?");
                            for (int i = 0; i < Tasks.Count(); i++)
                            {
                                Console.WriteLine($"[{i}] {Tasks[i].Name} : {Tasks[i].Description}  Due Date: {Tasks[i].DeadLine}");
                            }
                            if (int.TryParse(Console.ReadLine(), out userNum))
                            {

                                if (userNum > Tasks.Count() || userNum < 0)
                                {
                                    Console.WriteLine("\n Task Does Not Exist\n");
                                }
                                else
                                {
                                    Tasks[userNum].IsCompleted = true;
                                    Console.WriteLine("Task Marked for Completion");
                                }
                            }
                            break;
                        case 5:
                            break;
                        case 6:
                            var userPageInput = string.Empty;
                            while (userPageInput != "E")
                            {
                                foreach (var item in itemService.GetPage())
                                {
                                    Console.WriteLine(item);
                                }
                                userPageInput = Console.ReadLine();
                                switch (userPageInput)
                                {
                                    case "n":
                                        itemService.NextPage();
                                        break;
                                    case "p":
                                        itemService.PreviousPage();
                                        break;
                                }
                            }
                            break;
                        case 7:
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
                        case 10:
                            itemService.Load();
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
            Console.WriteLine("2. Delete an existing task.");
            Console.WriteLine("3. Edit an existing task.");
            Console.WriteLine("4. Complete a task.");
            Console.WriteLine("5. List all outstading (not complete) task.");
            Console.WriteLine("6. List all task.");
            Console.WriteLine("7. Search Task");
            Console.WriteLine("8. Search Attendes in Appointments");
            Console.WriteLine("9. Save");
            Console.WriteLine("10. Load");
            Console.WriteLine("0. Exit.");
            Console.WriteLine("\n \n");

        }
    }
}