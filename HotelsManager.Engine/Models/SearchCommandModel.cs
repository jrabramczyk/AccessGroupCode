namespace HotelsManager.Engine.Models;

public record SearchCommandModel(DateTime Arrival, DateTime Departure, int AvailableRoomsQuantity)
{
    public override string ToString()
    {
        return Arrival.Date == Departure.Date ? 
            $"({Arrival:yyyyMMdd}, {AvailableRoomsQuantity})" 
            : $"({Arrival:yyyyMMdd}-{Departure:yyyyMMdd}, {AvailableRoomsQuantity})";
    }
}