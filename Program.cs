using System.IO;

public static class FinalProject
{
    private static int GetLineCount(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException("File not found", path);
        }

        using StreamReader reader = new StreamReader(path);

        int count = 0;

        while (!reader.EndOfStream)
        {
            reader.ReadLine();
            count++;
        }

        return count;
    }

    private static void Read(string path, List<Sound> sound)
    {

        int lineCount = GetLineCount(path);
        try
        {
            using StreamReader reader = new StreamReader(path);

            reader.ReadLine();

            for (int i = 0; i < lineCount - 1; i++)
            {
                string line = reader.ReadLine();

                string[] cols = line.Split(",");

                string type = cols[0];
                string title = cols[1];
                int year = int.Parse(cols[4]);
                int month = int.Parse(cols[5]);
                int day = int.Parse(cols[6]);

                int duration = int.Parse(cols[7]);

                if (string.Equals(type, "Track", StringComparison.OrdinalIgnoreCase))
                {
                    string artist = cols[2];
                    string album = cols[3];
                    double rating = double.Parse(cols[8]);

                    sound.Add(new Track(title, artist, album, year, month, day, duration, rating));
                }
                else if (string.Equals(type, "AudioBook", StringComparison.OrdinalIgnoreCase))
                {
                    string author = cols[2];
                    AudioRating rating = (AudioRating)Enum.Parse(typeof(AudioRating), cols[8]);

                    sound.Add(new AudioBook(title, author, year, month, day, duration, rating));
                }
                else if (string.Equals(type, "Podcast", StringComparison.OrdinalIgnoreCase))
                {
                    string creator = cols[2];
                    int rating = int.Parse(cols[8]);

                    sound.Add(new Podcast(title, creator, year, month, day, duration, rating));
                }
            }
        }
        catch
        {
            Console.WriteLine("Error reading from file");
            return;
        }
    }

    private static void Write(List<Sound> sound, string path)
    {
        using StreamWriter writer = new StreamWriter(path);

        writer.WriteLine("Type, Title, Artist(Track) Author(AudioBook) or Creator(Podcast), Album(Track ONLY), Year, Month, Day, Duration, Rating");

        // first column is the TYPE
        // go in order of how each type is declared

        for (int i = 0; i < sound.Count; i++)
        {
            if (sound[i] is Track)
            {
                Track t = (Track)sound[i];
                writer.WriteLine($"Track,{t.Title},{t.Artist},{t.Album},{t.Year},{t.Month},{t.Day},{t.Duration},{t.Rating}");
            }
            else if (sound[i] is AudioBook)
            {
                AudioBook t = (AudioBook)sound[i];
                writer.WriteLine($"AudioBook,{t.Title},{t.Author},N/A,{t.Year},{t.Month},{t.Day},{t.Duration},{t.Rating}");
            }
            else if (sound[i] is Podcast)
            {
                Podcast t = (Podcast)sound[i];
                writer.WriteLine($"Podcast,{t.Title},{t.Creator},N/A,{t.Year},{t.Month},{t.Day},{t.Duration},{t.Rating}");
            }
        }
    }

    public static void Main()
    {

        // 10 tracks, 5 audio books, and 5 podcasts.  At least two tracks should have the same artist.
        //List<Sound> sound = new List<Sound>{
        //    new Track("Bound 2", "Kanye West", "Yeezus", 2013, 5, 2, 4, 4.8),
        //    new Track("Devil In A New Dress", "Kanye West", "My Beautiful Dark Twisted Fantasy",2010, 12, 3, 6, 5.0),
        //    new Track("Spaceship", "Kanye West", "The College Dropout", 2004, 11, 4, 6, 4.8),
        //    new Track("Hey Ya!", "OutKast", "Speakerboxxx/The Love Below", 2003, 8, 18, 4, 4.3),
        //    new Track("20 Min", "Lil Uzi Vert", "Luv Is Rage 2 (Deluxe)", 2017, 10, 2, 4, 4.4),
        //    new Track("Neon Guts", "Lil Uzi Vert", "Luv Is Rage 2 (Deluxe)", 2017, 6, 12, 5, 3.9),
        //    new Track("N Side", "Steve Lacy", "Apollo XXI", 2019, 5, 12, 4, 4.1),
        //    new Track("No Role Modelz", "J. Cole", "2014 Forest Hills Drive", 2014, 4, 30, 5, 4.3),
        //    new Track("HIBIKI", "Bad Bunny", "nadie sabe lo qua va a pasar mañana", 2023, 11, 27, 3, 4.5),
        //    new Track("FEEL.", "Kendrick Lamar", "DAMN.", 2017, 3, 17, 4, 3.9),

        //    new AudioBook("Exit West", "Mohsin Hamid", 2017, 4, 15, 282, AudioRating.Thumbs_up),
        //    new AudioBook("Yearbook", "Seth Rogen", 2021, 5, 17, 374, AudioRating.Thumbs_up),
        //    new AudioBook("My Sister the Serial Killer", "Oyinkan Braithwaite", 2018, 3, 28, 255, AudioRating.Thumbs_up),
        //    new AudioBook("My Name is Lucy Barton (Dramatic Production)", "Eliabeth Strout and Rona Munro", 2020, 4, 17, 86, AudioRating.Thumbs_down),
        //    new AudioBook("Creative Types", "Tom Bissell", 2021, 9, 13, 376, AudioRating.Thumbs_down),

        //    new Podcast("The Joe Rogan Experience", "Joe Rogan", 2023, 4, 12, 158, 8),
        //    new Podcast("The Daily", "Micahel Barbaro and Sabrina Tavernise", 2023, 12, 4, 28, 7),
        //    new Podcast("Stuff You Should Know", "Josh Clark and Wayne Bryant", 2023, 4, 16, 47, 6),
        //    new Podcast("WTF with Marc Maron", "Marc Maron", 2023, 9, 28, 75, 7),
        //    new Podcast("Radiolab", "WNYC", 2023, 5, 24, 48, 6),
        //};

        string path = "FinalProjectv1.csv";

        List<Sound> soundV2 = new List<Sound>();

        //Write(sound, path);

        Read(path, soundV2);

        if (!File.Exists(path))
        {
            Console.WriteLine("File does not exist!");
            return;
        }

        //for (int i = 0; i < soundV2.Count; i++)
        //{
        //    Console.WriteLine(soundV2[i]);
        //}


        string prompt = "Enter an option below!\n1. Print out all entries in the database\n2. Print out only the Tracks\n3. Print out only the Audio Books\n" +
            "4. Print out only the Podcasts\n5. Print out all entries with a given artist/author/creator\n6. Sort all entries by their rating in descending order\n" +
            "7. Sort all entries in ascending order based on their year\n8. Sort all entries by the lexicographical order of their title\n9. Print out all entries released on or after a given year\n" +
            "10. Allow the user to add a new entry to the database.\n11. Allow the user to remove an entry from the database.\n12. Quit";

        Console.WriteLine($"Hello User! Welcome to Owen's Music Database\n\n{prompt}");

        string? input = Console.ReadLine();

        while (!input.Equals("12"))
        {
            if (input.Equals("1"))
            {
                PrintAll(soundV2, IsSound, false, -1);
            }
            else if (input.Equals("2"))
            {
                PrintAll(soundV2, IsTrack, false, -1);
            }
            else if (input.Equals("3"))
            {
                PrintAll(soundV2, IsAudioBook, false, -1);
            }
            else if (input.Equals("4"))
            {
                PrintAll(soundV2, IsPodcast, false, -1);
            }
            else if (input.Equals("5"))
            {
                PrintAllByArtistAuthorOrCreator(soundV2);
            }
            else if (input.Equals("6"))
            {
                SortByTrack(soundV2);
                SortByAudioBook(soundV2);
                SortByPodcast(soundV2);
            }
            else if (input.Equals("7"))
            {
                soundV2.OrderBy(s => s.Date)
                    .ToList()
                    .ForEach(Console.WriteLine);
            }
            else if (input.Equals("8"))
            {
                soundV2.OrderBy(s => s.Title)
                    .ToList()
                    .ForEach(Console.WriteLine);
            }
            else if (input.Equals("9"))
            {
                Console.WriteLine("Type what year you want entries to be from or newer");
                string resp = Console.ReadLine();

                int newNum;

                try
                {
                    newNum = int.Parse(resp);
                    PrintAll(soundV2, IsSound, true, newNum);
                }
                catch
                {
                    Console.WriteLine("Invalid input, type in a number\n");
                }

            }
            else if (input.Equals("10"))
            {
                AddToFile(soundV2);
                Write(soundV2, path);
            }
            else if (input.Equals("11"))
            {
                RemoveFromFile(soundV2);
                Write(soundV2, path);
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number listed below.");
            }

            Console.WriteLine(prompt);
            input = Console.ReadLine();
        }
    }

    private static void PrintAll(List<Sound> sounds, Predicate<Sound> predicate, bool b, int year)
    {
        for (int i = 0; i < sounds.Count; i++)
        {
            if (predicate.Invoke(sounds[i]))
            {
                if (b)
                {
                    if (sounds[i].Year >= year)
                    {
                        Console.WriteLine(sounds[i]);
                    }
                }
                if (!b)
                {
                    Console.WriteLine(sounds[i]);
                }
                
            }
        }
    }

    private static bool IsSound(Sound s)
    {
        if (s is Sound)
        {
            return true;
        }
        return false;
    }

    private static bool IsTrack(Sound t)
    {
        if (t is Track)
        {
            return true;
        }
        return false;
    }

    private static bool IsAudioBook(Sound a)
    {
        if (a is AudioBook)
        {
            return true;
        }
        return false;
    }
    private static bool IsPodcast(Sound p)
    {
        if (p is Podcast)
        {
            return true;
        }
        return false;
    }

    private static void PrintAllByArtistAuthorOrCreator(List<Sound> arr)
    {
        string output = "";

        Console.WriteLine("Type what author you want look for:");
        string input = Console.ReadLine();

        for (int i = 0; i < arr.Count; i++)
        {
            if (arr[i] is Track)
            {
                Track t = (Track)arr[i];
                if (string.Equals(t.Artist, input, StringComparison.OrdinalIgnoreCase))
                {
                    output += t;
                }
            }
            else if (arr[i] is AudioBook)
            {
                AudioBook t = (AudioBook)arr[i];
                if (string.Equals(t.Author, input, StringComparison.OrdinalIgnoreCase))
                {
                    output += t;
                }
            }
            else if (arr[i] is Podcast)
            {
                Podcast t = (Podcast)arr[i];
                if (string.Equals(t.Creator, input, StringComparison.OrdinalIgnoreCase))
                {
                    output += t;
                }
            }
        }

        if (output.Equals(""))
        {
            output = $"There are no entries for {input}";
        }
        else
        {
            Console.WriteLine($"All of these entry(ies) were created by {input}:\n");
        }

        Console.WriteLine(output);
    }

    public static void SortByTrack(List<Sound> arr)
    {
        List<Track> sounds = new List<Track>();

        for (int i = 0; i < arr.Count; i++)
        {
            if(arr[i] is Track)
            {
                sounds.Add((Track)arr[i]);
            }
        }

        for (int i = 0; i < sounds.Count; i++)
        {
            int minIndex = i;

            for (int j = i + 1; j < sounds.Count; j++)
            {
                if (sounds[j].Rating.CompareTo(sounds[minIndex].Rating) > 0)
                {
                    minIndex = j;
                }
            }

            // tuple swap
            if (minIndex != i)
            {
                (sounds[minIndex], sounds[i]) = (sounds[i], sounds[minIndex]);
            }
        }

        Console.WriteLine($"\nSorting tracks by rating...\n{string.Join('\n', sounds)}");
    }

    public static void SortByAudioBook(List<Sound> arr)
    {
        List<AudioBook> sounds = new List<AudioBook>();

        for (int i = 0; i < arr.Count; i++)
        {
            if (arr[i] is AudioBook)
            {
                sounds.Add((AudioBook)arr[i]);
            }
        }

        for (int i = 0; i < sounds.Count; i++)
        {
            int minIndex = i;

            for (int j = i + 1; j < sounds.Count; j++)
            {
                if (sounds[j].Rating.CompareTo(sounds[minIndex].Rating) > 0)
                {
                    minIndex = j;
                }
            }

            // tuple swap
            if (minIndex != i)
            {
                (sounds[minIndex], sounds[i]) = (sounds[i], sounds[minIndex]);
            }
        }

        Console.WriteLine($"Sorting audio books by rating...\n{string.Join('\n', sounds)}");
    }

    public static void SortByPodcast(List<Sound> arr)
    {
        List<Podcast> sounds = new List<Podcast>();

        for (int i = 0; i < arr.Count; i++)
        {
            if (arr[i] is Podcast)
            {
                sounds.Add((Podcast)arr[i]);
            }
        }

        for (int i = 0; i < sounds.Count; i++)
        {
            int minIndex = i;

            for (int j = i + 1; j < sounds.Count; j++)
            {
                if (sounds[j].Rating.CompareTo(sounds[minIndex].Rating) > 0)
                {
                    minIndex = j;
                }
            }

            // tuple swap
            if (minIndex != i)
            {
                (sounds[minIndex], sounds[i]) = (sounds[i], sounds[minIndex]);
            }
        }

        Console.WriteLine($"Sorting podcasts by rating...\n{string.Join('\n', sounds)}");
    }


    public static Track AddTrackToFile()
    {
        string title;
        Console.WriteLine("Enter a title:");
        title = Console.ReadLine();
        int year;
        try
        {
            Console.WriteLine("Enter the year:");
            year = int.Parse(Console.ReadLine());
            if (year < 0 || year > 2023)
            {
                Console.WriteLine("Rating is out of range.");
                return null;
            }
        }
        catch
        {
            Console.WriteLine("Invalid input, not an integer");
            return null;
        }

        int month;
        try
        {
            Console.WriteLine("Enter the month:");
            month = int.Parse(Console.ReadLine());
            if (month < 1 || month > 12)
            {
                Console.WriteLine("Rating is out of range.");
                return null;
            }
        }
        catch
        {
            Console.WriteLine("Invalid input, not an integer");
            return null;
        }

        int day;
        try
        {
            Console.WriteLine("Enter the day:");
            day = int.Parse(Console.ReadLine());
            if (day < 1 || day > 32)
            {
                Console.WriteLine("Rating is out of range.");
                return null;
            }
        }
        catch
        {
            Console.WriteLine("Invalid input, not an integer");
            return null;
        }

        int duration;
        try
        {
            Console.WriteLine("Enter the duration of the file:");
            duration = int.Parse(Console.ReadLine());
        }
        catch
        {
            Console.WriteLine("Invalid input, not an integer");
            return null;
        }
        Console.WriteLine("Enter an artist");
        string? artist = Console.ReadLine();
        Console.WriteLine("Enter an album");
        string? album = Console.ReadLine();
        double rating;
        try
        {
            Console.WriteLine("Enter a rating (1.0-5.0)");
            rating = double.Parse(Console.ReadLine());
            if (rating < 0 || rating > 5)
            {
                Console.WriteLine("Rating is out of range.");
                return null;
            }
        }
        catch
        {
            Console.WriteLine("Invalid number...");
            return null;
        }

        return new Track(title, artist, album, year, month, day, duration, rating);
    }

    public static AudioBook AddAudioBookToFile()
    {
        string title;
        Console.WriteLine("Enter a title:");
        title = Console.ReadLine();
        int year;
        try
        {
            Console.WriteLine("Enter the year:");
            year = int.Parse(Console.ReadLine());
        }
        catch
        {
            Console.WriteLine("Invalid input, not an integer");
            return default;
        }

        int month;
        try
        {
            Console.WriteLine("Enter the month:");
            month = int.Parse(Console.ReadLine());
            if (month < 1 || month > 12)
            {
                Console.WriteLine("Rating is out of range.");
                return null;
            }
        }
        catch
        {
            Console.WriteLine("Invalid input, not an integer");
            return null;
        }

        int day;
        try
        {
            Console.WriteLine("Enter the day:");
            day = int.Parse(Console.ReadLine());
            if (day < 1 || day > 32)
            {
                Console.WriteLine("Rating is out of range.");
                return null;
            }
        }
        catch
        {
            Console.WriteLine("Invalid input, not an integer");
            return null;
        }

        int duration;
        try
        {
            Console.WriteLine("Enter the duration of the file:");
            duration = int.Parse(Console.ReadLine());
        }
        catch
        {
            Console.WriteLine("Invalid input, not an integer");
            return default;
        }
        //private string author
        Console.WriteLine("Enter an author");
        string? author = Console.ReadLine();
        AudioRating rating;
        Console.WriteLine("Enter a rating (thumbs up or thumbs down)");
        string r = Console.ReadLine();

        if (string.Equals(r, "thumbs up", StringComparison.OrdinalIgnoreCase))
        {
            rating = AudioRating.Thumbs_up;
        }
        else if (string.Equals(r, "thumbs down", StringComparison.OrdinalIgnoreCase))
        {
            rating = AudioRating.Thumbs_down;
        }
        else
        {
            Console.WriteLine("Invalid rating...");
            return null;
        }
        return new AudioBook(title, author, year, month, day, duration, rating);
    }

    public static Podcast AddPodcastToFile()
    {
        string title;
        Console.WriteLine("Enter a title:");
        title = Console.ReadLine();
        int year;
        try
        {
            Console.WriteLine("Enter the year:");
            year = int.Parse(Console.ReadLine());
        }
        catch
        {
            Console.WriteLine("Invalid input, not an integer");
            return default;
        }

        int month;
        try
        {
            Console.WriteLine("Enter the month:");
            month = int.Parse(Console.ReadLine());
            if (month < 1 || month > 12)
            {
                Console.WriteLine("Rating is out of range.");
                return null;
            }
        }
        catch
        {
            Console.WriteLine("Invalid input, not an integer");
            return null;
        }

        int day;
        try
        {
            Console.WriteLine("Enter the day:");
            day = int.Parse(Console.ReadLine());
            if (day < 1 || day > 32)
            {
                Console.WriteLine("Rating is out of range.");
                return null;
            }
        }
        catch
        {
            Console.WriteLine("Invalid input, not an integer");
            return null;
        }

        int duration;
        try
        {
            Console.WriteLine("Enter the duration of the file:");
            duration = int.Parse(Console.ReadLine());
        }
        catch
        {
            Console.WriteLine("Invalid input, not an integer");
            return default;
        }
        //private string author
        Console.WriteLine("Enter a creator");
        string? creator = Console.ReadLine();
        int rating;
        try
        {
            Console.WriteLine("Enter a rating (1-10)");
            rating = int.Parse(Console.ReadLine());
            if (rating < 0 || rating > 10)
            {
                Console.WriteLine("Rating is out of range.");
                return null;
            }
        }
        catch
        {
            Console.WriteLine("Invalid number...");
            return null;
        }

        return new Podcast(title, creator, year, month, day, duration, rating);
    }

    public static string AddToFile(List<Sound> arr)
    {

        Console.WriteLine("What type of audio do you want to make? (Track, Audiobook, or Podcast)");
        string? type = Console.ReadLine();

        if (!string.Equals(type, "Track", StringComparison.OrdinalIgnoreCase) &&
            !string.Equals(type, "Audiobook", StringComparison.OrdinalIgnoreCase) &&
            !string.Equals(type, "Podcast", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Invalid audio type...");
            return null;
        }
        else if (string.Equals(type, "Track", StringComparison.OrdinalIgnoreCase))
        {
            arr.Add(AddTrackToFile());
        }
        else if (string.Equals(type, "Audiobook", StringComparison.OrdinalIgnoreCase))
        {
            arr.Add(AddAudioBookToFile());
        }
        else if (string.Equals(type, "Podcast", StringComparison.OrdinalIgnoreCase))
        {
            arr.Add(AddPodcastToFile());
        }

        return null;
    }

    public static void RemoveFromFile(List<Sound> arr)
    {
        string removed = "";

        Console.WriteLine("Type the title of the audio you want to remove");
        string? type = Console.ReadLine();

        for (int i = 0; i<arr.Count; i++)
        {
            if (string.Equals(type, arr[i].Title, StringComparison.OrdinalIgnoreCase))
            {
                removed = "Removing title...";
                Console.WriteLine(removed);

                arr.Remove(arr[i]);
            }
        }

        if (removed.Equals(""))
        {
            Console.WriteLine("The title you searched is not in the database...sorry.");
        }
    }
}