using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Run_Python
{
    public class Python_Runner
    {
        public string python_path { get; set; } = @"C:\Users\admin\AppData\Local\Programs\Python\Python36\python.exe";
        
        public string script_path { get; set; } = @"E:\Deep Learning\Understanding Simple Speech Commands\SpeechRecognition\from_file.py";

        public List<string> arguments { get; set; } = new List<string>();

        public string errors { get; set; } = "";

        public string results { get; set; } = "";


        private string generate_argument_command()
        {
            string result = $"\"{script_path}\"";

            foreach (var item in arguments)
            {
                result += $" \"{item}\"";
            }

            return result;
        }

        public void run()
        {
            // 1) Create Process Info
            var psi = new ProcessStartInfo();
            psi.FileName = python_path;

            // 2) Create Script
            //var script = @"E:\Deep Learning\Understanding Simple Speech Commands\SpeechRecognition\from_file.py";
            //var start = "2019_10_30";
            psi.Arguments = generate_argument_command(); //$"\"{script_path}\"";

            // 3) Process Configuration
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;

            // 4) Execute process and get output

            using (var process = Process.Start(psi))
            {
                errors = process.StandardError.ReadToEnd();
                results = process.StandardOutput.ReadToEnd();

            }

        }
    }
}
