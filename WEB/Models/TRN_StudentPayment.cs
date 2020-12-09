using System;
using System.Text;

namespace QtImsEntity
{
	public class TRN_StudentPayment
	{
		public Int64 PaymentId { get; set; }
		public Int32 StudentId { get; set; }
        public string FullName { get; set; }
        public string PaymentPurpose { get; set; }
		public Int32 ReceivedById { get; set; }
		public string ReceiptNo { get; set; }
        public string Bank { get; set; }
        public string Branch { get; set; }
        public string ChequeNo { get; set; }
        public Decimal Amount { get; set; }
		public Decimal PreviousDue { get; set; }
		public Int32 PayMethodId { get; set; }
		public string PayMethodNo { get; set; }
		public Int32 UpdateBy { get; set; }
		public DateTime UpdateDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public Nullable<DateTime> PaymentChequeDate { get; set; }
        public bool IsActive { get; set; }
    }
}
