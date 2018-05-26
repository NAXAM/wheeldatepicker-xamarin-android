using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.Constraints;
using Android.Views;
using Android.Widget;
using Com.Aigestudio.Wheelpicker;
using WheelDatePicker;

namespace Naxam.Controls
{
    public class WheelDatePicker : Dialog
    {
        public event EventHandler<object> Done;
        private const int MAX_YEAR = 2100;
        private const int MIN_YEAR = 1900;
        private const int MIN_DAY = 1;
        private const int MIN_MONTH = 1;
        private const int MAX_MONTH = 12;

        View background;
        WheelPicker dayPicker;
        WheelPicker monthPicker;
        WheelPicker yearPicker;
        TextView btnDone;

        List<int> Years;
        List<string> Months;
        List<int> Days;

        public WheelDatePicker(Context context) : base(context)
        {
            Initialize();
        }

        public WheelDatePicker(Context context, int themeResId) : base(context, themeResId)
        {
            Initialize();
        }

        protected WheelDatePicker(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Initialize();
        }

        protected WheelDatePicker(Context context, bool cancelable, EventHandler cancelHandler) : base(context, cancelable, cancelHandler)
        {
            Initialize();
        }

        protected WheelDatePicker(Context context, bool cancelable, IDialogInterfaceOnCancelListener cancelListener) : base(context, cancelable, cancelListener)
        {
            Initialize();
        }

        void Initialize()
        {
            RequestWindowFeature((int)WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.wheel_picker); SetCancelable(false);
            Window.SetLayout(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
            Window.SetBackgroundDrawable(new ColorDrawable(Android.Graphics.Color.Transparent));

            background = FindViewById<View>(Resource.Id.background);
            dayPicker = FindViewById<WheelPicker>(Resource.Id.day_picker);
            monthPicker = FindViewById<WheelPicker>(Resource.Id.month_picker);
            yearPicker = FindViewById<WheelPicker>(Resource.Id.year_picker);
            btnDone = FindViewById<TextView>(Resource.Id.btn_done);

            dayPicker.ItemSelected += DayPicker_ItemSelected1; ;
            monthPicker.ItemSelected += MonthPicker_ItemSelected1; ;
            yearPicker.ItemSelected += YearPicker_ItemSelected1; ;
            btnDone.Click += BtnDone_Click;
            background.Click += Background_Click;

            Years = new List<int>();
            for (int i = MIN_YEAR; i <= MAX_YEAR; i++)
            {
                Years.Add(i);
            }

            Months = new List<string>();
            for (int i = MIN_MONTH; i <= MAX_MONTH; i++)
            {
                var month = new DateTime(1, i, 1);
                Months.Add(month.ToString("MMMM"));
            }
            Days = new List<int>();
            for (int i = MIN_DAY; i <= 31; i++)
            {
                Days.Add(i);
            }

            yearPicker.Data = Years;
            monthPicker.Data = Months;
            dayPicker.Data = Days;
        }

        private void YearPicker_ItemSelected1(object sender, Com.Aigestudio.Wheelpicker.WheelPicker.ItemSelectedEventArgs e)
        {
            SelectedYear = e.P2 + MIN_YEAR;
            HandleDayOfMonth(e.P2 + MIN_YEAR, SelectedMonth);
        }

        private void MonthPicker_ItemSelected1(object sender, Com.Aigestudio.Wheelpicker.WheelPicker.ItemSelectedEventArgs e)
        {
            SelectedMonth = e.P2 + MIN_MONTH;
            HandleDayOfMonth(SelectedYear, e.P2 + MIN_MONTH);
        }

        private void DayPicker_ItemSelected1(object sender, Com.Aigestudio.Wheelpicker.WheelPicker.ItemSelectedEventArgs e)
        {
            SelectedDay = e.P2 + MIN_DAY;
        }

        private void Background_Click(object sender, EventArgs e)
        {
            Dismiss();
            Done?.Invoke(sender, null);
        }

        private void BtnDone_Click(object sender, EventArgs e)
        {
            Dismiss();
            Done?.Invoke(sender, new DateTime(SelectedYear, SelectedMonth, SelectedDay));
        }

        void HandleDayOfMonth(int year, int month)
        {
            var maxDay = DateTime.DaysInMonth(year, month);
            Days.Clear();
            for (int i = MIN_DAY; i <= maxDay; i++)
            {
                Days.Add(i);
            }
            dayPicker.Data = Days;
            if (SelectedDay >= dayPicker.Data.Count)
            {
                SelectedDay = dayPicker.Data.Count;
            }
        }

        public int SelectedDay
        {
            get { return dayPicker.SelectedItemPosition + MIN_DAY; }
            set
            {
                if (dayPicker.Data == null || dayPicker.Data.Count == 0 || dayPicker.Data.Count <= (value - MIN_DAY)) return;
                dayPicker.SelectedItemPosition = value - MIN_DAY;
            }
        }

        public int SelectedMonth
        {
            get { return monthPicker.SelectedItemPosition + MIN_MONTH; }
            set
            {
                if (monthPicker.Data == null || monthPicker.Data.Count == 0 || monthPicker.Data.Count <= (value - MIN_MONTH)) return;
                monthPicker.SelectedItemPosition = value - MIN_MONTH;
            }
        }

        public int SelectedYear
        {
            get { return yearPicker.SelectedItemPosition + MIN_YEAR; }
            set
            {
                if (yearPicker.Data == null || yearPicker.Data.Count == 0 || yearPicker.Data.Count <= (value - MIN_YEAR)) return;
                yearPicker.SelectedItemPosition = value - MIN_YEAR;
            }
        }
    }
}