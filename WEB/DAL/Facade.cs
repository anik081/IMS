namespace QtImsDAL
{
    public static class Facade
    {
        public static LU_CourseProgramDAO LU_CourseProgram { get { return new LU_CourseProgramDAO(); } }
        public static LU_CoursePrerequisiteDAO LU_CoursePrerequisite { get { return new LU_CoursePrerequisiteDAO(); } }
        public static LU_SemesterTypeDAO LU_SemesterType { get { return new LU_SemesterTypeDAO(); } }
        public static LU_EmployeeDAO LU_Employee { get { return new LU_EmployeeDAO(); } }
        public static BU_InstituteDAO BU_Institute { get { return new BU_InstituteDAO(); } }
        public static LU_CourseDAO LU_Course { get { return new LU_CourseDAO(); } }
        public static LU_CampusDAO LU_Campus { get { return new LU_CampusDAO(); } }
        public static LU_EmployeeCampusDAO LU_EmployeeCampus { get { return new LU_EmployeeCampusDAO(); } }
        public static LU_MarkTypeDAO LU_MarkType { get { return new LU_MarkTypeDAO(); } }
        public static TRN_CourseOfferDAO TRN_CourseOffer { get { return new TRN_CourseOfferDAO(); } }
        public static TRN_CourseOfferScheduleDAO TRN_CourseOfferSchedule { get { return new TRN_CourseOfferScheduleDAO(); } }
        public static LU_ProgramDAO LU_Program { get { return new LU_ProgramDAO(); } }
        public static TRN_SemesterDAO TRN_Semester { get { return new TRN_SemesterDAO(); } }
        public static TRN_CourseRegistrationLogDAO TRN_CourseRegistrationLog { get { return new TRN_CourseRegistrationLogDAO(); } }
        public static LU_StudentDAO LU_Student { get { return new LU_StudentDAO(); } }
        public static LU_StudentEducationDAO LU_StudentEducation { get { return new LU_StudentEducationDAO(); } }
        public static TRN_StudentPaymentDAO TRN_StudentPayment { get { return new TRN_StudentPaymentDAO(); } }
        public static TRN_CourseMarkDAO TRN_CourseMark { get { return new TRN_CourseMarkDAO(); } }
        public static TRN_CourseMarkDetailDAO TRN_CourseMarkDetail { get { return new TRN_CourseMarkDetailDAO(); } }
        public static LU_FeesDAO LU_Fees { get { return new LU_FeesDAO(); } }
        public static TRN_StudentDueDAO TRN_StudentDue { get { return new TRN_StudentDueDAO(); } }
        public static TRN_CourseRegistrationDAO TRN_CourseRegistration { get { return new TRN_CourseRegistrationDAO(); } }
        public static TRN_AttendanceDAO TRN_Attendance { get { return new TRN_AttendanceDAO(); } }
        public static TRN_StudentDiscountDAO TRN_StudentDiscount { get { return new TRN_StudentDiscountDAO(); } }

        public static object LU_CourseDAO { get; internal set; }
      
    }
}
