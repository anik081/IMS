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
	public class LU_MarkTypeDAO //: IDisposible
	{
		private static volatile LU_MarkTypeDAO instance;
		private static readonly object lockObj = new object();
		public static LU_MarkTypeDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new LU_MarkTypeDAO();
			}
			return instance;
		}
		public static LU_MarkTypeDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new LU_MarkTypeDAO();
						}
					}
				}
				return instance;
			}
		}

		DBExecutor dbExecutor;

		public LU_MarkTypeDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<LU_MarkType> Get(Int32? markTypeId = null)
		{
			try
			{
				List<LU_MarkType> LU_MarkTypeLst = new List<LU_MarkType>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramMarkTypeId", markTypeId, DbType.Int32, ParameterDirection.Input)
				};
				LU_MarkTypeLst = dbExecutor.FetchData<LU_MarkType>(CommandType.StoredProcedure, "wsp_LU_MarkType_Get", colparameters);
				return LU_MarkTypeLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<LU_MarkType> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<LU_MarkType> LU_MarkTypeLst = new List<LU_MarkType>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				LU_MarkTypeLst = dbExecutor.FetchData<LU_MarkType>(CommandType.StoredProcedure, "wsp_LU_MarkType_GetDynamic", colparameters);
				return LU_MarkTypeLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(LU_MarkType _LU_MarkType, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[7]{
				new Parameters("@paramMarkTypeId", _LU_MarkType.MarkTypeId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramInstituteId", _LU_MarkType.InstituteId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramMarkTypeName", _LU_MarkType.MarkTypeName, DbType.String, ParameterDirection.Input),
				new Parameters("@paramIsActive", _LU_MarkType.IsActive, DbType.Boolean, ParameterDirection.Input),
				new Parameters("@paramUpdateBy", _LU_MarkType.UpdateBy, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramUpdateDate", _LU_MarkType.UpdateDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_LU_MarkType_Post", colparameters, true);
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
