public class Podcast : Sound
{
    private string creator;
    private int rating;

    public Podcast(string title, string creator, int year, int month, int day, int duration, int rating) : base(title, year, month, day, duration)
    {
        Creator = creator;
        Rating = rating;
    }

    public override string ToString()
    {
        return $"This podcast is called {Title} by {Creator}\n" +
            $"it came out on {Date} and is {Duration} minutes long\n" +
            $"{Title}'s rating is {Rating}/10\n";
    }

    public override int CompareTo(object? obj)
    {
        Sound track = obj as Sound;

        if ((int)track.Rating > (int)Rating)
        {
            return 1;
        }
        else if (track.Rating == Rating)
        {
            return 0;
        }
        else
        {
            return -1;
        }
    }

    public string Creator
    {
        get => creator;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(Creator));
            }
            creator = value;
        }
    }
    public override IComparable Rating
    {
        get => rating;
        set
        {
            if (value is <= (IComparable)0 or > (IComparable)10)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }
            rating = (int)value;
        }
    }
}

