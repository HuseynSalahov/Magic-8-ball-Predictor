using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Ultimate_Predictor
{
    public partial class Form1 : Form
    {
        private const string APP_NAME = "Ultimate Predictor";
        private readonly string PREDICTIONS_CONFIG_PATH = $"{Environment.CurrentDirectory}\\predictionsConfig.json";
        private string[] predictions;
        private Random random = new Random();
        public Form1()
        {
            InitializeComponent();
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private async void bPredict_Click(object sender, EventArgs e)
        {
            bPredict.Enabled = false;

            await Task.Run(() =>
            {
                for (int i = 1; i <= 100; i++)
                {
                    this.Invoke(new Action(() =>
                    {
                        UpdateProgressBar(i);
                        this.Text = $"{i}%"; 
                    }));

                    System.Threading.Thread.Sleep(20);
                }
            });

            var index = random.Next(predictions.Length);

            MessageBox.Show($"{predictions[index]}!");
            progressBar1.Value = 0;
            this.Text = APP_NAME;

            bPredict.Enabled = true;
        }

        private void UpdateProgressBar(int i)
        {
            if(i == progressBar1.Maximum)
            {
                progressBar1.Maximum = i + 1;
                progressBar1.Value = i + 1;
                progressBar1.Maximum = i;
            }
            else
            {
                progressBar1.Value = i + 1;
            }
            progressBar1.Value = i;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = APP_NAME;

            try
            {
                var data = File.ReadAllText(PREDICTIONS_CONFIG_PATH);

                predictions = JsonConvert.DeserializeObject<string[]>(data);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); 
            }
            finally
            {
                if(predictions == null)
                {
                    this.Close();
                }
                else if (predictions.Length == 0)
                {
                    MessageBox.Show("We ran out of predictions, movie's kaput! =)");
                    this.Close();
                }
            }
        }
    }
}
