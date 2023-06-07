using NetTopologySuite.Geometries;

namespace api_amanda.Entities {
    public class BtsRecord {
      
        public string cellid { get; set; } = null!;
        public double btsLat { get; set; } 
        public double btsLon { get; set; } 

        public Point? Location { get; set; }

    }
}
