using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Create videos
        Video video1 = new Video("Cooking Pasta", "Chef John", 600);
        video1.AddComment(new Comment("Alice", "Looks delicious!"));
        video1.AddComment(new Comment("Bob", "I tried this recipe, amazing."));
        video1.AddComment(new Comment("Charlie", "Can you share more tips?"));

        Video video2 = new Video("Gardening Basics", "GreenThumb", 900);
        video2.AddComment(new Comment("Dave", "Love the practical advice."));
        video2.AddComment(new Comment("Emma", "My garden looks so much better now."));
        video2.AddComment(new Comment("Frank", "What kind of soil did you use?"));

        Video video3 = new Video("Guitar Tutorial", "MusicMan", 450);
        video3.AddComment(new Comment("Grace", "I learned so much from this!"));
        video3.AddComment(new Comment("Hannah", "More tutorials, please."));
        video3.AddComment(new Comment("Ian", "Great job explaining."));

        // Add videos to a list
        List<Video> videos = new List<Video> { video1, video2, video3 };

        // Display video info
        foreach (Video v in videos)
        {
            v.DisplayVideoInfo();
        }
    }
}
