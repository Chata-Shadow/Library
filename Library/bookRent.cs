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
    public partial class bookRent : Form
    {
        static string ConnectionString = @"Data Source = .;Initial Catalog = Library;" + "Integrated Security = True";
        DataSet dataset = new DataSet();//创建数据集
        SqlConnection conn = new SqlConnection(ConnectionString);  //创建新连接
        public bookRent()
        {
            InitializeComponent();
            showData();
        }

        public void showData()
        {
            string searchWord = "select id,bookname,type,press from Book where isdelete = 0 and isrent = 0";
            try
            {
                if (conn == null) conn.Open();
                SqlDataAdapter DataAdapter = new SqlDataAdapter(searchWord, conn);

                if (dataGridView1.DataSource != null)
                {
                    while (this.dataGridView1.Rows.Count != 1)
                    {
                        this.dataGridView1.Rows.RemoveAt(0);
                    }
                }
                else
                {
                    dataGridView1.Rows.Clear();
                }

                DataAdapter.Fill(dataset, "Library_search");
                dataGridView1.DataSource = dataset;
                dataGridView1.DataMember = "Library_search";
                textBox1.DataBindings.Clear();
                textBox1.DataBindings.Add("Text", dataset, "Library_search.id");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }

        private void bookRent_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strSQL;
            string strSQL1;


            int userid = Library.main.userid;
            int bookid = int.Parse(textBox1.Text.Trim());

            strSQL = "insert into bookrent values(";
            strSQL +=  userid + ",";
            strSQL +=  bookid + ")";

            strSQL1 = "Update Book set ";
            strSQL1 += "isrent = 1 ";
            strSQL1 += " where id = " + bookid;

            int index = dataGridView1.CurrentRow.Index; //获取当记录的索引号 
            SqlCommand command = null;

            try
            {
                command = new SqlCommand();
                command.CommandText = strSQL;
                command.Connection = conn;
                conn.Open();
                command.ExecuteNonQuery();

                command.CommandText = strSQL1;
                command.Connection = conn;
                command.ExecuteNonQuery();

                int n = command.ExecuteNonQuery();
                if (n > 0) MessageBox.Show("成功借了一本图书！", "提示");
                showData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally {
                conn.Close();
                command.Dispose();
            }
            

        }
    }
}
