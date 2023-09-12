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

namespace CourseManagmnetPortal
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
          
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            using (var connection = new SqlConnection(@"Server=DESKTOP-7Q105NQ\SQL; Database=CourseManagmentPortal;Trusted_Connection=True;"))
            {
                connection.Open();
                comboBox1.Items.Clear();

                var cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "Select cs_Name from Course";
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    comboBox1.Items.Add(rd[0].ToString());
                }

                RefreshTeacherList();
            }
        }
        private void RefreshTeacherList()
        {
            using (var connection = new SqlConnection(@"Server=DESKTOP-7Q105NQ\SQL; Database=CourseManagmentPortal;Trusted_Connection=True;"))
            {
                connection.Open();
                var cmd = new SqlCommand("select * from Teachers", connection);
                SqlDataAdapter sdt = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sdt.Fill(dt);
                dataGridView1.DataSource = dt;

             
            }
        }

        private void st_Show_Click(object sender, EventArgs e)
        {
            RefreshTeacherList();
        }

        private void st_create_Click(object sender, EventArgs e)
        {
            using (var connection = new SqlConnection(@"Server=DESKTOP-7Q105NQ\SQL; Database=CourseManagmentPortal;Trusted_Connection=True;"))
            {
                connection.Open();
                var cmd = new SqlCommand("insert into Teachers(tc_Name,tc_Surname,tc_BirthDate,tc_Profession,tc_ModificationTime) values(@Name,@Surname,@Birthdate,@Profession,@ModificationTime)", connection);
                cmd.Parameters.Add(new SqlParameter("@Name", textBox1.Text));
                cmd.Parameters.Add(new SqlParameter("@Surname", textBox2.Text));
                cmd.Parameters.Add(new SqlParameter("@BirthDate", dateTimePicker1.Value));
                cmd.Parameters.Add(new SqlParameter("@Profession", comboBox1.SelectedItem));
                cmd.Parameters.Add(new SqlParameter("@ModificationTime", DateTime.Now));

                SqlDataAdapter sdt = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sdt.Fill(dt);
                dataGridView1.DataSource = dt;
                RefreshTeacherList();
                MessageBox.Show("Teacher is added succesfuly");
            }
        }

        private void st_update_Click(object sender, EventArgs e)
        {
            using (var connection = new SqlConnection(@"Server=DESKTOP-7Q105NQ\SQL; Database=CourseManagmentPortal;Trusted_Connection=True;"))
            {
                connection.Open();

                var cmd = new SqlCommand("update Teachers set tc_Name=@Name,tc_Surname=@Surname,tc_BirthDate=@Birthdate,tc_Profession=@Profession,tc_ModificationTime=@ModificationTime where tc_Id=@Id", connection);
                cmd.Parameters.Add(new SqlParameter("@Id", dataGridView1.CurrentRow.Cells[0].Value.ToString()));
                cmd.Parameters.Add(new SqlParameter("@Name", textBox1.Text));
                cmd.Parameters.Add(new SqlParameter("@Surname", textBox2.Text));
                cmd.Parameters.Add(new SqlParameter("@Birthdate", dateTimePicker1.Value));
                cmd.Parameters.Add(new SqlParameter("@Profession", comboBox1.SelectedItem));
                cmd.Parameters.Add(new SqlParameter("@ModificationTime", DateTime.Now));

                cmd.ExecuteNonQuery();

                RefreshTeacherList();
                MessageBox.Show("Teacher is updated!");
            }
        }

        private void st_delete_Click(object sender, EventArgs e)
        {
            using (var connection = new SqlConnection(@"Server=DESKTOP-7Q105NQ\SQL; Database=CourseManagmentPortal;Trusted_Connection=True;"))
            {
                connection.Open();

                var cmd = new SqlCommand("delete from  Teachers where tc_Id=@Id ", connection);
                cmd.Parameters.Add(new SqlParameter("@Id", dataGridView1.CurrentRow.Cells[0].Value.ToString()));
                cmd.ExecuteNonQuery();
                RefreshTeacherList();
                MessageBox.Show("Teacher is deleted!");
            }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
           comboBox1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();

        }
    }
}
