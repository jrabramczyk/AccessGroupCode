namespace AccessGroupCodeChallenge.InputConsoleServices;

public static class InputParser
{
    public static (string hotelsFilePath, string bookingsFilePath) TryParseArgs(string[] args)
    {
        // Check if the number of arguments is correct
        if (args.Length != 4)
        {
            throw new ArgumentException(
                "Invalid number of arguments. Example usage: HotelsManager.ConsoleApp --hotels Hotels.json --bookings Bookings.json");
        }
        
        //TODO: Add more validation of input args, different order, wrong naming etc.
        // Please forgive me for not adding more validation, It's super boring and I believe not the purpose of this challenge.

        var nameValueDictionary = new Dictionary<string, string>();

        for (int i = 0; i < args.Length; i += 2)
        {
            nameValueDictionary.Add(args[i], args[i + 1]);
        }
        
        return (nameValueDictionary[InputArgs.Hotels], nameValueDictionary[InputArgs.Bookings]);
    }
}