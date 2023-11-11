using System;
using System.IO;

// add Dispatcher
// dispose instance

namespace FileWatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            WatchDog monitor = new WatchDog(@"C:\tmp\watchdog\");


            while (Console.ReadLine() != "lol")
            {
                Console.ReadLine();
            }
            //     Dispatcher changeDispatcher = null;
            //     ManualResetEvent dispatcherStarted = new ManualResetEvent(false);
            //     Action changeThreadHandler = () => {
            //         changeDispatcher = Dispatcher.CurrentDispatcher;
            //         changeDispatcherStarted.Set();
            //         Dispatcher.Run();
            //     };

            //     new Thread(() => changeThreadHandler()) { IsBackground = true }.Start();
            //     changeDispatcherStarted.WaitOne();

            //     FileSystemWatcher watchDog = new FileSystemWatcher(@"C:\tmp\watchdog");

            //     watchDog.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.FileName;

            //     watchDog.Created += (sender, e) => changeDispatcher.BeginInvoke(new Action(() => OnCreated(sender, e)));
            //     watchDog.Filter = "*.*";

            //     // buffer in bytes
            //     watchDog.InternalBufferSize = 1024 * 32
            //     watchDog.EnableRaisingEvents = true;

            //     while (Console.Read() != 'q') ;

            //     watchDog.Dispose();
            //     changeDispatcher.BeginInvokeShutdown(DispatcherPriority.Normal);
            // }


            // public static void OnCreated(object sender, FileSystemEventArgs events)
            // { 
            //     // OnCreated Event Logic
            //     Console.WriteLine($"Created : {events.Name}");
        }
    }
}