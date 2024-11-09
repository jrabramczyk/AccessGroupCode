# Booking Engine Library

This library is a .NET Standard 2.0 library that provides a booking engine for hotels. It is used by the HotelsManager.ConsoleApp project.

## How to start

- Run the program by executing **HotelsManager.ConsoleApp.exe** with arguments **"--hotels Hotels.json --bookings Bookings.json"**
  > HotelsManager.ConsoleApp.exe --hotels Hotels.json --bookings Bookings.json
 
- Important: The **Hotels.json** and **Bookings.json** files must be inside folder called 'InputFiles' in the same folder as the executables.

## Usage of library with other apps

If you want to use the booking engine library from other projects please just add it to your dependency registrator by

```
containerBuilder.AddHotelsManagerEngine();
```