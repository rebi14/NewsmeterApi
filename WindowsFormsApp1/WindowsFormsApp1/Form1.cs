using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        ResultView re;

        public Form1()
        {
            re = new ResultView();


            InitializeComponent();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show(this, "Enter topic please.");
                return;
            }
            else if (numericUpDown1.Value == 0)
            {
                MessageBox.Show(this, "Enter MaxResult please.");
                return;
            }
            else if (dateTimePicker1.Value.Date == dateTimePicker2.Value.Date)
            {
                MessageBox.Show(this, "Change start time or end time, please.");
                return;
            }
            string searchAll = textBox1.Text;
            string searchAny = textBox2.Text;
            string searchExclude = textBox3.Text;
            string start = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string end = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            string lang = comboBox1.Text;
            decimal maxResult = numericUpDown1.Value;
            long maxid = -1;
            long minid = -1;
            re = ResultViewMethods.Post(searchAll, searchAny, searchExclude, start, end, maxResult, maxid, minid, lang);
            dataGridView1.DataSource = re.Results;


        }
       

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value >= dateTimePicker2.Value)
                dateTimePicker2.Value = dateTimePicker1.Value.AddDays(1);
        }


        private void NextButton_Click(object sender, EventArgs e)
        {

            string searchAll = textBox1.Text;
            string searchAny = textBox2.Text;
            string searchExclude = textBox3.Text;
            string start = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string end = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            string lang = comboBox1.Text;
            decimal maxResult = numericUpDown1.Value;
            long maxid = -1;
            long minid = -1;
            if (re != null && re.Results != null && re.Results.Count > 0)
            {
                minid = re.Results.Min(x => x.NewsId);
                re = ResultViewMethods.Post(searchAll, searchAny, searchExclude, start, end, maxResult, minid, maxid, lang);
                if (re.Results.Count == 1)
                {
                    MessageBox.Show("there isn't new news");
                }
                else
                {
                    dataGridView1.DataSource = re.Results;
                }
                
            }
            
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            string searchAll = textBox1.Text;
            string searchAny = textBox2.Text;
            string searchExclude = textBox3.Text;
            string start = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string end = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            string lang = comboBox1.Text;
            decimal maxResult = numericUpDown1.Value;
            long maxid = -1;
            long minid = -1;
            if (re != null && re.Results != null && re.Results.Count > 0)
            {
                maxid = re.Results.Max(x => x.NewsId);
                re = ResultViewMethods.Post(searchAll, searchAny, searchExclude, start, end, maxResult, minid, maxid, lang);
               
                if (re.Results.Count == 1)
                {
                    MessageBox.Show("there isn't new news");
                }
                else
                {
                    dataGridView1.DataSource = re.Results;
                }
            }
            

        }

        private void button4_Click(object sender, EventArgs e)
        {
            ResultViewMethods rmv = new ResultViewMethods();
            // rmv.InsertNews(re);

            rmv.insertByEfx(re);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            EntityFrameWorkForm form2 = new EntityFrameWorkForm();
            form2.Show();
        }
    }
}
