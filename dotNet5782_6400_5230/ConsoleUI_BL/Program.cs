using System;

namespace ConsoleUI_BL
{
    class Program
    {
        static Program program;
        BlApi.BL bl;

        static void Main(string[] args)
        {
            program = new Program();//start the pogram, so i can use the methods.
            program.bl = new BlApi.BL();//start the BL.

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

                try
                {
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
                }
                catch (BO.BLException ex)
                {
                    Console.WriteLine("Error! " + ex.Message);
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

        #region add function
        void AddBaseStation() 
        {
            Console.WriteLine("Please enter ID to the base station."); ///I get information from the user and enter it into the new station
            int id = int.Parse(Console.ReadLine());// 
            Console.WriteLine("Please enter nema for the base station.");
            string name = Console.ReadLine();
            Console.WriteLine("Please enter longitude of station");
            double lon = double.Parse(Console.ReadLine());
            Console.WriteLine("Please enter latitude of station");
            double lat = double.Parse(Console.ReadLine());
            Console.WriteLine("Please enter number of charging position");
            int numCharge = int.Parse(Console.ReadLine());

            program.bl.AddBaseStation(id, name, lon, lat, numCharge);
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

            program.bl.AddQuadocopter(id, moodle, weigh, id_bs);
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

            program.bl.AddClient(id, name, lon, lat, phoneNumber);
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
            Console.WriteLine("Please enter the priority of the package (enter 0 to reggular,2 to fast,3 to emergency)"); //accepting the priority of the package
            helpSTR = Console.ReadLine();
            int priority = int.Parse(helpSTR);

            //program.bl.AddPackage(id_send, id_colect, weigh, priority);
        }

        #endregion

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
                    program.up_Q();
                    break;
                case "bst":
                    program.up_baseStation();
                    break;
                case "cli":
                    program.up_client();
                    break;
                case "yes charge":
                    program.SendQtoCharging();
                    break;
                case "not charge":
                    program.ReleaseQfromCharging();
                    break;
                case "conect":
                    program.AssignPackageToQuadocopter();
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

        #region update function
        void up_Q()
        {
            Console.WriteLine("Please enter the ID of the quadocopter.");
            string helpSTR =Console.ReadLine();
            int id = int.Parse(helpSTR);
            Console.WriteLine("Please enter the name of the moodle of the quadocopter you want to change.");
            string modle =Console.ReadLine();

            program.bl.updateQdata(id, modle);
        }
        void up_baseStation()
        {
            string helpSTR;
            int id = 0;
            string anser = "";
            string name = "";
            int numCharge = 0;
            Console.WriteLine("Please enter the ID of the base station.");
            helpSTR = Console.ReadLine();
            id = int.Parse(helpSTR);
            Console.WriteLine("ys: If you want to update the name of the base station.\nno: If you don't want to update the name.");
            anser = Console.ReadLine();
            if(anser=="ys")
            {
                Console.WriteLine("Please enter the new name.");
                name = Console.ReadLine();
            }
            Console.WriteLine("ys: If you want to update the number of charging in the base station.\nno: If you don't want.");
            anser = Console.ReadLine();
            if (anser == "ys")
            {
                Console.WriteLine("Please enter the new number of charging positions..");
                helpSTR = Console.ReadLine();
                numCharge = int.Parse(helpSTR);
            }

            program.bl.updateSdata(id, name, numCharge);
        }
        void up_client()
        {
            string helpSTR;
            int id = 0;
            string anser = "";
            string name = "";
            int numPon = 0;
            Console.WriteLine("Please enter the ID of the client.");
            helpSTR = Console.ReadLine();
            id = int.Parse(helpSTR);
            Console.WriteLine("ys: If you want to update the name of the client.\nno: If you don't want.");
            anser = Console.ReadLine();
            if (anser == "ys")
            {
                Console.WriteLine("Please enter the new name.");
                name = Console.ReadLine();
            }
            Console.WriteLine("ys: If you want to update the number phon of the client.\nno: If you don't want.");
            anser = Console.ReadLine();
            if (anser == "ys")
            {
                Console.WriteLine("Please enter the new number phone.");
                helpSTR = Console.ReadLine();
                numPon = int.Parse(helpSTR);
            }

            program.bl.updateCdata(id, name, numPon);
        }


        void SendQtoCharging()
        {
            Console.WriteLine("Please enter the ID of the quadocopter you want to charge."); //accepting the id of the quadocopter
            string temp_str = Console.ReadLine();
            int id = int.Parse(temp_str);

            program.bl.sendQtoChrge(id);
        }
        /// <summary>
        /// release the quadocopter frp charging.
        /// </summary>
        void ReleaseQfromCharging()
        {
            Console.WriteLine("Please enter the ID of the quadocopter you want to charge."); //accepting the id of the quadocopter
            string temp_str = Console.ReadLine();
            int id = int.Parse(temp_str);

            Console.WriteLine("Please enter the number of hours the quadocopter was charging."); //accepting the id of the quadocopter
            temp_str = Console.ReadLine();
            double time = double.Parse(temp_str);

            program.bl.releaseQfromChrge(id, time);
        }
        /// <summary>
        /// update package to be belong to a quadocopter.
        /// </summary>
        void AssignPackageToQuadocopter()
        {
            Console.WriteLine("Please enter the ID of the package"); //accepting the id of the package
            string help = Console.ReadLine();
            int id = int.Parse(help);

            program.bl.assignPtoQ(id);
        }
        /// <summary>
        /// update package to be collected by quadocopter.
        /// </summary>
        void CollectPbyQ()
        {
            Console.WriteLine("Please enter the ID of the package"); //accepting the id of the package
            string help = Console.ReadLine();
            int id = int.Parse(help);

            program.bl.collectPbyQ(id);
        }
        void DeliveringPtoClient()
        {
            Console.WriteLine("Please enter the ID of the package"); //accepting the id of the package
            string help = Console.ReadLine();
            int id = int.Parse(help);
            program.bl.collectPbyQ(id);
        }

        #endregion

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

            int id = 0;///it is temp id to send to BL.
            string help = "";
            switch (userAnser)
            {
                case "bs":
                    Console.WriteLine("please enter id of station"); // the printing is by the id  that i asked from the user
                    help = Console.ReadLine();
                    id = int.Parse(help);
                    Console.WriteLine(program.bl.baseStationDisplay(id));
                    break;
                case "qu":
                    Console.WriteLine("please enter id of quadocopter"); // the printing is by the id  that i asked from the user
                    help = Console.ReadLine();
                    id = int.Parse(help);
                    Console.WriteLine(program.bl.QuDisplay(id));
                    break;
                case "cl":
                    Console.WriteLine("please enter id of client"); // the printing is by the id  that i asked from the user
                    help = Console.ReadLine();
                    id = int.Parse(help);
                    Console.WriteLine(program.bl.ClientDisplay(id));
                    break;
                case "pc":
                    Console.WriteLine("please enter id of package"); // the printing is by the id  that i asked from the user
                    help = Console.ReadLine();
                    id = int.Parse(help);
                    Console.WriteLine(program.bl.PackageDisplay(id));
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
                    program.bl.ListOfBaseStations().ForEach(Console.WriteLine);
                    break;
                case "qu":
                    program.bl.ListOfQ().ForEach(Console.WriteLine);
                    break;
                case "cl":
                    program.bl.ListOfClients().ForEach(Console.WriteLine);
                    break;
                case "pc":
                    program.bl.ListOfPackages().ForEach(Console.WriteLine);
                    break;
                case "pc not qu":
                    program.bl.ListOfPwithoutQ().ForEach(Console.WriteLine);
                    break;
                case "bs yes ch":
                    program.bl.ListOfStationsForCharging().ForEach(Console.WriteLine);
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
