using NetTopologySuite.Geometries;

namespace api_amanda.Entities {
    public class CsvRecord {
        public string mcc { get; set; } = null!;
        public string mnc { get; set; } = null!;
        public string lac { get; set; } = null!;
        public string cellid { get; set; } = null!;
        public double lat { get; set; }
        public double lon { get; set; }
        public string signal { get; set; } = null!;
        public long measured_at { get; set; }
        public string rating { get; set; }
        public string speed { get; set; } = null!;
        public string direction { get; set; } = null!;
        public string act { get; set; } = null!;
        public string ta { get; set; } = null!;
        public string psc { get; set; } = null!;
        public string tac { get; set; } = null!;
        public string pci { get; set; } = null!;
        public string sid { get; set; } = null!;
        public string nid { get; set; } = null!;
        public string bid { get; set; } = null!;

        public Point RecLocation { get; set; } = null!;
    }
}
