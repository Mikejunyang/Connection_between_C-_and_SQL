using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Drawing.Imaging;

namespace Data
{
    internal class Connect
    {
        private static String cliComConnectionString = GetConnectString();
        internal static String ConnectionString { get => cliComConnectionString; }
        private static String GetConnectString()
        { 
           SqlConnectionStringBuilder cs = new SqlConnectionStringBuilder();
           cs.DataSource = "(local)";
           cs.InitialCatalog = "College1en";
           cs.UserID = "sa";
           cs.Password = "sysadm";
           return cs.ConnectionString;
        }
    }

    internal class DataTables
    {
        private static SqlDataAdapter adapterStudents = InitAdapterStudents();
        private static SqlDataAdapter adapterEnrollments = InitAdapterEnrollments();
        private static SqlDataAdapter adapterCourses = InitAdapterCourses();
        private static SqlDataAdapter adapterPrograms = InitAdapterPrograms();
        private static SqlDataAdapter adapterDisplayEnrollments = InitAdapterDisplayEnrollments();

        private static DataSet ds = InitDataSet();

        private static SqlDataAdapter InitAdapterStudents()
        {
            SqlDataAdapter r = new SqlDataAdapter(
                "SELECT * FROM students ORDER BY StId",
                Connect.ConnectionString);
            SqlCommandBuilder builder = new SqlCommandBuilder(r);
            r.UpdateCommand = builder.GetUpdateCommand();

            return r;
        }

        private static SqlDataAdapter InitAdapterPrograms()
        {
            SqlDataAdapter r = new SqlDataAdapter(
                "SELECT * FROM Programs ORDER BY ProgId",
                Connect.ConnectionString);
            SqlCommandBuilder builder = new SqlCommandBuilder(r);
            r.UpdateCommand = builder.GetUpdateCommand();

            return r;
        }
        private static SqlDataAdapter InitAdapterCourses()
        {
            SqlDataAdapter r = new SqlDataAdapter(
                "SELECT * FROM Courses ORDER BY CId",
                Connect.ConnectionString);
            SqlCommandBuilder builder = new SqlCommandBuilder(r);
            r.UpdateCommand = builder.GetUpdateCommand();

            return r;
        }
        private static SqlDataAdapter InitAdapterEnrollments()
        {
            SqlDataAdapter r = new SqlDataAdapter(
                "SELECT * FROM Enrollments ORDER BY StId, CId",
                Connect.ConnectionString);
            SqlCommandBuilder builder = new SqlCommandBuilder(r);
            r.UpdateCommand = builder.GetUpdateCommand();

            return r;
        }

        private static SqlDataAdapter InitAdapterDisplayEnrollments()
        {
            SqlDataAdapter r = new SqlDataAdapter(
                "SELECT s.StId , s.StName , c.CId , c.CName , e.FinalGrade " +
                "FROM Students s , Courses c , Enrollments e  " +
                "WHERE e.StId = s.StId AND e.CId = c.CId  " +
                "ORDER BY StId , CId ",
                Connect.ConnectionString);

            return r;
        }

        private static DataSet InitDataSet()
        {
            DataSet ds = new DataSet();
            loadPrograms(ds);
            loadStudents(ds);
            loadCourses(ds);
            loadEnrollments(ds);
            loadDisplayEnrollments(ds);
            return ds;
        }

        private static void loadStudents(DataSet ds)
        {
            adapterStudents.Fill(ds, "Students");

            ds.Tables["Students"].Columns["StId"].AllowDBNull = false;
            ds.Tables["Students"].Columns["StName"].AllowDBNull = false;

            ds.Tables["Students"].PrimaryKey = new DataColumn[1]
                { ds.Tables["Students"].Columns["StId"]};
        }
        private static void loadEnrollments(DataSet ds)
        {
            adapterEnrollments.Fill(ds, "Enrollments");

            ds.Tables["Enrollments"].Columns["StId"].AllowDBNull = false;
            ds.Tables["Enrollments"].Columns["CId"].AllowDBNull = false;

            ds.Tables["Enrollments"].PrimaryKey = new DataColumn[2]
            {
                ds.Tables["Enrollments"].Columns["StId"],
                ds.Tables["Enrollments"].Columns["CId"] 
            };
        
            ForeignKeyConstraint myFK01 = new ForeignKeyConstraint("MyFK01",
                new DataColumn[] {
                    ds.Tables["Students"].Columns["StId"]
                },
                new DataColumn[] {
                    ds.Tables["Enrollments"].Columns["StId"]
                }
            );
            myFK01.DeleteRule = Rule.Cascade;
            myFK01.UpdateRule = Rule.Cascade;
            ds.Tables["Enrollments"].Constraints.Add(myFK01);

            ForeignKeyConstraint myFK02 = new ForeignKeyConstraint("MyFK02",
                new DataColumn[] {
                    ds.Tables["Courses"].Columns["CId"]
                },
                new DataColumn[] {
                    ds.Tables["Enrollments"].Columns["CId"]
                }
            );
            myFK02.DeleteRule = Rule.None;
            myFK02.UpdateRule = Rule.Cascade;
            ds.Tables["Enrollments"].Constraints.Add(myFK02);

        }



        private static void loadCourses(DataSet ds)
        {
            adapterCourses.Fill(ds, "Courses");

            ds.Tables["Courses"].Columns["CId"].AllowDBNull = false;
            ds.Tables["Courses"].Columns["CName"].AllowDBNull = false;

            ds.Tables["Courses"].PrimaryKey = new DataColumn[1]
                    { ds.Tables["Courses"].Columns["CId"]};
        }

        private static void loadPrograms(DataSet ds)
        {
            adapterPrograms.Fill(ds, "Programs");
            ds.Tables["Programs"].Columns["ProgId"].AllowDBNull = false;
            ds.Tables["Programs"].Columns["ProgName"].AllowDBNull = false;

            ds.Tables["Programs"].PrimaryKey = new DataColumn[1]
                    { ds.Tables["Programs"].Columns["ProgId"]};
        }

        private static void loadDisplayEnrollments(DataSet ds)
        {
            adapterDisplayEnrollments.Fill(ds, "DisplayEnrollments");

            ForeignKeyConstraint myFK01 = new ForeignKeyConstraint("MyFK01",
                new DataColumn[] {
                    ds.Tables["Enrollments"].Columns["StId"],
                    ds.Tables["Enrollments"].Columns["CId"]
                },
                new DataColumn[] {
                    ds.Tables["DisplayEnrollments"].Columns["StId"],
                    ds.Tables["DisplayEnrollments"].Columns["CId"]
                }
            );
            myFK01.DeleteRule = Rule.Cascade;
            myFK01.UpdateRule = Rule.Cascade;
            ds.Tables["DisplayEnrollments"].Constraints.Add(myFK01);
        }

        internal static SqlDataAdapter getAdapterStudents()
        {
            return adapterStudents;
        }
        internal static SqlDataAdapter getAdapterEnrollments()
        {
            return adapterEnrollments;
        }
        internal static SqlDataAdapter getAdapterCourses()
        {
            return adapterCourses;
        }
        internal static SqlDataAdapter getAdapterPrograms()
        {
            return adapterPrograms;
        }
        internal static SqlDataAdapter getAdapterDisplayEnrollments()
        {
            return adapterDisplayEnrollments;
        }
        internal static DataSet getDataSet()
        {
            return ds;
        }

    }
        internal class Programs
        {
            private static SqlDataAdapter adapter = DataTables.getAdapterPrograms();
            private static DataSet ds = DataTables.getDataSet();

            internal static DataTable GetPrograms()
            {
                return ds.Tables["Programs"];
            }

            internal static int UpdatePrograms()
            {
                if (!ds.Tables["Programs"].HasErrors)
                {
                    return adapter.Update(ds.Tables["Programs"]);
                }
                else
                {
                    return -1;
                }
            }
        }
        internal class Courses
        {
            private static SqlDataAdapter adapter = DataTables.getAdapterCourses();
            private static DataSet ds = DataTables.getDataSet();

            internal static DataTable GetCourses()
            {
                return ds.Tables["Courses"];
            }

            internal static int UpdateCourses()
            {
                if (!ds.Tables["Courses"].HasErrors)
                {
                    return adapter.Update(ds.Tables["Courses"]);
                }
                else
                {
                    return -1;
                }
            }
        }
        internal class Students
        {
            private static SqlDataAdapter adapter = DataTables.getAdapterStudents();
            private static DataSet ds = DataTables.getDataSet();

            internal static DataTable GetStudents()
            {
                return ds.Tables["Students"];
            }

            internal static int UpdateStudents()
            {
                if (!ds.Tables["Students"].HasErrors)
                {
                    return adapter.Update(ds.Tables["Students"]);
                }
                else
                {
                    return -1;
                }
            }
        }
        internal class Enrollments
        {
            private static SqlDataAdapter adapter = DataTables.getAdapterEnrollments();
            private static DataSet ds = DataTables.getDataSet();

            private static DataTable displayEnrollments = null;

            internal static DataTable GetDisplayEnrollments()
            {
                displayEnrollments = ds.Tables["DisplayEnrollments"];
                return displayEnrollments;
            }

            internal static int InsertData(string[] a)
            {
                var test = (
                     from enrollments in ds.Tables["Enrollments"].AsEnumerable()
                     where enrollments.Field<string>("StId") == a[0]
                     where enrollments.Field<string>("CId") == a[1]
                     select enrollments);
                if (test.Count() > 0)
                {
                    FinalProject.Form1.DALMessage("This assignment already exists");
                    return -1;
                }
                try
                {
                    DataRow line = ds.Tables["Enrollments"].NewRow();
                    line.SetField("StId", a[0]);
                    line.SetField("CId", a[1]);
                    ds.Tables["Enrollments"].Rows.Add(line);

                    adapter.Update(ds.Tables["Enrollments"]);

                    if (displayEnrollments != null)
                    {
                        var query = (
                            from students in ds.Tables["Students"].AsEnumerable()
                            from courses in ds.Tables["Courses"].AsEnumerable()
                            where students.Field<string>("StId") == a[0]
                            where courses.Field<string>("CId") == a[1]
                            select new
                            {
                                StId = students.Field<string>("StId"),
                                StName = students.Field<string>("StName"),
                                CId = courses.Field<string>("CId"),
                                CName = courses.Field<string>("CName"),
                                FinalGrade = line.Field<Nullable<int>>("FinalGrade")
                            });
                        var r = query.Single();
                        displayEnrollments.Rows.Add(new object[] { r.StId, r.StName, r.CId, r.CName, r.FinalGrade });
                    }
                    return 0;
                }
                catch (Exception)
                {
                    FinalProject.Form1.DALMessage("Insertion / Update rejected");
                    return -1;
                }
            }
            internal static int UpdateData(string[] a)
            {
                return 0;
            }

            internal static int DeleteData(List<string[]> eId)
            {
            try
            {
                var enrollmentsTable = ds.Tables["Enrollments"];
                var hasAssignedGrade = enrollmentsTable.AsEnumerable()
                        .Any(row => eId.Any(id => id[0] == row.Field<string>("StId") &&
                                                  id[1] == row.Field<string>("CId") &&
                                                  row.Field<string>("FinalGrade") != null)); 

                if (hasAssignedGrade)
                {
                    FinalProject.Form1.DALMessage("Can not delete Enrollments with assigned grades");
                    return -1;
                }

                foreach (var id in eId)
                {
                    var rowToDelete = enrollmentsTable.AsEnumerable()
                        .Where(row => row.Field<string>("StId") == id[0] && row.Field<string>("CId") == id[1]);

                    foreach (var row in rowToDelete)
                    { 
                       row.Delete();
                    }
                }
                adapter.Update(enrollmentsTable);
                return 0;


            }
            catch (Exception)
            {
                FinalProject.Form1.DALMessage("Update / Deletion rejected");
                return -1;
            }          
            }

            internal static int UpdateEnrollments(string[] a, Nullable<int> eval)
            {
                try
                {
                    var line = ds.Tables["Enrollments"].AsEnumerable()
                                        .Where(s =>
                                           (s.Field<string>("StId") == a[0] && s.Field<string>("CId") == a[1]))
                                        .Single();
                    line.SetField("FinalGrade", eval);

                    adapter.Update(ds.Tables["Enrollments"]);

                    if (displayEnrollments != null)
                    {
                          var r = displayEnrollments.AsEnumerable()
                                         .Where(s => 
                                               (s.Field<string>("StId") == a[0] && s.Field<string>("CId") == a[1]))
                                         .Single();
                           r.SetField("FinalGrade", eval);              
                    }

                    return 0;
                }
                catch (Exception)
                {
                    FinalProject.Form1.DALMessage("Update / Deletion rejected");
                    return -1;
                }
            }
        }
    }   

