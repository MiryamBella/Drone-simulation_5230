using System;
using IBL;

namespace ConsoleUI_BL
{
    class Program
    {
        static Program program;
        IBL.BL bl;

        static void Main(string[] args)
        {
            program = new Program();//start the pogram, so i can use the methods.
            program.bl = new IBL.BL();//start the BL.

            Console.WriteLine("Hello user!");
            string welcoming;
            welcoming = "\nChoose one of the following\n";
            welcoming += "ad: If you want to add somthing.\n";
            welcoming += "up: If you want to update somthing.\n";
            welcoming += "pr: If you want to see the data about some object in our colctsion.\n";
            welcoming += "ls: If you want to see the lists of all the objects in our data base.\n";
            welcoming += "ex: if you want to exit from the pogram.\n";

                string userAnser;
            do
            {
                Console.WriteLine(welcoming);
                userAnser = Console.ReadLine();

                switch (userAnser)
                {
                    case "ad":
                        program.add();
                        break;
                    case "up":
                        program.update();
                        break;
                    case "pr":
                        program.print();
                        break;
                    case "ls":
                        program.printList();
                        break;
                    case "ex":
                        Console.WriteLine("Goodby, come back again soon!");
                        break;
                    default:
                        Console.WriteLine("ERROR: try again and lisen to the diraction.");
                        break;
                }
            } while (userAnser != "ex");

        }

        //----------------------------------------------------


        /*the 4 funcs for the main program.*/
        /// <summary>
        /// Add new objects to our data base.
        /// </summary>
        void add()
        {
            string direction = "You Choose tho add somthing to our data base.\n";
            direction += "\nChoose one of the following\n";
            direction += "bs: If you want to add a new base station to the list in our data base.\n";
            direction += "qu: If you want to add a new quadocopter to the list in our data base.\n";
            direction += "cl: If you want to add a new client to the list in our data base.\n";
            direction += "pc: If you want to add a new packagh to the list in our data base.\n";
            direction += "ex: If you want to go back to the main screan.\n";
            string userAnser;
            Console.WriteLine(direction);
            userAnser = Console.ReadLine();

            switch (userAnser)
            {
                case "bs":
                    program.AddBaseStation();
                    break;
                case "qu":
                    program.AddQuadocopter();
                    break;
                case "cl":
                    program.AddClient();
                    break;
                case "pc":
                    program.AddPackage();
                    break;
                case "ex":
                    break;
                default:
                    Console.WriteLine("ERROR: You didn't lisen to the diraction.");
                    break;
            }
        }

        //add function
        void AddBaseStation() 
        {
            Console.WriteLine("Please enter ID to the base station."); ///I get information from the user and enter it into the new station
            string helpSTR = Console.ReadLine();
            int id = int.Parse(helpSTR);// i get a numbers as string and convert it to int
            Console.WriteLine("Please enter nema for the base station.");
            string name = Console.ReadLine();
            Console.WriteLine("Please enter longitude of station");
            helpSTR = Console.ReadLine();
            double lon = double.Parse(helpSTR);
            Console.WriteLine("Please enter latitude of station");
            helpSTR = Console.ReadLine();
            double lat = double.Parse(helpSTR);
            Console.WriteLine("Please enter number of charging position");
            helpSTR = Console.ReadLine();
            int numCharge = int.Parse(helpSTR);// i get a numbers as string and convert it to int
            
            //send(id, name, on, lan, numCharge)
        }
        void AddQuadocopter()
        {
            Console.WriteLine("Please enter the ID of the quadocpter"); ///i get the id of the Q.
            string helpSTR = Console.ReadLine();
            int id = int.Parse(helpSTR);
            Console.WriteLine("Please enter the modle of the quadocpter"); ///i get the modle of the Q.
            string moodle = Console.ReadLine();
            Console.WriteLine("Please enter the categorie of the weight (1 to easy, 2 to middle, 3 to heavy)");
            helpSTR = Console.ReadLine();/// I get from the user the weight by 1, 2 or 3 and in accordance to it i enter the weight
            int weigh = int.Parse(helpSTR);
            Console.WriteLine("Please enter the ID of the base station to put the qadocopter.");
            helpSTR = Console.ReadLine();/// I get from the user the id.
            int id_bs = int.Parse(helpSTR);

            //send(id, moodle, weigh, id_bs)
        }
        void AddClient() 
        {
            Console.WriteLine("Please enter the ID of the client");
            string helpSTR = Console.ReadLine();  ///accepting it as a string
            int id = int.Parse(helpSTR);   ///convert the string to int
            Console.WriteLine("Please enter the name of the client");
            string name = Console.ReadLine();
            Console.WriteLine("Please enter the phone number of the client");
            helpSTR = Console.ReadLine();
            int phoneNumber = int.Parse(helpSTR);
            Console.WriteLine("Please enter the longitude of the home");
            helpSTR = Console.ReadLine();
            double lon = double.Parse(helpSTR);
            Console.WriteLine("Please enter the latitude of the home");
            helpSTR = Console.ReadLine();
            double lat = double.Parse(helpSTR);

            //send(id, name, phoneNumber, lon, lat)
        }
        void AddPackage()
        {
            Console.WriteLine("Please enter the ID of the sender of the package"); //accepting the id of the sender
            string helpSTR = Console.ReadLine();
            int id_send = int.Parse(helpSTR);
            Console.WriteLine("Please enter the ID of the receiver of the package"); //accepting the id of the receiver
            helpSTR = Console.ReadLine();
            int id_colect = int.Parse(helpSTR);
            Console.WriteLine("Please enter the weight of the package (enter 1 to easy,2 to middle,3 to heavy)"); //accepting the weight of the package
            helpSTR = Console.ReadLine();
            int weigh = int.Parse(helpSTR);
            Console.WriteLine("Please enter the priority of the package (enter 1 to reggular,2 to fast,3 to emergency)"); //accepting the priority of the package
            helpSTR = Console.ReadLine();
            int priority = int.Parse(helpSTR);

            //send(id_send, id_colect, weigh, priority)
        }

        /*end of add.*/

        /// <summary>
        /// update things to our data base.
        /// </summary>
        void update()
        {
            string direction = "You Choose tho update somthing to our data base.\n";
            direction += "\nChoose one of the following\n";
            direction += "qua: If you want to update the name of the moodle of some quadocopter.\n";
            direction += "bst: If you want to update one of our base station.\n";
            direction += "cli: If you want to update one of our clients.\n\n";

            direction += "yes charge: If you want to send some quadocopter to charge.\n";
            direction += "not charge: If you want to stop thecharging of some quadocopter and release him.\n\n";

            direction += "conect: If you want to conect some packagh to quadocopter in our data base.\n";
            direction += "colect: If you want to colect the packagh from the quadocopter.\n";
            direction += "supply: If you want to update that the packagh was come to her place.\n";
            direction += "ex: If you want to go back to the main screan.\n";
            string userAnser;
            Console.WriteLine(direction);
            userAnser = Console.ReadLine();
            switch (userAnser)
            {
                case "qua":

                    break;
                case "bst":

                    break;
                case "cli":

                    break;
                case "yes charge":
                    program.SendQtoCharging();
                    break;
                case "not charge":
                    program.ReleaseQfromCharging();
                    break;
                case "conect":
                    program.AssignPtoQ();
                    break;
                case "colect":
                    program.CollectPbyQ();
                    break;
                case "supply":
                    program.DeliveringPtoClient();
                    break;
                case "ex":
                    break;
                default:
                    Console.WriteLine("ERROR: You didn't lisen to the diraction.");
                    break;
            }

        }

        void SendQtoCharging()
        {

            int[] indexs = new int[2];
            bool charge = true;
            bool allow = allwoToCharge(indexs, charge);
            int iq = indexs[0];
            int jb = indexs[1];
            if (!allow)
                return;
            if (DataSource.bstion[jb].chargingPositions == 0)
            {
                Console.WriteLine("The base station dont have plase to charging.");
                return;
            }


            //now after we chek if we can we will start the charging.
            Charging c = new Charging();
            c.baseStationID = DataSource.bstion[jb].IDnumber;
            c.quadocopterID = DataSource.qpter[iq].id;
            DataSource.bstion[jb].chargingPositions--;
            DataSource.qpter[iq].battery = 100;
            DataSource.qpter[iq].mode = statusOfQ.maintenance;
            DataSource.charge.Add(c);
        }
        void ReleaseQfromCharging()
        {

            int[] indexs = new int[2];
            bool charge = false;
            bool allow = allwoToCharge(indexs, charge);
            if (!allow)
                return;


            //now after we chek if we can we will start the charging.
            int iq = indexs[0];
            int jb = indexs[1];
            DataSource.bstion[jb].chargingPositions++;
            DataSource.qpter[iq].mode = statusOfQ.available;

            Charging c = new Charging();
            c.baseStationID = DataSource.bstion[jb].IDnumber;
            c.quadocopterID = DataSource.qpter[iq].id;
            DataSource.charge.Remove(c);
        }
        /// <summary>
        /// update package to be belong to a quadocopter.
        /// </summary>
        void AssignPtoQ()
        {
            Console.WriteLine("Please enter the ID of the package"); //accepting the id of the package
            string help = Console.ReadLine();
            int id = int.Parse(help);
            int i = 0;
            for (; i < DataSource.Config.index_packagh; i++) // look for the index that the package in it
                if (DataSource.packagh[i].id == id)
                    break;
            int j = 0;
            for (; j < DataSource.Config.index_quadocopter; j++) // look for index that contain quadocopter whice can take this package
                if (DataSource.qpter[j].mode == statusOfQ.available)
                    if (DataSource.qpter[j].weight == DataSource.packagh[i].weight)
                        break;
            DataSource.qpter[j].mode = statusOfQ.delivery; //change the mode of the qpter to delivery
            DataSource.packagh[i].idQuadocopter = DataSource.qpter[j].id; //enter the id of the qptr to the package
            DataSource.packagh[i].time_Belong_quadocopter = DateTime.Now; //update the appropriate time to be now 
        }
        void CollectPbyQ()
        {
            Console.WriteLine("Please enter the ID of the package"); //accepting the id of the package
            string help = Console.ReadLine();
            int id = int.Parse(help);
            for (int i = 0; i < DataSource.Config.index_packagh; i++) // look for the index that the package in it
                if (DataSource.packagh[i].id == id)
                {
                    DataSource.packagh[i].time_ColctedFromSender = DateTime.Now; //update the time
                    break;
                }
        }
        void DeliveringPtoClient()
        {
            Console.WriteLine("Please enter the ID of the package"); //accepting the id of the package
            string help = Console.ReadLine();
            int id = int.Parse(help);
            int i = 0;
            for (; i < DataSource.Config.index_packagh; i++)//look for the index of this package
                if (DataSource.packagh[i].id == id)
                    break;
            DataSource.packagh[i].time_ComeToColcter = DateTime.Now; //update the time
            for (int j = 0; j < DataSource.Config.index_quadocopter; j++) //look for index of the quadocopter whice take this package
                if (DataSource.qpter[j].id == DataSource.packagh[i].idQuadocopter)
                    DataSource.qpter[j].mode = statusOfQ.available; //update the qptr to be abailable
        }

        /*end of update*/

        /// <summary>
        /// print the data about any object from our data base.
        /// </summary>
        void print()
        {
            string direction = "You Choose tho print somthing from our data base.\n";
            direction += "\nChoose one of the following\n";
            direction += "bs: If you want to print the datails of base station.\n";
            direction += "qu: If you want to print the datails of quadocopter.\n";
            direction += "cl: If you want to print the datails of client.\n";
            direction += "pc: If you want to print the datails of packagh.\n";
            direction += "ex: If you want to go back to the main screan.\n";
            string userAnser;
            Console.WriteLine(direction);
            userAnser = Console.ReadLine();
            switch (userAnser)
            {
                case "bs":
                    dalObject.StationDisplay();
                    break;
                case "qu":
                    dalObject.QuDisplay();
                    break;
                case "cl":
                    dalObject.ClientDisplay();
                    break;
                case "pc":
                    dalObject.PackageDisplay();
                    break;
                case "ex":
                    break;
                default:
                    Console.WriteLine("ERROR: You didn't lisen to the diraction.");
                    break;
            }
        }
        /// <summary>
        /// print lists of objects from our data base.
        /// </summary>
        void printList()
        {
            string direction = "You Choose tho print the lists from our data base.\n";
            direction += "\nChoose one of the following\n";
            direction += "bs: If you want to print all the base stations in our data base.\n";
            direction += "qu: If you want to print all the quadocopter in our data base.\n";
            direction += "cl: If you want to print all the client in our data base.\n";
            direction += "pc: If you want to print all the packagh in our data base.\n\n";

            direction += "pc not qu: If you want to print all the packagh that not conect to quadocopter in our data base.\n";
            direction += "bs yes ch: If you want to print all the base stations with available charging positions in our data base.\n\n";

            direction += "ex: If you want to go back to the main screan.\n";
            string userAnser;
            Console.WriteLine(direction);
            userAnser = Console.ReadLine();
            switch (userAnser)
            {
                case "bs":
                    dalObject.ListOfStations();
                    break;
                case "qu":
                    dalObject.ListOfQ();
                    break;
                case "cl":
                    dalObject.ListOfClients();
                    break;
                case "pc":
                    dalObject.ListOfPackages();
                    break;
                case "pc not qu":
                    dalObject.ListOfPwithoutQ();
                    break;
                case "bs yes ch":
                    dalObject.ListOfStationsForCharging();
                    break;
                case "ex":
                    break;
                default:
                    Console.WriteLine("ERROR: You didn't lisen to the diraction.");
                    break;
            }
        }
    }
}
