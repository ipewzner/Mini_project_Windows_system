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

                switch (getUserChoise( 4))
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
                //Console.WriteLine("please press any key to continue");
                //Console.ReadKey();
                System.Threading.Thread.Sleep(5000);
                MainScreen();
            }

            void hostingUnitScreen()
            {
                Console.Clear();
                Console.WriteLine("1) new hosting unit");
                Console.WriteLine("2) Personal area");
                switch (getUserChoise( 2))
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
                Console.Clear();
                Console.WriteLine("1) Update husting unit");
                Console.WriteLine("2) Order");
                Console.WriteLine("3) Delete husting unit");
                Console.WriteLine("4) Main screen");

                switch (getUserChoise( 4))
                {
                    case 1:
                       // updateHustingUnit();
                        break;
                    case 2:
                     //   Order();
                        break;
                    case 3:
                     //   deleteHustingUnit();
                        break;
                    case 4:
                    default:
                        MainScreen();
                        break;
                }
            }

            void newHostingUnitScreen()
            {
                Console.Clear();
                HostingUnit hostingUnit = new HostingUnit();
                autoInfoUpdate(hostingUnit);
                Console.WriteLine("The new Hosting unit is register ");
                Console.WriteLine("thank you");
            }

            void siteMangerScreen()
            {
                Console.Clear();
                Console.WriteLine("1) Customer list query");
                Console.WriteLine("2) Hosting unit list query");
                Console.WriteLine("3) Order list query");
                Console.WriteLine("4) Host list query");
                Console.WriteLine("5) Statistics");
                Console.WriteLine("6) Main screen");

                switch (getUserChoise(6))
                {
                    case 1:
                  //      customerListQuery();
                        break;
                    case 2:
                  //      hostingUnitListQuery();
                        break;
                    case 3:
                  //      orderListQuery();
                        break;
                    case 4:
                  //      hostListQuery();
                        break;
                    case 5:
                   //     statistics();
                        break;
                    case 6:
                    default:
                        MainScreen();
                        break;
                }
            }

            int getUserChoise(int choises)
            {
                int result;
                try
                {
                    result = Convert.ToInt32(Console.ReadLine());
                    if (result < 1 || result > choises)
                        throw new IndexOutOfRangeException();
                }
                catch (Exception)
                {
                    Console.WriteLine("try again!");
                    getUserChoise(choises);
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

