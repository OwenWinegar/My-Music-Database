public class Track : Sound
{
    private string artist, album;
    private double rating;

    public Track(string title, string artist, string album, int year, int month, int day, int duration, double rating) : base(title, year, month, day, duration)
    {
        Artist = artist;
        Album = album;
        Rating = rating;
    }

    public override string ToString()
    {
        return $"This track is called {Title} by {Artist} on the album {Album}\n" +
            $"it came out in {Date} and is {Duration} minutes long\n" +
            $"{Title}'s rating is {Rating}/5\n";
    }

    public override int CompareTo(object? obj)
    {
        Sound track = obj as Sound;

        if ((double)track.Rating > (double)Rating)
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

    public string Artist
    {
        get => artist;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(Artist));
            }
            artist = value;
        }
    }
    public string Album
    {
        get => album;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(Album));
            }
            album = value;
        }
    }
    public override IComparable Rating
    {
        get => rating;
        set
        {
            if (value is < (IComparable)0 or > (IComparable)5)
            {
                throw new ArgumentOutOfRangeException(nameof(Rating));
            }
            rating = (double)value;
        }
    }
}

