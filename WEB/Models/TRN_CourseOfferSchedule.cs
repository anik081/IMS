using System;
using System.Text;

namespace QtImsEntity
{
	public class TRN_CourseOfferSchedule
	{
		public Int64 CourseOfferScheduleId { get; set; }
		public Int64 CourseOfferId { get; set; }
		public string DayName { get; set; }
		public string StartTime { get; set; }
		public string EndTime { get; set; }
        public Int32 CourseId { get; set; }

    }
}
