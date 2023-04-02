namespace api_amanda.Entities {
    public class CsvRecord {
        public string mcc { get; set; }
        public string mnc { get; set; }
        public string lac { get; set; }
        public string cellid { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
        public string signal { get; set; }
        public long measured_at { get; set; }
        public string rating { get; set; }
        public string speed { get; set; }
        public string direction { get; set; }
        public string act { get; set; }
        public string ta { get; set; }
        public string psc { get; set; }
        public string tac { get; set; }
        public string pci { get; set; }
        public string sid { get; set; }
        public string nid { get; set; }
        public string bid { get; set; }
    }
}
