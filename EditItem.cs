using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ToDoList
{
    [Activity(Label = "Edit an item")]
    public class EditItem : Activity
    {
  // comes in from main activity 
            int ListId;
            string Title;
            string Details;

            // binds to layout 
            TextView txtTitle;
            TextView txtDetails;
            Button btnEdit;
            Button btnDelete;

        /// <summary>
        /// The on create.
        /// </summary>
        /// <param name="savedInstanceState">The saved instance state.</param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
          

            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.EditItem);
            //binding to layout
            txtTitle = FindViewById<TextView>(Resource.Id.txtEditTitle);
            txtDetails = FindViewById<TextView>(Resource.Id.txtEditDescription);

            btnEdit = FindViewById<Button>(Resource.Id.btnEdit);
            btnDelete = FindViewById<Button>(Resource.Id.btnDelete);
            btnDelete.Click += onBtnDelete_Click;
            btnEdit.Click += onBtnEdit_Click;
            //getting data from MainActivity
            ListId = Intent.GetIntExtra("ListID", 0);
            Title = Intent.GetStringExtra("Title");
            Details = Intent.GetStringExtra("Details");
        }

        /// <summary>
        /// The on btn delete_ click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void onBtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                 DatabaseManager.DeleteItem(ListId); //saves changes to DB
                Toast.MakeText(this, "Note was Delete", ToastLength.Long).Show();
                this.Finish();
                 StartActivity(typeof(MainActivity));
            }
            catch (Exception ex)
            {

                Toast.MakeText(this, "it Borked! " + ex.Message, ToastLength.Long).Show();
                Console.WriteLine("error Occurred:" + ex.Message);
            }

          
        }

        /// <summary>
        /// The on btn edit_ click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void onBtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                DatabaseManager.EditItem(txtTitle.Text, txtDetails.Text, ListId); //saves changes to DB
                Toast.MakeText(this, "Note was Edited", ToastLength.Long).Show();
                this.Finish();
                StartActivity(typeof(MainActivity));
            }
            catch (Exception ex)
            {

                Toast.MakeText(this, "it Borked! " + ex.Message, ToastLength.Long).Show();
                Console.WriteLine("error Occurred:" + ex.Message);
            }
        }
    }
}