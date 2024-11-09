using Autofac;

namespace HotelsManager.Engine;

public static class DependencyRegistrar
{
    public static void AddHotelsManagerEngine(this ContainerBuilder services)
    {
        services.RegisterType<BookingEngine>().As<IBookingEngine>();
        services.RegisterType<HotelRepository>().As<IHotelRepository>().SingleInstance();
    }
}