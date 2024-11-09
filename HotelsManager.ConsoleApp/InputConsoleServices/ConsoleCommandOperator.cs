using System.Globalization;
using System.Text.RegularExpressions;
using HotelsManager.Engine;

namespace AccessGroupCodeChallenge.InputConsoleServices;

public static class ConsoleCommandOperator
{
    //Availability(H1, 20240901, SGL)
    //Availability(H1, 20240901-20240903, DBL)
    private const string AvailabilityCommandPattern = @"Availability\((\w+), (\d+)(?:-(\d+))?, (\w+)\)";
    
    //Search(H1, 365, SGL)
    private const string SearchCommandPattern = @"Search\((\w+), (\d+), (\w+)\)";

    public static void Start(IBookingEngine bookingEngine)
    {
        while (true)
        {
            Console.WriteLine("Please enter a command:");
            var command = Console.ReadLine();

            if (string.IsNullOrEmpty(command))
            {
                break;
            }
            else if (command.Contains(InputArgs.AvailabilityCommand))
            {
                ProcessAvailabilityCommand(bookingEngine, command);
            }
            else if (command.Contains(InputArgs.SearchCommand))
            {
                ProcessSearchCommand(bookingEngine, command);
            }
            else
            {
                Console.WriteLine("Command is not recognized. Please try again.");
            }
        }
    }

    private static void ProcessSearchCommand(IBookingEngine bookingEngine, string command)
    {
        var match = Regex.Match(command, SearchCommandPattern);

        var hotelName = match.Groups[1].Value;
        var daysToLookAhead = int.Parse(match.Groups[2].Value);
        var roomType = match.Groups[3].Value;
                
        //TODO: Add validation in case of invalid input
        
        var availabilities = bookingEngine.Search(hotelName, daysToLookAhead, roomType);
        
        Console.WriteLine(string.Join(',', availabilities));
    }

    private static void ProcessAvailabilityCommand(IBookingEngine bookingEngine, string command)
    {
        var match = Regex.Match(command, AvailabilityCommandPattern);

        var hotelId = match.Groups[1].Value;
        var startDateString = match.Groups[2].Value;
        var endDateString = match.Groups[3].Value;
        var roomType = match.Groups[4].Value;
        
        //TODO: Add validation in case of invalid input
        
        //if endDateString is not provided, then use startDate as endDate
        var endDate = DateTime.ParseExact( string.IsNullOrEmpty(endDateString) ? startDateString : endDateString, "yyyyMMdd", CultureInfo.InvariantCulture);
        var startDate = DateTime.ParseExact(startDateString, "yyyyMMdd", CultureInfo.InvariantCulture);
        
        var availableRoomsCount = bookingEngine.CheckAvailability(hotelId, roomType, startDate, endDate);
        
        Console.WriteLine($"Available rooms: {availableRoomsCount}");
    }
}