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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            using (var connection = new SqlConnection(@"Server=DESKTOP-7Q105NQ\SQL; Database=CourseManagmentPortal;Trusted_Connection=True;"))
            {
                connection.Open();

                var cmd = new SqlCommand("select cs_Name from Course", connection);

                var sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    comboBox1.Items.Add(sdr[0].ToString());
                }
                sdr.Close();

                var cmd1 = new SqlCommand("select st_Name+' '+st_Surname from Students", connection);
                var sdr1 = cmd1.ExecuteReader();
                while (sdr1.Read())
                {
                    comboBox2.Items.Add(sdr1[0].ToString());
                }
                sdr1.Close();

               
            }
            RefreshPlanningCourse();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var connection = new SqlConnection(@"Server=DESKTOP-7Q105NQ\SQL; Database=CourseManagmentPortal;Trusted_Connection=True;"))
            {
                connection.Open();
                var cmd1 = new SqlCommand("Select cs_Price from Course where cs_Name=@Name", connection);
                cmd1.Parameters.Add(new SqlParameter("@Name", comboBox1.SelectedItem));


                var cmd2 = new SqlCommand("Select cs_Duration from Course where cs_Name=@Name", connection);
                cmd2.Parameters.Add(new SqlParameter("@Name", comboBox1.SelectedItem));

                var cmd3 = new SqlCommand("select tc_Name+' '+tc_Surname from Teachers where tc_Profession=@Prof", connection);
                cmd3.Parameters.Add(new SqlParameter("@Prof", comboBox1.SelectedItem));


                var cmd = new SqlCommand("insert into PlanningCourse(Course,Student,Teacher,Duration,Price,PlannedStartingDate,StartedDate) " +
                    "values(@Course,@Student,@Teacher,@Duration,@Price,@PlannedStartingDate,null)", connection);
                cmd.Parameters.Add(new SqlParameter("@Course", comboBox1.SelectedItem));
                cmd.Parameters.Add(new SqlParameter("@Student", comboBox2.SelectedItem));

                var sdr3 = cmd3.ExecuteReader();
                while (sdr3.Read())
                {
                    var f = sdr3[0].ToString();
                    cmd.Parameters.Add(new SqlParameter("@Teacher", f));
                }
                sdr3.Close();

                cmd.Parameters.Add(new SqlParameter("@PlannedStartingDate", dateTimePicker1.Value));
                var sdr = cmd1.ExecuteReader();
                while (sdr.Read())
                {
                    var s = sdr[0].ToString();
                    cmd.Parameters.Add(new SqlParameter("@Price", s));
                }
                sdr.Close();
                var sd2 = cmd2.ExecuteReader();
                while (sd2.Read())
                {
                    var d = sd2[0].ToString();
                    cmd.Parameters.Add(new SqlParameter("@Duration", d));
                }
                sd2.Close();

                cmd.ExecuteNonQuery();
                RefreshPlanningCourse();
                MessageBox.Show("Course is planned!");
            }
        }

        private void RefreshPlanningCourse()
        {
            var connection = new SqlConnection(@"Server=DESKTOP-7Q105NQ\SQL; Database=CourseManagmentPortal;Trusted_Connection=True;");

            connection.Open();

            var cmd = new SqlCommand("Select * from PlanningCourse order by Course", connection);

            var sda = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            comboBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();

            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var connection = new SqlConnection(@"Server=DESKTOP-7Q105NQ\SQL; Database=CourseManagmentPortal;Trusted_Connection=True;"))
            {
                connection.Open();

                var cmd1 = new SqlCommand("select COUNT(Student) from PlanningCourse where Course=@C", connection);
                cmd1.Parameters.Add(new SqlParameter("@C", comboBox1.SelectedItem));

                var b = Convert.ToInt32(cmd1.ExecuteScalar());
                if (b >= 5)
                {
                    var cmd = new SqlCommand("Update PlanningCourse set StartedDate=@Date where Course=@C", connection);
                    cmd.Parameters.Add(new SqlParameter("@Date", DateTime.Now));
                    cmd.Parameters.Add(new SqlParameter("@C", comboBox1.SelectedItem));
                    cmd.ExecuteNonQuery();
                    RefreshPlanningCourse();
                    MessageBox.Show("Course is started!");

                }
                else
                {
                    MessageBox.Show("Student count can't be less than 5");
                }

            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (var connection = new SqlConnection(@"Server=DESKTOP-7Q105NQ\SQL; Database=CourseManagmentPortal;Trusted_Connection=True;"))
            {
                connection.Open();

                var cmd = new SqlCommand("Delete from PlanningCourse where Student=@St", connection);
                cmd.Parameters.Add(new SqlParameter("@St", comboBox2.SelectedItem));
                cmd.ExecuteNonQuery();
                RefreshPlanningCourse();
                MessageBox.Show("Select Course is Deleted!!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (var connection = new SqlConnection(@"Server=DESKTOP-7Q105NQ\SQL; Database=CourseManagmentPortal;Trusted_Connection=True;"))
            {
                connection.Open();

                var cmd = new SqlCommand("select *from PlanningCourse where StartedDate is not null", connection);

                var sda = new SqlDataAdapter(cmd);

                var dt = new DataTable();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (var connection = new SqlConnection(@"Server=DESKTOP-7Q105NQ\SQL; Database=CourseManagmentPortal;Trusted_Connection=True;"))
            {
                connection.Open();

                var cmd = new SqlCommand("select *from PlanningCourse where StartedDate is  null", connection);

                var sda = new SqlDataAdapter(cmd);

                var dt = new DataTable();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            RefreshPlanningCourse();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
