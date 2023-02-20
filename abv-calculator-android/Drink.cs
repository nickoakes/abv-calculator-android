using System;

namespace abv_calculator_android
{
    public class Drink
    {
        public string Name { get; set; }
        public double BaseSugar { get; set; }
        public bool BaseSugarPerLitre { get; set; }
        public bool DefaultSugar { get; set; }
        public double TotalBaseSugar { get; set; }
        public double AdditionalSugar { get; set; }
        public double TotalVolume { get; set; }
        public double ABV { get; set; }

        public Drink(string name, double baseSugar, bool baseSugarPerLitre)
        {
            Name = name;
            BaseSugar = baseSugar;
            BaseSugarPerLitre = baseSugarPerLitre;
        }

        public void SetHoneyBaseSugarMass(double honeyMass)
        {
            BaseSugar = 0.81 * honeyMass;
        }

        public void CalculateTotalBaseSugar()
        {
            TotalBaseSugar = BaseSugarPerLitre ? BaseSugar * TotalVolume : BaseSugar;
        }

        public double CalculateABV()
        {
            CalculateTotalBaseSugar();

            double totalSugarMass = TotalBaseSugar + AdditionalSugar;

            double sucroseMoles = totalSugarMass / Constants.SucroseMolarMass;

            TotalVolume *= 1000;

            //C12H22O11 + H2O => 2C6H12O6
            double glucoseMoles = sucroseMoles * 2;

            //C6H12O6 => 2C2H6O + 2CO2
            double ethanolMoles = glucoseMoles * 2;

            double ethanolMass = ethanolMoles * Constants.EthanolMolarMass;

            double ethanolVolume = ethanolMass * Constants.EthanolDensity;

            return Math.Round((ethanolVolume / TotalVolume) * 100, 1);
        }
    }
}
