using System;
using System.IO;
using System.Timers;
using System.Threading;
using System.Collections.Generic;

namespace FileWatcher
{
    public class WatchDog : IDisposable
    {
        private List<string> inputXML;
        private ReaderWriterLockSlim rwLock;
        private System.Timers.Timer timer;
        private readonly string watchPath;
        private FileSystemWatcher watchDog;


        public FileSystemMonitor(string inputPath)
        {
            inputXML = new List<string>();

            rwLock = new ReaderWriterLockSlim();

            this.watchPath = inputPath;
            RunFileSystemWatcher();
        }

        private void RunFileSystemWatcher()
        {
            watchDog = new FileSystemWatcher();
            watchDog.Filter = "*.*";
            watchDog.Created += watchDog_OnCreated; // call on created
            watchDog.Path = watchPath;
            watchDog.InternalBufferSize = 1024 * 1024 * 10; // 10 mb fail threshold
            watchDog.EnableRaisingEvents = true;
        }

        // Expect the exception, recreate
        private void watchDog_Died(object sender, ErrorEventArgs error)
        {
            // to log error
            RunFileSystemWatcher();
        }

        private void watchDog_OnCreated(object sender, FileSystemEventArgs events)
        {
            try
            {
                rwLock.EnterWriteLock();
                inputXML.Add(events.FullPath);

                if (timer == null)
                {
                    timer = new System.Timers.Timer(2048);
                    timer.Elapsed += ProcessQueue;
                    timer.Start();
                }
                else
                {
                    timer.Stop();
                    timer.Start();
                }
            }

            finally
            {
                rwLock.ExitWriteLock();
            }
        }

        void ProcessQueue(object sender, ElapsedEventArgs args)
        {
            try
            {
                Console.WriteLine($"[-] Queue Initiated with {inputXML.Count} files");
                rwLock.EnterReadLock();

                foreach (string xml in inputXML)
                {
                    Console.WriteLine(xml);
                }
                inputXML.Clear();
            }
            finally
            {
                if (timer != null)
                {
                    // rwLock.ExitWriteLock();
                    timer.Stop();
                    timer.Dispose();
                    timer = null;
                    // OnFileListCreated(new FileListeEventArgs { FileList = inputXML}); //hmm
                    // inputXML.Clear();
                }
                rwLock.ExitReadLock();
            }
        }

        protected virtual void Dispose(bool dispose)
        {
            if (dispose)
            {
                if (rwLock != null)
                {
                    rwLock.Dispose();
                    rwLock = null;
                }

                if (watchDog != null)
                {
                    watchDog.EnableRaisingEvents = false;
                    watchDog.Dispose();
                    watchDog = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}