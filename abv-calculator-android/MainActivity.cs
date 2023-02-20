using System;
using Android.App;
using Android.OS;
using AndroidX.AppCompat.App;
using Android.Widget;

namespace abv_calculator_android
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private Drink _selectedDrink = new Drink("beer", Constants.BeerKit, false);

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.content_main);

            //Start

            Button startButton = FindViewById<Button>(Resource.Id.start_button);
            startButton.Click += StartButton_Click;
        }

        private void Initiate(object sender, EventArgs e)
        {
            SetContentView(Resource.Layout.content_main);

            //Start

            Button startButton = FindViewById<Button>(Resource.Id.start_button);
            startButton.Click += StartButton_Click;
        }

        private void GetDrinkSelection()
        {
            if (FindViewById<RadioButton>(Resource.Id.drink_selection_beer).Checked)
            {
                _selectedDrink = new Drink("beer", Constants.BeerKit, false);
            } 
            else if (FindViewById<RadioButton>(Resource.Id.drink_selection_wine).Checked)
            {
                _selectedDrink = new Drink("wine", Constants.GrapeJuicePerLitre, true);
            } 
            else if (FindViewById<RadioButton>(Resource.Id.drink_selection_cider).Checked)
            {
                _selectedDrink = new Drink("cider", Constants.AppleJuicePerLitre, true);
            } 
            else if (FindViewById<RadioButton>(Resource.Id.drink_selection_mead).Checked)
            {
                _selectedDrink = new Drink("mead", Constants.HoneyPer100g, false);
            }
        }

        private bool GetDefaultSugar()
        {
            if (FindViewById<RadioButton>(Resource.Id.default_sugar_yes).Checked)
            {
                _selectedDrink.DefaultSugar = true;
                return true;
            } 
            else
            {
                _selectedDrink.DefaultSugar = false;
                return false;
            }
        }



        private void AdvanceToAdditionalSugar()
        {
            SetContentView(Resource.Layout.additional_sugar);

            Button additionalSugarContinue = FindViewById<Button>(Resource.Id.additional_sugar_continue);
            additionalSugarContinue.Click += AdditionalSugarContinue_Click;
        }

        private void GetResult()
        {
            _selectedDrink.ABV = _selectedDrink.CalculateABV();
            TextView resultText = FindViewById<TextView>(Resource.Id.result_text);
            resultText.Text = $"Your {_selectedDrink.Name} is approximately {_selectedDrink.ABV}% A.B.V.";

            Button startAgainButton = FindViewById<Button>(Resource.Id.start_again_button);
            startAgainButton.Click += Initiate;
        }

        #region Button Clicks

        private void StartButton_Click(object sender, EventArgs e)
        {
            SetContentView(Resource.Layout.drink_selection);

            Button drinkSelectionContinue = FindViewById<Button>(Resource.Id.drink_selection_continue);
            drinkSelectionContinue.Click += DrinkSelectionContinue_Click;
        }

        private void DrinkSelectionContinue_Click(object sender, EventArgs e)
        {
            GetDrinkSelection();

            SetContentView(Resource.Layout.default_sugar);
            FindViewById<TextView>(Resource.Id.default_sugar_title).Text = $"Would you like to use the default amount of sugar for {_selectedDrink.Name}?";

            Button defaultSugarContinue = FindViewById<Button>(Resource.Id.default_sugar_continue);
            defaultSugarContinue.Click += DefaultSugarContinue_Click;
        }

        private void DefaultSugarContinue_Click(object sender, EventArgs e)
        {
            bool defaultSugar = GetDefaultSugar();
            _selectedDrink.DefaultSugar = defaultSugar;
            _selectedDrink.BaseSugarPerLitre = defaultSugar;

            if (defaultSugar)
            {
                if (_selectedDrink.Name == "mead")
                {
                    SetContentView(Resource.Layout.honey_mass);

                    Button honeyMassContinue = FindViewById<Button>(Resource.Id.honey_mass_continue);
                    honeyMassContinue.Click += HoneyMassContinue_Click;
                }
                else
                {
                    AdvanceToAdditionalSugar();
                }
            }
            else
            {
                SetContentView(Resource.Layout.set_base_sugar);

                Button setBaseSugarContinue = FindViewById<Button>(Resource.Id.set_base_sugar_continue);
                setBaseSugarContinue.Click += SetBaseSugarContinue_Click;
            }
        }

        private void HoneyMassContinue_Click(object sender, EventArgs e)
        {
            string honeyMass = FindViewById<EditText>(Resource.Id.honey_mass_input).Text;

            TextView validation = FindViewById<TextView>(Resource.Id.honey_mass_validation);

            if (honeyMass == "")
            {
                validation.Visibility = Android.Views.ViewStates.Visible;
            }
            else
            {
                validation.Visibility = Android.Views.ViewStates.Invisible;

                _selectedDrink.SetHoneyBaseSugarMass(double.Parse(honeyMass));

                AdvanceToAdditionalSugar();
            }
        }

        private void SetBaseSugarContinue_Click(object sender, EventArgs e)
        {
            string baseSugar = FindViewById<EditText>(Resource.Id.set_base_sugar_input).Text;

            TextView validation = FindViewById<TextView>(Resource.Id.base_sugar_validation);

            if (baseSugar == "")
            {
                validation.Visibility = Android.Views.ViewStates.Visible;
            } 
            else
            {
                validation.Visibility = Android.Views.ViewStates.Invisible;

                _selectedDrink.BaseSugar = double.Parse(baseSugar);

                AdvanceToAdditionalSugar();
            }
        }

        private void AdditionalSugarContinue_Click(object sender, EventArgs e)
        {
            string additionalSugar = FindViewById<EditText>(Resource.Id.additional_sugar_input).Text;

            TextView validation = FindViewById<TextView>(Resource.Id.additional_sugar_validation);

            if (additionalSugar == "")
            {
                validation.Visibility = Android.Views.ViewStates.Visible;
            } 
            else
            {
                validation.Visibility = Android.Views.ViewStates.Invisible;

                _selectedDrink.AdditionalSugar = double.Parse(additionalSugar);

                SetContentView(Resource.Layout.total_volume);

                Button totalVolumeContinue = FindViewById<Button>(Resource.Id.total_volume_continue);
                totalVolumeContinue.Click += TotalVolumeContinue_Click;
            }
        }

        private void TotalVolumeContinue_Click(object sender, EventArgs e)
        {
            string totalVolume = FindViewById<EditText>(Resource.Id.total_volume_input).Text;

            TextView validation = FindViewById<TextView>(Resource.Id.total_volume_validation);

            if (totalVolume == "")
            {
                validation.Visibility = Android.Views.ViewStates.Visible;
            } 
            else
            {
                validation.Visibility = Android.Views.ViewStates.Invisible;

                _selectedDrink.TotalVolume = double.Parse(totalVolume);

                SetContentView(Resource.Layout.result);

                GetResult();
            }
        }

        #endregion
    }
}
