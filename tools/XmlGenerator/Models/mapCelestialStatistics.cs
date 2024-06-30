namespace EVEMon.XmlGenerator.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class mapCelestialStatistics
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int celestialID { get; set; }

        public double? temperature { get; set; }

        [StringLength(10)]
        public string spectralClass { get; set; }

        public double? luminosity { get; set; }

        public double? age { get; set; }

        public double? life { get; set; }

        public double? orbitRadius { get; set; }

        public double? eccentricity { get; set; }

        public double? massDust { get; set; }

        public double? massGas { get; set; }

        public bool? fragmented { get; set; }

        public double? density { get; set; }

        public double? surfaceGravity { get; set; }

        public double? escapeVelocity { get; set; }

        public double? orbitPeriod { get; set; }

        public double? rotationRate { get; set; }

        public bool? locked { get; set; }

        public double? pressure { get; set; }

        public double? radius { get; set; }

        public double? mass { get; set; }
    }
}
