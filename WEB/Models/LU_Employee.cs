using System;
using System.Text;

namespace QtImsEntity
{
	public class LU_Employee
	{
		public Int32 EmployeeId { get; set; }
		public string EmployeeType { get; set; }
        public string EmployeeName { get; set; }
        public bool IsActive { get; set; }
		public Int32 UpdateBy { get; set; }
		public DateTime UpdateDate { get; set; }
	}
}
