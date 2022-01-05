using System;
//using DalObject;
using System.Collections.Generic;

namespace ConsoleUI
{
    class Program
    {
        static Program program;

        //DalObject.DalObject dalObject;
        static void Main(string[] args) { }
    }
}
//        {
//            program = new Program();//start the pogram, so i can use the methods.
//            program.dalObject = new DalObject.DalObject();//start the dalObject.

//            Console.WriteLine("Hello user!");
//            string welcoming;
//            welcoming = "\nChoose one of the following\n";
//            welcoming += "ad: If you want to add somthing.\n";
//            welcoming += "up: If you want to update somthing.\n";
//            welcoming += "pr: If you want to see the data about some object in our colctsion.\n";
//            welcoming += "ls: If you want to see the lists of all the objects in our data base.\n";
//            welcoming += "ex: if you want to exit from the pogram.\n";

//            //string userAnser;
//            //do
//            //{
//            //    Console.WriteLine(welcoming);
//            //    userAnser = Console.ReadLine();
//            //
//            //    switch (userAnser)
//            //    {
//            //        case "ad":
//            //            program.add();
//            //            break;
//            //        case "up":
//            //            program.update();
//            //            break;
//            //        case "pr":
//            //            program.print();
//            //            break;
//            //        case "ls":
//            //            program.printList();
//            //            break;
//            //        case "ex":
//            //            Console.WriteLine("Goodby, come again soon!");
//            //            break;
//            //        default:
//            //            Console.WriteLine("ERROR: try again and lisen to the diraction.");
//            //            break;
//            //    }
//            //} while (userAnser != "ex");
//        }


////        //----------------------------------------------------


////        /*the 4 funcs for the main program.*/
////        /// <summary>
////        /// Add new objects to our data base.
////        /// </summary>
////        void add()
////        {
////            string direction = "You Choose tho add somthing to our data base.\n";
////            direction += "\nChoose one of the following\n";
////            direction += "bs: If you want to add a new base station to the list in our data base.\n";
////            direction += "qu: If you want to add a new quadocopter to the list in our data base.\n";
////            direction += "cl: If you want to add a new client to the list in our data base.\n";
////            direction += "pc: If you want to add a new packagh to the list in our data base.\n";
////            direction += "ex: If you want to go back to the nain screan.\n";
////            string userAnser;
////            Console.WriteLine(direction);
////            userAnser = Console.ReadLine();
////            switch (userAnser)
////            {
////                case "bs":
////                    addBS();
////                    break;
////                case "qu":
////                    addQU();
////                    break;
////                case "cl":
////                    addCL();
////                    break;
////                case "pc":
////                    addPC();
////                    break;
////                case "ex":
////                    break;
////                default:
////                    Console.WriteLine("ERROR: You didn't lisen to the diraction.");
////                    break;
////            }
////        }
////        /// <summary>
////        /// update our data base.
////        /// </summary>
////        void update()
////        {
////            string direction = "You Choose tho update somthing to our data base.\n";
////            direction += "\nChoose one of the following\n";
////            direction += "conect: If you want to conect some packagh to quadocopter in our data base.\n";
////            direction += "colect: If you want to colect the packagh from the quadocopter.\n";
////            direction += "supply: If you want to update that the packagh was come to her place.\n";
////            direction += "charge: If you want to send some quadocopter to charge.\n";
////            direction += "stop charge: If you want to stop thecharging of some quadocopter and release him.\n";
////            direction += "ex: If you want to go back to the nain screan.\n";
////            string userAnser;
////            Console.WriteLine(direction);
////            userAnser = Console.ReadLine();
////            switch (userAnser)
////            {
////                case "conect":
////                    dalObject.AssignPtoQ();
////                    break;
////                case "colect":
////                    dalObject.CollectPbyQ();
////                    break;
////                case "supply":
////                    dalObject.DeliveringPtoClient();
////                    break;
////                case "charge":
////                    dalObject.SendQtoCharging();
////                    break;
////                case "stop charge":
////                    dalObject.ReleaseQfromCharging();
////                    break;
////                case "ex":
////                    break;
////                default:
////                    Console.WriteLine("ERROR: You didn't lisen to the diraction.");
////                    break;
////            }

////        }
////        /// <summary>
////        /// print the data about any object from our data base.
////        /// </summary>
////        void print()
////        {
////            string direction = "You Choose tho print somthing from our data base.\n";
////            direction += "\nChoose one of the following\n";
////            direction += "bs: If you want to print the datails of base station.\n";
////            direction += "qu: If you want to print the datails of quadocopter.\n";
////            direction += "cl: If you want to print the datails of client.\n";
////            direction += "pc: If you want to print the datails of packagh.\n";
////            direction += "ex: If you want to go back to the nain screan.\n";
////            string userAnser;
////            Console.WriteLine(direction);
////            userAnser = Console.ReadLine();
////            switch (userAnser)
////            {
////                case "bs":
////                    dalObject.StationDisplay();
////                    break;
////                case "qu":
////                    dalObject.QuDisplay();
////                    break;
////                case "cl":
////                    dalObject.ClientDisplay();
////                    break;
////                case "pc":
////                    dalObject.PackageDisplay();
////                    break;
////                case "ex":
////                    break;
////                default:
////                    Console.WriteLine("ERROR: You didn't lisen to the diraction.");
////                    break;
////            }
////        }
////        /// <summary>
////        /// print lists of objects from our data base.
////        /// </summary>
////        void printList()
////        {
////            string direction = "You Choose tho print the lists from our data base.\n";
////            direction += "\nChoose one of the following\n";
////            direction += "bs: If you want to print all the base stations in our data base.\n";
////            direction += "qu: If you want to print all the quadocopter in our data base.\n";
////            direction += "cl: If you want to print all the client in our data base.\n";
////            direction += "pc: If you want to print all the packagh in our data base.\n";
////            direction += "pnq: If you want to print all the packagh that not conect to quadocopter in our data base.\n";
////            direction += "bec: If you want to print all the base stations with available charging positions in our data base.\n";
////            direction += "ex: If you want to go back to the nain screan.\n";
////            string userAnser;
////            Console.WriteLine(direction);
////            userAnser = Console.ReadLine();
////            switch (userAnser)
////            {
////                case "bs":
////                    IEnumerable<IDAL.DO.BaseStation> b = dalObject.ListOfStations();
////                    foreach (IDAL.DO.BaseStation i in b)
////                        Console.WriteLine(i);
////                    break;
////                case "qu":
////                    IEnumerable<IDAL.DO.Quadocopter> q = dalObject.ListOfQ();
////                    foreach (IDAL.DO.Quadocopter i in q)
////                        Console.WriteLine(i);
////                    break;
////                case "cl":
////                    IEnumerable<IDAL.DO.Client> c = dalObject.ListOfClients();
////                    foreach (IDAL.DO.Client i in c)
////                        Console.WriteLine(i);
////                    break;
////                case "pc":
////                    IEnumerable<IDAL.DO.Package> p = dalObject.ListOfPackages();
////                    foreach (IDAL.DO.Package i in p)
////                        Console.WriteLine(i);
////                    break;
////                case "pnq":
////                    IEnumerable<IDAL.DO.Package> pWithoutQ = dalObject.ListOfPwithoutQ();
////                    foreach (IDAL.DO.Package i in pWithoutQ)
////                        Console.WriteLine(i);
////                    break;
////                case "bec":
////                    IEnumerable<IDAL.DO.BaseStation> station = dalObject.ListOfStationsForCharging();
////                    foreach (IDAL.DO.BaseStation i in station)
////                        Console.WriteLine(i);
////                    break;
////                case "ex":
////                    break;
////                default:
////                    Console.WriteLine("ERROR: You didn't lisen to the diraction.");
////                    break;
////            }

////        }
////        //------------the function that do the work af accept the data and add it----------  
////        /// <summary>
////        /// add base station
////        /// </summary>
////        void addBS()
////        {
////            Console.WriteLine("enter id, name, number of charging position and location(longitude and latitude)");
////            int id, num;
////            double lon, lat;
////            string name;
////            id = int.Parse(Console.ReadLine());
////            name = Console.ReadLine();
////            num = int.Parse(Console.ReadLine());
////            lon = double.Parse(Console.ReadLine());
////            lat = double.Parse(Console.ReadLine());
////            dalObject.AddBaseStation(id, name, num, lon, lat);
////        }
////        /// <summary>
////        /// add qudocopter 
////        /// </summary>
////        void addQU()
////        {
////            Console.WriteLine("enter id, moodle and whight(1 to easy, 2 to middle, 3 to heavy");
////            int id, whight;
////            string moodle;
////            id = int.Parse(Console.ReadLine());
////            moodle = Console.ReadLine();
////            whight = int.Parse(Console.ReadLine());
////            dalObject.AddQuadocopter(id, moodle, whight);
////        }
////        /// <summary>
////        /// add client 
////        /// </summary>
////        void addCL()
////        {
////            Console.WriteLine("enter id, name, phone number and location(longitude and latitude)");
////            int id, phone;
////            double lon, lat;
////            string name;
////            id = int.Parse(Console.ReadLine());
////            name = Console.ReadLine();
////            phone = int.Parse(Console.ReadLine());
////            lon = double.Parse(Console.ReadLine());
////            lat = double.Parse(Console.ReadLine());
////            dalObject.AddClient(id, name, phone, lon, lat);
////        }
////        /// <summary>
////        /// add package 
////        /// </summary>
////        void addPC()
////        {
////            Console.WriteLine("enter id of sender, id of colecter, whight and priority(1 to reggular, 2 to fast, 3 to emergency)");
////            int sender, receiver, weight, priority;
////            sender = int.Parse(Console.ReadLine());
////            receiver = int.Parse(Console.ReadLine());
////            weight = int.Parse(Console.ReadLine());
////            priority = int.Parse(Console.ReadLine());
////            dalObject.AddPackage(sender, receiver, weight, priority);
////        }
//    }
//}

