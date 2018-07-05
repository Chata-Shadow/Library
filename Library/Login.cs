using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Library
{
    public partial class Login : Form
    {
        string ConnectionString = @"Data Source = .;Initial Catalog = Library;" + "Integrated Security = True";
        public Login()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username;
            string password;
            string searchWord;
            int userid;
            int rolenum;

            DataSet dataset = new DataSet();//创建数据集
            //创建新连接
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            //连接数据库

            username = textBox1.Text.Trim();
            password = textBox2.Text.Trim();


            if ((username != "" || password != "") && (radioButton1.Checked == true || radioButton2.Checked == true))
            {
                try
                {
                    username = "'" + username + "'";
                    password = "'" + password + "'"; //要查询的列值一定要加单引号

                    searchWord = "select * from Login where username =" + username + " and password = "+ password + "and role =";
                    if (radioButton1.Checked == true) {
                        int role;
                        role = 1;
                        searchWord += role;
                    }
                    if (radioButton2.Checked == true)
                    {
                        int role;
                        role = 0;
                        searchWord += role;
                    }


                    SqlCommand sqlcomm = new SqlCommand(searchWord,conn);
                    sqlcomm.CommandType = CommandType.Text;
                    SqlDataReader reader = sqlcomm.ExecuteReader();

                    if (reader.Read()) {
                        userid = int.Parse(reader["id"].ToString());
                        rolenum = int.Parse(reader["role"].ToString());
                        MessageBox.Show("欢迎回来", "提示");
                        Library.main.userid = userid;
                        Library.main.rolenum = rolenum;
                        this.DialogResult = DialogResult.OK;
                        conn.Close();
                        this.Hide();
                    }
                    else {
                        MessageBox.Show("用户名或密码错误", "提示");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("用户名或密码不能为空", "提示");
            }
        }
    }
}
