using System;
using System.Text;

namespace QtImsEntity
{
	public class TRN_CourseMark
	{
		public Int64 CourseMarkId { get; set; }
		public Int64 CourseOfferId { get; set; }
		public Int32 MarkTypeId { get; set; }
		public DateTime MarkingDate { get; set; }
		public Int32 UpdateBy { get; set; }
		public DateTime UpdateDate { get; set; }
	}
}
