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
	public class LU_CoursePrerequisiteDAO //: IDisposible
	{
		private static volatile LU_CoursePrerequisiteDAO instance;
		private static readonly object lockObj = new object();
		public static LU_CoursePrerequisiteDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new LU_CoursePrerequisiteDAO();
			}
			return instance;
		}
		public static LU_CoursePrerequisiteDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new LU_CoursePrerequisiteDAO();
						}
					}
				}
				return instance;
			}
		}

		DBExecutor dbExecutor;

		public LU_CoursePrerequisiteDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<LU_CoursePrerequisite> Get(Int32? coursePrerequisiteId = null)
		{
			try
			{
				List<LU_CoursePrerequisite> LU_CoursePrerequisiteLst = new List<LU_CoursePrerequisite>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramCoursePrerequisiteId", coursePrerequisiteId, DbType.Int32, ParameterDirection.Input)
				};
				LU_CoursePrerequisiteLst = dbExecutor.FetchData<LU_CoursePrerequisite>(CommandType.StoredProcedure, "wsp_LU_CoursePrerequisite_Get", colparameters);
				return LU_CoursePrerequisiteLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<LU_CoursePrerequisite> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<LU_CoursePrerequisite> LU_CoursePrerequisiteLst = new List<LU_CoursePrerequisite>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				LU_CoursePrerequisiteLst = dbExecutor.FetchData<LU_CoursePrerequisite>(CommandType.StoredProcedure, "wsp_LU_CoursePrerequisite_GetDynamic", colparameters);
				return LU_CoursePrerequisiteLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(LU_CoursePrerequisite _LU_CoursePrerequisite, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[4]{
				new Parameters("@paramCoursePrerequisiteId", _LU_CoursePrerequisite.CoursePrerequisiteId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramCourseId", _LU_CoursePrerequisite.CourseId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramPrerequisiteCourseId", _LU_CoursePrerequisite.PrerequisiteCourseId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_LU_CoursePrerequisite_Post", colparameters, true);
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
