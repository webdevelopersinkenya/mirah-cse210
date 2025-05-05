using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("What is your first name?.");
        string firstname = Console.ReadLine();

        Console.Write("What is your second Name?");
        string secondname = Console.ReadLine();
        Console.WriteLine($"Your name is {secondname}, {firstname}, {secondname}");
    }
}