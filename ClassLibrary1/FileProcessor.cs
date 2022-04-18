using System;
using static System.Console;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1
{
    class FileProcessor
    {
        public string InputFilePath { get; }
        public FileProcessor(string filePath) => InputFilePath = filePath;

        public void Process()
        {
            WriteLine($"Begin process of {InputFilePath}");
        }
        
    }
}
