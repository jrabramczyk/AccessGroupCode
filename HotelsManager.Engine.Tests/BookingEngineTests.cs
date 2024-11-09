using FluentAssertions;
using HotelsManager.Engine.Models.Entities;
using Moq;

namespace HotelsManager.Engine.Tests;

[TestFixture]
public class BookingEngineTests
{
    private BookingEngine _sut;
    private Mock<IHotelRepository> _hotelRepository;

    [SetUp]
    public void SetUp()
    {
        _hotelRepository = new Mock<IHotelRepository>();
        
        _sut = new BookingEngine(_hotelRepository.Object);
    }
    
    
    [TestCase("2024-11-01", "2024-11-03")]
    [TestCase("2024-11-01", "2024-11-01")]
    [TestCase("2024-11-02", "2024-11-02")]
    [TestCase("2024-11-02", "2024-11-12")]
    public void CheckAvailability_WhenNoBookings_ReturnsAllRooms(DateTime startDate, DateTime endDate)
    {
        //arrange
        _hotelRepository.Setup(x => x.GetRoomsQuantity(It.IsAny<string>(), It.IsAny<string>())).Returns(10);
        _hotelRepository.Setup(x => x.GetBookings(It.IsAny<string>(), It.IsAny<string>())).Returns(Array.Empty<Booking>());
        
        
        //act
        var result = _sut.CheckAvailability("IrrelevantName_Using_IsAny()", "IrrelevantName_Using_IsAny()", startDate, endDate);
        
        //assert
        result.Should().Be(10);
    }
    
    [TestCase("2024-11-01", "2024-11-03", "2024-11-03", "2024-11-05", 9)]
    [TestCase("2024-11-01", "2024-11-02", "2024-11-03", "2024-11-05", 10)]
    [TestCase("2024-11-01", "2024-11-06", "2024-11-03", "2024-11-05", 9)]
    [TestCase("2024-11-05", "2024-11-08", "2024-11-03", "2024-11-05", 9)]
    [TestCase("2024-11-06", "2024-11-08", "2024-11-03", "2024-11-05", 10)]
    public void CheckAvailability_WhenBookingsExist_ReturnsCorrectValue(DateTime startDate, DateTime endDate, 
        DateTime bookingStartDate, DateTime bookingEndDate, int expectingAvailableRooms)
    {
        //arrange
        const int roomsCount = 10;
        var bookings = new[]
        {
            new Booking { Arrival = bookingStartDate, Departure = bookingEndDate },
        };
        
        _hotelRepository.Setup(x => x.GetRoomsQuantity(It.IsAny<string>(), It.IsAny<string>())).Returns(roomsCount);
        _hotelRepository.Setup(x => x.GetBookings(It.IsAny<string>(), It.IsAny<string>())).Returns(bookings);
        
        
        //act
        var result = _sut.CheckAvailability("IrrelevantName_Using_IsAny()", "IrrelevantName_Using_IsAny()", startDate, endDate);
        
        //assert
        result.Should().Be(expectingAvailableRooms);
    }
    
    //It's the same test as above but with multiple (3) bookings at same date range
    [TestCase("2024-11-01", "2024-11-03", "2024-11-03", "2024-11-05", 7)]
    [TestCase("2024-11-01", "2024-11-02", "2024-11-03", "2024-11-05", 10)]
    [TestCase("2024-11-01", "2024-11-06", "2024-11-03", "2024-11-05", 7)]
    [TestCase("2024-11-05", "2024-11-08", "2024-11-03", "2024-11-05", 7)]
    [TestCase("2024-11-06", "2024-11-08", "2024-11-03", "2024-11-05", 10)]
    public void CheckAvailability_WhenMultipleBookingsExist_ReturnsCorrectValue(DateTime startDate, DateTime endDate, 
        DateTime bookingStartDate, DateTime bookingEndDate, int expectingAvailableRooms)
    {
        //arrange
        const int roomsCount = 10;
        var bookings = new[]
        {
            new Booking { Arrival = bookingStartDate, Departure = bookingEndDate },
            new Booking { Arrival = bookingStartDate, Departure = bookingEndDate },
            new Booking { Arrival = bookingStartDate, Departure = bookingEndDate },
        };
        
        _hotelRepository.Setup(x => x.GetRoomsQuantity(It.IsAny<string>(), It.IsAny<string>())).Returns(roomsCount);
        _hotelRepository.Setup(x => x.GetBookings(It.IsAny<string>(), It.IsAny<string>())).Returns(bookings);
        
        
        //act
        var result = _sut.CheckAvailability("IrrelevantName_Using_IsAny()", "IrrelevantName_Using_IsAny()", startDate, endDate);
        
        //assert
        result.Should().Be(expectingAvailableRooms);
    }
}