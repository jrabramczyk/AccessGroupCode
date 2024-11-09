namespace HotelsManager.Engine.Models.Entities;

public class Hotel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public RoomType[] RoomTypes { get; set; }
    public Room[] Rooms { get; set; }
}