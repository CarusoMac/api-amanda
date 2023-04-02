using api_amanda.Entities;
using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;
//using System.Formats.Asn1;
//using System.Globalization;
//using CsvHelper;
using Microsoft.EntityFrameworkCore;
using api_amanda.DTOs;
//using AutoMapper;
//using Newtonsoft.Json;
//using System.ComponentModel.DataAnnotations;
//using Microsoft.AspNetCore.Hosting.Server;
//using System.IO;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
//using System.Text.Json;
//using System.Data;
using api_amanda.Services;

namespace api_amanda.Controllers {

    [Route("api/logs")]
    [ApiController]
    public class LogsController : ControllerBase {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ITableCsvService tableCsvService;

        //constructor
        public LogsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, ITableCsvService tableCsvService) {
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
            this.tableCsvService = tableCsvService;
        }


        [HttpGet("import")]
        //public async Task<ActionResult<List<CsvFileDTO>>> Get([FromQuery] PaginationDTO paginationDTO) {
        public async Task<ActionResult<List<CsvFileDTO>>> Get() {

            //var queryable = context.CsvFiles.AsQueryable();
            //double count = await queryable.CountAsync();
            //Response.Headers.Add("TotalRecordsAmount", count.ToString());
            //var logs = await queryable.Skip((paginationDTO.Page - 1) * paginationDTO.RecordsPerPage)
            //                          .Take(paginationDTO.RecordsPerPage)
            //                          .ToListAsync();
            var logs = await context.CsvFiles.ToListAsync();
            return logs;
        }

        //[HttpGet ("{id:int}")]
        //public ActionResult<Log> Get(int Id) {

        //    var log = repository.GetLogById(Id);
        //    if (log == null) {
        //        logger.LogWarning($"Log with Id: {Id} not found");
        //        return NotFound();
        //    }s
        //    return log;
        //}


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

            //creating table for files if not exist
           // tableCsvService.CreateCsvTable();

            //looking for min and max timestamp 
            var minMeasuredAt = records.Min(r => r.measured_at);
            var maxMeasuredAt = records.Max(r => r.measured_at);

            //assigning values to newly creating record
            foreach (var record in records) {
                var tempId = Guid.NewGuid().ToString();
                CsvRecordDTO newRecord = new CsvRecordDTO()
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
            CsvFileDTO newFile = new CsvFileDTO()
            {
                csvFileId = csvId,
                csvFileName = file.FileName,
                userId = "user",
                uploadDate = DateTime.Now.ToString(),
                firstTimeStamp = minMeasuredAt,
                lastTimeStamp = maxMeasuredAt

            };
            

            //saving changes
            
                context.CsvFiles.Add(newFile);
                context.SaveChanges();
            
           

            return Ok("CSV file converted to JSON and sent to database successfully.");
        }




    }
    }

