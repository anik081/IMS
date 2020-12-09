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
	public class LU_StudentEducationDAO //: IDisposible
	{
		private static volatile LU_StudentEducationDAO instance;
		private static readonly object lockObj = new object();
		public static LU_StudentEducationDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new LU_StudentEducationDAO();
			}
			return instance;
		}
		public static LU_StudentEducationDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new LU_StudentEducationDAO();
						}
					}
				}
				return instance;
			}
		}

		DBExecutor dbExecutor;

		public LU_StudentEducationDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<LU_StudentEducation> Get(Int32? studentEducationId = null)
		{
			try
			{
				List<LU_StudentEducation> LU_StudentEducationLst = new List<LU_StudentEducation>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramStudentEducationId", studentEducationId, DbType.Int32, ParameterDirection.Input)
				};
				LU_StudentEducationLst = dbExecutor.FetchData<LU_StudentEducation>(CommandType.StoredProcedure, "wsp_LU_StudentEducation_Get", colparameters);
				return LU_StudentEducationLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<LU_StudentEducation> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<LU_StudentEducation> LU_StudentEducationLst = new List<LU_StudentEducation>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				LU_StudentEducationLst = dbExecutor.FetchData<LU_StudentEducation>(CommandType.StoredProcedure, "wsp_LU_StudentEducation_GetDynamic", colparameters);
				return LU_StudentEducationLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public string Post(LU_StudentEducation _LU_StudentEducation, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[9]{
				new Parameters("@paramStudentEducationId", _LU_StudentEducation.StudentEducationId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramStudentId", _LU_StudentEducation.StudentId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramExamType", _LU_StudentEducation.ExamType, DbType.String, ParameterDirection.Input),
				new Parameters("@paramBoardName", _LU_StudentEducation.BoardName, DbType.String, ParameterDirection.Input),
				new Parameters("@paramPassingYear", _LU_StudentEducation.PassingYear, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramGpaOrClass", _LU_StudentEducation.GpaOrClass, DbType.String, ParameterDirection.Input),
				new Parameters("@paramInstituteName", _LU_StudentEducation.InstituteName, DbType.String, ParameterDirection.Input),
				new Parameters("@paramInstituteAddress", _LU_StudentEducation.InstituteAddress, DbType.String, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_LU_StudentEducation_Post", colparameters, true);
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
