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
	public class LU_EmployeeDAO //: IDisposible
	{
		private static volatile LU_EmployeeDAO instance;
		private static readonly object lockObj = new object();
		public static LU_EmployeeDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new LU_EmployeeDAO();
			}
			return instance;
		}
		public static LU_EmployeeDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new LU_EmployeeDAO();
						}
					}
				}
				return instance;
			}
		}

		DBExecutor dbExecutor;

		public LU_EmployeeDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<LU_Employee> Get(Int32? employeeId = null)
		{
			try
			{
				List<LU_Employee> LU_EmployeeLst = new List<LU_Employee>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramEmployeeId", employeeId, DbType.Int32, ParameterDirection.Input)
				};
				LU_EmployeeLst = dbExecutor.FetchData<LU_Employee>(CommandType.StoredProcedure, "wsp_LU_Employee_Get", colparameters);
				return LU_EmployeeLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<LU_Employee> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<LU_Employee> LU_EmployeeLst = new List<LU_Employee>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				LU_EmployeeLst = dbExecutor.FetchData<LU_Employee>(CommandType.StoredProcedure, "wsp_LU_Employee_GetDynamic", colparameters);
				return LU_EmployeeLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(LU_Employee _LU_Employee, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[7]{
				new Parameters("@paramEmployeeId", _LU_Employee.EmployeeId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramEmployeeType", _LU_Employee.EmployeeType, DbType.String, ParameterDirection.Input),
				new Parameters("@paramEmployeeName", _LU_Employee.EmployeeName, DbType.String, ParameterDirection.Input),
				new Parameters("@paramIsActive", _LU_Employee.IsActive, DbType.Boolean, ParameterDirection.Input),
				new Parameters("@paramUpdateBy", _LU_Employee.UpdateBy, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramUpdateDate", _LU_Employee.UpdateDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_LU_Employee_Post", colparameters, true);
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
