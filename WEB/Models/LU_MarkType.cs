using System;
using System.Text;

namespace QtImsEntity
{
	public class LU_MarkType
	{
		public Int32 MarkTypeId { get; set; }
		public Int32 InstituteId { get; set; }
		public string MarkTypeName { get; set; }
		public bool IsActive { get; set; }
		public Int32 UpdateBy { get; set; }
		public DateTime UpdateDate { get; set; }
	}
}
