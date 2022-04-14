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
            string headerFile = "Name;Key;Iv";
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

        public static void AESKeyaddRecord(string name, string key, string iv)
        {
            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(FullPath, true))
                {
                    file.WriteLine(name + ";" + key + ";" + iv);
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
                        keys.Add($"Name: {r.Name} - Key: {r.Key} - Iv: {r.Iv}");
                    }
                    return keys;
                        
            
            


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
                        keys.Add(new AESKey {Name = r.Name, Key = r.Key, Iv = r.Iv });
                    }

                    Keys = keys;

                }
            }
          
        }






    }
}
