using System;
using System.Text;

namespace QtImsEntity
{
	public class TRN_CourseRegistrationLog
	{
		public Int64 CourseRegistrationLogId { get; set; }
		public Int64 CourseOfferId { get; set; }
		public DateTime LogDate { get; set; }
		public Int32 StudentId { get; set; }
		public Int32 RegStatusId { get; set; }
		public Int32 Counseledby { get; set; }
		public Int32 UpdateBy { get; set; }
		public DateTime UpdateDate { get; set; }
	}
}
