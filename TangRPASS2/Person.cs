//Author: Ryan Tang
//File Name: Person.cs
//Project Name: TangRPASS2
//Creation Date:
//Modified Date: 
//Description: Protects attributes for both coaches and wrestlers

using System;

namespace TangRPASS2
{
    internal class Person
    {
        //Store the attributes of both coaches and wrestlers
        protected string firstName;
        protected string lastName;
        protected string school;
        protected string gender;
        protected int yearsExperience;

        //Pre: All variables are attributes of a person
        //Post: None
        //Desc: Store the incoming attributes into secure variables
        public Person(string firstName, string lastName, string school, string gender, int yearsExperience)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.school = school;
            this.gender = gender;
            this.yearsExperience = yearsExperience;
        }
        
        //Pre: None
        //Post: None
        //Desc: Displays a person's data onto the screen
        public virtual void DisplayData()
        {
            Console.WriteLine("Person");
            Console.WriteLine("");
            Console.WriteLine("Last Name: " + lastName);
            Console.WriteLine("First Name: " + firstName);
            Console.WriteLine("Gender: " + gender);
            Console.WriteLine("School: " + school);
            Console.WriteLine("Years of Experience: " + yearsExperience);
        }

        //Pre: The years variable is the new years of experience of a person
        //Post: None
        //Desc: Modify and store the new years of experience of a person
        public void SetYears(int years)
        {
            yearsExperience = years;
        }
    }
}
