using Microsoft.EntityFrameworkCore;

namespace api_amanda.Services {
    public interface ITableCsvService {
        void CreateCsvTable();
    }

    public class TableCsvService : ITableCsvService {
        private readonly ApplicationDbContext context;

        public TableCsvService(ApplicationDbContext context) {
            this.context = context;
        }

        public void CreateCsvTable() {
            var existsTableCSV = context.Database
                    .ExecuteSqlRaw("SELECT EXISTS (SELECT 1 FROM information_schema.tables WHERE table_name = 'CsvFiles');")
                    .ToString();

            if (existsTableCSV == "False") {
                context.Database.ExecuteSqlRaw(@"
                CREATE TABLE CsvFiles (
                    csvFileId VARCHAR(255) PRIMARY KEY,
                    csvName VARCHAR(255) NOT NULL,
                    uploadedAt VARCHAR NOT NULL,
                    userName VARCHAR(255) NOT NULL,
                    firstTimeStamp BIGINT NOT NULL,
                    lastTimeStamp BIGINT NOT NULL
                );
            ");
            }
        }

        public void CreateCsvRecordTable() {
            var existsTableCsvRecord = context.Database
                    .ExecuteSqlRaw("SELECT EXISTS (SELECT 1 FROM information_schema.tables WHERE table_name = 'Records');")
                    .ToString();

            if (existsTableCsvRecord == "False") {
                context.Database.ExecuteSqlRaw(@"
        CREATE TABLE Recoeds (
            recordId VARCHAR(255) PRIMARY KEY,
            csvFileId VARCHAR(255) NOT NULL,
            mcc INTEGER NOT NULL,
            mnc INTEGER NOT NULL,
            lac INTEGER NOT NULL,
            cellid BIGINT NOT NULL,
            lat FLOAT NOT NULL,
            lon FLOAT NOT NULL,
            signal INTEGER,
            measured_at BIGINT NOT NULL,
            rating FLOAT,
            speed FLOAT,
            direction FLOAT,
            act INTEGER,
            ta INTEGER,
            psc INTEGER,
            tac INTEGER,
            pci INTEGER,
            sid INTEGER,
            nid INTEGER,
            bid INTEGER,
            FOREIGN KEY (csvFileId) REFERENCES CsvFiles(csvFileId)
        );
");
    }
}

    }






}
