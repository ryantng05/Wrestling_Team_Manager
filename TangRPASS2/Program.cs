//Author: Ryan Tang
//File Name: Program.cs
//Project Name: TangRPASS2
//Creation Date: April 3, 2023
//Modified Date: April 16, 2023
//Description: Runs the entire management program

using System;
using System.IO;

namespace TangRPASS2
{
    internal class Program
    {
        //Store the File IO systems
        static StreamReader infile;
        static StreamWriter outfile;

        //Store the new team object
        static Team teams = new Team();

        //Store the alphabet
        const string ALPHABET = "abcdefghijklmnopqrstuvwxyz";

        //Store all the menu options
        const string CREATE_TEAM = "1";
        const string LOAD_TEAM = "2";
        const string EXIT = "3";
        const string DISPLAY_UNIFORM_SIGNED_OUT = "1";
        const string SORT = "2";
        const string SEARCH = "3";
        const string DISPLAY_TEAM_STATS = "4";
        const string ADD_WRESTLER = "5";
        const string ADD_COACH = "6";
        const string EXIT_LOAD = "7";
        const string WRESTLER_SORT = "1";
        const string COACH_SORT = "2";
        const string EXIT_SORT = "3";

        static void Main(string[] args)
        {
            //Store the user menu loop variable
            bool done = false;

            //Store the loop variable for the options after loading
            bool doneLoad;

            //Store the user menu loop variable for sorting
            bool doneSort = false;

            //Store the user menu options
            string userMenuOption = "";
            string loadOption;
            string personOption;

            //Store the user file name requests
            string userCreateFileName = "";
            string tempCreateFileName;
            string userLoadFileName;

            //Store the sort type request
            string sortType;

            //Loop the user main menu until user exits
            while (!done)
            {
                //Clear the screen
                Console.Clear();

                //Display menu options
                Console.WriteLine("Welcome to the Wrestling Team Manager!");
                Console.WriteLine("");
                Console.WriteLine("1. Create a Team File\n2. Load a Team File\n3. Exit");
                Console.WriteLine("");
                userMenuOption = Console.ReadLine();

                //Go through specific actions depending on user option
                switch (userMenuOption)
                {
                    case CREATE_TEAM:
                        //Clear the team out of the lists
                        teams.ClearTeam();

                        //Ask for the file name to create a store it into the temp variable
                        Console.WriteLine("Enter a File name to create (without .txt)");
                        tempCreateFileName = Console.ReadLine() + ".txt";

                        //Store the file name into the userCreateFileName variable only if there are letters in it
                        for (int i = 0; i < ALPHABET.Length; i++)
                        {
                            for (int j = 0; j < tempCreateFileName.Length; j++)
                            {
                                if (ALPHABET[i].Equals(tempCreateFileName[j]))
                                {
                                    userCreateFileName = tempCreateFileName;
                                }
                            }
                        }

                        //Give user feedback if they entered an invalid file name and create team if it is valid
                        if (userCreateFileName.Equals(""))
                        {
                            Console.Clear();
                            teams.UserFeedback("Invalid File Name!", "", ConsoleColor.Red);
                            Console.ReadLine();
                        }
                        else if (File.Exists(userCreateFileName).Equals(true))
                        {
                            Console.Clear();
                            Console.WriteLine("File already exist");
                            Console.WriteLine("Press Enter to Continue!");
                            Console.ReadLine();
                        }
                        else
                        {
                            CreateTeam(userCreateFileName);
                        }    
                        break;

                    case LOAD_TEAM:
                        //Reset Loop
                        doneLoad = false;

                        //Clear the team out of the lists
                        teams.ClearTeam();

                        //Ask the user for a file name to load
                        Console.WriteLine("Enter a File name to load (without .txt)");
                        userLoadFileName = Console.ReadLine() + ".txt";

                        //Give user feedback if they entered an invalid file name and load team if it is valid
                        if (File.Exists(userLoadFileName).Equals(false))
                        {
                            Console.Clear();
                            teams.UserFeedback("Invalid File Name!", "", ConsoleColor.Red);
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.Clear();

                            //Load the team requested
                            LoadTeam(userLoadFileName);

                            //Give feedback if the team is not a real team which does not contain a coach or wrestler
                            if (teams.GetNumCoaches().Equals(0) || teams.GetNumWrestlers().Equals(0))
                            {
                                //Display can't load team because the team does not contain a coach or wrestler
                                teams.UserFeedback("Cannot Load Team,", "Team Does not Contain a Coach or Wrestler", ConsoleColor.Red);
                                Console.ReadLine();

                                //If the team cannot load then don't start the loop
                                doneLoad = true;
                            }

                            //Keep looping until user exits the load menu
                            while (!doneLoad)
                            {
                                Console.Clear();

                                //Ask user to select a menu option and store it
                                Console.WriteLine("Select an option!");
                                Console.WriteLine("");
                                Console.WriteLine("1. Display Wrestlers Uniform Signed Out\n2. Sort\n3. Search\n4. Display Team Stats\n5. Add Wrestler\n6. Add Coach\n7. Exit");
                                loadOption = Console.ReadLine();

                                //Go through specific actions depending on user option
                                switch (loadOption)
                                {
                                    case DISPLAY_UNIFORM_SIGNED_OUT:
                                        //Display all the wrestlers who have a uniform signed out
                                        Console.Clear();
                                        teams.DisplayWrestlersUniformSignedOut();
                                        break;

                                    case SORT:
                                        //Reset loop
                                        doneSort = false;

                                        //Keep looping the sort menu until the user exits
                                        while (!doneSort)
                                        {
                                            Console.Clear();

                                            //Ask the user which person they want to sort and store it
                                            Console.WriteLine("Select which person to sort!");
                                            Console.WriteLine("");
                                            Console.WriteLine("1. Wrestler\n2. Coach\n3. Exit");
                                            personOption = Console.ReadLine();

                                            //Go through specific actions depending on user option
                                            switch (personOption)
                                            {
                                                case WRESTLER_SORT:
                                                    Console.Clear();

                                                    //Display the sorting menu and store user request
                                                    Console.WriteLine("Select which sort type");
                                                    Console.WriteLine("");
                                                    Console.WriteLine("1. Gender and Last Name\n2. Gender and Weight Category\n3. Gender and Wins");
                                                    sortType = Console.ReadLine();

                                                    //Sort wrestlers based on user option
                                                    teams.SortWrestlers(sortType);
                                                    break;

                                                case COACH_SORT:
                                                    Console.Clear();

                                                    //Display the sorting menu and store user request
                                                    Console.WriteLine("Select which sort type");
                                                    Console.WriteLine("");
                                                    Console.WriteLine("1. Last Name\n2. Gender\n3. Coach Type");
                                                    sortType = Console.ReadLine();

                                                    //Sort coaches based on user option
                                                    teams.SortCoaches(sortType);
                                                    break;

                                                case EXIT_SORT:
                                                    //Exit the sort menu
                                                    doneSort = true;
                                                    break;
                                                
                                                default:
                                                    //If the user entered an invalid option then keep displaying the menu with feedback
                                                    Console.WriteLine("Invalid Option\nPress Enter to Continue!");
                                                    Console.ReadLine();
                                                    break;
                                            }
                                        }
                                        break;

                                    case SEARCH:
                                        //Search for a coach or wrestler
                                        teams.Search(userLoadFileName);
                                        break;

                                    case DISPLAY_TEAM_STATS:
                                        //Display the total team stats
                                        teams.DisplayTeamStats();
                                        break;

                                    case ADD_WRESTLER:
                                        //Ask and write a wrestler to the team file
                                        WriteWrestler(userLoadFileName);
                                        break;

                                    case ADD_COACH:
                                        //Ask and write a coach to the team file
                                        WriteCoach(userLoadFileName);
                                        break;

                                    case EXIT_LOAD:
                                        //Exit the load menu
                                        doneLoad = true;
                                        break;

                                    default:
                                        //If user entered an invalid request then keep displaying sort menu with feedback
                                        Console.WriteLine("Invalid Option\nPress Enter to Continue!");
                                        Console.ReadLine();
                                        break;
                                }
                            }
                        }
                        break;

                    case EXIT:
                        //Exit the program
                        done = true;
                        break;
                    
                    default:
                    //If user enters invalid option then keep displaying menu options with feedback
                    Console.WriteLine("Invalid Option\nPress Enter to Continue!");
                    Console.ReadLine();
                    break;
                }
            }

        }

        //Pre: userCreateFileName is valid or invalid
        //Post: None
        //Desc: Asks for coach and wrestler data and creates a new team file writes a team inside it
        public static void CreateTeam(string userCreateFileName)
        {
            //Store all the menu options
            const string ADD_COACH = "1";
            const string ADD_WRESTLER = "2";
            const string FINISH_CREATE_TEAM = "3";
            const string EXIT_CREATE_TEAM = "4";

            //Store the create team menu option
            string createTeamOption;

            //Stores the menu options loop for adding coaches, wrestlers, and creating the team
            bool done = false;

            //Keep looping until the user is done adding coaches and wrestlers then exits
            while (!done)
            {
                Console.Clear();

                //Display the menu options for add to a team and store it
                Console.WriteLine("Select an option");
                Console.WriteLine("");
                Console.WriteLine("1. Add a Coach\n2. Add Wrestler\n3. Finish Create Team (if possible)\n4. Exit");
                createTeamOption = Console.ReadLine();

                //Go through specific actions depending on user option
                switch (createTeamOption)
                {
                    case ADD_COACH:
                        //Ask and write the coaches data into the created file
                        WriteCoach(userCreateFileName);
                        break;

                    case ADD_WRESTLER:
                        //Ask and write the coaches data into the created file
                        WriteWrestler(userCreateFileName);
                        break;

                    case FINISH_CREATE_TEAM:
                        //Check if there are coaches and wrestlers on the team and create the team
                        if (teams.GetNumCoaches().Equals(0) && teams.GetNumWrestlers().Equals(0))
                        {
                            Console.Clear();
                            Console.WriteLine("Please Add a Coach and Wrestler First!");
                            Console.WriteLine("Press Enter to Continue!");
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Team Created!");
                            Console.WriteLine("Press Enter to Continue!");
                            Console.ReadLine();
                            done = true;
                        }
                        break;
                    case EXIT_CREATE_TEAM:
                        //Exit the create team menu
                        done = true;
                        break;

                    default:
                        //If user entered an invalid request then keep displaying sort menu with feedback
                        Console.WriteLine("Invalid Option\nPress Enter to Continue!");
                        Console.ReadLine();
                        break;
                }
            }
        }

        //Pre: userLoadFileName is for loading a file
        //Post: None
        //Desc: Adds all the wrestlers and coaches to their lists
        public static void LoadTeam(string userLoadFileName)
        {
            //Store the SLSS team File
            string line;
            string[] data;
            string userType;

            //Load the SLSS team File
            infile = File.OpenText(userLoadFileName);
            while (!infile.EndOfStream)
            {
                //Store the SLSS team File
                line = infile.ReadLine();
                data = line.Split(',');
                userType = data[0];
                

                //Add the Wrestlers and Coaches based on the user type
                if (userType.Equals("Wrestler"))
                {
                    string lastName = data[1];
                    string firstName = data[2];
                    string gender = data[3];
                    string school = data[4];
                    int yearsOfExperience = Convert.ToInt32(data[5]);
                    string birthDate = data[6];
                    double weight = Convert.ToDouble(data[7]);
                    double weightCategory = Convert.ToDouble(data[8]);
                    int wins = Convert.ToInt32(data[9]);
                    int losses = Convert.ToInt32(data[10]);
                    int totalPoints = Convert.ToInt32(data[11]);
                    int winsByPin = Convert.ToInt32(data[12]);
                    string status = data[13];
                    bool uniform = Convert.ToBoolean(data[14]);
                    teams.AddWrestler(lastName, firstName, gender, school, yearsOfExperience, birthDate, weight, weightCategory, wins, losses, totalPoints, winsByPin, status, uniform);
                }
                else
                {
                    string lastName = data[1];
                    string firstName = data[2];
                    string gender = data[3];
                    string school = data[4];
                    int yearsOfExperience = Convert.ToInt32(data[5]);
                    string type = data[6];
                    teams.AddCoach(lastName, firstName, gender, school, yearsOfExperience, type);
                }
            }
            infile.Close();
        }

        //Pre: userFileName is for loading or creating a file
        //Post: None
        //Desc: Asks and writes the wrestler data into the requested file
        private static void WriteWrestler(string userFileName)
        {
            //Create or open the requested file name
            outfile = File.CreateText(userFileName);
            outfile.Close();

            //Temporarily store the wrestler's attributes
            string lastName;
            string firstName;
            string gender;
            string school;
            string birthDate;
            string status;
            string uniform;
            int yearsOfExperience;
            double weight;
            int wins;
            int losses;
            int totalPoints;
            int winsByPin;

            //Ask for all the wrestler's data
            Console.Clear();
            Console.WriteLine("Enter the Last Name!");
            lastName = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Enter the First Name!");
            firstName = Console.ReadLine();
            Console.Clear();
            gender = teams.AskString("Select the Gender!\n1. Male\n2. Female", "Male", "Female");
            Console.Clear();
            Console.WriteLine("Enter the School!");
            school = Console.ReadLine();
            Console.Clear();
            yearsOfExperience = teams.AskInt("Enter the Years of Experience!");
            Console.Clear();
            Console.WriteLine("Enter the birth date in the form mm/dd/yyyy!");
            birthDate = Console.ReadLine();
            Console.Clear();
            weight = teams.AskDouble("Enter the Weight! (kg)");
            Console.Clear();
            wins = teams.AskInt("Enter the number of Wins!");
            Console.Clear();
            losses = teams.AskInt("Enter the number of Losses!");
            Console.Clear();
            totalPoints = teams.AskInt("Enter the number of Total Points!");
            Console.Clear();
            winsByPin = teams.AskInt("Enter the number of Wins by Pin!"); 
            Console.Clear();
            status = teams.AskString("Select the Status!\n1. Active\n2. Quit", "Active", "Quit");
            Console.Clear();
            uniform = teams.AskString("Select the Uniform Status!\n1. true\n2. false", "true", "false");
            Console.Clear();

            //Add the wrestler to the list of wrestlers
            teams.AddWrestler(lastName, firstName, gender, school, yearsOfExperience, birthDate, weight, teams.CalculateWeightCategory(gender, weight), wins, losses, totalPoints, winsByPin, status, Convert.ToBoolean(uniform));

            //Update the team file
            teams.WriteTeam(userFileName);

            //Provide feedback on adding the wrestler
            teams.UserFeedback("Wrestler Added!", "", ConsoleColor.Green);
            Console.ReadLine();
        }

        //Pre: userFileName is for loading or creating a file
        //Post: None
        //Desc: Asks and writes the coach data into the requested file
        private static void WriteCoach(string userFileName)
        {
            //Create or open the requested file name
            outfile = File.CreateText(userFileName);
            outfile.Close();

            //Temporarily store the coach's attributes
            string lastName;
            string firstName;
            string gender;
            string school;
            string type;
            int yearsOfExperience;

            //Ask for all the coaches's data
            Console.Clear();
            Console.WriteLine("Enter the Last Name!");
            lastName = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Enter the First Name!");
            firstName = Console.ReadLine();
            Console.Clear();
            gender = teams.AskString("Select the Gender!\n1. Male\n2. Female", "Male", "Female");
            Console.Clear();
            Console.WriteLine("Enter the school!");
            school = Console.ReadLine();
            Console.Clear();
            yearsOfExperience = teams.AskInt("Enter the Years of Experience!");
            Console.Clear();
            type = teams.AskString("Select the Type of Coach!\n1. Hands-on\n2. Support", "Hands-on", "Support");
            Console.Clear();

            //Add the coach to the list of coaches
            teams.AddCoach(lastName, firstName, gender, school, yearsOfExperience, type);

            //Update the team file
            teams.WriteTeam(userFileName);

            //Provide feedback on adding the coach
            teams.UserFeedback("Coach Added!", "", ConsoleColor.Green);
            Console.ReadLine();
        }
    }
}
