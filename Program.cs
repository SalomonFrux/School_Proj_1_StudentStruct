using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace StudentsStruct
{
    public class Program
    {
        static List<Student> Nstudent = new List<Student>(30);
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Title = "FRUX SALOMON";
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            StudentDataColletor();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Press any key to display the list from the serialised data\n");
            Console.ReadKey(intercept: true);
            StudentDataReader();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Press any key to display the list of promoted students \n");
            Console.ReadKey(intercept: true);
            StudentPromoter();
            Console.Read();
        }

        public static void StudentDataColletor()
        {
            try
            {
                int SInfoMark, SPhyMark, SMathMark;
                bool IsdateOfBirth, isInteger;
                Student student = new Student();
                string userInput = string.Empty;
                do
                {

                    Console.WriteLine("Enter the student Surname");
                    string SN = Console.ReadLine().ToUpper();
                    Console.WriteLine("Enter the student firstName");
                    string FN = Console.ReadLine().ToUpper();

                    Console.WriteLine("Type " + "\"" + "Male" + "\"" + " If the student is male or " + "\"" + "Femmale" + "\"" + " if female");
                    string SS = Console.ReadLine().ToUpper();

                    DateTime SDOB;
                    do
                    {
                        Console.WriteLine("Enter student date of birth: ie 02/05/2010 ");
                        IsdateOfBirth = DateTime.TryParse(Console.ReadLine(), out SDOB);

                    } while (!IsdateOfBirth);



                    do
                    {
                        Console.WriteLine("Enter student mathematics mark in number");
                        isInteger = Int32.TryParse(Console.ReadLine(), out SMathMark);
                        Console.WriteLine("Enter the student physics mark in number");
                        isInteger = Int32.TryParse(Console.ReadLine(), out SPhyMark);
                        Console.WriteLine("Enter the student informatics mark in number");
                        isInteger = Int32.TryParse(Console.ReadLine(), out SInfoMark);
                    } while (!isInteger);

                    Console.WriteLine("Type" + "\"" + "Yes" + "\"" + "if scholaship received or" + "\"" + " No if not received\n");
                    string SCholar = Console.ReadLine().ToUpper();
                    DateTime today = DateTime.Today;
                    student = new Student
                    {
                        surName = SN,
                        firstName = FN,
                        sex = SS,
                        dateOfBirth = SDOB,
                        informaticsMark = SInfoMark,
                        mathematicsMark = SMathMark,
                        physicsMark = SPhyMark,
                        scholarship = SCholar,
                        Age = (today.Year - SDOB.Year)
                    };
                    Nstudent.Add(student);

                    do
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.WriteLine("Press C to add a new list or S to stop");
                        userInput = Console.ReadLine().ToUpper();

                    } while (userInput != "C" && userInput != "S");



                } while (userInput == "C");



                if (userInput == "S")
                {
                    using (FileStream fs = new FileStream("StudentData.txt", FileMode.Append, FileAccess.Write))
                    {
                        foreach (Student Stud in Nstudent)
                        {
                            BinaryFormatter formatter = new BinaryFormatter();
                            formatter.Serialize(fs, Stud);
                        }

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Student data saved \n");
                    }
                }

            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("An error occured, the administrator will fix it");
            }
        }


        public static void StudentDataReader()
        {
            FileStream fs = new FileStream("StudentData.txt", FileMode.Open, FileAccess.Read);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            Nstudent.Clear();
            while (fs.Position < fs.Length)
            {
                Nstudent.Add((Student)binaryFormatter.Deserialize(fs));
            }

            foreach (Student Std in Nstudent)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Surname:\t " + Std.surName);
                Console.WriteLine("Name:  \t\t " + Std.firstName);
                Console.WriteLine("Gender: \t " + Std.sex);
                Console.WriteLine("IT Mark: \t " + Std.informaticsMark);
                Console.WriteLine("Math Mark:\t " + Std.mathematicsMark);
                Console.WriteLine("Physics:\t " + Std.physicsMark);
                Console.WriteLine("Scholarship:\t " + Std.scholarship + "\n");
            }
            fs.Close();


        }


        public static void StudentPromoter()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("LIST OF PROMOTED STUDENTS:");
            foreach (Student std in Nstudent)
            {

                if (std.informaticsMark + std.mathematicsMark + std.physicsMark > 140)
                {
                    Console.ForegroundColor = ConsoleColor.White;

                   Console.WriteLine("\n" + "Full Name: {0} \t\t| Age:{1} ", std.firstName + " " + std.surName, std.Age);
                }
            }

        }
    }
}




