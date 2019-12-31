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
            var newBL = new MyBl();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();
            MainScreen();

            #region screen
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
                customerDemands();
            }
            void hostingUnitScreen()
            {
                Console.Clear();
                Console.WriteLine("1) new hosting unit");
                Console.WriteLine("2) Personal area");
                switch (getUserChoise( 2))
                {
                    case 1:
                        newHostingUnit();
                        break;
                    case 2:
                        personalAreaScreen();
                        break;
                    default:
                        MainScreen();
                        break;
                }
            }
            void OrderScreen()
            {  /*
                Console.Clear();
                Console.WriteLine("Welcome to NetMotel");
                Console.WriteLine("Please choose action");
                Console.WriteLine("1) Customer demands");
                Console.WriteLine("2) Hosting unit");
                Console.WriteLine("3) Site manger");
                Console.WriteLine("4) Exit");

                switch (getUserChoise(4))
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
                }*/
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
                        updateHustingUnit();
                        break;
                    case 2:
                        OrderScreen();
                        break;
                    case 3:
                        deleteHustingUnit();
                        break;
                    case 4:
                    default:
                        MainScreen();
                        break;
                }
            }
            #endregion
            

            #region function
            void customerDemands()
            {
                GuestRequest NewCastumer = new GuestRequest();
                autoInfoUpdate(NewCastumer);
                if (newBL.AddGuestRequest(NewCastumer)) throw new Exception("adding fail!");
                Console.WriteLine("Your order received in the system");
                Console.WriteLine("You will be notified by email about offers");
                Console.WriteLine("thank you");
                System.Threading.Thread.Sleep(5000);
                MainScreen();
            }
            void newHostingUnit()
            {
                Console.Clear();
                HostingUnit hu = new HostingUnit();
                autoInfoUpdate(hu);
                newBL.AddHostingUnit(hu);
                Console.WriteLine("The new Hosting unit is register ");
                Console.WriteLine("thank you");
                System.Threading.Thread.Sleep(5000);
                MainScreen();
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
            bool deleteHustingUnit()
            {
                Console.WriteLine("Enter hosting unit key");
                return newBL.UnitRemove(inputSarielNumber(10000000));
            }

                      //
            void updateHustingUnit()
            {
                Console.WriteLine("Enter hosting unit key");
                HostingUnit hu = newBL.GetHostingUnit(inputSarielNumber(10000000));
                newBL.UnitRemove(hu.HostingUnitKey);
                autoInfoUpdate(hu);
                newBL.AddHostingUnit(hu);
                Console.WriteLine("Your order received in the system");
                Console.WriteLine("You will be notified by email about offers");
                Console.WriteLine("thank you");
                System.Threading.Thread.Sleep(5000);
                MainScreen();
            }

            #endregion
            #region helper functin
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
                    result = getUserChoise(choises);
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
            int inputSarielNumber(int max) {
                int result;
                try
                {
                    result = Convert.ToInt32(Console.ReadLine());
                    if (result < 0 || result > max)
                        throw new IndexOutOfRangeException();
                }
                catch (Exception)
                {
                    Console.WriteLine("try again!");
                    result= inputSarielNumber(max);
                }
                return result;
            }
            #endregion
        }

    }
}

