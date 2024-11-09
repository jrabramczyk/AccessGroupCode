namespace HotelsManager.Engine.Models.Entities;

public class RoomType
{
    public string Code { get; set; }
    public string Description { get; set; }
    public string[] Amenities { get; set; }
    public string[] Features { get; set; }
}