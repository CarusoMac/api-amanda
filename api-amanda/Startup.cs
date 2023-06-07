
using api_amanda.DTOs;
using api_amanda.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using NetTopologySuite;
using Npgsql.EntityFrameworkCore.PostgreSQL.NetTopologySuite;
using NetTopologySuite.Geometries;

namespace api_amanda {
    public class Startup {
       

        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //services
        public void ConfigureServices(IServiceCollection services) {
            //basic
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            //cors
            services.AddCors(options =>
            {
                var frontendURL = Configuration.GetValue<string>("frontend_url");
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins(frontendURL).AllowAnyMethod().AllowAnyHeader().WithExposedHeaders(new string[] { "Content-Disposition", "TotalRecordsAmount" }); ;
                });
            });

            //db
            services.AddDbContext<ApplicationDbContext>(options => 
            {
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"),o=>o.UseNetTopologySuite());
            }
            ) ;

            //BTS coordinates load

            using (var serviceProvider = services.BuildServiceProvider()) {
                var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
                //coverting csv

                if (!dbContext.BtsCoordinates.Any()) {
                    var BtsfilePath = "C:\\Users\\Acer\\Documents\\samostudium\\IT\\oop\\weby\\React\\leaflet-project\\project-amanda\\api-amanda\\api-amanda\\Data\\BTScoor.csv";
                    var BtsRecords = ReadBtsFile(BtsfilePath);
                    //assigning values to newly creating record
                    foreach (var record in BtsRecords) {
                        double latitude = record.btsLat;
                        double longitude = record.btsLon;
                        var coordinate = new Coordinate(latitude, longitude);
                        CsvBtsDTO newBtsRecord = new()
                        {
                            cellid = record.cellid,
                            btsLat = record.btsLat,
                            btsLon = record.btsLon,
                            Location = new Point(coordinate) { SRID = 4326 }
                        };
                        dbContext.BtsCoordinates.Add(newBtsRecord);
                    }

                    dbContext.SaveChanges();

                }
            }
            //BTS coordinates end


        }
        public static BtsRecord[] ReadBtsFile(string filePath) {
            var lines = System.IO.File.ReadAllLines(filePath);
            var headerLine = lines.First();
            var propertyNames = headerLine.Split(',');
            var propertyCount = propertyNames.Length;

            var objects = new BtsRecord[lines.Length - 1];
            for (var i = 1; i < lines.Length; i++) {
                var values = lines[i].Split(',');
                var obj = new BtsRecord();
                for (var j = 0; j < propertyCount; j++) {
                    var propertyName = propertyNames[j].Trim();
                    var propertyValue = values[j].Trim();
                    var property = typeof(BtsRecord).GetProperty(propertyName);
                    if (property != null) {
                        property.SetValue(obj, Convert.ChangeType(propertyValue, property.PropertyType));
                    }
                }
                objects[i - 1] = obj;
            }

            return objects;
        }
            public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            //app.UseAuthorization();

            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

            });



        }
    }
}