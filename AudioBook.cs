public class AudioBook : Sound
{
    private string author;
    private AudioRating rating;

    public AudioBook(string title, string author, int year, int month, int day, int duration, AudioRating rating) : base(title, year, month, day, duration)
    {
        Author = author;
        Rating = rating;
    }

    public override string ToString()
    {
        return $"This audio book is called {Title} by {Author}\n" +
            $"it came out on {Date} and is {Duration} minutes long\n" +
            $"{Title}'s rating is a {Rating}\n";
    }

    public override int CompareTo(object? obj)
    {
        Sound track = obj as Sound;

        if ((double)track.Rating == (double)Rating)
        {
            return 0;
        }
        else if((double)track.Rating < (double)Rating)
        {
            return -1;
        }
        else
        {
            return 1;
        }
    }

    public string Author
    {
        get => author;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(Author));
            }
            author = value;
        }
    }

    public override IComparable Rating
    {
        get;
        set;
    }
}

