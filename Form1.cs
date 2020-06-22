using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
