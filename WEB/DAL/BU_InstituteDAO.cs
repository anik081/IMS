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
	public class BU_InstituteDAO //: IDisposible
	{
		private static volatile BU_InstituteDAO instance;
		private static readonly object lockObj = new object();
		public static BU_InstituteDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new BU_InstituteDAO();
			}
			return instance;
		}
		public static BU_InstituteDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new BU_InstituteDAO();
						}
					}
				}
				return instance;
			}
		}

		DBExecutor dbExecutor;

		public BU_InstituteDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<BU_Institute> Get(Int32? instituteId = null)
		{
			try
			{
				List<BU_Institute> BU_InstituteLst = new List<BU_Institute>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramInstituteId", instituteId, DbType.Int32, ParameterDirection.Input)
				};
				BU_InstituteLst = dbExecutor.FetchData<BU_Institute>(CommandType.StoredProcedure, "wsp_BU_Institute_Get", colparameters);
				return BU_InstituteLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<BU_Institute> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<BU_Institute> BU_InstituteLst = new List<BU_Institute>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				BU_InstituteLst = dbExecutor.FetchData<BU_Institute>(CommandType.StoredProcedure, "wsp_BU_Institute_GetDynamic", colparameters);
				return BU_InstituteLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(BU_Institute _BU_Institute, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramInstituteId", _BU_Institute.InstituteId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_BU_Institute_Post", colparameters, true);
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
