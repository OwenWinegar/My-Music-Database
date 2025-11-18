# My Music Database
A console-based music/media database that loads, displays, filters, sorts, and manages Tracks, AudioBooks, and Podcasts.

## Overview
This application reads entries from a CSV file and converts each line into an object of type:
- Track
- AudioBook
- Podcast
Each of these inherits from the abstract base class Sound.

Users can interact with the program through a menu system, print media lists, sort items by rating or year, filter by properties (artist, author, or creator), and add or remove entries.

## Features
Data Handling:
- Reads media data from a CSV file
- Converts CSV lines into objects
- Writes updated data back to the CSV
- Input validation for all fields
- Handles multiple rating types: double, enum, and int

Menu functionality:
1. Display all media
2. Display only tracks
3. Display only audiobooks
4. Display only podcasts
5. Filter by artist/author/creator
6. Sort by rating
7. Sort by year
8. Sort by title
9. Filter entries by year
10. Add a new entry
11. Remove an entry
12. Quit the program

## Project File Structure
- `FinalProject.cs` - main program + menu
- `Sound.cs` - base class
- `Track.cs` - Track subclass
- `Audiobook.cs` - Audiobook Subclass
- `Podcast.cs` - Podcast Subclass
- `AudioRating.cs` - enum for audiobook rating
- `FinalProjectv1.csv` - data file
- `READEME.md` - this file

## Class Overview
### Sound (Abstract Base Class)
Shared Properties:
- Title
- Data
- Year, Month, Day
- Duration
- Abstract: Rating
- Implements IComparable

### Track
#### Adds:
- Artist
- Album
- Rating (double, 0-5)
#### Comparison:
- Sorts by rating (descending)

### AudioBook
#### Adds:
- Author
- Rating (enum AudioRating { Thumbs_up, Thumbs_down })
#### Comparison:
- Sorts by rating (Thumbs_up > Thumbs_down)

### Podcast
#### Adds:
- Creator
- Rating (int 1-10)
#### Comparison:
- Sorts numerically

## CSV Format
### Each row contains:
Type,Title,Artist/Author/Creator,Album (or N/A),Year,Month,Day,Duration,Rating
### Example Entries:
Track,Bound 2,Kanye West,Yeezus,2013,5,2,4,4.8
AudioBook,Exit West,Mohsin Hamid,N/A,2017,4,15,282,Thumbs_up
Podcast,The Daily,Michael Barbaro,N/A,2023,12,4,28,7

## Reading from CSV
Program:
- Opens the file
- Skips the header
- Splits each line by commas
- Determines media type
- Constructs the appropriate Sound child object
- Adds each entry to a List<Sound>
All invalid formatting is rejected through property validation

## Writing to CSV
The Write() method:
- Overwrites the existing file
- Rewrites the header
- Loops through all Sound objects
- Formats each line according to its media type
- Outputs the updated list in a consistent, clean structure

## Sorting Algorithms
The project uses selection sort, manually implemented, for:
- Sort by rating (Track, AudioBook, Podcast -- each separately)
- Sort by year
- Sort by title
Because each media type stores rating differently, the sort uses the overriden CompareTo() implementations inside each class

## How to Run the Program
1. Open the projeect folder in Visual Studio or VS Code
2. Ensure FinalProjectv1.csv is in the project directory
3. Build and run the program
4. Use the menu system to interact with the media database
