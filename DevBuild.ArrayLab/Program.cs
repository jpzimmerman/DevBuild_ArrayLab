using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevBuild.Utilities;

namespace DevBuild.ArrayLab
{
    class Program
    {
        public enum ProgramState  { MainScreen, EnterStudentNumber, EnterStudentInfo, DisplayStudentInfo }
        public enum StudentData { Name = 0, Hometown = 1, Age = 2, FavoriteFood = 3 };

        public static bool validData = false;

        static void Main(string[] args)
        {
            YesNoAnswer userAnswer = YesNoAnswer.AnswerNotGiven;
            uint userSelection = 0;
            string userEntry = "";
            
            string[,] ourStudents = new string[4, 4] {
                { "Geoff", "Ferndale, MI", "28", "Tacos" },
                { "Katie", "Oswego, MI", "26", "Coney dogs"},
                { "Hector","Dearborn, MI","32","Pasta" },
                { "Jennifer","Streetsboro, OH","23","Nachos" }
            };

            while (true)
            {
                ProgramStart:
                Console.WriteLine("Welcome to our C# class. Which student would you like to learn more about?");
                for (int i = 0; i < ourStudents.GetLength(0); i++)
                {
                    Console.WriteLine($"{i + 1}) " + ourStudents[i, 0]);
                }
                validData = false;
                while (!validData)
                {
                    try
                    {
                        Console.Write($"Please enter a selection from 1 to {ourStudents.GetLength(0)}: ");
                        string userInput = Console.ReadLine();
                        if (!uint.TryParse(userInput, out userSelection))
                        {
                            throw new FormatException();
                        }
                        if (userSelection < 1 || userSelection > ourStudents.GetLength(0))
                        {
                            throw new IndexOutOfRangeException();
                        }
                        validData = true;
                        userInput = "";
                    }
                    catch (IndexOutOfRangeException)
                    {
                        Console.WriteLine("I couldn't match that number to a student.");
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("I didn't recognize that format.");
                    }
                }
                Console.WriteLine($"Student {userSelection} is {ourStudents[userSelection - 1, 0]}");

                validData = false;
                while (!validData)
                {
                    Console.Write($"What would you like to know about {ourStudents[userSelection - 1, 0]}? ");
                    while (!validData)
                    {
                        userEntry = Console.ReadLine().Trim();
                        validData = FetchAndPrintData(ourStudents, userEntry, userSelection);
                    }

                    userAnswer = UserInput.GetYesOrNoAnswer("Do you want information for another student? (y/n) ");
                    switch (userAnswer)
                    {
                        case YesNoAnswer.Yes:
                            {
                                userAnswer = YesNoAnswer.AnswerNotGiven;
                                userEntry = "";
                                userSelection = 0;
                                validData = true;
                                Console.WriteLine("\n");
                                goto ProgramStart;
                            }
                        case YesNoAnswer.No: return;
                    }
                }
            }
        }

        static bool FetchAndPrintData(string[,] records, string requestedData, uint userSelection)
        {
            try
            {
                switch (requestedData.ToLower())
                {
                    case "name":
                    case "last name":
                        {
                            Console.WriteLine($"All {records[userSelection - 1, 0]} gave us to work with was {records[userSelection - 1, 0]}");
                            break;
                        }
                    case "home town":
                    case "hometown":
                        {
                            validData = true;
                            Console.WriteLine($"{records[userSelection - 1, 0]}'s home town is {records[userSelection - 1, 1]}");
                            break;
                        }
                    case "age":
                        {
                            validData = true;
                            Console.WriteLine($"{records[userSelection - 1, 0]}'s age is {records[userSelection - 1, 2]}");
                            break;
                        }
                    case "favorite food":
                    case "favoritefood":
                    case "food":
                        {
                            validData = true;
                            Console.WriteLine($"{records[userSelection - 1, 0]}'s favorite food is {records[userSelection - 1, 3]}");
                            break;
                        }
                    case "back":
                    case "go back": break;
                    case "done": break;
                    default:
                        {
                            validData = false;
                            throw new FormatException();
                        }
                }
                return validData;
            }
            catch (FormatException)
            {
                Console.WriteLine("I didn't recognize that entry.");
                Console.Write($"What would you like to know about {records[userSelection - 1, 0]}? Current valid fields are ");
                //use this loop to print what valid pieces of information we have for student records
                for (int i = 0; i < Enum.GetNames(typeof(StudentData)).Length; i++)
                {
                    bool lastItem = (i == Enum.GetNames(typeof(StudentData)).Length - 1);
                    Console.Write(  (lastItem ? "and " : "") +
                                    Enum.GetNames(typeof(StudentData))[i] +
                                    (lastItem ? ".\n" :", "));
                }

                return validData;
            }
        }
    }
}
