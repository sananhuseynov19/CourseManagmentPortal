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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void st_create_Click(object sender, EventArgs e)
        {
            using (var connection = new SqlConnection(@"Server=DESKTOP-7Q105NQ\SQL; Database=CourseManagmentPortal;Trusted_Connection=True;"))
            {
                connection.Open();
                var cmd = new SqlCommand("insert into Students(st_Name,st_Surname,st_Birthdate,st_CreationTime,st_ModificationTime) values(@Name,@Surname,@Birthdate,@CreationTime,@ModificationTime)", connection);
                cmd.Parameters.Add(new SqlParameter("@Name", textBox1.Text));
                cmd.Parameters.Add(new SqlParameter("@Surname", textBox2.Text));
                cmd.Parameters.Add(new SqlParameter("@Birthdate", dateTimePicker1.Value));
                cmd.Parameters.Add(new SqlParameter("@CreationTime", DateTime.Now));
                cmd.Parameters.Add(new SqlParameter("@ModificationTime", DateTime.Now));
                //  cmd.ExecuteNonQuery();
                var sta = new SqlDataAdapter(cmd);
                DataTable dtb = new DataTable();
                sta.Fill(dtb);
                dataGridView1.DataSource = dtb;
                RefreshStudentList();
                MessageBox.Show("Student is added succesfuly");
            }
     
        }

         void RefreshStudentList()
        {
            using (var connection = new SqlConnection(@"Server=DESKTOP-7Q105NQ\SQL; Database=CourseManagmentPortal;Trusted_Connection=True;"))
            {
                connection.Open();

                var cmd = new SqlCommand("Select st_Id,st_Name,st_Surname,st_BirthDate,st_CreationTime,st_ModificationTime from Students", connection);

                SqlDataAdapter sdt = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sdt.Fill(dt);
                dataGridView1.DataSource = dt;
                connection.Close();
            }
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void st_Show_Click(object sender, EventArgs e)
        {
            RefreshStudentList();
        }

        private void st_update_Click(object sender, EventArgs e)
        {
            using (var connection = new SqlConnection(@"Server=DESKTOP-7Q105NQ\SQL; Database=CourseManagmentPortal;Trusted_Connection=True;"))
            {
                connection.Open();

                var cmd = new SqlCommand("update Students set st_Name=@Name,st_Surname=@Surname,st_BirthDate=@Birthdate,st_ModificationTime=@ModificationTime where st_Id=@Id", connection);

                cmd.Parameters.Add(new SqlParameter("@Id", dataGridView1.CurrentRow.Cells[0].Value.ToString()));
                cmd.Parameters.Add(new SqlParameter("@Name", textBox1.Text));
                cmd.Parameters.Add(new SqlParameter("@Surname", textBox2.Text));
                cmd.Parameters.Add(new SqlParameter("@Birthdate", dateTimePicker1.Value));
                cmd.Parameters.Add(new SqlParameter("@ModificationTime", DateTime.Now));
                cmd.ExecuteNonQuery();
                MessageBox.Show("Student is updated!!");
                RefreshStudentList();
            }
          
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            RefreshStudentList();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void st_delete_Click(object sender, EventArgs e)
        {
            using (var connection = new SqlConnection(@"Server=DESKTOP-7Q105NQ\SQL; Database=CourseManagmentPortal;Trusted_Connection=True;"))
            {
                connection.Open();

                var cmd = new SqlCommand("delete from Students where st_Id=@Id", connection);

                cmd.Parameters.Add(new SqlParameter("@Id", dataGridView1.CurrentRow.Cells[0].Value.ToString()));

                cmd.ExecuteNonQuery();
                MessageBox.Show("Student is deleted!!");
                RefreshStudentList();
            }
        }
    }
}
