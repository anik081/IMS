using System;
using System.Text;

namespace QtImsEntity
{
	public class LU_SemesterType
	{
		public Int32 SemesterTypeId { get; set; }
		public string SemesterTypeName { get; set; }
		public bool IsActive { get; set; }
		public Int32 UpdateBy { get; set; }
		public DateTime UpdateDate { get; set; }
	}
}
