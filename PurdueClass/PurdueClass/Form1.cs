using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace SeatRemain
{
    public partial class Form1 : Form
    {
        WebClient client = new WebClient();
        System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Ryan\Desktop\a.txt");

        public Form1()
        {
            InitializeComponent();
            getSubject();
            //this.textBox1.Text = LoadProfilePage(@"http://www.google.com/");

        }


        public void getSeatRemain(int crn)
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            HtmlAgilityPack.HtmlWeb getHtml = new HtmlAgilityPack.HtmlWeb();
            try
            {
                doc = getHtml.Load("https://selfservice.mypurdue.purdue.edu/prod/bzwsrch.p_schedule_detail?term=CURRENT&crn=" + crn.ToString());
                for (int i = 0; i < 50; i++)
                    this.progressBar1.PerformStep();
                int count = 0;
                foreach (HtmlAgilityPack.HtmlNode link in doc.DocumentNode.SelectNodes("//td[@class=\"dddefault\"]"))
                {
                    this.progressBar1.PerformStep();
                    count++;
                    if (count == 2)
                        this.textBox1.Text = link.FirstChild.WriteTo();
                    else if (count == 3)
                        this.textBox2.Text = link.FirstChild.WriteTo();
                    else if (count == 4)
                        this.textBox3.Text = link.FirstChild.WriteTo();

                }
            }
            catch (Exception e) { }
        }

        public void getSubject()
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            HtmlAgilityPack.HtmlWeb getHtml = new HtmlAgilityPack.HtmlWeb();
            doc = getHtml.Load("https://selfservice.mypurdue.purdue.edu/prod/bzwsrch.p_search_catalog?term=CURRENT");
            //int count = 0;
            HtmlAgilityPack.HtmlNode link = doc.DocumentNode.SelectSingleNode("//select[@name=\"sel_subj\"]");
            foreach (HtmlAgilityPack.HtmlNode node in link.ChildNodes)
            {
                String[] s = node.InnerText.Split('-');
                if (!s[0].Equals(""))
                    this.comboBox1.Items.Add(s[0]);
            }
        }

        public void getCourses(String subject)
        {

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            HtmlAgilityPack.HtmlWeb getHtml = new HtmlAgilityPack.HtmlWeb();
            try
            {

                doc = getHtml.Load("https://selfservice.mypurdue.purdue.edu/prod/bzwsrch.p_search_catalog?term=CURRENT&subject=" + subject);
                foreach (HtmlAgilityPack.HtmlNode link in doc.DocumentNode.SelectNodes("//td[@class=\"nttitle\"]"))
                {
                    this.listBox1.Items.Add(link.FirstChild.InnerText);
                    String[] s = link.FirstChild.InnerText.Split(' ');
                    //file.WriteLine(link.OuterHtml);
                    
                    //file.Flush();
                }


                /*doc = getHtml.Load("https://selfservice.mypurdue.purdue.edu/prod/bzwsrch.p_search_schedule?term=CURRENT&subject=" + subject + "&campus=PWL&levl=UG");
                //int count = 0;
                
                
                
                String currentNum = null;
                
                foreach (HtmlAgilityPack.HtmlNode link in doc.DocumentNode.SelectNodes("//th[@class=\"ddlabel\"]"))
                {
                    String []s = link.FirstChild.InnerText.Split(new char[]{'-',' '});
                    String courseNum = s[s.Length-4];
                    
                    if (!courseNum.Equals(currentNum))
                    {
                        this.listBox1.Items.Add(courseNum);
                        currentNum = courseNum;
                    }
                }*/
            }
            catch (Exception e)
            {

            }
        }

        public void getCrn(String subject,String cnbr)
        {
            file.Flush();
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            HtmlAgilityPack.HtmlWeb getHtml = new HtmlAgilityPack.HtmlWeb();
            try
            {
                doc = getHtml.Load("https://selfservice.mypurdue.purdue.edu/prod/bzwsrch.p_search_schedule?term=CURRENT&subject=" + subject + "&campus=PWL&levl=UG&cnbr=" + cnbr);
                //int count = 0;




                foreach (HtmlAgilityPack.HtmlNode link in doc.DocumentNode.SelectNodes("//th[@class=\"ddlabel\"]"))
                {
                    String[] s = link.FirstChild.Attributes.First().Value.Split('=');
                    this.listBox2.Items.Add(s[s.Length-1]);
                }
            }catch(Exception e){
            }
        }

        
    }
}
