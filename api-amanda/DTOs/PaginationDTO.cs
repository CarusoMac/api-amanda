namespace api_amanda.DTOs {
    public class PaginationDTO {

        public int Page { get; set; } = 1;
        private int recordsPerPage = 5;
        private readonly int maxRecordsPerPage = 10;

        public int RecordsPerPage {
            get {
                return recordsPerPage;
            }
            set {

                recordsPerPage = (value > maxRecordsPerPage) ? maxRecordsPerPage : value;
            }
        
        }
    }
}
