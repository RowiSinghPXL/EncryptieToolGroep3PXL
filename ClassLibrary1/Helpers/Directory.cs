using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ClassLibrary1.Helpers
{
    public static class Directory
    {
        public static string Path { get;} = @"c:\EncryptieToolGroep3";
       


        public static void InitDirectory()
        {
            //string path = @"c:\EncryptieToolGroep3";

            try
            {
                // Determine whether the directory exists.
                if (System.IO.Directory.Exists(Path))
                {
                    Console.WriteLine("That path exists already.");
                    return;
                }

                // Try to create the directory.
                DirectoryInfo di = System.IO.Directory.CreateDirectory(Path);
                Console.WriteLine("The directory was created successfully at {0}.", System.IO.Directory.GetCreationTime(Path));

                // Delete the directory.
                //di.Delete();
                //Console.WriteLine("The directory was deleted successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
            finally { }
        }

        

    }
}
