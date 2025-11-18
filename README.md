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
`FinalProject.cs` - main program + menu
`Sound.cs` - base class
`Track.cs` - Track subclass
`Audiobook.cs` - Audiobook Subclass
`Podcast.cs` - Podcast Subclass
`AudioRating.cs` - enum for audiobook rating
`FinalProjectv1.csv` - data file
`READEME.md` - this file

## Class Overview
### Sound (Abstract Base Class)
Shared Properties:
- Title
- Data
- Year, Month, Day
- Duration
- Abstract: Rating
- Implements IComparable
