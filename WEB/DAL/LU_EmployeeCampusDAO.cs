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
	public class LU_EmployeeCampusDAO //: IDisposible
	{
		private static volatile LU_EmployeeCampusDAO instance;
		private static readonly object lockObj = new object();
		public static LU_EmployeeCampusDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new LU_EmployeeCampusDAO();
			}
			return instance;
		}
		public static LU_EmployeeCampusDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new LU_EmployeeCampusDAO();
						}
					}
				}
				return instance;
			}
		}

		DBExecutor dbExecutor;

		public LU_EmployeeCampusDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<LU_EmployeeCampus> Get(Int32? employeeCampusId = null)
		{
			try
			{
				List<LU_EmployeeCampus> LU_EmployeeCampusLst = new List<LU_EmployeeCampus>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramEmployeeCampusId", employeeCampusId, DbType.Int32, ParameterDirection.Input)
				};
				LU_EmployeeCampusLst = dbExecutor.FetchData<LU_EmployeeCampus>(CommandType.StoredProcedure, "wsp_LU_EmployeeCampus_Get", colparameters);
				return LU_EmployeeCampusLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<LU_EmployeeCampus> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<LU_EmployeeCampus> LU_EmployeeCampusLst = new List<LU_EmployeeCampus>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				LU_EmployeeCampusLst = dbExecutor.FetchData<LU_EmployeeCampus>(CommandType.StoredProcedure, "wsp_LU_EmployeeCampus_GetDynamic", colparameters);
				return LU_EmployeeCampusLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(LU_EmployeeCampus _LU_EmployeeCampus, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[4]{
				new Parameters("@paramEmployeeCampusId", _LU_EmployeeCampus.EmployeeCampusId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramEmployeeId", _LU_EmployeeCampus.EmployeeId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramCampusId", _LU_EmployeeCampus.CampusId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_LU_EmployeeCampus_Post", colparameters, true);
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
