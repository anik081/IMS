using System;
using System.Text;

namespace QtImsEntity
{
	public class TRN_CourseMarkDetail
	{
		public Int64 CourseMarkDetailId { get; set; }
		public Int64 CourseMarkId { get; set; }
		public Int32 StudentId { get; set; }
		public Decimal StudentMark { get; set; }
	}
}
