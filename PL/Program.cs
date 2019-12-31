using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using BE;
using BL;
using System.Reflection;

namespace PL
{
    public class Program
    {
         
        
         static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();
            MainScreen();



            void MainScreen()
            {
                Console.Clear();
                Console.WriteLine("Welcome to NetMotel");
                Console.WriteLine("Please choose action");
                Console.WriteLine("1) Customer demands");
                Console.WriteLine("2) Hosting unit");
                Console.WriteLine("3) Site manger");
                Console.WriteLine("4) Exit");

                switch (getUserChoise(1, 4))
                {
                    case 1:
                        customerDemandsScreen();
                        break;
                    case 2:
                        hostingUnitScreen();
                        break;
                    case 3:
                        siteMangerScreen();
                        break;
                    case 4:
                        break;
                    default:
                        MainScreen();
                        break;
                }
            }

            void customerDemandsScreen()
            {
                GuestRequest NewCastumer = new GuestRequest();
                autoInfoUpdate(NewCastumer);
                Console.WriteLine("Your order received in the system");
                Console.WriteLine("You will be notified by email about offers");
                Console.WriteLine("thank you");

            }

            void hostingUnitScreen()
            {
                Console.Clear();
                Console.WriteLine("1) new hosting unit");
                Console.WriteLine("2) Personal area");
                switch (getUserChoise(1, 2))
                {
                    case 1:
                        newHostingUnitScreen();
                        break;
                    case 2:
                        personalAreaScreen();
                        break;
                    default:
                        MainScreen();
                        break;
                }
            }
                  
           
            void personalAreaScreen()
            {
                //To-Do
            }

            void newHostingUnitScreen()
            {
                HostingUnit hostingUnit = new HostingUnit();
                autoInfoUpdate(hostingUnit);
                Console.WriteLine("The new Hosting unit is register ");
                Console.WriteLine("thank you");
            }



            void siteMangerScreen()
            {
                //To-Do
            }

            int getUserChoise(int min, int max)
            {
                int result;
                try
                {
                    result = Convert.ToInt32(Console.ReadLine());
                    if (result < min || result > max)
                        throw new IndexOutOfRangeException();
                }
                catch (Exception)
                {
                    Console.WriteLine("try again!");
                    getUserChoise(min, max);
                }
                return result;
            }

            
            void autoInfoUpdate<T>(T info)
            {
                Console.Clear();
                Console.WriteLine("Please enter info:");
                foreach (PropertyInfo p in info.GetType().GetProperties())
                {
                    Console.WriteLine("{0,5} , {1}\n", p.Name, p.GetValue(info, null));
                    try
                    {
                        string input = Console.ReadLine();
                        p.SetValue(info, Convert.ChangeType(input, p.PropertyType));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
            }
        }

    }
}

