using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library
{
    public partial class bookManage : Form
    {
        static string ConnectionString = @"Data Source = .;Initial Catalog = Library;" + "Integrated Security = True";
        DataSet dataset = new DataSet();//创建数据集
        SqlConnection conn = new SqlConnection(ConnectionString);  //创建新连接
        public bookManage()
        {
            InitializeComponent();
            showData();
        }

        public void showData() {
            string searchWord = "select id,bookname,type,press from Book where isdelete = 0 and isrent = 0";
            try
            {
                if(conn==null) conn.Open();
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
                textBox2.DataBindings.Clear();
                textBox3.DataBindings.Clear();
                textBox4.DataBindings.Clear();
                textBox1.DataBindings.Add("Text", dataset, "Library_search.id"); //数据绑定 
                textBox2.DataBindings.Add("Text", dataset, "Library_search.bookname"); //数据绑定 
                textBox3.DataBindings.Add("Text", dataset, "Library_search.press"); //数据绑定 
                textBox4.DataBindings.Add("Text", dataset, "Library_search.type"); //数据绑定 
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
            showData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string id;
            string bookname;
            string type;
            string press;

            id = textBox1.Text.Trim();
            bookname = textBox2.Text.Trim();
            press = textBox3.Text.Trim();
            type = textBox4.Text.Trim();

            if (bookname != "" && press != "" && type != "")
            {
                bookname = "'" + bookname + "'";
                press = "'" + press + "'";
                type = "'" + type + "'";

                string strSQL = "insert into book values(";
                strSQL += bookname + ",";
                strSQL += press + ",";
                strSQL += type + ",";
                strSQL += 0 + ",";
                strSQL += 0 + ")";

                SqlCommand command = null;

                try
                {
                    command = new SqlCommand(strSQL, conn);
                    conn.Open();
                    int n = command.ExecuteNonQuery();
                    if (n > 0) MessageBox.Show("成功添加了一本图书！","提示");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    if (conn != null) conn.Close();
                    command.Dispose();
                }
            }
            showData();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string id;
            string bookname;
            string type;
            string press;
            int index = dataGridView1.CurrentRow.Index; //获取当记录的索引号 
            SqlCommand command = null;

            id = textBox1.Text.Trim();
            bookname = textBox2.Text.Trim();
            press = textBox3.Text.Trim();
            type = textBox4.Text.Trim();

            string strSQL = "Update Book set ";
            if (bookname != "" && press != "" && type != "")
            {
                bookname = "'" + bookname + "'";
                press = "'" + press + "'";
                type = "'" + type + "'";

                strSQL += "bookname = " + bookname + ",";
                strSQL += "type = " + type + ",";
                strSQL += "press = " + press;
                strSQL += "where id = " + id;

            }
            else
            {
                MessageBox.Show("一些信息不能为空", "提示");
            }

            try
            {
                command = new SqlCommand();
                command.Connection = conn;
                command.CommandText = strSQL;
                conn.Open();
                int n = command.ExecuteNonQuery(); //执行SQL语句 
                if (n > 0) MessageBox.Show("成功更新图书数据，有" + n.ToString() + "本图书受到更新");
                showData();
                this.dataGridView1.CurrentCell = this.dataGridView1.Rows[index].Cells[0];
                dataGridView1.Rows[index].Selected = true; //加亮显示
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

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string id;
            int isdelete = 1;
            SqlCommand command = null;

            id = textBox1.Text.Trim();

            string strSQL = "Update Book set ";
            strSQL += "isdelete = " + isdelete;
            strSQL += "where id = " + id;
            try
            {
                command = new SqlCommand();
                command.Connection = conn;
                command.CommandText = strSQL;
                conn.Open();
                int n = command.ExecuteNonQuery(); //执行SQL语句 
                if (n > 0) MessageBox.Show("成功删除图书，有" + n.ToString() + "本受到删除");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.Close();
                command.Dispose();
            }
            showData();

        }
    }
}
