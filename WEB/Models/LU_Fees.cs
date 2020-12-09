using System;
using System.Text;

namespace QtImsEntity
{
	public class LU_Fees
	{
		public Int32 FeesId { get; set; }
		public Int32 InstituteId { get; set; }
		public string FeesName { get; set; }
		public bool IsAuto { get; set; }
		public string ApplyAt { get; set; }
		public bool IsActive { get; set; }
		public Int32 UpdateBy { get; set; }
		public DateTime UpdateDate { get; set; }
	}
}
