using System;
using System.Text;

namespace QtImsEntity
{
	public class TRN_CourseOffer
	{
		public Int64 CourseOfferId { get; set; }
		public Int32 CampusId { get; set; }
		public Int32 CourseId { get; set; }
		public Int32 SemesterId { get; set; }
		public Int32 InstructorId { get; set; }
		public Int32 TotalSeat { get; set; }
		public string ScheduleSummary { get; set; }
		public bool IsActive { get; set; }
		public Int32 UpdateBy { get; set; }
		public DateTime UpdateDate { get; set; }
        public string CourseCode { get; set; }
        public string CourseTitle { get; set; }
        public Int32 Credits { get; set; }
    }
}
