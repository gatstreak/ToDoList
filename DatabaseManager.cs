using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace ToDoList
{
    
        public static class DatabaseManager
        {
            public static SQLiteConnection conn;
            public static string databasePath;
            public static string databaseName;

            static DatabaseManager()
            {//Set the DB connection string
                databaseName = "ToDoDB.sqlite";
            // assets folder
                databasePath = Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, databaseName); 
                conn = new SQLiteConnection(databasePath);

            if (databasePath != null)
            {
                conn = new SQLiteConnection(databasePath);
            }
            else
            {
                //create a new instance 
                conn.CreateTable<tblToDo>();
            }
            }

            public static List<tblToDo> ViewAll()
            {
            try
            {
                //  return conn.Query<tblToDo>("select * from tblToDo");
                return conn.Table<tblToDo>().ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error:" + e.Message);
                //making some fake items to stop the system from crashing when the DB doesn't connect
                List<tblToDo> fakeitem = new List<tblToDo>();
                //make a single item
                tblToDo item = new tblToDo
                {

                    ListID = 100,
                    Date = DateTime.Now.Date,
                    Details = "There are no items",
                    Title = "No Items",

                };
                fakeitem.AddRange(new[] { item }); //add it to the fake item list
                return fakeitem;
            }
                
            }

            public static void AddItem(string title, string details)
            {
                try
                {
                    var addThis = new tblToDo() { Title = title, Details = details, Date = DateTime.Now.Date };
                    conn.Insert(addThis);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Add Error:" + e.Message);
                }
            }

            public static void EditItem(string title, string details, int listid)
            {
                try
                {
                    // http://stackoverflow.com/questions/14007891/how-are-sqlite-records-updated 
                    var EditThis = new tblToDo() { Title = title, Details = details, ListID = listid };
                    conn.Update(EditThis);
                    //or this
                    //db.Execute("UPDATE tblToDoList Set Title = ?, Details =, WHERE ID = ?", title, details, listid);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Update Error:" + e.Message);
                }
            }

            public static void DeleteItem(int listid)
            {
                // https://developer.xamarin.com/guides/cross-platform/application_fundamentals/data/part_3_using_sqlite_orm/ 
                try
                {
                    conn.Delete<tblToDo>(listid);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Delete Error:" + ex.Message);
                }
            }
        }
    }
