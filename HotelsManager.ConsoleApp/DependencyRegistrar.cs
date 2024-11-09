using Autofac;
using HotelsManager.Engine;

namespace AccessGroupCodeChallenge;

public static class DependencyRegistrar
{
    public static IContainer RegisterContainer()
    {
        var builder = new ContainerBuilder();
        
        //register HotelManager.Engine library
        builder.AddHotelsManagerEngine();

        var container = builder.Build();
        
        return container;
    }
}