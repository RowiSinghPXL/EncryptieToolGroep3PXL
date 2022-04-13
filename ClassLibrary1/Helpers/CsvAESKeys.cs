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



    }
}
