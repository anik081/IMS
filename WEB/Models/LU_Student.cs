using System;
using System.Text;

namespace QtImsEntity
{
	public class LU_Student
	{
		public Int32 StudentId { get; set; }
		public Int32 CampusId { get; set; }
		public Int32 ProgramId { get; set; }
		public Int32 BatchNo { get; set; }
		public string StudentStatus { get; set; }
		public bool IsActive { get; set; }
		public string FullName { get; set; }
		public string MobileNo { get; set; }
		public string Email { get; set; }
		public string FatherName { get; set; }
		public string FatherOccupation { get; set; }
		public string MotherName { get; set; }
		public string MotherOccupation { get; set; }
		public string GuardianName { get; set; }
		public string RelationWithGuardian { get; set; }
		public string GuardianMobileNo { get; set; }
		public string GuardianEmail { get; set; }
		public string PhotoIdType { get; set; }
		public string PhotoIdNo { get; set; }
		public DateTime DateOfBirth { get; set; }
		public string PlaceofBirth { get; set; }
		public string BloodGroup { get; set; }
		public string Gender { get; set; }
		public string MaritalStatus { get; set; }
		public string HoldingNoAndRoad { get; set; }
		public string PoliceStation { get; set; }
		public string PostOffice { get; set; }
		public string Upazila { get; set; }
		public string District { get; set; }
        public string RegularAmount { get; set; }
        public string CommittedAmount { get; set; }
        public string Division { get; set; }
		public Int32 UpdateBy { get; set; }
		public DateTime UpdateDate { get; set; }
		public string IdCard { get; set; }
		public string ImgaeLocation { get; set; }
	}
}
