using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using ClassLibrary1.Model;
using CsvHelper;

namespace ClassLibrary1.Helpers
{
    public static class CsvAESKeys
    {
        public static string FileKeysAES { get; } = @"AesKey.csv";
        public static string FullPath { get; } = Directory.Path + @"\" + FileKeysAES;
        public static List<AESKey> Keys { get; set; }
        public static void InitAESKeyFile()
        {
            string headerFile = "Key;Iv";
            //var csvPath = Directory.Path + @"\" + FileKeysAES;


            //var records = new List<AESKey> { };

            try
            {
                if (!File.Exists(FullPath)) { 
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(FullPath, true))
                    {
                        file.WriteLine(headerFile);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Fout bij het initialiseren van het csv bestand", ex);
            }
        }

        public static void AESKeyaddRecord(string key, string iv)
        {
            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(FullPath, true))
                {
                    file.WriteLine(key + ";" + iv);
                }
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Fout bij het opslaan van de key en iv", ex);
            }
        }

        public static List<string> GetAESKeyRecords()
        {
            List<string> keys = new List<string>();
         
                    foreach(var r in Keys)
                    {
                        keys.Add($"Key: {r.Key} - Iv: {r.Iv}");
                    }
                    return keys;
                        
            
            


        }

        private static List<TSource> ToList<TSource>(this IEnumerable<TSource> source)
        {
              return new List<TSource>(source);
            
           
        }

        public static void InitKeyRecords()
        {
            List<AESKey> keys = new List<AESKey>();
            using (var reader = new StreamReader(FullPath))
            {
                using (var csv = new CsvReader(reader, CultureInfo.CurrentCulture))
                {
                    var records = csv.GetRecords<AESKey>();

                    foreach(var r in records)
                    {
                        keys.Add(new AESKey { Key = r.Key, Iv = r.Iv });
                    }

                    Keys = keys;

                }
            }
          
        }








    }
}
