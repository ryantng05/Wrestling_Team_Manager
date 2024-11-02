using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TangRPASS2
{
    internal class Team
    {
        //Store all the constants for magic numbers
        private const string WRESTLER_SORT_BY_LASTNAME = "1";
        private const string WRESTLER_SORT_BY_WEIGHTCATEGORY = "2";
        private const string WRESTLER_SORT_BY_WINS = "3";
        private const string COACH_SORT_BY_GENDER = "1";
        private const string COACH_SORT_BY_LASTNAME = "2";
        private const string COACH_SORT_BY_TYPE = "3";
        private const string PERSON_OPTION_COACH = "1";
        private const string PERSON_OPTION_WRESTLER = "2";
        private const string PERSON_OPTION_EXIT = "3";
        private const string COACH_MANAGE_MODIFY = "1";
        private const string COACH_MANAGE_REMOVE = "2";
        private const string COACH_MANAGE_EXIT = "3";
        private const string COACH_MODIFY_YEARSEXPERIENCE = "1";
        private const string COACH_MODIFY_TYPE = "2";
        private const string COACH_MODIFY_EXIT = "3";
        private const string WRESTLER_MANAGE_MODIFY = "1";
        private const string WRESTLER_MANAGE_REMOVE = "2";
        private const string WRESTLER_MANAGE_EXIT = "3";
        private const string WRESTLER_MODIFY_YEARSEXPERIENCE = "1";
        private const string WRESTLER_MODIFY_WEIGHT = "2";
        private const string WRESTLER_MODIFY_WINS = "3";
        private const string WRESTLER_MODIFY_LOSSES = "4";
        private const string WRESTLER_MODIFY_TOTALPOINTS = "5";
        private const string WRESTLER_MODIFY_WINSBYPIN = "6";
        private const string WRESTLER_MODIFY_STATUS = "7";
        private const string WRESTLER_MODIFY_UNIFORM = "8";
        private const string WRESTLER_MODIFY_EXIT = "9";
        private const string ASKSTRING_ATTRIBUTE1 = "1";
        private const string ASKSTRING_ATTRIBUTE2 = "2";
        private const int ALL_BREAKDOWN_15 = 15;
        private const int ALL_BREAKDOWN_16 = 16;
        private const int ALL_BREAKDOWN_17 = 17;
        private const int ALL_BREAKDOWN_18 = 18;
        private const int FIRST_TEN_WRESTLERS = 10;
        private const int FIRST_THREE_COACHES = 3;

        //Store the File IO systems
        private StreamWriter outfile;

        //Store the coaches and wrestlers objects
        private List<Coach> coaches = new List<Coach>();
        private List<Wrestler> wrestlers = new List<Wrestler>();

        //Store the male and female weight categories
        private double[] maleWeightCategory = { 38, 41, 44, 47.5, 51, 54, 57.5, 61, 64, 67.5, 72, 77, 83, 89, 95, 130, 131 };
        private double[] femaleWeightCategory = { 38, 41, 44, 47.5, 51, 54, 57.5, 61, 64, 67.5, 72, 77, 83, 89, 95, 115, 116 };

        //Store the team statistics
        private int numMaleWrestlers;
        private int numFemaleWrestlers;
        private int numHandsOnCoaches;
        private int numSupportCoaches;
        private int numMaleCoaches;
        private int numFemaleCoaches;
        private int totalNumWins;
        private int totalNumLosses;
        private int totalPoints;
        private int totalPinCount;
        private int[] breakDownNumMaleWrestlersWeightCategory = new int[17];
        private int[] breakDownNumFemaleWrestlersWeightCategory = new int[17];
        private int[] breakDownNumAllWrestlersWeightCategory = new int[19];

        //Pre: None
        //Post: None
        //Desc: something
        public Team()
        {

        }

        //Pre: The incoming varaibles are the coach's attributes
        //Post: None
        //Desc: Add all the coaches and update team statistics
        public void AddCoach(string lastName, string firstName, string gender, string school, int yearsExperience, string type)
        {
            //Add coach
            coaches.Add(new Coach(lastName, firstName, gender, school, yearsExperience, type));

            //Update number of male and female coaches
            if (gender.Equals("Male"))
            {
                numMaleCoaches++;
            }
            else
            {
                numFemaleCoaches++;
            }

            //Update number of hands-on and support coaches
            if (type.Equals("Hands-on"))
            {
                numHandsOnCoaches++;
            }
            else
            {
                numSupportCoaches++;
            }
        }

        //Pre: The incoming varaibles are the wrestler's attributes
        //Post: None
        //Desc: Add all the wrestlers and update team statistics
        public void AddWrestler(string lastName, string firstName, string gender, string school, int yearsExperience, string birthday, double weight, double weightCategory, int wins, int losses, int totalPoints, int winsByPin, string status, bool uniform)
        {
            //Add wrestler
            wrestlers.Add(new Wrestler(lastName, firstName, gender, school, yearsExperience, birthday, weight, weightCategory, wins, losses, totalPoints, winsByPin, status, uniform));
            
            //Update the team stats based on male or female wrestlers
            if (gender.Equals("Male"))
            {
                numMaleWrestlers++;
                for (int i = 0; i < maleWeightCategory.Length; i++)
                {
                    if (weightCategory.Equals(maleWeightCategory[i]))
                    {
                        if (i.Equals(15))
                        {
                            breakDownNumAllWrestlersWeightCategory[ALL_BREAKDOWN_17]++;
                        }
                        else if (i.Equals(16))
                        {
                            breakDownNumAllWrestlersWeightCategory[ALL_BREAKDOWN_18]++;
                        }
                        else
                        {
                            breakDownNumAllWrestlersWeightCategory[i]++;
                        }
                        breakDownNumMaleWrestlersWeightCategory[i]++;
                    }
                }
            }
            else
            {
                numFemaleWrestlers++;
                for (int i = 0; i < femaleWeightCategory.Length; i++)
                {
                    if (weightCategory.Equals(femaleWeightCategory[i]))
                    {
                        breakDownNumAllWrestlersWeightCategory[i]++;
                        breakDownNumFemaleWrestlersWeightCategory[i]++;
                    }
                }
            }

            //Update team stats
            totalNumWins += wins;
            totalNumLosses += losses;
            this.totalPoints += totalPoints;
            totalPinCount += winsByPin;
        }

        //Pre: idx is a positive searched number
        //Post: None
        //Desc: Removes a specific coach and updates team statistics
        public void RemoveCoach(int idx)
        {
            //Update team stats based on hands-on and support coaches
            if (coaches[idx].GetType().Equals("Hands-on"))
            {
                numHandsOnCoaches -= 1;
            }
            else
            {
                numSupportCoaches -= 1;
            }

            //Update team stats based on male and female coaches
            if (coaches[idx].GetGender().Equals("Male"))
            {
                numMaleCoaches -= 1;
            }
            else
            {
                numFemaleCoaches -= 1;
            }

            //Remove specific coach
            coaches.RemoveAt(idx);
        }

        //Pre: idx is a positive searched number
        //Post: None
        //Desc: Removes a specific wrestler and updates team statistics
        public void RemoveWrestler(int idx)
        {
            //Update team stats
            totalNumWins -= wrestlers[idx].GetWins();
            totalNumLosses -= wrestlers[idx].GetLosses();
            totalPoints -= wrestlers[idx].GetTotalPoints();
            totalPinCount -= wrestlers[idx].GetWinsByPin();

            //Update team stats based on male and female wrestlers
            if (wrestlers[idx].GetGender().Equals("Male"))
            {
                numMaleWrestlers -= 1;
            }
            else
            {
                numFemaleWrestlers -= 1;
            }

            //Update weight category team stats based on male and female wrestlers
            UpdateWeightCategoryBreakdown(idx, "-=");

            //Remove specific wrestler
            wrestlers.RemoveAt(idx);
        }

        //Pre: None
        //Post: Return the number of people on the team as an int
        //Desc: Returns total number of people on the team
        public int GetNumPeople()
        {
            return coaches.Count + wrestlers.Count;
        }

        //Pre: None
        //Post: Return the number of wrestlers as an int
        //Desc: Returns total number of wrestlers
        public int GetNumWrestlers()
        {
            return wrestlers.Count;
        }

        //Pre: None
        //Post: Return the number of coaches as an int
        //Desc: Returns total number of coaches
        public int GetNumCoaches()
        {
            return coaches.Count;
        }

        //Pre: None
        //Post: None
        //Desc: Displays the entire team statistics
        public void DisplayTeamStats()
        {
            //Display team stats
            Console.Clear();
            Console.WriteLine("Number of People on the Team: " + (coaches.Count + wrestlers.Count));
            Console.WriteLine("Number of Wrestlers: " + wrestlers.Count);
            Console.WriteLine("Number of Male Wrestlers: " + numMaleWrestlers);
            Console.WriteLine("Number of Female Wrestlers: " + numFemaleWrestlers);
            Console.WriteLine("Number of Coaches: " + coaches.Count);
            Console.WriteLine("Number of Hands-on Coaches: " + numHandsOnCoaches);
            Console.WriteLine("Number of Support Coaches: " + numSupportCoaches);
            Console.WriteLine("Number of Male Coaches: " + numMaleCoaches);
            Console.WriteLine("Number of Female Coaches: " + numFemaleCoaches);
            Console.WriteLine("Team's Total Number of Wins: " + totalNumWins);
            Console.WriteLine("Team's Total Number of Losses: " + totalNumLosses);
            Console.WriteLine("Team's Win Percentage: " + Math.Round(totalNumWins / (double)(totalNumWins + totalNumLosses) * 100));
            Console.WriteLine("Team's Loss Percentage: " + Math.Round(totalNumLosses / (double)(totalNumWins + totalNumLosses) * 100));
            Console.WriteLine("Team's Total Point Count: " + totalPoints);
            Console.WriteLine("Team's Total Pin Count: " + totalPinCount);
            Console.WriteLine("Team's Total Number of Matches: " + (totalNumWins + totalNumLosses));
            Console.WriteLine("Team's Average Points Per Match Count: " + totalPoints / (totalNumWins + totalNumLosses));
            Console.WriteLine("");
            Console.WriteLine("Breakdown of the Number of All Wrestlers Per Weight Category:");
            for (int i = 0; i < ALL_BREAKDOWN_15; i++)
            {
                Console.Write(maleWeightCategory[i] + ":" + breakDownNumAllWrestlersWeightCategory[i] + " ");
            }
            Console.Write("115" + ":" + breakDownNumAllWrestlersWeightCategory[ALL_BREAKDOWN_15]);
            Console.Write(" 115+" + ":" + breakDownNumAllWrestlersWeightCategory[ALL_BREAKDOWN_16]);
            Console.Write(" 130" + ":" + breakDownNumAllWrestlersWeightCategory[ALL_BREAKDOWN_17]);
            Console.Write(" 130+" + ":" + breakDownNumAllWrestlersWeightCategory[ALL_BREAKDOWN_18]);
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Breakdown of the Number of Male Wrestlers Per Weight Category:");
            for (int i = 0; i < ALL_BREAKDOWN_16; i++)
            {
                Console.Write(maleWeightCategory[i] + ":" + breakDownNumMaleWrestlersWeightCategory[i] + " ");
            }
            Console.Write("130+" + ":" + breakDownNumMaleWrestlersWeightCategory[ALL_BREAKDOWN_16]);
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Breakdown of the Number of Female Wrestlers Per Weight Category:");
            for (int i = 0; i < ALL_BREAKDOWN_16; i++)
            {
                Console.Write(femaleWeightCategory[i] + ":" + breakDownNumFemaleWrestlersWeightCategory[i] + " ");
            }
            Console.Write("115+" + ":" + breakDownNumFemaleWrestlersWeightCategory[ALL_BREAKDOWN_16]);
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Press Enter to Continue!");
            Console.ReadLine();
        }

        //Pre: sortType is 1, 2, or 3
        //Post: None
        //Desc: Sort the wrestlers based on the requested sort type
        public void SortWrestlers(string sortType)
        {
            //Go through specific actions depending on user sortType
            switch (sortType)
            {
                case WRESTLER_SORT_BY_LASTNAME:
                    //Sort wrestlers on gender and last name
                    wrestlers = wrestlers.OrderBy(x => x.GetGender()).ThenBy(x => x.GetLastName()).ToList();

                    //Display wrestlers based on sort type
                    Console.Clear();
                    DisplayWrestlers();
                    break;

                case WRESTLER_SORT_BY_WEIGHTCATEGORY:
                    //Sort wrestlers on gender and weight category
                    wrestlers = wrestlers.OrderBy(x => x.GetGender()).ThenBy(x => x.GetWeightCategory()).ToList();

                    //Display wrestlers based on sort type
                    Console.Clear();
                    DisplayWrestlers();
                    break;

                case WRESTLER_SORT_BY_WINS:
                    //Sort wrestlers on gender and wins
                    wrestlers = wrestlers.OrderBy(x => x.GetGender()).ThenBy(x => x.GetWins()).ToList();

                    //Display wrestlers based on sort type
                    Console.Clear();
                    DisplayWrestlers();
                    break;
                
                default:
                    //If user entered an invalid request then keep displaying sort menu with feedback
                    Console.WriteLine("Invalid Option\nPress Enter to Continue!");
                    Console.ReadLine();
                    break;
            }
        }

        //Pre: sortType is 1, 2, or 3
        //Post: None
        //Desc: Sort the coaches based on the requested sort type
        public void SortCoaches(string sortType)
        {
            //Go through specific actions depending on user sortType
            switch (sortType)
            {
                case COACH_SORT_BY_GENDER:
                    //Sort coaches on gender
                    coaches = coaches.OrderBy(x => x.GetGender()).ToList();

                    //Display coaches based on sort type
                    Console.Clear();
                    DisplayCoaches();
                    break;

                case COACH_SORT_BY_LASTNAME:
                    //Sort coaches on last name
                    coaches = coaches.OrderBy(x => x.GetLastName()).ToList();

                    //Display coaches based on sort type
                    Console.Clear();
                    DisplayCoaches();
                    break;

                case COACH_SORT_BY_TYPE:
                    //Sort coaches on coach type
                    coaches = coaches.OrderBy(x => x.GetType()).ToList();

                    //Display coaches based on sort type
                    Console.Clear();
                    DisplayCoaches();
                    break;

                default:
                    //If user entered an invalid request then keep displaying sort menu with feedback
                    Console.WriteLine("Invalid Option\nPress Enter to Continue!");
                    Console.ReadLine();
                    break;
            }
        }

        //Pre: userFileName is valid
        //Post: None
        //Desc: Search for a requested coach or wrestler and modify or delete them
        public void Search(string userFileName)
        {
            //Store the first and last names of the user's search person
            string firstName;
            string lastName;

            //Store the user person choice 
            string personOption;

            //Store the user manage coach and wrestler options
            string manageCoachOption;
            string manageWrestlerOption;

            //Store the user manage attribute options for coaches and wrestlers
            string manageCoachAttribute;
            string manageWrestlerAttribute;

            //Store new wrestler and coach int attributes
            string type;
            string status;
            string uniform;

            //Store the index the index of the coach or wrestler the user wants to search for
            int searchCoach = -1;
            int searchWrestler = -1;

            //Store new wrestler and coach int attributes
            int yearsOfExperience;
            int wins;
            int losses;
            int totalPoints;
            int winsByPin;

            //Store the new wrestler weight
            double weight;

            //Store the loop varaible for the search menu
            bool done = false;

            //Store the managing coach and wrestler loops
            bool doneManageCoach = false;
            bool doneManageWrestler = false;

            //Store the modification loops for coaches and wrestlers
            bool doneModifyCoach = false;
            bool doneModifyWrestler = false;

            //Keep looping until the user exits the sort menu
            while (!done)
            {
                Console.Clear();

                //Ask which person the user would like to search for and store it
                Console.WriteLine("Select which person to search for: ");
                Console.WriteLine("");
                Console.WriteLine("1. Coach\n2. Wrestler\n3. Exit");
                personOption = Console.ReadLine();

                //Go through specific actions depending on user person option
                switch (personOption)
                {
                    case PERSON_OPTION_COACH:
                        Console.Clear();

                        //Ask for the first name of the coach the user wants to search for and store it
                        Console.WriteLine("Enter the First Name!");
                        firstName = Console.ReadLine();

                        Console.Clear();

                        //Ask for the last name of the coach the user wants to search for and store it
                        Console.WriteLine("Enter the Last Name!");
                        lastName = Console.ReadLine();
                        Console.Clear();

                        //Search for the coach
                        for (int i = 0; i < coaches.Count; i++)
                        {
                            //Check if the user input matches the coaches on the team and store the index
                            if (lastName.Equals(coaches[i].GetLastName()) && firstName.Equals(coaches[i].GetFirstName()))
                            {
                                searchCoach = i;
                            }
                        }

                        //Check if the user requested coach is on the team
                        if (searchCoach >= 0)
                        {
                            //Reset Loop
                            doneManageCoach = false;

                            //Keep looping until the user is done managing the coach
                            while (!doneManageCoach)
                            {
                                Console.Clear();

                                //Display the requested coach's data
                                coaches[searchCoach].DisplayData();

                                Console.WriteLine("");

                                //Ask what they want to do with the coach and store it
                                Console.WriteLine("Select an Option: ");
                                Console.WriteLine("");
                                Console.WriteLine("1. Modify Person\n2. Delete Person\n3. Exit");
                                manageCoachOption = Console.ReadLine();

                                //Go through specific actions depending on user management option
                                switch (manageCoachOption)
                                {
                                    case COACH_MANAGE_MODIFY:
                                        //Reset Loop
                                        doneModifyCoach = false;

                                        //Keeping looping until the user is done modifying the coach
                                        while (!doneModifyCoach)
                                        {
                                            Console.Clear();
                                            //Ask which attribute of the coach to modify
                                            Console.WriteLine("Choose which attribute to modify!");
                                            Console.WriteLine("");
                                            Console.WriteLine("1. Years of Experience\n2. Type of Coach\n3. Exit");
                                            manageCoachAttribute = Console.ReadLine();

                                            //Go through specific actions depending on user modification option
                                            switch (manageCoachAttribute)
                                            {
                                                case COACH_MODIFY_YEARSEXPERIENCE:
                                                    //Ask for the new years of experience and store it
                                                    yearsOfExperience = AskInt("Enter the years of experience!");
                                                    Console.Clear();

                                                    //Set the new years of experience of the coach
                                                    coaches[searchCoach].SetYears(yearsOfExperience);

                                                    //Update the team file
                                                    WriteTeam(userFileName);

                                                    //Give feedback of the modification
                                                    UserFeedback("Years of Experience modified to", Convert.ToString(yearsOfExperience), ConsoleColor.Green);
                                                    Console.ReadLine();
                                                    break;

                                                case COACH_MODIFY_TYPE:
                                                    //Ask for the new type of coach
                                                    type = AskString("Select the type of coach!\n1. Hands-on\n2. Support", "Hands-on", "Support");
                                                    Console.Clear();

                                                    //Depending on the new type of coach, update team stats
                                                    if (type.Equals("Hands-on") && coaches[searchCoach].GetType() != "Hands-on")
                                                    {
                                                        numHandsOnCoaches++;
                                                        numSupportCoaches--;
                                                    }
                                                    else if (type.Equals("Support") && coaches[searchCoach].GetType() != "Support")
                                                    {
                                                        numHandsOnCoaches--;
                                                        numSupportCoaches++;
                                                    }

                                                    //Set the new type of coach
                                                    coaches[searchCoach].SetType(type);

                                                    //Update the team file
                                                    WriteTeam(userFileName);

                                                    //Give feedback of the modification
                                                    UserFeedback("Type of Coach modified to", type, ConsoleColor.Green);
                                                    Console.ReadLine();
                                                    break;

                                                case COACH_MODIFY_EXIT:
                                                    //User is done modifying the coach
                                                    doneModifyCoach = true;
                                                    break;

                                                default:
                                                    //If user entered an invalid request then keep displaying sort menu with feedback
                                                    Console.WriteLine("Invalid Option\nPress Enter to Continue!");
                                                    Console.ReadLine();
                                                    break;
                                            }
                                        }
                                        break;

                                    case COACH_MANAGE_REMOVE:
                                        //Remove the requested coach
                                        RemoveCoach(searchCoach);

                                        //Update the team file
                                        WriteTeam(userFileName);
                                        Console.Clear();

                                        //Give feedback of the deletion
                                        UserFeedback("Coach Deleted!", "", ConsoleColor.Red);
                                        Console.ReadLine();

                                        //User is done managing the coach
                                        doneManageCoach = true;
                                        break;

                                    case COACH_MANAGE_EXIT:
                                        //User is done managing the coach
                                        doneManageCoach = true;
                                        break;

                                    default:
                                        //If user entered an invalid request then keep displaying sort menu with feedback
                                        Console.WriteLine("Invalid Option\nPress Enter to Continue!");
                                        Console.ReadLine();
                                        break;
                                }
                            }
                        }
                        else
                        {
                            //Give feedback of the coach not being found
                            UserFeedback("Coach not found...", "", ConsoleColor.Red);
                            Console.ReadLine();
                        }
                        break;

                    case PERSON_OPTION_WRESTLER:
                        Console.Clear();

                        //Ask for the first name of the wrestler the user wants to search for and store it
                        Console.WriteLine("Enter the First Name!");
                        firstName = Console.ReadLine();

                        Console.Clear();

                        //Ask for the last name of the wrestler the user wants to search for and store it
                        Console.WriteLine("Enter the Last Name!");
                        lastName = Console.ReadLine();
                        Console.Clear();

                        //Search for wrestler
                        for (int i = 0; i < wrestlers.Count; i++)
                        {
                            //Check if the user input matches the wrestlers on the team and store the index
                            if (lastName.Equals(wrestlers[i].GetLastName()) && firstName.Equals(wrestlers[i].GetFirstName()))
                            {
                                searchWrestler = i;
                            }
                        }

                        //Check if the user requested wrestler is on the team
                        if (searchWrestler >= 0)
                        {
                            //Reset Loop
                            doneManageWrestler = false;

                            //Keeping looping until the user is done modifying the wrestler
                            while (!doneManageWrestler)
                            {
                                Console.Clear();

                                //Display the requested wrestler's data
                                wrestlers[searchWrestler].DisplayData();

                                Console.WriteLine("");

                                //Ask which attribute of the coach to modify and store it
                                Console.WriteLine("Select an Option: ");
                                Console.WriteLine("");
                                Console.WriteLine("1. Modify Person\n2. Delete Person\n3. Exit");
                                manageWrestlerOption = Console.ReadLine();

                                //Go through specific actions depending on user menu option
                                switch (manageWrestlerOption)
                                {
                                    case WRESTLER_MANAGE_MODIFY:
                                        //Reset Loop
                                        doneModifyWrestler = false;

                                        //Keep looping until the user is finished modifying the wrestler
                                        while (!doneModifyWrestler)
                                        {
                                            //Ask which attribute of the wrestler to modify and store it
                                            Console.Clear();
                                            Console.WriteLine("Choose which attribute to modify!");
                                            Console.WriteLine("");
                                            Console.WriteLine("1. Years of Experience\n2. Weight\n3. Wins\n4. Losses\n5. Total Points\n6. Wins By Pin\n7. Status\n8. Uniform\n9. Exit");
                                            manageWrestlerAttribute = Console.ReadLine();

                                            //Go through specific actions depending on user management option
                                            switch (manageWrestlerAttribute)
                                            {
                                                case WRESTLER_MODIFY_YEARSEXPERIENCE:
                                                    //Ask for the new years of experience of the wrestler and store it
                                                    yearsOfExperience = AskInt("Enter the years of experience!");
                                                    Console.Clear();

                                                    //Set the new number of years of experience of the wrestler
                                                    wrestlers[searchWrestler].SetYears(yearsOfExperience);

                                                    //Update team file
                                                    WriteTeam(userFileName);

                                                    //Give feedback of the modification
                                                    UserFeedback("Years of Experience modified to", Convert.ToString(yearsOfExperience), ConsoleColor.Green);
                                                    Console.ReadLine();
                                                    break;

                                                case WRESTLER_MODIFY_WEIGHT:
                                                    //Ask for the new weight of the wrestler and store it
                                                    weight = AskDouble("Enter the weight!");
                                                    Console.Clear();

                                                    //Update the weight category breakdown without the old wrestler
                                                    UpdateWeightCategoryBreakdown(searchWrestler, "-=");

                                                    //Set the new weight of the wrestler
                                                    wrestlers[searchWrestler].SetWeight(weight);

                                                    //Update team file
                                                    WriteTeam(userFileName);

                                                    //Update the weight category breakdown with the new wrestler
                                                    UpdateWeightCategoryBreakdown(searchWrestler, "+=");

                                                    //Give feedback of the modification
                                                    UserFeedback("Weight modified to", Convert.ToString(weight), ConsoleColor.Green);
                                                    Console.ReadLine();
                                                    break;

                                                case WRESTLER_MODIFY_WINS:
                                                    //Update the total num of team wins without the old wrestler
                                                    totalNumWins -= wrestlers[searchWrestler].GetWins();

                                                    //Ask for the new number of wins of the wrestler and store it
                                                    wins = AskInt("Enter the number of wins!");
                                                    Console.Clear();

                                                    //Update the total num of team wins with the new wrestler
                                                    totalNumWins += wins;

                                                    //Set the new number of wins of the wrestler
                                                    wrestlers[searchWrestler].SetWins(wins);

                                                    //Update team file
                                                    WriteTeam(userFileName);

                                                    //Give feedback of the modification
                                                    UserFeedback("Wins modified to", Convert.ToString(wins), ConsoleColor.Green);
                                                    Console.ReadLine();
                                                    break;

                                                case WRESTLER_MODIFY_LOSSES:
                                                    //Update the total num of team losses without the old wrestler
                                                    totalNumLosses -= wrestlers[searchWrestler].GetLosses();

                                                    //Ask for the new number of wins of the wrestler and store it
                                                    losses = AskInt("Enter the number of losses!");

                                                    //Update the total num of team losses with the new wrestler
                                                    totalNumLosses += losses;

                                                    //Set the new number of losses of the wrestler
                                                    wrestlers[searchWrestler].SetLosses(losses);

                                                    //Update team file
                                                    WriteTeam(userFileName);

                                                    //Give feedback of the modification
                                                    UserFeedback("Losses modified to", Convert.ToString(losses), ConsoleColor.Green);
                                                    Console.ReadLine();
                                                    break;

                                                case WRESTLER_MODIFY_TOTALPOINTS:
                                                    //Update the total num of points without the old wrestler
                                                    this.totalPoints -= wrestlers[searchWrestler].GetTotalPoints();

                                                    //Ask for the new number of total points of the wrestler and store it
                                                    totalPoints = AskInt("Enter the number of total points!");
                                                    Console.Clear();

                                                    //Set the new number of total points of the wrestler
                                                    this.totalPoints += totalPoints;

                                                    //Set the new number of total points of the wrestler
                                                    wrestlers[searchWrestler].SetTotalPoints(totalPoints);

                                                    //Update team file
                                                    WriteTeam(userFileName);

                                                    //Give feedback of the modification
                                                    UserFeedback("Total Points modified to", Convert.ToString(totalPoints), ConsoleColor.Green);
                                                    Console.ReadLine();
                                                    break;

                                                case WRESTLER_MODIFY_WINSBYPIN:
                                                    //Update the total num of team wins by pin without the old wrestler
                                                    totalPinCount -= wrestlers[searchWrestler].GetWinsByPin();

                                                    //Ask for the new number of wins by pin of the wrestler and store it
                                                    winsByPin = AskInt("Enter the number of wins by pin!");
                                                    Console.Clear();

                                                    //Update the total num of team wins by pin with the new wrestler
                                                    totalPinCount += winsByPin;

                                                    //Set the new number of wins by pin of the wrestler
                                                    wrestlers[searchWrestler].SetWinsByPin(winsByPin);

                                                    //update team file
                                                    WriteTeam(userFileName);

                                                    //Give feedback of the modification
                                                    UserFeedback("Wins By Pin modified to", Convert.ToString(winsByPin), ConsoleColor.Green);
                                                    Console.ReadLine();
                                                    break;

                                                case WRESTLER_MODIFY_STATUS:
                                                    //Ask for the new status of the wrestler and store it
                                                    status = AskString("Select the status!\n1. Active\n2. Quit", "Active", "Quit");
                                                    Console.Clear();

                                                    //Set the new status of the wrestler
                                                    wrestlers[searchWrestler].SetStatus(status);

                                                    //Update the team file
                                                    WriteTeam(userFileName);

                                                    //Give feedback of the modification
                                                    UserFeedback("Status modified to", status, ConsoleColor.Green);
                                                    Console.ReadLine();
                                                    break;

                                                case WRESTLER_MODIFY_UNIFORM:
                                                    //Ask for the new status of the uniform of the wrestler and store it
                                                    uniform = AskString("Select the uniform status!\n1. true\n2. false", "true", "false");
                                                    Console.Clear();

                                                    //Set the new status of the uniform of the wrestler
                                                    wrestlers[searchWrestler].SetUniform(Convert.ToBoolean(uniform));

                                                    //Update the team file
                                                    WriteTeam(userFileName);

                                                    //Give feedback of the modification
                                                    UserFeedback("Uniform modified to", uniform, ConsoleColor.Green);
                                                    Console.ReadLine();
                                                    break;

                                                case WRESTLER_MODIFY_EXIT:
                                                    //Exit the modifying wrestler loop
                                                    doneModifyWrestler = true;
                                                    break;

                                                default:
                                                    //If user entered an invalid request then keep displaying sort menu with feedback
                                                    Console.WriteLine("Invalid Option\nPress Enter to Continue!");
                                                    Console.ReadLine();
                                                    break;
                                            }
                                        }
                                        break;

                                    case WRESTLER_MANAGE_REMOVE:
                                        //Remove wrestler at the requested index
                                        RemoveWrestler(searchWrestler);

                                        //Update the team file
                                        WriteTeam(userFileName);
                                        Console.Clear();

                                        //Give feedback of the deletion
                                        UserFeedback("Wrestler Deleted!", "", ConsoleColor.Red);
                                        Console.ReadLine();

                                        //User is done managing managing the wrestler
                                        doneManageWrestler = true;
                                        break;

                                    case WRESTLER_MANAGE_EXIT:
                                        //User is done managing managing the wrestler
                                        doneManageWrestler = true;
                                        break;

                                    default:
                                        //If user entered an invalid request then keep displaying sort menu with feedback
                                        Console.WriteLine("Invalid Option\nPress Enter to Continue!");
                                        Console.ReadLine();
                                        break;
                                }
                            }
                        }
                        else
                        {
                            //Give feedback of the user not being found
                            UserFeedback("Wrestler not found...", "", ConsoleColor.Red);
                            Console.ReadLine();
                        }
                        break;
                    case PERSON_OPTION_EXIT:
                        //Exit the search menu
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

        //Pre: Prompt is passed in
        //Post: Returns the new attribute
        //Desc: Asks for and returns user's requested attribute
        public double AskDouble(string prompt)
        {
            //Store the user's attribute
            double attribute = 0;

            //If the attribute is less than or equal to zero then keep asking for a new attribute
            while (attribute <= 0)
            {
                Console.Clear();

                //Ask for a double
                Console.WriteLine(prompt);

                //Change the user input to a double
                if (!Double.TryParse(Console.ReadLine(), out attribute) || attribute < 0)
                {
                    //If user entered an invalid request then keep displaying sort menu with feedback
                    Console.WriteLine("Invalid Option\nPress Enter to Continue!");
                    Console.ReadLine();
                }
            }

            //Return the attribute
            return attribute;
        }

        //Pre: Prompt is passed in
        //Post: Returns the new attribute
        //Desc: Asks for and returns user's requested attribute
        public int AskInt(string prompt)
        {
            //Store the user's attribute
            int attribute = -1;
            Console.Clear();

            //Ask for an int
            Console.WriteLine(prompt);

            //Change the user input to an int
            while (!int.TryParse(Console.ReadLine(), out attribute) || attribute < 0)
            {
                Console.Clear();

                //Ask for an int
                Console.WriteLine(prompt);
                Console.WriteLine();

                //If user entered an invalid request then keep displaying sort menu with feedback
                Console.WriteLine("Invalid Option\nPlease Enter a new number!");
            }

            //Return the attribute
            return attribute;
        }

        //Pre: Prompt is passed in
        //Post: Returns the new attribute
        //Desc: Asks for and returns user's requested attribute
        public string AskString(string prompt, string restriction1, string restriction2)
        {
            //Store the user's attribute
            string attribute = "";

            //Store the menu loop
            bool done = false;

            //Keep looping until the user enters a valid attribute
            while (!done)
            {
                Console.Clear();

                //Ask for an option
                Console.WriteLine(prompt);

                //Go through specific actions depending on user option
                switch (Console.ReadLine())
                {
                    case ASKSTRING_ATTRIBUTE1:
                        //If user chooses option one then make the attribute the first option and exit the menu
                        attribute = restriction1;
                        done = true;
                        break;

                    case ASKSTRING_ATTRIBUTE2:
                        //If user chooses option two then make the attribute the second option and exit the menu
                        attribute = restriction2;
                        done = true;
                        break;

                    default:
                        //If user entered an invalid request then keep displaying sort menu with feedback
                        Console.WriteLine("Invalid Option\nPress Enter to Continue!");
                        Console.ReadLine();
                        break;
                }
            }

            //Return the attribute
            return attribute;
        }

        //Pre: Feedback message, what was changed, and green or red colour are passed in
        //Post: None
        //Desc: Displays feedback and green or red colours depending on the scenario
        public void UserFeedback(string attributeFeedback, string attribute, ConsoleColor colour)
        {
            //Change the colour
            Console.ForegroundColor = colour;

            //Display the feedback
            Console.WriteLine(attributeFeedback + " " + attribute);

            //Reset colour
            Console.ResetColor();
            Console.WriteLine("");
            Console.WriteLine("Press Enter to Continue!");
        }

        //Pre: userFileName is used for saving a team file
        //Post: None
        //Desc: Rewrites all coaches and wrestlers in the given file
        public void WriteTeam(string userFileName)
        {
            //Open the text file
            outfile = File.CreateText(userFileName);

            //Write all the wrestlers to the text file
            for (int i = 0; i < wrestlers.Count; i++)
            {
                outfile.WriteLine("Wrestler" + "," + wrestlers[i].GetLastName() + "," + wrestlers[i].GetFirstName() + "," + wrestlers[i].GetGender() + "," + wrestlers[i].GetSchool() + "," + wrestlers[i].GetYearsExperience() + "," + wrestlers[i].GetBirthday() + "," + wrestlers[i].GetWeight() + "," + wrestlers[i].GetWeightCategory() + "," + wrestlers[i].GetWins() + "," + wrestlers[i].GetLosses() + "," + wrestlers[i].GetTotalPoints() + "," + wrestlers[i].GetWinsByPin() + "," + wrestlers[i].GetStatus() + "," + wrestlers[i].GetUniform());
            }

            //Write all the coaches to the text file
            for (int i = 0; i < coaches.Count; i++)
            {
                outfile.WriteLine("Coach" + "," + coaches[i].GetLastName() + "," + coaches[i].GetFirstName() + "," + coaches[i].GetGender() + "," + coaches[i].GetSchool() + "," + coaches[i].GetYearsExperience() + "," + coaches[i].GetType());
            }

            //Close the text file
            outfile.Close();
        }

        //Pre: None
        //Post: None
        //Desc: Clears the list of coaches and wrestlers and resets all the team stats
        public void ClearTeam()
        {
            //Clear the wrestlers and coaches list
            wrestlers.Clear();
            coaches.Clear();

            //Set all the team stats to 0
            numMaleWrestlers = 0;
            numFemaleWrestlers = 0;
            numHandsOnCoaches = 0;
            numSupportCoaches = 0;
            numMaleCoaches = 0;
            numFemaleCoaches = 0;
            totalNumWins = 0;
            totalNumLosses = 0;
            totalPoints = 0;
            totalPinCount = 0;

            //Set the all weight category breakdown to 0
            for (int i = 0; i < breakDownNumAllWrestlersWeightCategory.Length; i ++)
            {
                breakDownNumAllWrestlersWeightCategory[i] = 0;
            }

            //Set both the male and female weight categories to 0
            for (int i = 0; i < breakDownNumMaleWrestlersWeightCategory.Length; i++)
            {
                breakDownNumMaleWrestlersWeightCategory[i] = 0;
                breakDownNumFemaleWrestlersWeightCategory[i] = 0;
            }
        }

        //Pre: idx is the index of the requested coach or wrestler and sign is the operator used for deleting or adding
        //Post: None
        //Desc: Clears the list of wrestlers
        private void UpdateWeightCategoryBreakdown(int idx, string sign)
        {
            //Check if the sign is deleting and the gender is male or female and update the weight category breakdown
            if (sign.Equals("-="))
            {
                if (wrestlers[idx].GetGender().Equals("Male"))
                {
                    for (int i = 0; i < maleWeightCategory.Length; i++)
                    {
                        if (wrestlers[idx].GetWeightCategory().Equals(maleWeightCategory[i]))
                        {
                            if (i.Equals(15))
                            {
                                breakDownNumAllWrestlersWeightCategory[17] -= 1;
                            }
                            else if (i.Equals(16))
                            {
                                breakDownNumAllWrestlersWeightCategory[18] -= 1;
                            }
                            else
                            {
                                breakDownNumAllWrestlersWeightCategory[i] -= 1;
                            }
                            breakDownNumMaleWrestlersWeightCategory[i] -= 1;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < femaleWeightCategory.Length; i++)
                    {
                        if (wrestlers[idx].GetWeightCategory().Equals(femaleWeightCategory[i]))
                        {
                            breakDownNumAllWrestlersWeightCategory[i] -= 1;
                            breakDownNumFemaleWrestlersWeightCategory[i] -= 1;
                        }
                    }
                }
            }
            else
            {
                if (wrestlers[idx].GetGender().Equals("Male"))
                {
                    for (int i = 0; i < maleWeightCategory.Length; i++)
                    {
                        if (wrestlers[idx].GetWeightCategory().Equals(maleWeightCategory[i]))
                        {
                            if (i.Equals(15))
                            {
                                breakDownNumAllWrestlersWeightCategory[17] += 1;
                            }
                            else if (i.Equals(16))
                            {
                                breakDownNumAllWrestlersWeightCategory[18] += 1;
                            }
                            else
                            {
                                breakDownNumAllWrestlersWeightCategory[i] += 1;
                            }
                            breakDownNumMaleWrestlersWeightCategory[i] += 1;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < femaleWeightCategory.Length; i++)
                    {
                        if (wrestlers[idx].GetWeightCategory().Equals(femaleWeightCategory[i]))
                        {
                            breakDownNumAllWrestlersWeightCategory[i] += 1;
                            breakDownNumFemaleWrestlersWeightCategory[i] += 1;
                        }
                    }
                }
            }
        }

        //Pre: None
        //Post: None
        //Desc: Displays the first ten wrestlers
        public void DisplayWrestlers()
        {
            //Display the first ten wrestler's data
            if (wrestlers.Count < FIRST_TEN_WRESTLERS)
            {
                for (int i = 0; i < wrestlers.Count; i++)
                {
                    wrestlers[i].DisplayData();
                }
            }
            else
            {
                for (int i = 0; i < FIRST_TEN_WRESTLERS; i++)
                {
                    wrestlers[i].DisplayData();
                }
            }
            Console.WriteLine("");
            Console.WriteLine("Press Enter to Continue");
            Console.ReadLine();
        }

        //Pre: None
        //Post: None
        //Desc: Displays the first three coaches
        public void DisplayCoaches()
        {
            //Display the first three coach's data
            if (coaches.Count < FIRST_THREE_COACHES)
            {
                for (int i = 0; i < coaches.Count; i++)
                {
                    coaches[i].DisplayData();
                }
            }
            else
            {
                for (int i = 0; i < FIRST_THREE_COACHES; i++)
                {
                    coaches[i].DisplayData();
                }

            }
            Console.WriteLine("");
            Console.WriteLine("Press Enter to Continue");
            Console.ReadLine();
        }

        //Pre: None
        //Post: None
        //Desc: Displays the wrestlers who have a uniform signed out
        public void DisplayWrestlersUniformSignedOut()
        {
            Console.Clear();

            //Display all the wrestler's first and last name who have a uniform signed out
            for (int i = 0; i < wrestlers.Count; i ++)
            {
                if (wrestlers[i].GetUniform().Equals(true))
                {
                    if (i < 10)
                    {
                        Console.WriteLine(i + "  Wrestler, " + "Last Name: " + wrestlers[i].GetLastName() + ", First Name: " + wrestlers[i].GetFirstName() + ", Uniform: " + wrestlers[i].GetUniform());
                    }
                    else
                    {
                        Console.WriteLine(i + " Wrestler, " + "Last Name: " + wrestlers[i].GetLastName() + ", First Name: " + wrestlers[i].GetFirstName() + ", Uniform: " + wrestlers[i].GetUniform());
                    }
                }
            }
            Console.WriteLine("");
            Console.WriteLine("Press Enter to Continue!");
            Console.ReadLine();
        }

        //Pre: Gender is male or female and weight is a positive int or double
        //Post: Returns the calculated weight category
        //Desc: Calculates the weight category with the given weight depending on gender
        public double CalculateWeightCategory(string gender, double weight)
        {
            //Store the weight category
            double weightCategory = 0;

            //If the gender is male then calculate weight category depending using the male categories otherwise use female
            if (gender.Equals("Male"))
            {
                if (weight <= maleWeightCategory[0])
                {
                    weightCategory = maleWeightCategory[0];
                }
                else if (weight <= maleWeightCategory[1])
                {
                    weightCategory = maleWeightCategory[1];
                }
                else if (weight <= maleWeightCategory[2])
                {
                    weightCategory = maleWeightCategory[2];
                }
                else if (weight <= maleWeightCategory[3])
                {
                    weightCategory = maleWeightCategory[3];
                }
                else if (weight <= maleWeightCategory[4])
                {
                    weightCategory = maleWeightCategory[4];
                }
                else if (weight <= maleWeightCategory[5])
                {
                    weightCategory = maleWeightCategory[5];
                }
                else if (weight <= maleWeightCategory[6])
                {
                    weightCategory = maleWeightCategory[6];
                }
                else if (weight <= maleWeightCategory[7])
                {
                    weightCategory = maleWeightCategory[7];
                }
                else if (weight <= maleWeightCategory[8])
                {
                    weightCategory = maleWeightCategory[8];
                }
                else if (weight <= maleWeightCategory[9])
                {
                    weightCategory = maleWeightCategory[9];
                }
                else if (weight <= maleWeightCategory[10])
                {
                    weightCategory = maleWeightCategory[10];
                }
                else if (weight <= maleWeightCategory[11])
                {
                    weightCategory = maleWeightCategory[11];
                }
                else if (weight <= maleWeightCategory[12])
                {
                    weightCategory = maleWeightCategory[12];
                }
                else if (weight <= maleWeightCategory[13])
                {
                    weightCategory = maleWeightCategory[13];
                }
                else if (weight <= maleWeightCategory[14])
                {
                    weightCategory = maleWeightCategory[14];
                }
                else if (weight <= maleWeightCategory[15])
                {
                    weightCategory = maleWeightCategory[15];
                }
                else if (weight <= maleWeightCategory[16])
                {
                    weightCategory = maleWeightCategory[16];
                }
                else if (weight >= maleWeightCategory[16])
                {
                    weightCategory = maleWeightCategory[16];
                }
            }
            else
            {
                if (weight <= femaleWeightCategory[0])
                {
                    weightCategory = femaleWeightCategory[0];
                }
                else if (weight <= femaleWeightCategory[1])
                {
                    weightCategory = maleWeightCategory[1];
                }
                else if (weight <= femaleWeightCategory[2])
                {
                    weightCategory = femaleWeightCategory[2];
                }
                else if (weight <= femaleWeightCategory[3])
                {
                    weightCategory = femaleWeightCategory[3];
                }
                else if (weight <= femaleWeightCategory[4])
                {
                    weightCategory = maleWeightCategory[4];
                }
                else if (weight <= femaleWeightCategory[5])
                {
                    weightCategory = femaleWeightCategory[5];
                }
                else if (weight <= femaleWeightCategory[6])
                {
                    weightCategory = femaleWeightCategory[6];
                }
                else if (weight <= femaleWeightCategory[7])
                {
                    weightCategory = femaleWeightCategory[7];
                }
                else if (weight <= femaleWeightCategory[8])
                {
                    weightCategory = maleWeightCategory[8];
                }
                else if (weight <= femaleWeightCategory[9])
                {
                    weightCategory = femaleWeightCategory[9];
                }
                else if (weight <= femaleWeightCategory[10])
                {
                    weightCategory = maleWeightCategory[10];
                }
                else if (weight <= femaleWeightCategory[11])
                {
                    weightCategory = femaleWeightCategory[11];
                }
                else if (weight <= femaleWeightCategory[12])
                {
                    weightCategory = maleWeightCategory[12];
                }
                else if (weight <= femaleWeightCategory[13])
                {
                    weightCategory = femaleWeightCategory[13];
                }
                else if (weight <= femaleWeightCategory[14])
                {
                    weightCategory = femaleWeightCategory[14];
                }
                else if (weight <= femaleWeightCategory[15])
                {
                    weightCategory = femaleWeightCategory[15];
                }
                else if (weight <= femaleWeightCategory[16])
                {
                    weightCategory = femaleWeightCategory[16];
                }
                else if (weight >= femaleWeightCategory[16])
                {
                    weightCategory = femaleWeightCategory[16];
                }
            }

            //Return the calculated weight category
            return weightCategory;
        }
    }
}