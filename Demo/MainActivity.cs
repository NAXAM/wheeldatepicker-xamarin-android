using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using System;
using Naxam.Controls;

namespace Demo
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            var btnPick = FindViewById<Button>(Resource.Id.btn_pick);
            btnPick.Click += BtnPick_Click;
        }

        private void BtnPick_Click(object sender, System.EventArgs e)
        {
            WheelDatePicker picker = new WheelDatePicker(this);
            picker.SelectedDay = 25;
            picker.SelectedMonth = 10;
            picker.SelectedYear = 1995;
            picker.Done += Picker_Done;
            picker.Show();

        }

        private void Picker_Done(object sender, object e)
        {
            if (e is DateTime date)
            {
                Toast.MakeText(this, "You just choice:" + (date.ToShortDateString()), ToastLength.Long).Show();
            }
        }
    }
}

