using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;


namespace ArchieBirds.Models
{
    public class Archie
    {
        public int ID { get; set; }
        [Required(ErrorMessage="Specimen name is required and should be from 3 to 30 characters")]
        [StringLength(30, MinimumLength = 3)]
        public string SpecimenName { get; set; }

        [Range(0, long.MaxValue)]
        public decimal Height { get; set; }
        [Range(0, long.MaxValue)]
        public decimal Weight { get; set; }
        [Range(0, long.MaxValue)]
        public decimal Length { get; set; }
        [Range(0, long.MaxValue)]
        public decimal Girth { get; set; }
        [Range(0, long.MaxValue)]
        public decimal WingWidth { get; set; }
        
        [Display(Name = "Location Latitude")]
        public double Latitude { get; set; }
        [Display(Name = "Location Longtitude")]
        public double Longtitude { get; set; }

        public ArchieBirds.Controllers.Units currentUnit = ArchieBirds.Controllers.Units.Metric;

        /// <summary>
        /// The wingspan is the sum of both wing's span plus the girth.
        /// If the bird's skeleton is incomplete but we have one wing, 
        /// then we can estimate the wingspan as double the one wing’s span plus a 6th of the bird’s length.
        /// If the bird's skeleton is missing both wings then we can't estimate its wingspan 
        /// and must return an appropriate value indicating this. (-1 ?)
        /// </summary>
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
        public decimal Wings { get; set; }
        [Range(0, 2, ErrorMessage = "Invalid input")]
        public decimal HandThings { get; set; }
        [Range(0, 1, ErrorMessage = "Invalid input")]
        public decimal Skull { get; set; }
        [Range(0, 300, ErrorMessage = "Invalid input")]
        public decimal Teeth { get; set; }
        [Range(0, 2, ErrorMessage = "Invalid input")]
        public decimal Feet { get; set; }
        [Range(0, 1, ErrorMessage = "Invalid input")]
        public decimal Tail { get; set; }
        [Range(0, 1, ErrorMessage = "Invalid input")]
        public decimal Spine { get; set; }

        public void SetUnit(ArchieBirds.Controllers.Units unit)
        {
            ToMetric();
            switch (unit)
            {
                case ArchieBirds.Controllers.Units.Milliarchieops:
                    MetricToMilliops();
                    break;
                case ArchieBirds.Controllers.Units.Imperial:
                    MetricToImperial();
                    break;
            }
        }

        public void ToMetric()
        {
            switch (currentUnit)
            {
                case ArchieBirds.Controllers.Units.Milliarchieops:
                    MilliopsToMetric();
                    break;
                case ArchieBirds.Controllers.Units.Imperial:
                    ImperialToMetric();
                    break;
                case ArchieBirds.Controllers.Units.Metric:
                default:
                    break;
            }
            currentUnit = ArchieBirds.Controllers.Units.Metric;
        }
        
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


    public class ArchiesDBContext : DbContext
    {
        public DbSet<Archie> Archies { get; set; }
    }

}