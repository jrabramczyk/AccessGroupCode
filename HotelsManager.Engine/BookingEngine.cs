using HotelsManager.Engine.Models;

namespace HotelsManager.Engine;

public interface IBookingEngine
{
    int CheckAvailability(string hotelId, string roomType, DateTime startDate, DateTime endDate );
    List<SearchCommandModel> Search(string hotelId, int daysToLookAhead, string roomType);
}

public class BookingEngine(IHotelRepository hotelRepository) : IBookingEngine
{
    public int CheckAvailability(string hotelId, string roomType, DateTime startDate, DateTime endDate)
    {
        var allRoomsCount = hotelRepository.GetRoomsQuantity(hotelId, roomType);
        var bookings = hotelRepository.GetBookings(hotelId, roomType);

        var sumOfBookingsAtGivenDate = 0;
        
        foreach (var booking in bookings)
        {
            if (startDate >= booking.Arrival && startDate <= booking.Departure)
            {
                sumOfBookingsAtGivenDate++;
            }
            else if (endDate >= booking.Arrival && endDate <= booking.Departure)
            {
                sumOfBookingsAtGivenDate++;
            }
            else if (startDate <= booking.Arrival && endDate >= booking.Departure)
            {
                sumOfBookingsAtGivenDate++;
            }
        }
        
        return allRoomsCount - sumOfBookingsAtGivenDate;
    }

    public List<SearchCommandModel> Search(string hotelId, int daysToLookAhead, string roomType)
    {
        var bookingsDict = GetBookingsQuantityPerEachDay(hotelId, roomType);
        var allRoomsCount = hotelRepository.GetRoomsQuantity(hotelId, roomType);

        // var startDate = DateTime.Today;
        var startDate = new DateTime(2024, 11, 1);
        var currentDate = startDate;
        
        // var occupiedRooms = bookingsDictionary.GetValueOrDefault(startDate.ToString("yyyyMMdd"), 0);
        var occupiedRooms = bookingsDict.GetValueOrDefault(startDate, 0);
        var availableRooms = allRoomsCount - occupiedRooms;
                             
        var result = new List<SearchCommandModel>();
        
        for (var i = 1; i <= daysToLookAhead; i++)
        {
            currentDate = currentDate.AddDays(1); //go to next day
            // var currentlyOccupiedRooms = bookingsDictionary.GetValueOrDefault(currentDate.ToString("yyyyMMdd"), 0);
            var currentlyOccupiedRooms = bookingsDict.GetValueOrDefault(currentDate, 0);

            // if the number of occupied rooms has changed then close this period of available rooms for given date range, and start a new one
            // or if it's the last day of the period then close the period
            if (currentlyOccupiedRooms != occupiedRooms || i == daysToLookAhead)
            {
                // don't show periods with no available rooms
                if (availableRooms > 0)
                {
                    result.Add(new SearchCommandModel(startDate, currentDate.AddDays(-1), availableRooms));
                }
                    
                startDate = currentDate;
                
                occupiedRooms = currentlyOccupiedRooms;
                availableRooms = allRoomsCount - occupiedRooms;
            }
        }

        return result;
    }

    private Dictionary<DateTime, int> GetBookingsQuantityPerEachDay(string hotelId, string roomType)
    {
        var bookingsDict = new Dictionary<DateTime, int>();
        var bookings = hotelRepository.GetBookings(hotelId, roomType);

        foreach (var currentBooking in bookings)
        {
            //add booking to dictionary per each day
            var daysOfStay = (currentBooking.Departure - currentBooking.Arrival).Days;
            for (var i = 0; i <= daysOfStay; i++)
            {
                var currentDate = currentBooking.Arrival.AddDays(i);
                // var currentDateAsString = currentDate.ToString("yyyyMMdd");
                // var currentDateAsString = currentDate;
                
                if (bookingsDict.ContainsKey(currentDate))
                {
                    bookingsDict[currentDate]++;
                }
                else
                {
                    bookingsDict.Add(currentDate, 1);
                }
            }
        }
        
        return bookingsDict;
    }
}