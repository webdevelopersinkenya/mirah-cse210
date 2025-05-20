using System;
using System.Collections.Generic;
using System.Linq;

class Reference
{
    public string Book { get; private set; }
    public int StartVerse { get; private set; }
    public int? EndVerse { get; private set; }

    // Constructor for single verse
    public Reference(string book, int startVerse)
    {
        Book = book;
        StartVerse = startVerse;
        EndVerse = null;
    }

    // Constructor for verse range
    public Reference(string book, int startVerse, int endVerse)
    {
        Book = book;
        StartVerse = startVerse;
        EndVerse = endVerse;
    }

    public string GetDisplayText()
    {
        if (EndVerse.HasValue)
            return $"{Book} {StartVerse}-{EndVerse.Value}";
        else
            return $"{Book} {StartVerse}";
    }
}

class Word
{
    private string text;
    private bool hidden;

    public Word(string text)
    {
        this.text = text;
        hidden = false;
    }

    public void Hide()
    {
        hidden = true;
    }

    public bool IsHidden()
    {
        return hidden;
    }

    public string GetDisplayText()
    {
        if (hidden)
            return new string('_', text.Length);
        else
            return text;
    }
}

class Scripture
{
    private Reference reference;
    private List<Word> words;

    public Scripture(Reference reference, string text)
    {
        this.reference = reference;
        words = new List<Word>();
        // Split on spaces ( I can improve to keep punctuation if needed)
        foreach (var word in text.Split(' '))
        {
            words.Add(new Word(word));
        }
    }

    public void HideRandomWords(int count)
    {
        var rand = new Random();

        // For core requirement: select any word at random, even if hidden
        for (int i = 0; i < count; i++)
        {
            int index = rand.Next(words.Count);
            words[index].Hide();
        }

    }

    public string GetDisplayText()
    {
        string refText = reference.GetDisplayText();
        string scriptureText = string.Join(" ", words.Select(w => w.GetDisplayText()));
        return $"{refText}\n{scriptureText}";
    }

    public bool IsFullyHidden()
    {
        return words.All(w => w.IsHidden());
    }
}

class Program
{
    static void Main()
    {
        // Example scripture: Proverbs 3:5-6
        var reference = new Reference("Proverbs", 3, 5);
        string scriptureText = "Trust in the Lord with all your heart and lean not on your own understanding.";

        var scripture = new Scripture(reference, scriptureText);

        while (true)
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
            if (scripture.IsFullyHidden())
            {
                Console.WriteLine("\nAll words are hidden. Well done!");
                break;
            }
            Console.WriteLine("\nPress Enter to hide more words or type 'quit' to exit.");
            string input = Console.ReadLine();
            if (input.ToLower() == "quit")
                break;

            // Hide 3 words per iteration (I can adjust)
            scripture.HideRandomWords(3);
        }
    }
}
