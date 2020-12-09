using System;
using System.Text;

namespace QtImsEntity
{
	public class TRN_Attendance
	{
		public Int64 AttendanceId { get; set; }
		public Int64 CourseOfferId { get; set; }
		public DateTime AttendanceDate { get; set; }
		public Int32 AttendanceTypeId { get; set; }
		public Int32 StudentId { get; set; }
		public bool IsAttend { get; set; }
		public Int32 UpdateBy { get; set; }
		public DateTime UpdateDate { get; set; }
	}
}
