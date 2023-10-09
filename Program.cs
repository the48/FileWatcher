using System;
using System.IO;

// Path & EnableRaisingEvents = TRUE
namespace FileWatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            FileSystemWatcher watchDog = new FileSystemWatcher(@"C:\tmp\watchdog");

            watchDog.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.FileName;

            watchDog.Created += OnCreated;
            watchDog.Filter = "*.*";

            watchDog.EnableRaisingEvents = true;

            while (Console.Read() != 'q') ;
        }

        
        public static void OnCreated(object sender, FileSystemEventArgs events)
        { 
            // OnCreated Event Logic
            Console.WriteLine($"Created : {events.Name}");
        }
    }
}
