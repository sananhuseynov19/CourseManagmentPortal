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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
          
          
            f3.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();
           
           
        }

      

        private void st_create_Click(object sender, EventArgs e)
        {
            using (var connection = new SqlConnection(@"Server=DESKTOP-7Q105NQ\SQL; Database=CourseManagmentPortal;Trusted_Connection=True;"))
            {
                connection.Open();

                var cmd = new SqlCommand("insert into Course(cs_Name,cs_Duration,cs_Price,cs_CreationTime,cs_ModificationTime) values(@Name,@Duration,@Price,@CreationTime,@ModificationTime)", connection);
                cmd.Parameters.Add(new SqlParameter("@Name", textBox1.Text));
                cmd.Parameters.Add(new SqlParameter("@Duration", textBox2.Text));
                cmd.Parameters.Add(new SqlParameter("@Price", textBox3.Text));
                cmd.Parameters.Add(new SqlParameter("@CreationTime", DateTime.Now));
                cmd.Parameters.Add(new SqlParameter("@ModificationTime", DateTime.Now));
                cmd.ExecuteNonQuery();
                RefreshCourseList();
                MessageBox.Show("Course is added succesfuly!");
            }
        }

        public void RefreshCourseList()
        {
            using (var connection = new SqlConnection(@"Server=DESKTOP-7Q105NQ\SQL; Database=CourseManagmentPortal;Trusted_Connection=True;"))
            {
                connection.Open();

                var cmd = new SqlCommand("Select * from Course", connection);
                cmd.ExecuteNonQuery();
                SqlDataAdapter sqd = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sqd.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            
        }

        private void st_Show_Click(object sender, EventArgs e)
        {
            RefreshCourseList();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshCourseList();
        }

        private void st_update_Click(object sender, EventArgs e)
        {
            using (var connection = new SqlConnection(@"Server=DESKTOP-7Q105NQ\SQL; Database=CourseManagmentPortal;Trusted_Connection=True;"))
            {
                connection.Open();

                var cmd = new SqlCommand("update Course set cs_Name=@Name,cs_Duration=@Duration,cs_Price=@Price,cs_ModificationTime=@ModificationTime where cs_id=@Id", connection);
                cmd.Parameters.Add(new SqlParameter("@Id", dataGridView1.CurrentRow.Cells[0].Value.ToString()));
                cmd.Parameters.Add(new SqlParameter("@Name", textBox1.Text));
                cmd.Parameters.Add(new SqlParameter("@Duration", textBox2.Text));
                cmd.Parameters.Add(new SqlParameter("@Price", textBox3.Text));
                cmd.Parameters.Add(new SqlParameter("@ModificationTime", DateTime.Now));
                cmd.ExecuteNonQuery();
                RefreshCourseList();
                MessageBox.Show("Course is updated!");
            }
        }

        private void st_delete_Click(object sender, EventArgs e)
        {
            var connection = new SqlConnection(@"Server=DESKTOP-7Q105NQ\SQL; Database=CourseManagmentPortal;Trusted_Connection=True;");

            connection.Open();

            var cmd = new SqlCommand("delete from Course where cs_id=@Id", connection);
            cmd.Parameters.Add(new SqlParameter("@Id", dataGridView1.CurrentRow.Cells[0].Value.ToString()));
            cmd.ExecuteNonQuery();
            RefreshCourseList();
            MessageBox.Show("Course id deleted!");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form5 f5 = new Form5();
            f5.ShowDialog();
        }

       
    }
}
