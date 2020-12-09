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
	public class LU_FeesDAO //: IDisposible
	{
		private static volatile LU_FeesDAO instance;
		private static readonly object lockObj = new object();
		public static LU_FeesDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new LU_FeesDAO();
			}
			return instance;
		}
		public static LU_FeesDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new LU_FeesDAO();
						}
					}
				}
				return instance;
			}
		}

		DBExecutor dbExecutor;

		public LU_FeesDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<LU_Fees> Get(Int32? feesId = null)
		{
			try
			{
				List<LU_Fees> LU_FeesLst = new List<LU_Fees>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramFeesId", feesId, DbType.Int32, ParameterDirection.Input)
				};
				LU_FeesLst = dbExecutor.FetchData<LU_Fees>(CommandType.StoredProcedure, "wsp_LU_Fees_Get", colparameters);
				return LU_FeesLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<LU_Fees> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<LU_Fees> LU_FeesLst = new List<LU_Fees>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				LU_FeesLst = dbExecutor.FetchData<LU_Fees>(CommandType.StoredProcedure, "wsp_LU_Fees_GetDynamic", colparameters);
				return LU_FeesLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(LU_Fees _LU_Fees, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[9]{
				new Parameters("@paramFeesId", _LU_Fees.FeesId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramInstituteId", _LU_Fees.InstituteId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramFeesName", _LU_Fees.FeesName, DbType.String, ParameterDirection.Input),
				new Parameters("@paramIsAuto", _LU_Fees.IsAuto, DbType.Boolean, ParameterDirection.Input),
				new Parameters("@paramApplyAt", _LU_Fees.ApplyAt, DbType.String, ParameterDirection.Input),
				new Parameters("@paramIsActive", _LU_Fees.IsActive, DbType.Boolean, ParameterDirection.Input),
				new Parameters("@paramUpdateBy", _LU_Fees.UpdateBy, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramUpdateDate", _LU_Fees.UpdateDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_LU_Fees_Post", colparameters, true);
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
