//Author: Ryan Tang
//File Name: Wrestler.cs
//Project Name: TangRPASS2
//Creation Date:
//Modified Date: 
//Description: Manage attributes of wrestlers

using System;

namespace TangRPASS2
{
    internal class Wrestler : Person
    {
        //Store integers based on the highest weight categories
        private const int highestMaleWeightCategory = 131;
        private const int highestFemaleWeightCategory = 116;

        //Create a new team object
        private Team teams = new Team();

        //Store the attributes of wrestlers
        private string birthday;
        private int wins;
        private int losses;
        private double weight;
        private int totalPoints;
        private int winsByPin;
        private string status;
        private bool uniform;
        private double weightCategory;

        //Pre: Incoming variables are attributes of wrestlers
        //Post: None
        //Desc: Stores the incoming attributes of wrestlers
        public Wrestler(string lastName, string firstName, string gender, string school, int yearsExperience, string birthday, double weight, double weightCategory, int wins, int losses, int totalPoints, int winsByPin, string status, bool uniform) : base(firstName, lastName, school, gender, yearsExperience)
        {
            this.birthday = birthday;
            this.weight = weight;
            this.weightCategory = weightCategory;
            this.wins = wins;
            this.losses = losses;
            this.totalPoints = totalPoints;
            this.winsByPin = winsByPin;
            this.status = status;
            this.uniform = uniform;
        }

        //Pre: None
        //Post: None
        //Desc: Returns the gender of the wrestler
        public string GetGender()
        {
            return gender;
        }

        //Pre: None
        //Post: None
        //Desc: Returns the last name of the wrestler
        public string GetLastName()
        {
            return lastName;
        }

        //Pre: None
        //Post: None
        //Desc: Returns the first name of the wrestler
        public string GetFirstName()
        {
            return firstName;
        }

        //Pre: None
        //Post: None
        //Desc: Returns the school of the wrestler
        public string GetSchool()
        {
            return school;
        }

        //Pre: None
        //Post: None
        //Desc: Returns the birthday of the wrestler
        public string GetBirthday()
        {
            return birthday;
        }

        //Pre: None
        //Post: None
        //Desc: Returns the years of experience of the wrestler
        public int GetYearsExperience()
        {
            return yearsExperience;
        }

        //Pre: None
        //Post: None
        //Desc: Returns the wins of the wrestler
        public int GetWins()
        {
            return wins;
        }

        //Pre: None
        //Post: None
        //Desc: Returns the losses of the wrestler
        public int GetLosses()
        {
            return losses;
        }

        //Pre: None
        //Post: None
        //Desc: Returns the total points of the wrestler
        public int GetTotalPoints()
        {
            return totalPoints;
        }

        //Pre: None
        //Post: None
        //Desc: Returns the wins by pin of the wrestler
        public int GetWinsByPin()
        {
            return winsByPin;
        }

        //Pre: None
        //Post: None
        //Desc: Returns the uniform of the wrestler
        public bool GetUniform()
        {
            return uniform;
        }

        //Pre: None
        //Post: None
        //Desc: Returns the status of the wrestler
        public string GetStatus()
        {
            return status;
        }

        //Pre: None
        //Post: None
        //Desc: Returns the weight category of the wrestler
        public double GetWeightCategory()
        {
            return teams.CalculateWeightCategory(gender, weight);
        }

        //Pre: None
        //Post: None
        //Desc: Returns the weight of the wrestler
        public double GetWeight()
        {
            return weight;
        }

        //Pre: The variable is the old weight
        //Post: None
        //Desc: Sets the new weight of the wrestler
        public void SetWeight(double weight)
        {
            this.weight = weight;
        }

        //Pre: The variable is the old wins
        //Post: None
        //Desc: Sets the new wins of the wrestler
        public void SetWins(int wins)
        {
            this.wins = wins;
        }

        //Pre: The variable is the old number of losses
        //Post: None
        //Desc: Sets the new number of losses of the wrestler
        public void SetLosses(int losses)
        {
            this.losses = losses;
        }

        //Pre: The variable is the old number of total points
        //Post: None
        //Desc: Sets the new number of total points of the wrestler
        public void SetTotalPoints(int totalPoints)
        {
            this.totalPoints = totalPoints;
        }

        //Pre: The variable is the old number of wins by pin
        //Post: None
        //Desc: Sets the new number of wins by pin of the wrestler
        public void SetWinsByPin(int winsByPin)
        {
            this.winsByPin = winsByPin;
        }

        //Pre: The variable is the old status
        //Post: None
        //Desc: Sets the new status of the wrestler
        public void SetStatus(string status)
        {
            this.status = status;
        }

        //Pre: The variable is the old uniform
        //Post: None
        //Desc: Sets the new uniform of the wrestler
        public void SetUniform(bool uniform)
        {
            this.uniform = uniform;
        }

        //Pre: None
        //Post: None
        //Desc: Displays the data of a wrestler on the screen
        public override void DisplayData()
        {
            //Display Wrestler personal data
            Console.WriteLine("Wrestler");
            Console.WriteLine("");
            Console.WriteLine("Last Name: " + lastName);
            Console.WriteLine("First Name: " + firstName);
            Console.WriteLine("Gender: " + gender);
            Console.WriteLine("School: " + school);
            Console.WriteLine("Years of Experience: " + yearsExperience);
            Console.WriteLine("Birth Date: " + birthday);
            Console.WriteLine("Weight: " + weight);

            //Display wrestler gender and display weight category
            if (gender.Equals("Male") && teams.CalculateWeightCategory(gender, weight).Equals(highestMaleWeightCategory))
            {
                Console.WriteLine("Weight Category: 130+");
            }
            else if (gender.Equals("Female") && teams.CalculateWeightCategory(gender, weight).Equals(highestFemaleWeightCategory))
            {
                Console.WriteLine("Weight Category: 115+");
            }
            else
            {
                Console.WriteLine("Weight Category: " + teams.CalculateWeightCategory(gender, weight));
            }
            
            //Display wrestler experience data
            Console.WriteLine("Wins: " + wins);
            Console.WriteLine("Losses: " + losses);
            Console.WriteLine("Total Points: " + totalPoints);
            Console.WriteLine("Wins by Pin: " + winsByPin);
            Console.WriteLine("Status: " + status);
            Console.WriteLine("Uniform Signed Out: " + uniform);
            Console.WriteLine("");
        }
    }
}
