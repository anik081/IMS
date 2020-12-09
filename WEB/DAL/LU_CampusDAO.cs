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
	public class LU_CampusDAO //: IDisposible
	{
		private static volatile LU_CampusDAO instance;
		private static readonly object lockObj = new object();
		public static LU_CampusDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new LU_CampusDAO();
			}
			return instance;
		}
		public static LU_CampusDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new LU_CampusDAO();
						}
					}
				}
				return instance;
			}
		}

		DBExecutor dbExecutor;

		public LU_CampusDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<LU_Campus> Get(Int32? campusId = null)
		{
			try
			{
				List<LU_Campus> LU_CampusLst = new List<LU_Campus>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramCampusId", campusId, DbType.Int32, ParameterDirection.Input)
				};
				LU_CampusLst = dbExecutor.FetchData<LU_Campus>(CommandType.StoredProcedure, "wsp_LU_Campus_Get", colparameters);
				return LU_CampusLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<LU_Campus> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<LU_Campus> LU_CampusLst = new List<LU_Campus>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				LU_CampusLst = dbExecutor.FetchData<LU_Campus>(CommandType.StoredProcedure, "wsp_LU_Campus_GetDynamic", colparameters);
				return LU_CampusLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(LU_Campus _LU_Campus, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[4]{
				new Parameters("@paramCampusId", _LU_Campus.CampusId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramInstituteId", _LU_Campus.InstituteId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramCampusName", _LU_Campus.CampusName, DbType.String, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_LU_Campus_Post", colparameters, true);
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
