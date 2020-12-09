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
	public class LU_CourseProgramDAO //: IDisposible
	{
		private static volatile LU_CourseProgramDAO instance;
		private static readonly object lockObj = new object();
		public static LU_CourseProgramDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new LU_CourseProgramDAO();
			}
			return instance;
		}
		public static LU_CourseProgramDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new LU_CourseProgramDAO();
						}
					}
				}
				return instance;
			}
		}

		DBExecutor dbExecutor;

		public LU_CourseProgramDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<LU_CourseProgram> Get(Int32? courseProgramId = null)
		{
			try
			{
				List<LU_CourseProgram> LU_CourseProgramLst = new List<LU_CourseProgram>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramCourseProgramId", courseProgramId, DbType.Int32, ParameterDirection.Input)
				};
				LU_CourseProgramLst = dbExecutor.FetchData<LU_CourseProgram>(CommandType.StoredProcedure, "wsp_LU_CourseProgram_Get", colparameters);
				return LU_CourseProgramLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<LU_CourseProgram> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<LU_CourseProgram> LU_CourseProgramLst = new List<LU_CourseProgram>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				LU_CourseProgramLst = dbExecutor.FetchData<LU_CourseProgram>(CommandType.StoredProcedure, "wsp_LU_CourseProgram_GetDynamic", colparameters);
				return LU_CourseProgramLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(LU_CourseProgram _LU_CourseProgram, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[4]{
				new Parameters("@paramCourseProgramId", _LU_CourseProgram.CourseProgramId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramCourseId", _LU_CourseProgram.CourseId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramProgramId", _LU_CourseProgram.ProgramId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_LU_CourseProgram_Post", colparameters, true);
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
