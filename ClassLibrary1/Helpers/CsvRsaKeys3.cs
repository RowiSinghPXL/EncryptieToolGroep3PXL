using ClassLibrary1.Model;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace ClassLibrary1.Helpers
{
    public static class CsvRsaKeys3
    {
        public static string FilePath { get; set; } = "";
        public static string FileKeysRSA { get; } = @"RsaKey.csv";
        public static string FullPath { get; } = Directory.Path + @"\" + FileKeysRSA;
        public static List<RSAKey> Keys { get; set; }
        public static void InitRSAKeyFile()
        {
            string headerFile = "Naam;PublicKey;PrivateKey";
            //var csvPath = Directory.Path + @"\" + FileKeysAES;


            //var records = new List<AESKey> { };

            try
            {
                if (!File.Exists(FullPath))
                {
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

        public static void RSAKeyaddRecord(string name, string publicKey, string privateKey)
        {
            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(FullPath, true))
                {
                    file.WriteLine(name + ";" + publicKey + ";" + privateKey);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Fout bij het opslaan van de key en iv", ex);
            }
        }

        public static List<string> GetRSAKeyRecords()
        {
            List<string> keys = new List<string>();

            foreach (var r in Keys)
            {
                keys.Add($"Name: {r.Naam} \nPublicKey: {r.PublicKey} \nPrivateKey: {r.PrivateKey}\n--------------------------");
            }
            return keys;





        }



        public static void InitKeyRecords()
        {
            List<RSAKey> keys = new List<RSAKey>();
            using (var reader = new StreamReader(FullPath))
            {
                using (var csv = new CsvReader(reader, CultureInfo.CurrentCulture))
                {
                    var records = csv.GetRecords<RSAKey>();

                    foreach (var r in records)
                    {
                        keys.Add(new RSAKey { Naam = r.Naam, PublicKey = r.PublicKey, PrivateKey = r.PrivateKey });
                    }

                    Keys = keys;

                }
            }

        }
    }
}
