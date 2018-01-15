using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;


namespace ArchieBirds.Models
{
    public enum Units
    {
        Milliarchieops,
        Metric,
        Imperial,
    }

    public class Archie
    {
        public int ID { get; set; }
        [Required(ErrorMessage="Specimen name is required and should be from 3 to 30 characters")]
        [StringLength(30, MinimumLength = 3)]
        [Display(Name = "Specimen Name")]
        public string SpecimenName { get; set; }

        [Range(0, long.MaxValue)]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal Height { get; set; }
        [Range(0, long.MaxValue)]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal Weight { get; set; }
        [Range(0, long.MaxValue)]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal Length { get; set; }
        [Range(0, long.MaxValue)]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal Girth { get; set; }
        [Range(0, long.MaxValue)]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal WingWidth { get; set; }
        
        [Range(-90d, 90d)]
        public double Latitude { get; set; }
        [Range(-180d, 180d)]
        public double Longtitude { get; set; }

        public Units currentUnit = Units.Metric;

        /// <summary>
        /// The wingspan is the sum of both wing's span plus the girth.
        /// If the bird's skeleton is incomplete but we have one wing, 
        /// then we can estimate the wingspan as double the one wing’s span plus a 6th of the bird’s length.
        /// If the bird's skeleton is missing both wings then we can't estimate its wingspan 
        /// and must return an appropriate value indicating this. 
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public double Wingspan
        {
            get 
            {
                if (Wings == 0)
                {
                    return 0;
                }
                decimal girth = (Girth == 0) ? Length / 6 : Girth;
                decimal wingspan = WingWidth * 2 + girth;

                return Decimal.ToDouble(wingspan);
            }
        }

        [Range(0, 2, ErrorMessage = "Invalid input")]
        public int Wings { get; set; }
        [Range(0, 2, ErrorMessage = "Invalid input")]
        public int HandThings { get; set; }
        [Range(0, 1, ErrorMessage = "Invalid input")]
        public int Skull { get; set; }
        [Range(0, 300, ErrorMessage = "Invalid input")]
        public int Teeth { get; set; }
        [Range(0, 2, ErrorMessage = "Invalid input")]
        public int Feet { get; set; }
        [Range(0, 1, ErrorMessage = "Invalid input")]
        public int Tail { get; set; }
        [Range(0, 1, ErrorMessage = "Invalid input")]
        public int Spine { get; set; }


        /// <summary>
        /// Convert measurement units to requested one.
        /// Update currentUnit value. 
        /// </summary>
        /// <param name="unit"></param>
        public void SetUnit(Units unit)
        {
            ToMetric();
            switch (unit)
            {
                case Units.Milliarchieops:
                    MetricToMilliops();
                    break;
                case Units.Imperial:
                    MetricToImperial();
                    break;
            }
        }

        /// <summary>
        /// Convert archie's parameters to metric system. 
        /// metric is used by default to store in database.
        /// </summary>
        public void ToMetric()
        {
            switch (currentUnit)
            {
                case Units.Milliarchieops:
                    MilliopsToMetric();
                    break;
                case Units.Imperial:
                    ImperialToMetric();
                    break;
                case Units.Metric:
                default:
                    break;
            }
            currentUnit = Units.Metric;
        }
        
        /// <summary>
        /// Convert inches to cm, lb to kg.
        /// </summary>
        private void ImperialToMetric()
        {
            // in -> cm
            Height = Height * 2.54m;
            Length = Length  * 2.54m;
            Girth  = Girth  * 2.54m;
            WingWidth = WingWidth * 2.54m;

            // lb -> kg
            Weight = Weight * 0.45359237m;
        }

        /// <summary>
        /// Convert mops to cm.
        /// Special unit milliarchieops (mops) are the equivalent of 1/7th of an inch.
        /// </summary>
        private void MilliopsToMetric()
        {
            // mops -> cm
            Height = Height * 7.0m * 2.54m;
            Length = Length  * 7.0m * 2.54m;
            Girth  = Girth  * 7.0m * 2.54m;
            WingWidth = WingWidth * 7.0m * 2.54m;

            // lb -> kg
            Weight = Weight * 0.45359237m;
        }
        
        /// <summary>
        /// Convert inches to centimeters, kg to lb.
        /// </summary>
        private void MetricToImperial()
        {
            // cm -> in
            Height = Height / 2.54m;
            Length = Length / 2.54m;
            Girth  = Girth  / 2.54m;
            WingWidth = WingWidth / 2.54m;

            // kg -> lb
            Weight = Weight / 0.45359237m;
        }

        /// <summary>
        /// Convert cm to mops.
        /// Special unit milliarchieops are the equivalent of 1/7th of an inch.
        /// </summary>
        private void MetricToMilliops()
        {
            Height = (Height / 2.54m) / 7.0m;
            Length = (Length / 2.54m) / 7.0m;
            Girth = (Girth / 2.54m) / 7.0m;
            WingWidth = (WingWidth / 2.54m) / 7.0m;

            // kg -> lb
            Weight = Weight / 0.45359237m;
        }
    }
}