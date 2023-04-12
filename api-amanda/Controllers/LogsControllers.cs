using api_amanda.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_amanda.DTOs;

namespace api_amanda.Controllers {

    [Route("api/logs")]
    [ApiController]
    public class LogsController : ControllerBase {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;

        //constructor
        public LogsController(ApplicationDbContext context, 
            IWebHostEnvironment webHostEnvironment) {
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        //list of files
        [HttpGet("export")]
        public async Task<ActionResult<List<CsvFileDTO>>> Get() {
            var logs = await context.CsvFiles.ToListAsync();
            return logs;
        }

        //title
        [HttpGet("export/{csvFileId}")]
        public async Task<ActionResult<CsvFileDTO>> Get(string csvFileId) {

            var file = await context.CsvFiles.FirstOrDefaultAsync(f => f.csvFileId == csvFileId);
            if (file == null) {
                return NotFound();
            }
            return file;
        }


        //////getting all records related to specific file
        [HttpGet("{fileId}")]
        public ActionResult<IEnumerable<TrackInfoDTO>> GetRecordsByFileId(string fileId) {
            var records = (from rec in context.Records
                           join bts in context.BtsCoordiantes
                           on rec.cellid.Trim() equals bts.cellid.Trim() into gj
                           from subquery in gj.DefaultIfEmpty()
                           where rec.csvFileId == fileId //passing id as parameter should prevent injection
                           select new TrackInfoDTO{
                               cellid = subquery.cellid,
                               btsLat = subquery.btsLat,
                               btsLon = subquery.btsLon,
                               csvFileId = rec.csvFileId,
                               recordId = rec.recordId,
                               mcc = rec.mcc,
                               mnc = rec.mnc,
                               lac = rec.lac,
                               lat = rec.lat,
                               lon = rec.lon,
                               signal = rec.signal,
                               measured_at = rec.measured_at,
                               rating = rec.rating,
                               speed = rec.speed,
                               direction = rec.direction,
                               act = rec.act,
                               ta = rec.ta,
                               psc = rec.psc,
                               tac = rec.tac,
                               pci = rec.pci,
                               sid = rec.sid,
                               nid = rec.nid,
                               bid = rec.bid
                           })
               //.Where(x => x.csvFileId == fileId.ToString())
               .OrderBy(x => x.measured_at)
               .ToList();
            return records;
        }


        //uploading records
        [HttpPost("upload")]
        public async Task<IActionResult> UploadCsvFile(IFormFile file) {
            if (file == null || file.Length == 0) {
                return BadRequest("No file selected or file is empty.");
            }
            //creating temp directory
            var tempDirectoryName = Guid.NewGuid().ToString();
            var tempDirectoryPath = Path.Combine(webHostEnvironment.ContentRootPath, "TempTest", tempDirectoryName);
            Directory.CreateDirectory(tempDirectoryPath);

            var filePath = Path.Combine(tempDirectoryPath, file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create)) {
                await file.CopyToAsync(stream);
            }

            //coverting csv
            var records = CsvFileReader.ReadCsvFile(filePath);
            
            //assigning new ID for new file
            var csvId = Guid.NewGuid().ToString();

            //looking for min and max timestamp 
            var minMeasuredAt = records.Min(r => r.measured_at);
            var maxMeasuredAt = records.Max(r => r.measured_at);

            //assigning values to newly creating record
            foreach (var record in records) {
                var tempId = Guid.NewGuid().ToString();
                CsvRecordDTO newRecord = new()
                {
                    csvFileId = csvId,
                    recordId = tempId,
                    mcc = record.mcc,
                    mnc = record.mnc,
                    lac = record.lac,
                    cellid = record.cellid,
                    lat = record.lat,
                    lon = record.lon,
                    signal = record.signal,
                    measured_at = record.measured_at,
                    rating = record.rating,
                    speed = record.speed,
                    direction = record.direction,
                    act = record.act,
                    ta = record.ta,
                    psc = record.psc,
                    tac = record.tac,
                    pci = record.pci,
                    sid = record.sid,
                    nid = record.nid,
                    bid = record.bid
                };
                context.Records.Add(newRecord);
            }

            //assigning values to newly created File record
            CsvFileDTO newFile = new()
            {
                csvFileId = csvId,
                csvFileName = file.FileName,
                userId = "user",
                uploadDate = DateTime.Now.ToString(),
                firstTimeStamp = minMeasuredAt,
                lastTimeStamp = maxMeasuredAt,
                fileTitle = file.FileName
            };
                context.CsvFiles.Add(newFile);
                context.SaveChanges();
            // delete temporary directory
            Directory.Delete(tempDirectoryPath, true);
            return Ok("CSV file converted to JSON and sent to database successfully.");
        }

        //custom title
        [HttpPut("update/{csvFileId}")]
        public async Task<ActionResult> UpdateFileTitle(string csvFileId, [FromBody] FileTitleDTO fileTitle) {
            var file = await context.CsvFiles.FirstOrDefaultAsync(f => f.csvFileId == csvFileId);
            if (file == null) {
                return NotFound();
            }
            file.fileTitle = fileTitle.FileTitle;
            await context.SaveChangesAsync();
            return Ok();
        }

    }
    }

