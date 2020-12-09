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
	public class TRN_SemesterDAO //: IDisposible
	{
		private static volatile TRN_SemesterDAO instance;
		private static readonly object lockObj = new object();
		public static TRN_SemesterDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new TRN_SemesterDAO();
			}
			return instance;
		}
		public static TRN_SemesterDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new TRN_SemesterDAO();
						}
					}
				}
				return instance;
			}
		}

		DBExecutor dbExecutor;

		public TRN_SemesterDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<TRN_Semester> Get(Int64? semesterId = null)
		{
			try
			{
				List<TRN_Semester> TRN_SemesterLst = new List<TRN_Semester>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramSemesterId", semesterId, DbType.Int64, ParameterDirection.Input)
				};
				TRN_SemesterLst = dbExecutor.FetchData<TRN_Semester>(CommandType.StoredProcedure, "wsp_TRN_Semester_Get", colparameters);
				return TRN_SemesterLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<TRN_Semester> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<TRN_Semester> TRN_SemesterLst = new List<TRN_Semester>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				TRN_SemesterLst = dbExecutor.FetchData<TRN_Semester>(CommandType.StoredProcedure, "wsp_TRN_Semester_GetDynamic", colparameters);
				return TRN_SemesterLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(TRN_Semester _TRN_Semester, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[15]{
				new Parameters("@paramSemesterId", _TRN_Semester.SemesterId, DbType.Int64, ParameterDirection.Input),
				new Parameters("@paramSemesterTypeId", _TRN_Semester.SemesterTypeId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramYearId", _TRN_Semester.YearId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramSemesterName", _TRN_Semester.SemesterName, DbType.String, ParameterDirection.Input),
				new Parameters("@paramBatchNo", _TRN_Semester.BatchNo, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramStartDate", _TRN_Semester.StartDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramEndDate", _TRN_Semester.EndDate, DbType.DateTime, ParameterDirection.Input),
                new Parameters("@paramAddDropStartDate", _TRN_Semester.AddDropStartDate, DbType.DateTime, ParameterDirection.Input),
                new Parameters("@paramAddDropEndDate", _TRN_Semester.AddDropEndDate, DbType.DateTime, ParameterDirection.Input),
                new Parameters("@paramWithdrawStartDate", _TRN_Semester.WithdrawStartDate, DbType.DateTime, ParameterDirection.Input),
                new Parameters("@paramWithdrawEndDate", _TRN_Semester.WithdrawEndDate, DbType.DateTime, ParameterDirection.Input),
                new Parameters("@paramIsActive", _TRN_Semester.IsActive, DbType.Boolean, ParameterDirection.Input),
				new Parameters("@paramUpdateBy", _TRN_Semester.UpdateBy, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramUpdateDate", _TRN_Semester.UpdateDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_TRN_Semester_Post", colparameters, true);
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
