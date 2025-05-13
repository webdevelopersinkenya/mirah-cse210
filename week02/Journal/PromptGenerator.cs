using System;
using System.Collections.Generic;

public class PromptGenerator
{
    private List<string> _prompts = new List<string>
    {
        "What was the highlight of your day?",
        "What did you learn today?",
        "How did you see the hand of the Lord today?",
        "What could you have done better today?",
        "What made you smile today?"
    };

    private Random _random = new Random();

    public string GetRandomPrompt()
    {
        int index = _random.Next(_prompts.Count);
        return _prompts[index];
    }
}
