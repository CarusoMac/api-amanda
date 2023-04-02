using System;

namespace api_amanda.Entities {
    public static class CsvFileReader {
        public static CsvRecord[] ReadCsvFile(string filePath) {
            var lines = System.IO.File.ReadAllLines(filePath);
            var headerLine = lines.First();
            var propertyNames = headerLine.Split(',');
            var propertyCount = propertyNames.Length;

            var objects = new CsvRecord[lines.Length - 1];
            for (var i = 1; i < lines.Length; i++) {
                var values = lines[i].Split(',');
                var obj = new CsvRecord();
                for (var j = 0; j < propertyCount; j++) {
                    var propertyName = propertyNames[j].Trim();
                    var propertyValue = values[j].Trim();
                    var property = typeof(CsvRecord).GetProperty(propertyName);
                    if (property != null) {
                        property.SetValue(obj, Convert.ChangeType(propertyValue, property.PropertyType));
                    }
                }
                objects[i - 1] = obj;
            }

            return objects;
        }
    }
}
