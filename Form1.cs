using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Windows.Forms;

namespace Run_Python
{
    public partial class Form1 : Form
    {

        Python_Runner python_runner = new Python_Runner();

        public Form1()
        {
            InitializeComponent();

            SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));

            var lst = new List<string>();
            lst.Add("FREEZE");
            lst.Add("PRINT");
            lst.Add("HOLD");
            lst.Add("TEST");

            Choices choice = new Choices(lst.ToArray());
            GrammarBuilder builder = new GrammarBuilder(choice);
            builder.Culture = new System.Globalization.CultureInfo("en-US");//??en-GB
            Grammar g = new Grammar(builder);
            recognizer.LoadGrammar(g);
            recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(recognizer_SpeechRecognized);
            try
            {
                recognizer.SetInputToDefaultAudioDevice();
                recognizer.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            RecognizedAudio audio = e.Result.Audio;
            
            TimeSpan start = audio.AudioPosition - audio.AudioPosition;
            TimeSpan duration = audio.Duration;

            // Add code to verify and persist the audio.  
            string path = @"E:\Deep Learning\Understanding Simple Speech Commands\nameAudio.wav";
            using (Stream outputStream = new FileStream(path, FileMode.Create))
            {
                RecognizedAudio nameAudio = audio.GetRange(start, duration);
                nameAudio.WriteToWaveStream(outputStream);
                outputStream.Close();
            }


            python_runner.python_path = @"C:\Users\admin\AppData\Local\Programs\Python\Python36\python.exe";
            python_runner.script_path = @"E:\Deep Learning\Understanding Simple Speech Commands\SpeechRecognition\from_file.py";

            python_runner.arguments.Add(path);

            python_runner.run();

            string sphinx_result = python_runner.results;


            python_runner.python_path = @"C:\Users\admin\AppData\Local\Programs\Python\Python36\python.exe";
            python_runner.script_path = @"E:\Deep Learning\Understanding Simple Speech Commands\deepspeech\client_from_file.py";

            python_runner.arguments.Add(path);

            python_runner.run();

            string deep_speech_result = python_runner.errors;

            textBox1.Text = e.Result.Text + "----------------" + sphinx_result + "----------------" + deep_speech_result;



            /*
            txt_out.AppendText(e.Result.Text + ", " + (int)(100 * e.Result.Confidence) + Environment.NewLine);

            //char beginer = (char)7;
            //char terminator = (char)10;
            //string message =beginer+getCommandForWord(e.Result.Text)+terminator;

            string message = getCommandForWord(e.Result.Text, e.Result.Confidence);

            var bytes = ASCIIEncoding.ASCII.GetBytes(message);

            foreach (var c in clients)
            {
                c.GetStream().Write(bytes, 0, bytes.Length);
            }*/
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            TB_file_path.Text = openFileDialog1.FileName;   
        }

        private void button2_Click(object sender, EventArgs e)
        {
            python_runner.python_path = @"C:\Users\admin\AppData\Local\Programs\Python\Python36\python.exe";
            python_runner.script_path = @"E:\Deep Learning\Understanding Simple Speech Commands\SpeechRecognition\from_file.py";

            python_runner.arguments.Add(TB_file_path.Text);

            python_runner.run();

            textBox1.Text = python_runner.results;
        }
    }
}
