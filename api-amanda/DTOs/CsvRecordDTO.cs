using api_amanda.Entities;
using System.ComponentModel.DataAnnotations;

namespace api_amanda.DTOs {
    public class CsvRecordDTO : CsvRecord {
        [Key]
        public string recordId { get; set; }
        public string csvFileId { get; set; }
    }
}

