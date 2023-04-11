using System.ComponentModel.DataAnnotations;

namespace api_amanda.DTOs {
    public class CsvFileDTO {
        [Key]
        public string csvFileId { get; set; }
        public string csvFileName { get; set; }
        public string userId { get; set; }
        public string uploadDate { get; set; }
        public long firstTimeStamp { get; set; }
        public long lastTimeStamp { get; set; }

        public string fileTitle { get; set; }
    }
}
