using System;
using System.Text;

namespace QtImsEntity
{
	public class TRN_CourseRegistration
	{
		public Int64 CourseRegistrationId { get; set; }
		public Int64 CourseOfferId { get; set; }
		public DateTime RegistrationDate { get; set; }
		public Int32 StudentId { get; set; }
		public Int32 RegStatusId { get; set; }
		public Int32 Counseledby { get; set; }
		public string Remarks { get; set; }
		public string Result { get; set; }
		public Decimal? GPA { get; set; }
		public Int32 UpdateBy { get; set; }
		public DateTime UpdateDate { get; set; }
        public string CourseTitle { get; set; }
        public string ScheduleSummary { get; set; }
        public string CourseCode { get; set; }
        public string EmployeeName { get; set; }
        public Int32 Credits { get; set; }
        public string FullName { get; set; }
    }
}
