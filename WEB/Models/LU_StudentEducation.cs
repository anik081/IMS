using System;
using System.Text;

namespace QtImsEntity
{
	public class LU_StudentEducation
	{
		public Int32 StudentEducationId { get; set; }
		public Int32 StudentId { get; set; }
		public string ExamType { get; set; }
		public string BoardName { get; set; }
		public Int32 PassingYear { get; set; }
		public string GpaOrClass { get; set; }
		public string InstituteName { get; set; }
		public string InstituteAddress { get; set; }
	}
}
