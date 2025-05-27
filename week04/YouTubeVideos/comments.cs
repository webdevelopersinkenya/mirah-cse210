public class Comment
{
    public string CommenterName { get; set; }
    public string CommentText { get; set; }

    public Comment(string name, string text)
    {
        CommenterName = name;
        CommentText = text;
    }
}
