using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using ClassLibrary1.Model;
using CsvHelper;

namespace ClassLibrary1.Helpers
{
    public static class Csv
    {
        public static string FileKeysAES { get; } = @"AesKey.csv";
        public static void InitAESKeyFile()
        {
            string headerFile = "Key, Iv";
            var csvPath = Directory.Path + @"\" + FileKeysAES;


            //var records = new List<AESKey> { };

            using (var writer = new StreamWriter(csvPath))
            using (var csv = new CsvWriter(writer, CultureInfo.CurrentCulture))
            {
                csv.WriteHeader<AESKey>();
               
            }
        }
    }
}
