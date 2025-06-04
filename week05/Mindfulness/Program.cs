using System;
using System.Collections.Generic;
using System.Threading;

class Activity
{
    private string _name;
    private string _description;
    private int _duration;

    public Activity(string name, string description)
    {
        _name = name;
        _description = description;
    }

    public void Start()
    {
        Console.Clear();
        Console.WriteLine($"--- {_name} ---");
        Console.WriteLine(_description);
        Console.WriteLine();
        Console.Write("Enter duration in seconds: ");
        while (!int.TryParse(Console.ReadLine(), out _duration) || _duration <= 0)
        {
            Console.Write("Please enter a valid positive integer for duration: ");
        }
        Console.WriteLine("\nGet ready...");
        PauseWithAnimation(3);
        Console.Clear();
    }

    protected void PauseWithAnimation(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write($"\r{i} seconds remaining... ");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }

    public virtual void RunActivity()
    {
        // To be overridden by subclasses
    }

    public void End()
    {
        Console.WriteLine("\nWell done!");
        Console.WriteLine($"You completed the {_name} for {_duration} seconds.");
        PauseWithAnimation(3);
    }

    protected int Duration => _duration;
}

class BreathingActivity : Activity
{
    public BreathingActivity()
        : base("Breathing Activity",
            "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    {
    }

    public override void RunActivity()
    {
        int elapsed = 0;
        while (elapsed < Duration)
        {
            Console.WriteLine("Breathe in...");
            PauseWithAnimation(4);
            elapsed += 4;
            if (elapsed >= Duration) break;

            Console.WriteLine("Breathe out...");
            PauseWithAnimation(6);
            elapsed += 6;
        }
    }
}

class ReflectionActivity : Activity
{
    private List<string> _prompts = new List<string>()
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private List<string> _questions = new List<string>()
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public ReflectionActivity()
        : base("Reflection Activity",
            "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
    {
    }

    public override void RunActivity()
    {
        Random rand = new Random();
        string prompt = _prompts[rand.Next(_prompts.Count)];
        Console.WriteLine(prompt);
        Console.WriteLine();

        int elapsed = 0;
        while (elapsed < Duration)
        {
            string question = _questions[rand.Next(_questions.Count)];
            Console.WriteLine(question);
            PauseSpinner(5);
            elapsed += 5;
        }
    }

    private void PauseSpinner(int seconds)
    {
        string[] spinner = { "/", "-", "\\", "|" };
        int count = 0;
        for (int i = 0; i < seconds * 4; i++)
        {
            Console.Write($"\r{spinner[count++ % spinner.Length]} ");
            Thread.Sleep(250);
        }
        Console.WriteLine();
    }
}

class ListingActivity : Activity
{
    private List<string> _prompts = new List<string>()
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity()
        : base("Listing Activity",
            "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    {
    }

    public override void RunActivity()
    {
        Random rand = new Random();
        string prompt = _prompts[rand.Next(_prompts.Count)];
        Console.WriteLine(prompt);
        Console.WriteLine("You may begin in:");
        PauseWithAnimation(5);
        Console.WriteLine("Start listing your items:");

        List<string> items = new List<string>();
        DateTime endTime = DateTime.Now.AddSeconds(Duration);

        while (DateTime.Now < endTime)
        {
            Console.Write("> ");
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                items.Add(input.Trim());
            }
        }

        Console.WriteLine($"\nYou listed {items.Count} items.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Mindfulness Program");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Quit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();
            Activity activity = null;

            switch (choice)
            {
                case "1":
                    activity = new BreathingActivity();
                    break;
                case "2":
                    activity = new ReflectionActivity();
                    break;
                case "3":
                    activity = new ListingActivity();
                    break;
                case "4":
                    Console.WriteLine("Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Press Enter to try again.");
                    Console.ReadLine();
                    continue;
            }

            activity.Start();
            activity.RunActivity();
            activity.End();

            Console.WriteLine("\nPress Enter to return to the menu.");
            Console.ReadLine();
        }
    }
}
