using System;
using System.Text;

namespace QtImsEntity
{
	public class LU_CoursePrerequisite
	{
		public Int32 CoursePrerequisiteId { get; set; }
		public Int32 CourseId { get; set; }
		public Int32 PrerequisiteCourseId { get; set; }
        public string CourseCode { get; set; }
    }
}
