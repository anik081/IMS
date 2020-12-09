using System;
using System.Text;

namespace QtImsEntity
{
	public class TRN_StudentDiscount
	{
		public Int64 DiscountId { get; set; }
		public Int32 StudentId { get; set; }
		public string ApplyOn { get; set; }
		public bool IsPercent { get; set; }
		public Decimal Figure { get; set; }
		public string StartDate { get; set; }
		public string EndDate { get; set; }
		public Int32 UpdateBy { get; set; }
		public DateTime UpdateDate { get; set; }
	}
}
