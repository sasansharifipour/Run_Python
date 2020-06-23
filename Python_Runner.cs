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
        public string python_path { get; set; } = "";
        
        public string script_path { get; set; } = "";

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
            var psi = new ProcessStartInfo();
            psi.FileName = python_path;

            psi.Arguments = generate_argument_command();

            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;

            using (var process = Process.Start(psi))
            {
                errors = process.StandardError.ReadToEnd();
                results = process.StandardOutput.ReadToEnd();
            }

        }
    }
}
