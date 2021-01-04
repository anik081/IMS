using System;
using System.Text;

namespace QtImsEntity
{
	public class LU_Program
	{
		public Int32 ProgramId { get; set; }
		public Int32 InstituteId { get; set; }
		public Int32 ProgramHeadId { get; set; }
        public string EmployeeName { get; set; }
        public string ProgramTitle { get; set; }
		public string ProgramCode { get; set; }
		public Int32 TotalCredit { get; set; }
		public Int32 DurationInMonth { get; set; }
		public Int32 MinCreditPerSemester { get; set; }
		public Int32 MaxCreditPerSemester { get; set; }
		public bool IsActive { get; set; }
		public Int32 UpdateBy { get; set; }
		public DateTime UpdateDate { get; set; }
	}
}
