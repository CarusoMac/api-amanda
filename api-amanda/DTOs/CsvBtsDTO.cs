using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;

    namespace api_amanda.DTOs {
        public class CsvBtsDTO {
        [Key]
        //public string cellid { get; set; } = null!;
        public string cellid { get; set; }

        public double btsLat { get; set; }
        public double btsLon { get; set; }

        public Point? Location { get; set; }

    }
    }
