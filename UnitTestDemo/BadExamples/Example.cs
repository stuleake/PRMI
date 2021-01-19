using System;
using System.IO;

namespace UnitTestDemo.BadExamples
{
    public class Example
    {
        #region Inversion Of Control
        // Bad - how can I even test this ???  Set the system date time >>>>
        public void DoSomeThingBasedOnDate()
        {
            var date = DateTime.Now;

            if (date == date.AddMinutes(200))
            {
                // do something
            }
            else
            {
                // do something else
            }
        }

        // Good (Inversion Of Control IOC)
        public void DoSomeThingBasedOnDate(DateTime date)
        {
            if (date == date.AddMinutes(200))
            {
                // do something
            }
            else
            {
                // do something else
            }
        }
        #endregion

        #region Static Classes and Methods

        // Bad - this can't be mocked
        public static class FileHandler
        {
            public static string GetFileContents(string fileName)
            {
                return !File.Exists(fileName) ? string.Empty : File.ReadAllText(fileName);
            }
        }
        
        // Good - this can be mocked
        public class FileHandlerWrapper 
        {
            public string GetFileContents(string fileName)
            {
                return FileHandler.GetFileContents(fileName);
            }
        }
        #endregion

        #region Singletons

        public class Singleton
        {
            private static Singleton _instance;
            protected Singleton() { }

            public static Singleton Instance()
            {
                return _instance ??= new Singleton();
            }

            public void WipeDatabase() { }
            public void BackupDatabase() { }
        }

        // Bad - Not the singleton pattern, it's just the way it's used here.
        // We want to mock the actions here -> not actually carry them out !
        public class DataHandlerBad
        {
            private readonly Singleton singleton = Singleton.Instance();
            public void DoAction(DateTime dateTime)
            {
                if (dateTime == DateTime.Now)
                {
                    singleton.BackupDatabase();
                }
                else
                {
                    singleton.WipeDatabase();
                }
            }
        }

        // Good - Higher Order function (nothing to do with god)
        // You can still use the Singleton pattern and we can mock the actions so that we don't do anything bad
        public class DataHandlerGood
        {
            public void DoAction(DateTime dateTime, Action backupDatabase, Action wipeDatabase)
            {
                if (dateTime == DateTime.Now)
                {
                    backupDatabase();
                }
                else
                {
                    wipeDatabase();
                }
            }
        }
        #endregion
    }
}
