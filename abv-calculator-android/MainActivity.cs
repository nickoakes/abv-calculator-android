using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.App;
using Google.Android.Material.Snackbar;
using Android.Widget;

namespace abv_calculator_android
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private string _selectedDrinkOption = "beer";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.content_main);

            //Start

            Button startButton = FindViewById<Button>(Resource.Id.start_button);
            startButton.Click += StartButton_Click;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            SetContentView(Resource.Layout.drink_selection);

            //Drink selection

            RadioButton beerOption = FindViewById<RadioButton>(Resource.Id.drink_selection_beer),
                        wineOption = FindViewById<RadioButton>(Resource.Id.drink_selection_wine),
                        ciderOption = FindViewById<RadioButton>(Resource.Id.drink_selection_cider),
                        meadOption = FindViewById<RadioButton>(Resource.Id.drink_selection_mead);

            beerOption.Click += BeerOption_Click;
            wineOption.Click += WineOption_Click;
            ciderOption.Click += CiderOption_Click;
            meadOption.Click += MeadOption_Click;

            Button drinkSelectionContinue = FindViewById<Button>(Resource.Id.drink_selection_continue);
            drinkSelectionContinue.Click += DrinkSelectionContinue_Click;
        }

        private void DrinkSelectionContinue_Click(object sender, EventArgs e)
        {
            SetContentView(Resource.Layout.default_sugar);
            FindViewById<TextView>(Resource.Id.default_sugar_title).Text = $"Would you like to use the default amount of sugar for {_selectedDrinkOption}?";
        }

        private void MeadOption_Click(object sender, EventArgs e)
        {
            _selectedDrinkOption = "mead";
        }

        private void CiderOption_Click(object sender, EventArgs e)
        {
            _selectedDrinkOption = "cider";
        }

        private void WineOption_Click(object sender, EventArgs e)
        {
            _selectedDrinkOption = "wine";
        }

        private void BeerOption_Click(object sender, EventArgs e)
        {
            _selectedDrinkOption = "beer";
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
	}
}
