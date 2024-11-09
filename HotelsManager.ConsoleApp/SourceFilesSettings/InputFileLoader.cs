using HotelsManager.Engine.Models;
using HotelsManager.Engine.Models.Entities;

namespace AccessGroupCodeChallenge.SourceFilesSettings;

public static class InputFileLoader
{
    private static string? GetSolutionFolderPath
    {
        get
        {
            var solutionFolderPath = Directory.GetCurrentDirectory();
            var parent = Directory.GetParent(solutionFolderPath)?.Parent?.Parent?.Parent?.FullName;
            
            return parent;
        }
    }
    
    public static Booking[] LoadBookings(string filePath)
    {
        var bookings = Load<Booking>(filePath);
        
        return bookings;
    }
    
    public static Hotel[] LoadHotels(string filePath)
    {
        var hotels = Load<Hotel>(filePath);
        
        return hotels;
    }
    
    private static T[] Load<T> (string filePath)
    {
        var parent = "."; // for RELEASE mode
        
        #if DEBUG
                parent = GetSolutionFolderPath;
        #endif
        
        
        var json = File.ReadAllText(Path.Combine(Path.Combine($"{parent}/InputFiles", filePath)));
        var items = Newtonsoft.Json.JsonConvert.DeserializeObject<T[]>(json, [new CustomDateTimeConverter()]) ?? [];
        
        return items;
    }
}