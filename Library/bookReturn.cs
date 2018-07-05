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
    public partial class bookReturn : Form
    {
        static string ConnectionString = @"Data Source = .;Initial Catalog = Library;" + "Integrated Security = True";
        DataSet dataset = new DataSet();//创建数据集
        SqlConnection conn = new SqlConnection(ConnectionString);  //创建新连接
        int userid = Library.main.userid;
        public bookReturn()
        {
            InitializeComponent();
            showData();
        }

        public void showData()
        {

            string searchWord = "select Book.id,book.bookname from Book,Bookrent where Book.id = Bookrent.bookid and bookrent.userid = ";
            searchWord += userid;

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

                DataAdapter.Fill(dataset, "Rent_search");
                dataGridView1.DataSource = dataset;
                dataGridView1.DataMember = "Rent_search";
                label2.DataBindings.Clear();
                label2.DataBindings.Add("Text", dataset, "Rent_search.id");
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
        private void button1_Click(object sender, EventArgs e)
        {
            int bookid = int.Parse(label2.Text.Trim());
            string strSQL;
            string strSQL1;
            SqlCommand command = null;

            strSQL = "delete from bookrent where bookid = ";
            strSQL += bookid;

            strSQL1 = "Update Book set ";
            strSQL1 += "isrent = 0 ";
            strSQL1 += " where id = " + bookid;

            try
            {
                command = new SqlCommand();
                command.CommandText = strSQL;
                command.Connection = conn;
                conn.Open();
                command.ExecuteNonQuery();

                command.CommandText = strSQL1;

                int n = command.ExecuteNonQuery();
                if (n > 0) MessageBox.Show("成功归还了一本图书！", "提示");
                showData();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.Close();
                command.Dispose();

            }
            
        }
    }
}
