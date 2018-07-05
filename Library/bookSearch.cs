using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library
{
    public partial class bookSearch : Form
    {
        string ConnectionString = @"Data Source = .;Initial Catalog = Library;" + "Integrated Security = True";
        public bookSearch()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataSet dataset = new DataSet();//创建数据集
            //创建新连接
            SqlConnection conn = new SqlConnection(ConnectionString);
            //连接数据库

            string input = textBox1.Text.Trim();
            string searchWord = "select id,bookname,type,press from Book where isdelete = 0 and isrent = 0";
            if (input == "") {
                MessageBox.Show("查询条件不能为空", "提示");
                return;
            }

            input = "'" + input + "'";

            if (radioButton1.Checked || radioButton2.Checked)
            {
                if (radioButton1.Checked)
                {
                    searchWord += " and bookname = " + input;
                }

                if (radioButton2.Checked)
                {
                    searchWord += " and press = " + input;
                }

                try
                {
                    conn.Open();
                    SqlDataAdapter DataAdapter = new SqlDataAdapter(searchWord, conn);
                    DataAdapter.Fill(dataset, "Library_search");
                    dataGridView1.DataSource = dataset;
                    dataGridView1.DataMember = "Library_search";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }


            }
            else
            {
                MessageBox.Show("请至少选择一种查询方式", "提示");
            }
        }
    }
}
