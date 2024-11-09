using HotelsManager.Engine.Models.Entities;

namespace HotelsManager.Engine;

public interface IHotelRepository
{
    void AddHotels(Hotel[] hotels);
    void AddBookings(Booking[] bookings);
    int GetRoomsQuantity(string hotelId, string roomType);
    IEnumerable<Booking> GetBookings(string hotelId, string roomType);
}

public class HotelRepository : IHotelRepository
{
    private readonly List<Hotel> _hotels = [];
    private readonly List<Booking> _bookings = [];

    public void AddHotels(Hotel[] hotels)
    {
        _hotels.AddRange(hotels);
    }

    public void AddBookings(Booking[] bookings)
    {
        _bookings.AddRange(bookings);
    }

    public int GetRoomsQuantity(string hotelId, string roomType)
    {
        var hotel = _hotels.FirstOrDefault(x => x.Id == hotelId);

        if (hotel is not null)
        {
            var rooms = hotel.Rooms.Count(x => x.RoomType == roomType);

            return rooms;
        }
        
        return 0;
    }

    public IEnumerable<Booking> GetBookings(string hotelId, string roomType)
    {
        return _bookings.Where(x => x.HotelId == hotelId && x.RoomType == roomType);
    }
}