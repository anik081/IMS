using System;
using System.Text;

namespace QtImsEntity
{
	public class TRN_Semester
	{
		public Int64 SemesterId { get; set; }
		public Int32 SemesterTypeId { get; set; }
		public Int32 YearId { get; set; }
		public string SemesterName { get; set; }
		public Int32 BatchNo { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
        public DateTime AddDropStartDate { get; set; }
        public DateTime AddDropEndDate { get; set; }
        public DateTime WithdrawStartDate { get; set; }
        public DateTime WithdrawEndDate { get; set; }
        public bool IsActive { get; set; }
		public Int32 UpdateBy { get; set; }
		public DateTime UpdateDate { get; set; }
	}
}
