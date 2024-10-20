using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProject
{
    public partial class Form1 : Form
    {
        internal enum Grids
        { 
           Students,
           Programs,
           Courses,
           Enrollments
        }
        private bool OkToChange = true;

        private Grids grids;

        internal static Form1 current;
        public Form1()
        {
            current = this;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            new Form2();
            Form2.current.Visible = false;

            dataGridView1.Dock = DockStyle.Fill;
        }

        private void studentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OkToChange)
            {
                grids = Grids.Students;
                dataGridView1.ReadOnly = false;
                dataGridView1.AllowUserToAddRows = true;
                dataGridView1.AllowUserToDeleteRows = true;
                dataGridView1.RowHeadersVisible = true;
                dataGridView1.Dock = DockStyle.Fill;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                bindingSource3.DataSource = Data.Students.GetStudents();
                bindingSource3.Sort = "StId";
                dataGridView1.DataSource = bindingSource3;

                dataGridView1.Columns["StName"].HeaderText = "Student Name";
                dataGridView1.Columns["StId"].DisplayIndex = 0;
                dataGridView1.Columns["StName"].DisplayIndex = 1;
                dataGridView1.Columns["ProgId"].DisplayIndex = 2;
            }
        }

        private void enrollmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OkToChange)
            {
                grids = Grids.Enrollments;
                dataGridView1.ReadOnly = false;
                dataGridView1.AllowUserToAddRows = true;
                dataGridView1.AllowUserToDeleteRows = true;
                dataGridView1.RowHeadersVisible = true;
                dataGridView1.Dock = DockStyle.Fill;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                bindingSource4.DataSource = Data.Enrollments.GetDisplayEnrollments();
                bindingSource4.Sort = "StId , CId";
                dataGridView1.DataSource = bindingSource4;

                dataGridView1.Columns["StId"].HeaderText = "Student ID";
                dataGridView1.Columns["StId"].DisplayIndex = 0;
                dataGridView1.Columns["CId"].HeaderText = "Courses ID";
                dataGridView1.Columns["CId"].DisplayIndex = 1;
                dataGridView1.Columns["FinalGrade"].DisplayIndex = 2;
            }
        }

        private void coursesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OkToChange)
            {
                grids = Grids.Courses;
                dataGridView1.ReadOnly = false;
                dataGridView1.AllowUserToAddRows = true;
                dataGridView1.AllowUserToDeleteRows = true;
                dataGridView1.RowHeadersVisible = true;
                dataGridView1.Dock = DockStyle.Fill;
                dataGridView1.AutoSizeColumnsMode |= DataGridViewAutoSizeColumnsMode.Fill;

                bindingSource2.DataSource = Data.Courses.GetCourses();
                bindingSource2.Sort = "CId";
                dataGridView1.DataSource = bindingSource2;

                dataGridView1.Columns["CId"].HeaderText = "Courses ID";
                dataGridView1.Columns["CId"].DisplayIndex = 0;
                dataGridView1.Columns["CName"].DisplayIndex = 1;
                dataGridView1.Columns["ProgId"].DisplayIndex = 2;

            }
        }

        private void programsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OkToChange)
            {
                grids = Grids.Programs;
                dataGridView1.ReadOnly = false;
                dataGridView1.AllowUserToAddRows = true;
                dataGridView1.AllowUserToDeleteRows = true;
                dataGridView1.RowHeadersVisible = true;
                dataGridView1.Dock = DockStyle.Fill;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                bindingSource1.DataSource = Data.Programs.GetPrograms();
                bindingSource1.Sort = "ProgId";
                dataGridView1.DataSource = bindingSource1;

                dataGridView1.Columns["ProgId"].HeaderText = "Programs ID";
                dataGridView1.Columns["ProgId"].DisplayIndex = 0;
                dataGridView1.Columns["ProgName"].DisplayIndex = 1;



            }
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            if (BusinessLayer.Programs.UpdatePrograms() == -1)
            {
                bindingSource1.ResetBindings(false);
            }
        }

        private void bindingSource2_CurrentChanged(object sender, EventArgs e)
        {
            if (BusinessLayer.Courses.UpdateCourses() == -1)
            {
                bindingSource4.ResetBindings(false);
            }
        }

        private void bindingSource3_CurrentChanged(object sender, EventArgs e)
        {
            if (BusinessLayer.Students.UpdateStudents() == -1)
            { 
                bindingSource3.ResetBindings(false);
            }
        }

        private void bindingSource4_CurrentChanged(object sender, EventArgs e)
        {
            
        }

        private void menuStrip1_Click(object sender, EventArgs e)
        {
            OkToChange = true;
            Validate();

            if (grids == Grids.Programs)
            {
                if (BusinessLayer.Programs.UpdatePrograms() == -1)
                {
                    OkToChange = false;
                }
            }
            
            if (grids == Grids.Courses)
            {
                if (BusinessLayer.Courses.UpdateCourses() == -1)
                {
                    OkToChange = false;
                }
            }
            if (grids == Grids.Students)
            {
                if (BusinessLayer.Students.UpdateStudents() == -1)
                {
                    OkToChange = false;
                }
            }
        }
        internal static void BLLMessage(string s)
        {
            MessageBox.Show("Business Layer: " + s);
        }

        internal static void DALMessage(string s)
        {
            MessageBox.Show("Data Layer: " + s);
        }

        private void insertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2.current.Start(Form2.Modes.INSERT, null);
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection c = dataGridView1.SelectedRows;
            if (c.Count == 0)
            {
                MessageBox.Show("A line must be selected for update");
            }
            else if (c.Count > 1)
            {

                MessageBox.Show("Only one line must be selected for update");
            }
            else
            {
                if ("" + c[0].Cells["FinalGrade"].Value == "")
                {
                    Form2.current.Start(Form2.Modes.UPDATE, c);
                }
                else
                {
                    MessageBox.Show("To update this line, final grade value must be removed first.");
                }
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection c = dataGridView1.SelectedRows;
            if (c.Count == 0)
            {
                MessageBox.Show("At least one line must be selected for deletion");
            }
            else 
            {
                List<string[]> lId = new List<string[]>();
                bool hasGrades = false;
                for (int i = 0; i < c.Count; i++)
                {
                    string stId = "" + c[i].Cells["StId"].Value;
                    string cId = "" + c[i].Cells["CId"].Value;
                    string grade = "" + c[i].Cells["FinalGrade"].Value; 

                    if (!string.IsNullOrEmpty(grade))
                    {
                        hasGrades = true;
                        break;
                    }

                    lId.Add(new string[] { stId, cId });
                }

                if (hasGrades)
                {
                    MessageBox.Show("Cannot delete enrollments with assigned grades.");
                }
                else
                {
                    Data.Enrollments.DeleteData(lId);
                }

            }
        }
        private void finalGradeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection c = dataGridView1.SelectedRows;
            if (c.Count == 0)
            {
                MessageBox.Show("A line must be selected for final grade update");
            }
            else if (c.Count > 1)
            {
                MessageBox.Show("Only one line must be selected for update");
            }
            else
            {
                Form2.current.Start(Form2.Modes.FINALGRADE, c);
            }
        }
        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Impossible to insert / update / delete");
            e.Cancel = false;
            OkToChange = false;
        }

        
    }
}


