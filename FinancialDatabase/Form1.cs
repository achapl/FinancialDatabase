using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinancialDatabase
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Make default selection "Item"
            comboBox1.SelectedIndex = 0;
        }

        // Remove escape characters
        private string formatQuery(string query)
        {
            string escapeChar = "^";
            // Add '^' before special characters ('*', '<', '>', .etc), 


            StringBuilder sb = new StringBuilder(query, 1024);
            int count = 0;
            // Note: for edge case, special char at start of string, copy swithc inside for loop, and modify it outside for case i=0
            for (int i = 1; i < query.Length; i++)
            {
                switch (query[i])
                {
                    case '*':
                    case '<':
                    case '>':
                    case '&':
                    case '"':
                        sb.Insert(i+count, escapeChar);
                        count++;
                        break;
                }
            }

            return sb.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();
            const string DEFAULTQUERY = "SELECT * FROM item;";

            // Activate Python Script 'DtbConnAndQuery.py' as process 'p'
            string query = this.textBox1.Text;
            if (query == "")
            {
                query = DEFAULTQUERY;
            }

            query = formatQuery(query);

            //Console.WriteLine(query);
            string cmdText = @"/K python C:\Users\Owner\source\repos\FinancialDatabaseSolution\FinancialDatabase\Python\Connection\DtbConnAndQuery.py " + query;
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = "CMD.exe";
            p.StartInfo.Arguments = cmdText;
            p.Start();
            

            // Show redirected output of process 'p's standard output
            string s   = p.StandardOutput.ReadLine();
            string eos = "EOS";     // end-of-stream
            
            // Until end of stream is shown, keep writing the next line to the console, and adding it to the listBox1

            while(s.CompareTo(eos) != 0)
            {                 
                Console.WriteLine(s);
                this.listBox1.Items.Add(s);
                s = p.StandardOutput.ReadLine();
            }

            // End process 'p'
            try
            {
                p.Kill();
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        // Search Button in Query Tab
        private void button2_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();
            string search = searchBox.Text;
            List <string> result = runSearch(search);
            for (int i = 0; i < result.Count; i++)
            {
                this.listBox1.Items.Add(result[i]);
            }
        }

        private List<string> runIndividualSearch(string term)
        {
            string query;

            if (term.CompareTo("") == 0){
                query = "SELECT * FROM item;";
            }else            {
                query = "SELECT * FROM item WHERE name LIKE '%" + term + "%';";
            }


            string cmdText = @"/K python C:\Users\Owner\source\repos\FinancialDatabaseSolution\FinancialDatabase\Python\Connection\DtbConnAndQuery.py " + query;
            System.Diagnostics.Process p = new System.Diagnostics.Process();

            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = "CMD.exe";
            p.StartInfo.Arguments = cmdText;
            p.Start();


            // Show redirected output of process 'p's standard output
            string s = p.StandardOutput.ReadLine();
            string eos = "EOS";     // end-of-stream
            List<string> retArr = new List<string>();
            // Until end of stream is shown, keep writing the next line to the console, and adding it to the listBox1
            while (s.CompareTo(eos) != 0)
            {
                Console.WriteLine(s);
                retArr.Add(s);
                s = p.StandardOutput.ReadLine();
            }

            // End process 'p'
            try
            {
                p.Kill();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return retArr;
        }


        
        private List<string> runSearch(string search)
        {
            if (search.CompareTo("") == 0)
            {
                
            }
            string[] terms = search.Split(' ');

            List<string> items = new List<string>();
            List<int>    hits  = new List<int>();
            List<Tuple<int,string>> combined = new List<Tuple<int, string>>();

            for(int i = 0; i < terms.Count(); i++)
            {
                Console.WriteLine(terms[i]);
                List <string> result = runIndividualSearch(terms[i]);
                for (int j = 0; j < result.Count; j++)
                {
                    if (!items.Contains(result[j]))
                    {
                        items.Add(result[j]);
                        hits.Add(1);
                    }
                    else
                    {
                        int index = items.IndexOf(result[j]);
                        hits[index]++;
                    }
                }
            }

            for(int i = 0; i < items.Count(); i++)
            {
                combined.Add(new Tuple<int, string>(hits[i], items[i]));
            }
            combined.Sort(delegate (Tuple<int,string>a, Tuple<int, string> b)
            {
                return b.Item1 - a.Item1;
            });
            for (int i = 0; i < items.Count(); i++)
            {
                items[i] = combined[i].Item2;
            }


            return items;
        }
    }
}
