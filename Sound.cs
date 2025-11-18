public abstract class Sound : IComparable
{
    //make icomparable
    //compareTo
    private string title;
    private int month, day, year, duration;

    public Sound(string title, int year, int month, int day, int duration)
    {
        Title = title;
        Date = new DateTime(year, month, day);
        Year = year;
        Month = month;
        Day = day;
        Duration = duration;
    }

    public abstract IComparable Rating {
        get;
        set;
    }


    public abstract int CompareTo(object? obj);

    public string Title
    {
        get => title;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(Title));
            }
            title = value;
        }
    }

    public DateTime Date
    {
        get;
        set;
    }

    public int Year
    {
        get => year;
        set
        {
            if (value < 0 || value > 2023)
            {
                throw new ArgumentOutOfRangeException(nameof(Year));
            }
            year = value;
        }
    }

    public int Month
    {
        get => month;
        set
        {
            if (value < 0 || value > 12)
            {
                throw new ArgumentOutOfRangeException(nameof(Month));
            }
            month = value;
        }
    }

    public int Day
    {
        get => day;
        set
        {
            if (value < 0 || value > 31)
            {
                throw new ArgumentOutOfRangeException(nameof(Day));
            }
            day = value;
        }
    }

    public int Duration
    {
        get => duration;
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(Duration));
            }
            duration = value;
        }
    }
}

