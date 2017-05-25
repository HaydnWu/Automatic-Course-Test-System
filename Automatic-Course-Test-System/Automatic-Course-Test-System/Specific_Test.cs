﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace Automatic_Course_Test_System
{
    public partial class Specific_Test : Form
    {
        private string ceshiming;
        private string zhanghao;
        private string kaoshiming;
        private Form FatherForm = null;
        private bool Close = true;
        private string score;
        private int num = 0;
        private int nownum = 0;
        List<Class_Upload> anwser = new List<Class_Upload>();
        public Specific_Test(Form Sign_in)
        {
            InitializeComponent();
            this.FatherForm = Sign_in;
            Close = true;
           
            
        }
        public void gettest(string c,string k,string z)
        {
            ceshiming = c;
            kaoshiming = k;
            zhanghao = z;
            string html = "";
            try
            {
                Encoding encoding = Encoding.GetEncoding("utf-8");
                byte[] getWeatherUrl = encoding.GetBytes("http://169.254.0.52:81/Server_Sign.ashx?action=test&kemu=" + ceshiming + "&kaoshi=" + kaoshiming);
                HttpWebRequest webReq = (HttpWebRequest)HttpWebRequest.Create("http://169.254.0.52:81/Server_Sign.ashx?action=test&kemu=" + ceshiming + "&kaoshi=" + kaoshiming);
                webReq.Method = "post";
                webReq.ContentType = "text/xml";

                Stream outstream = webReq.GetRequestStream();
                outstream.Write(getWeatherUrl, 0, getWeatherUrl.Length);
                outstream.Flush();
                outstream.Close();

                webReq.Timeout = 2000;
                HttpWebResponse webResp = (HttpWebResponse)webReq.GetResponse();
                Stream stream = webResp.GetResponseStream();
                StreamReader sr = new StreamReader(stream, encoding);
                html = sr.ReadToEnd();
                sr.Close();
                stream.Close();


              

            }
            catch
            {
                MessageBox.Show("链接失败");
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Close = false;
            Results f = new Results(FatherForm);
            //提交

            //接受成绩
            f.getscore(ceshiming, kaoshiming, score);
            f.Show();
            this.Close();
        }

        private void Specific_Test_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Close == true)
                Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (nownum == num)
            {
                Class_Upload c = new Class_Upload();
                if (radioButton1.Checked)
                {
                    c.Choice_answer = "A";
                }
                else if (radioButton2.Checked)
                {
                    c.Choice_answer = "B";
                }
                else if (radioButton3.Checked)
                {
                    c.Choice_answer = "C";
                }
                else if (radioButton4.Checked)
                {
                    c.Choice_answer = "D";
                }
                else
                {
                    c.Answer = textBox2.Text;
                }
                
                c.Subject = ceshiming;
                c.Test = kaoshiming;
                c.Testnumber = Convert.ToString(num);
                num++;
                nownum++;
                anwser.Add(c);
            }
            else
            {
                Class_Upload c = new Class_Upload();
                if (radioButton1.Checked)
                {
                    c.Choice_answer = "A";
                }
                else if (radioButton2.Checked)
                {
                    c.Choice_answer = "B";
                }
                else if (radioButton3.Checked)
                {
                    c.Choice_answer = "C";
                }
                else if (radioButton4.Checked)
                {
                    c.Choice_answer = "D";
                }
                else
                {
                    c.Answer = textBox2.Text;
                }
                anwser[num].copyto(c);
                num++;
            }

            //显示题目
        }

        private void button1_Click(object sender, EventArgs e)
        {
            num--;
            //显示题目
        }
    }
}
