using System;
using System.Text;

namespace QtImsEntity
{
	public class TRN_StudentDue
	{
		public Int64 DueId { get; set; }
		public Int32 StudentId { get; set; }
		public Int32 FeesId { get; set; }
        public string FeesName { get; set; }
        public Int64 SemesterId { get; set; }
		public Decimal FeesAmount { get; set; }
		public bool IsDiscounted { get; set; }
		public Decimal DsicAmount { get; set; }
		public Int64 DiscountId { get; set; }
		public Int32 UpdateBy { get; set; }
		public DateTime UpdateDate { get; set; }
        public string SemesterName { get; set; }
        public string FullName { get; set; }
        public bool IsActive { get; set; }
    }
}
