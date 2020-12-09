using System;
using System.Text;

namespace QtImsEntity
{
	public class LU_Course
	{
		public Int32 CourseId { get; set; }
		public Int32 InstituteId { get; set; }
		public string CourseCode { get; set; }
		public string CourseTitle { get; set; }
		public Int32 Credits { get; set; }
		public Int32 InstructorId { get; set; }
		public bool IsActive { get; set; }
		public Int32 UpdateBy { get; set; }
		public DateTime UpdateDate { get; set; }

    }
}
