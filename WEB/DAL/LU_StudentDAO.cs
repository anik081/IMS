using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Data.Common;
using DbExecutor;
using QtImsEntity;

namespace QtImsDAL
{
    public class LU_StudentDAO //: IDisposible
    {
        private static volatile LU_StudentDAO instance;
        private static readonly object lockObj = new object();
        public static LU_StudentDAO GetInstance()
        {
            if (instance == null)
            {
                instance = new LU_StudentDAO();
            }
            return instance;
        }
        public static LU_StudentDAO GetInstanceThreadSafe
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObj)
                    {
                        if (instance == null)
                        {
                            instance = new LU_StudentDAO();
                        }
                    }
                }
                return instance;
            }
        }

        DBExecutor dbExecutor;

        public LU_StudentDAO()
        {
            //dbExecutor = DBExecutor.GetInstanceThreadSafe;
            dbExecutor = new DBExecutor();
        }

        public List<LU_Student> Get(Int32? studentId = null)
        {
            try
            {
                List<LU_Student> LU_StudentLst = new List<LU_Student>();
                Parameters[] colparameters = new Parameters[1]{
                new Parameters("@paramStudentId", studentId, DbType.Int32, ParameterDirection.Input)
                };
                LU_StudentLst = dbExecutor.FetchData<LU_Student>(CommandType.StoredProcedure, "wsp_LU_Student_Get", colparameters);
                return LU_StudentLst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<LU_Student> GetDynamic(string whereCondition, string orderByExpression)
        {
            try
            {
                List<LU_Student> LU_StudentLst = new List<LU_Student>();
                Parameters[] colparameters = new Parameters[2]{
                new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
                new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
                };
                LU_StudentLst = dbExecutor.FetchData<LU_Student>(CommandType.StoredProcedure, "wsp_LU_Student_GetDynamic", colparameters);
                return LU_StudentLst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Post(LU_Student _LU_Student, string transactionType)
        {
            string ret = string.Empty;
            if (_LU_Student.ImgaeLocation == string.Empty || _LU_Student.ImgaeLocation == null)
            {
                _LU_Student.ImgaeLocation = "";
            }

                try
                {
                    Parameters[] colparameters = new Parameters[37]{
                new Parameters("@paramStudentId", _LU_Student.StudentId, DbType.Int32, ParameterDirection.Input),
                new Parameters("@paramCampusId", _LU_Student.CampusId, DbType.Int32, ParameterDirection.Input),
                new Parameters("@paramProgramId", _LU_Student.ProgramId, DbType.Int32, ParameterDirection.Input),
                new Parameters("@paramBatchNo", _LU_Student.BatchNo, DbType.Int32, ParameterDirection.Input),
                new Parameters("@paramStudentStatus", _LU_Student.StudentStatus, DbType.String, ParameterDirection.Input),
                new Parameters("@paramIsActive", _LU_Student.IsActive, DbType.Boolean, ParameterDirection.Input),
                new Parameters("@paramFullName", _LU_Student.FullName, DbType.String, ParameterDirection.Input),
                new Parameters("@paramMobileNo", _LU_Student.MobileNo, DbType.String, ParameterDirection.Input),
                new Parameters("@paramEmail", _LU_Student.Email, DbType.String, ParameterDirection.Input),
                new Parameters("@paramFatherName", _LU_Student.FatherName, DbType.String, ParameterDirection.Input),
                new Parameters("@paramFatherOccupation", _LU_Student.FatherOccupation, DbType.String, ParameterDirection.Input),
                new Parameters("@paramMotherName", _LU_Student.MotherName, DbType.String, ParameterDirection.Input),
                new Parameters("@paramMotherOccupation", _LU_Student.MotherOccupation, DbType.String, ParameterDirection.Input),
                new Parameters("@paramGuardianName", _LU_Student.GuardianName, DbType.String, ParameterDirection.Input),
                new Parameters("@paramRelationWithGuardian", _LU_Student.RelationWithGuardian, DbType.String, ParameterDirection.Input),
                new Parameters("@paramGuardianMobileNo", _LU_Student.GuardianMobileNo, DbType.String, ParameterDirection.Input),
                new Parameters("@paramGuardianEmail", _LU_Student.GuardianEmail, DbType.String, ParameterDirection.Input),
                new Parameters("@paramPhotoIdType", _LU_Student.PhotoIdType, DbType.String, ParameterDirection.Input),
                new Parameters("@paramPhotoIdNo", _LU_Student.PhotoIdNo, DbType.String, ParameterDirection.Input),
                new Parameters("@paramDateOfBirth", _LU_Student.DateOfBirth, DbType.DateTime, ParameterDirection.Input),
                new Parameters("@paramPlaceofBirth", _LU_Student.PlaceofBirth, DbType.String, ParameterDirection.Input),
                new Parameters("@paramBloodGroup", _LU_Student.BloodGroup, DbType.String, ParameterDirection.Input),
                new Parameters("@paramGender", _LU_Student.Gender, DbType.String, ParameterDirection.Input),
                new Parameters("@paramMaritalStatus", _LU_Student.MaritalStatus, DbType.String, ParameterDirection.Input),
                new Parameters("@paramHoldingNoAndRoad", _LU_Student.HoldingNoAndRoad, DbType.String, ParameterDirection.Input),
                new Parameters("@paramPoliceStation", _LU_Student.PoliceStation, DbType.String, ParameterDirection.Input),
                new Parameters("@paramPostOffice", _LU_Student.PostOffice, DbType.String, ParameterDirection.Input),
                new Parameters("@paramUpazila", _LU_Student.Upazila, DbType.String, ParameterDirection.Input),
                new Parameters("@paramDistrict", _LU_Student.District, DbType.String, ParameterDirection.Input),
                new Parameters("@paramRegularAmount", _LU_Student.RegularAmount, DbType.String, ParameterDirection.Input),
                new Parameters("@paramCommittedAmount", _LU_Student.CommittedAmount, DbType.String, ParameterDirection.Input),
                new Parameters("@paramDivision", _LU_Student.Division, DbType.String, ParameterDirection.Input),
                new Parameters("@paramUpdateBy", _LU_Student.UpdateBy, DbType.Int32, ParameterDirection.Input),
                new Parameters("@paramUpdateDate", _LU_Student.UpdateDate, DbType.DateTime, ParameterDirection.Input),
                new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input),
                new Parameters("@paramIdCard", _LU_Student.IdCard, DbType.String, ParameterDirection.Input),
                new Parameters("@paramImgaeLocation", _LU_Student.ImgaeLocation, DbType.String, ParameterDirection.Input)
,
                };
                    dbExecutor.ManageTransaction(TransactionType.Open);
                    ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_LU_Student_Post", colparameters, true);
                    dbExecutor.ManageTransaction(TransactionType.Commit);
                }
                catch (DBConcurrencyException except)
                {
                    dbExecutor.ManageTransaction(TransactionType.Rollback);
                    throw except;
                }
                catch (Exception ex)
                {
                    dbExecutor.ManageTransaction(TransactionType.Rollback);
                    throw ex;
                }
                return ret;
                }

    }
    }
