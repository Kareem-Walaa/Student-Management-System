using System;
using System.Collections.Generic;

namespace StudentGradeManagementSystem
{
    // Enum for storing grades
    enum Grade
    {
        A, B, C, D, F
    }

    // Struct for storing student information
    struct Student
    {
        public int Id;
        public string Name;
        public double[] Scores;
        public double Average;
        public Grade Grade;
    }

    class Program
    {
        static List<Student> students = new List<Student>();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n\nStudent Grade Management System");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Update Student Scores");
                Console.WriteLine("3. Display All Students");
                Console.WriteLine("4. Display Statistics");
                Console.WriteLine("5. Exit");
                Console.Write("\nEnter your choice (1-5): ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddStudent();
                        break;
                    case "2":
                        UpdateStudent();
                        break;
                    case "3":
                        DisplayAllStudents();
                        break;
                    case "4":
                        DisplayStatistics();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void AddStudent()
        {
            Console.WriteLine("\nAdd New Student");
            
            Student student = new Student();
            student.Scores = new double[3];

            Console.Write("Enter Student ID: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                student.Id = id;
            }
            else
            {
                Console.WriteLine("Invalid ID. Press any key to continue...");
                Console.ReadKey();
                return;
            }

            Console.Write("Enter Student Name: ");
            student.Name = Console.ReadLine();

            for (int i = 0; i < 3; i++)
            {
                Console.Write($"Enter Score {i + 1}: ");
                if (double.TryParse(Console.ReadLine(), out double score))
                {
                    if (score < 0 || score > 100)
                    {
                        Console.WriteLine("Score must be between 0 and 100. Press any key to continue...");
                        Console.ReadKey();
                        return;
                    }
                    student.Scores[i] = score;
                }
                else
                {
                    Console.WriteLine("Invalid score. Press any key to continue...");
                    Console.ReadKey();
                    return;
                }
            }

            CalculateGrade(ref student);
            students.Add(student);

            Console.WriteLine("\nStudent added successfully! Press any key to continue...");
            Console.ReadKey();
        }

        static void UpdateStudent()
        {
            Console.WriteLine("\nUpdate Student Scores");
            Console.Write("Enter Student ID to update: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                int index = students.FindIndex(s => s.Id == id);
                if (index != -1)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        Console.Write($"Enter new Score {i + 1}: ");
                        if (double.TryParse(Console.ReadLine(), out double score))
                        {
                            if (score < 0 || score > 100)
                            {
                                Console.WriteLine("Score must be between 0 and 100. Press any key to continue...");
                                Console.ReadKey();
                                return;
                            }
                            Student student = students[index];
                            student.Scores[i] = score;
                            CalculateGrade(ref student);
                            students[index] = student;
                        }
                        else
                        {
                            Console.WriteLine("Invalid score. Press any key to continue...");
                            Console.ReadKey();
                            return;
                        }
                    }
                    Console.WriteLine("Scores updated successfully!");
                }
                else
                {
                    Console.WriteLine("Student not found!");
                }
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        static void DisplayAllStudents()
        {
            Console.WriteLine("\nAll Students:");
            Console.WriteLine("ID\tName\tAverage\tGrade\tScores");
            Console.WriteLine("------------------------------------------------");

            foreach (var student in students)
            {
                Console.WriteLine($"{student.Id}\t{student.Name}\t{student.Average:F1}\t{student.Grade}\t{student.Scores[0]:F1}, {student.Scores[1]:F1}, {student.Scores[2]:F1}");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        static void DisplayStatistics()
        {
            if (students.Count == 0)
            {
                Console.WriteLine("\nNo students in the system.");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                return;
            }

            // Find highest score
            Student topStudent = students[0];
            foreach (var student in students)
            {
                if (student.Average > topStudent.Average)
                    topStudent = student;
            }

            Console.WriteLine("\nStatistics:");
            Console.WriteLine($"Top Student: {topStudent.Name} (Average: {topStudent.Average:F1})");

            Console.WriteLine("\nPassed Students:");
            foreach (var student in students)
            {
                if (student.Grade != Grade.F)
                    Console.WriteLine($"{student.Name} - Grade: {student.Grade} (Average: {student.Average:F1})");
            }

            Console.WriteLine("\nFailed Students:");
            foreach (var student in students)
            {
                if (student.Grade == Grade.F)
                    Console.WriteLine($"{student.Name} - Grade: {student.Grade} (Average: {student.Average:F1})");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        static void CalculateGrade(ref Student student)
        {
            student.Average = (student.Scores[0] + student.Scores[1] + student.Scores[2]) / 3.0;

            if (student.Average >= 90) student.Grade = Grade.A;
            else if (student.Average >= 80) student.Grade = Grade.B;
            else if (student.Average >= 70) student.Grade = Grade.C;
            else if (student.Average >= 60) student.Grade = Grade.D;
            else student.Grade = Grade.F;
        }
    }
}
