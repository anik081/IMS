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
	public class LU_SemesterTypeDAO //: IDisposible
	{
		private static volatile LU_SemesterTypeDAO instance;
		private static readonly object lockObj = new object();
		public static LU_SemesterTypeDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new LU_SemesterTypeDAO();
			}
			return instance;
		}
		public static LU_SemesterTypeDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new LU_SemesterTypeDAO();
						}
					}
				}
				return instance;
			}
		}

		DBExecutor dbExecutor;

		public LU_SemesterTypeDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<LU_SemesterType> Get(Int32? semesterTypeId = null)
		{
			try
			{
				List<LU_SemesterType> LU_SemesterTypeLst = new List<LU_SemesterType>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramSemesterTypeId", semesterTypeId, DbType.Int32, ParameterDirection.Input)
				};
				LU_SemesterTypeLst = dbExecutor.FetchData<LU_SemesterType>(CommandType.StoredProcedure, "wsp_LU_SemesterType_Get", colparameters);
				return LU_SemesterTypeLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<LU_SemesterType> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<LU_SemesterType> LU_SemesterTypeLst = new List<LU_SemesterType>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				LU_SemesterTypeLst = dbExecutor.FetchData<LU_SemesterType>(CommandType.StoredProcedure, "wsp_LU_SemesterType_GetDynamic", colparameters);
				return LU_SemesterTypeLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(LU_SemesterType _LU_SemesterType, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[6]{
				new Parameters("@paramSemesterTypeId", _LU_SemesterType.SemesterTypeId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramSemesterTypeName", _LU_SemesterType.SemesterTypeName, DbType.String, ParameterDirection.Input),
				new Parameters("@paramIsActive", _LU_SemesterType.IsActive, DbType.Boolean, ParameterDirection.Input),
				new Parameters("@paramUpdateBy", _LU_SemesterType.UpdateBy, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramUpdateDate", _LU_SemesterType.UpdateDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_LU_SemesterType_Post", colparameters, true);
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
