using AccessGroupCodeChallenge.InputConsoleServices;
using AccessGroupCodeChallenge.SourceFilesSettings;
using Autofac;
using HotelsManager.Engine;

namespace AccessGroupCodeChallenge;

class Program
{
    static void Main(string[] args)
    {
        var container = DependencyRegistrar.RegisterContainer();
        var bookingEngine = container.Resolve<IBookingEngine>();
        
        LoadSourceFilesIntoStorage(args, container);

        ConsoleCommandOperator.Start(bookingEngine);
    }

    private static void LoadSourceFilesIntoStorage(string[] args, IContainer container)
    {
        // read Hotels.json and Booking.json file paths from input args
        var (hotelsFilePath, bookingsFilePath) = InputParser.TryParseArgs(args);
        
        // serialize hotels and bookings from json files into collections
        var hotels = InputFileLoader.LoadHotels(hotelsFilePath);
        var bookings = InputFileLoader.LoadBookings(bookingsFilePath);
        
        var hotelRepository = container.Resolve<IHotelRepository>();
        hotelRepository.AddHotels(hotels);
        hotelRepository.AddBookings(bookings);
    }
}