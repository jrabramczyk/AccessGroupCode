using FluentAssertions;
using HotelsManager.Engine.Models.Entities;

namespace HotelsManager.Engine.Tests;

[TestFixture]
public class HotelRepositoryTests
{
    private HotelRepository _sut;

    [SetUp]
    public void Setup()
    {
        _sut = new HotelRepository();
    }

    [Test]
    public void GetBookings_ReturnsAllBookingsOfGivenType()
    {
        //arrange
        var expectingHotelName = "H1";
        var expectingRoomType = "SGL";
        
        var bookingSet1 = CreateBookingSet(expectingHotelName, expectingRoomType, 10);
        var bookingSet2 = CreateBookingSet(expectingHotelName, expectingRoomType, 10);
        var invalidBookingSet1 = CreateBookingSet("WRONG_HOTEL_NAME", expectingRoomType, 10);
        var invalidBookingSet2 = CreateBookingSet(expectingHotelName, "WRONG_ROOM_TYPE", 10);
        
        _sut.AddBookings(bookingSet1.Concat(bookingSet2).Concat(invalidBookingSet1).Concat(invalidBookingSet2).ToArray());
        
        //act
        var result = _sut.GetBookings(expectingHotelName, expectingRoomType);

        //assert
        result.Should().HaveCount( 10 + 10, "Because only sets 'bookingSet1' and 'bookingSet2' of bookings should be returned");
    }
    
    [TestCase("H1", "SGL")]
    public void GetBookings_WhenNoRoom_ShouldReturnEmptyCollection(string hotelName, string roomType)
    {
        //arrange
        var bookingSet = CreateBookingSet(hotelName, roomType, 10);
        
        _sut.AddBookings(bookingSet);
        
        //act
        var result = _sut.GetBookings("NOT_EXISTING_HOTEL_NAME", roomType);
        var result2 = _sut.GetBookings(hotelName, "NOT_EXISTING_ROOM_TYPE");

        //assert
        result.Should().HaveCount(0);
        result2.Should().HaveCount(0);
    }
    
    private Booking[] CreateBookingSet(string hotelId, string roomType, int count)
    {
        return Enumerable.Range(0, count).Select(x => new Booking
        {
            HotelId = hotelId,
            RoomType = roomType
        }).ToArray();
    }
}