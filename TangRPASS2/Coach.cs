//Author: Ryan Tang
//File Name: Coach.cs
//Project Name: TangRPASS2
//Creation Date:
//Modified Date: 
//Description: Stores attributes of coaches

using System;

namespace TangRPASS2
{
    internal class Coach : Person
    {
        //Store the type of coach
        private string type;

        //Pre: All variables are attributes of a Coach
        //Post: None
        //Desc: Store the incoming attributes into variables
        public Coach(string lastName, string firstName, string gender, string school, int yearsExperience, string type) : base(firstName, lastName, school, gender, yearsExperience)
        {
            this.type = type;
        }

        //Pre: None
        //Post: None
        //Desc: Returns the last name of a coach
        public string GetLastName()
        {
            return lastName;
        }

        //Pre: None
        //Post: None
        //Desc: Returns the first name of a coach
        public string GetFirstName()
        {
            return firstName;
        }

        //Pre: None
        //Post: None
        //Desc: Returns the gender of a coach
        public string GetGender()
        {
            return gender;
        }

        //Pre: None
        //Post: None
        //Desc: Returns the school of a coach
        public string GetSchool()
        {
            return school;
        }

        //Pre: None
        //Post: None
        //Desc: Returns the years of experience of a coach
        public int GetYearsExperience()
        {
            return yearsExperience;
        }

        //Pre: None
        //Post: None
        //Desc: Returns the type of a coach
        public string GetType()
        {
            return type;
        }

        //Pre: The variable is the old type of coach
        //Post: None
        //Desc: Sets the new type of the coach
        public void SetType(string type)
        {
            this.type = type;
        }

        //Pre: None
        //Post: None
        //Desc: Displays the data of a coach on the screen
        public override void DisplayData()
        {
            Console.WriteLine("Coach");
            Console.WriteLine("");
            Console.WriteLine("Last Name: " + lastName);
            Console.WriteLine("First Name: " + firstName);
            Console.WriteLine("Gender: " + gender);
            Console.WriteLine("School: " + school);
            Console.WriteLine("Years of Experience: " + yearsExperience);
            Console.WriteLine("Type: " + type);
            Console.WriteLine("");
        }
    }
}