using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Runtime.Remoting.Messaging;

namespace BusinessLayer
{
    class Programs
    {
        internal static int UpdatePrograms()
        {
            DataSet ds = Data.DataTables.getDataSet();

            DataTable dt = ds.Tables["Programs"]
                              .GetChanges(DataRowState.Added | DataRowState.Modified);
            if (dt != null)
            {
                if (dt.AsEnumerable().Any(r => !isValidProgId(r.Field<string>("ProgId"))))
                {
                    FinalProject.Form1.BLLMessage("Invalid Id for Programs");
                    ds.RejectChanges();
                    return -1;
                }
                else
                {
                    return Data.Programs.UpdatePrograms();
                }
            }
            else 
            {
                return Data.Programs.UpdatePrograms();
            }

        }
        private static bool isValidProgId(string progId) 
        {
            bool r = true;
            if (progId.Length != 5) { r = false; }
            else if (progId[0] != 'P') { r = false; }
            else
            {
                for (int i = 1; i < progId.Length; i++)
                {
                    r = r && Char.IsDigit(progId[i]);
                }
            }
            return r;

        }
    }
    internal class Courses
    {
        internal static int UpdateCourses()
        {
            DataSet ds = Data.DataTables.getDataSet();
            DataTable dt = ds.Tables["Courses"]
                             .GetChanges(DataRowState.Added | DataRowState.Modified);
            if (dt != null)
            {
                if (dt.AsEnumerable().Any(r => !isValidCId(r.Field<string>("CId"))))
                {
                    FinalProject.Form1.BLLMessage("Invalid ID for courses");
                    ds.RejectChanges();
                    return -1;
                }
                else
                {
                    return Data.Courses.UpdateCourses();
                }
            }
            else 
            {
                return Data.Courses.UpdateCourses();
            }
        }
        private static bool isValidCId(string cId) 
        {
            bool r = true;
            if (cId.Length != 7) { r = false; }
            else if (cId[0] != 'C') { r = false; }
            else
            {
                for (int i = 1; i < cId.Length; i++)
                { 
                    r = r && Char.IsDigit(cId[i]);
                }
            }
            return r;
        }
    }
    internal class Students
    {
        internal static int UpdateStudents()
        {
            DataSet ds = Data.DataTables.getDataSet();

            DataTable dt = ds.Tables["Students"]
                             .GetChanges(DataRowState.Added | DataRowState.Modified);

            if (dt != null)
            {
                if (dt.AsEnumerable().Any(r => !IsValidStId(r.Field<string>("StId"))))
                {
                    FinalProject.Form1.BLLMessage("Invalid ID for Students");
                    ds.RejectChanges();
                    return -1;
                }
                else
                {
                    return Data.Students.UpdateStudents();
                }
            }
            else
            {
                return Data.Students.UpdateStudents();
            }
        }
        private static bool IsValidStId(string stid)
        {
            bool r = true;
            if (stid.Length != 10) { r = false; }
            else if (stid[0] != 'S') { r = false; }
            else
            {
                for (int i = 1; i < stid.Length; i++)
                { 
                    r = r && Char.IsDigit(stid[i]);
                }
            }
            return r;
        }
    }
    internal class Enrollments
    {
        internal static int UpdateEnrollments(string[] a, string el)
        {
            Nullable<int> finalgrade;
            int temp;

            if (el == "")
            {
                finalgrade = null;
            }
            else if (int.TryParse(el, out temp) && (0 <= temp && temp <= 100))
            {
                finalgrade = temp;
            }
            else
            {
                FinalProject.Form1.BLLMessage("Final Grade must be an integer between 0 and 100");
                return -1;
            }
            return Data.Enrollments.UpdateEnrollments(a, finalgrade);
                                      
        }
    }



}
