    using System.ComponentModel.DataAnnotations;

    namespace api_amanda.DTOs {
        public class CsvBtsDTO {
            [Key]
            public string cellid { get; set; } = null!;
        public decimal btsLat { get; set; }
        public decimal btsLon { get; set; }

    }
    }
