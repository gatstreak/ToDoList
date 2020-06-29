using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.Collections.Generic;
using System;
using Android.Content;

namespace ToDoList
{
    [Activity(Label = "The Awesome to Do List", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        ListView ListToDoList;
            List<tblToDo> myList = new List<tblToDo>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

           
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            myList.AddRange(DatabaseManager.ViewAll());
            ListToDoList = FindViewById<ListView>(Resource.Id.listView1);
            ListToDoList.Adapter = new DataAdapter(this, myList);
            ListToDoList.ItemClick += onListToDoList_click;

             Xamarin.Essentials.Platform.Init(this, savedInstanceState);

        }

        private void onListToDoList_click(object sender, AdapterView.ItemClickEventArgs e)
        {
            var Position = e.Position; // get the number of the list 
            var ToDoItem = myList[Position]; // get the data from the list at the position 

            var edititem = new Intent(this, typeof(EditItem);
            edititem.PutExtra("Title", ToDoItem.Title);
            edititem.PutExtra("details", ToDoItem.Details);
            edititem.PutExtra("ListID", ToDoItem.ListID);

            StartActivity(edititem);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}