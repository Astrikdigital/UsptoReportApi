using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converge.Shared.Helper
{
    public class EnumHelper
    {
        public enum Role
        {
            Admin = 1,
            Faculty = 2,
            Student = 3
        }

        enum NotificationEnum
        {
            All = 1,
            AllFaculties = 2,
            AllStudents = 3,
            Batch = 4,
            OneStudent = 5,
            OneFaculty = 6
        }

        public enum User
        {
            Faculty = 1,
            Student = 2
        }

        public enum CourseAttachmentFolder
        {
            CourseOutline = 1,
            LecturesAndPresentations = 2,
            ReadingList = 3,
            Course = 4,
            CourseMaterial = 5,
            FacultyUpload = 6,
            FacultyProfilePicture = 7,
            StudentProfilePicture = 8,
            Program= 9,
            Notification=10,
            StudentUpload = 11,
            Administrator = 12,
            Query=13
        }
    }
}
