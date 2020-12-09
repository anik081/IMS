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
	public class TRN_StudentDueDAO //: IDisposible
	{
		private static volatile TRN_StudentDueDAO instance;
		private static readonly object lockObj = new object();
		public static TRN_StudentDueDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new TRN_StudentDueDAO();
			}
			return instance;
		}
		public static TRN_StudentDueDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new TRN_StudentDueDAO();
						}
					}
				}
				return instance;
			}
		}

		DBExecutor dbExecutor;

		public TRN_StudentDueDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<TRN_StudentDue> Get(Int64? dueId = null)
		{
			try
			{
				List<TRN_StudentDue> TRN_StudentDueLst = new List<TRN_StudentDue>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramDueId", dueId, DbType.Int64, ParameterDirection.Input)
				};
				TRN_StudentDueLst = dbExecutor.FetchData<TRN_StudentDue>(CommandType.StoredProcedure, "wsp_TRN_StudentDue_Get", colparameters);
				return TRN_StudentDueLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<TRN_StudentDue> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<TRN_StudentDue> TRN_StudentDueLst = new List<TRN_StudentDue>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				TRN_StudentDueLst = dbExecutor.FetchData<TRN_StudentDue>(CommandType.StoredProcedure, "wsp_TRN_StudentDue_GetDynamic", colparameters);
				return TRN_StudentDueLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(TRN_StudentDue _TRN_StudentDue, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[11]{
				new Parameters("@paramDueId", _TRN_StudentDue.DueId, DbType.Int64, ParameterDirection.Input),
				new Parameters("@paramStudentId", _TRN_StudentDue.StudentId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramFeesId", _TRN_StudentDue.FeesId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramSemesterId", _TRN_StudentDue.SemesterId, DbType.Int64, ParameterDirection.Input),
				new Parameters("@paramFeesAmount", _TRN_StudentDue.FeesAmount, DbType.Decimal, ParameterDirection.Input),
				new Parameters("@paramIsDiscounted", _TRN_StudentDue.IsDiscounted, DbType.Boolean, ParameterDirection.Input),
				new Parameters("@paramDsicAmount", _TRN_StudentDue.DsicAmount, DbType.Decimal, ParameterDirection.Input),
				new Parameters("@paramDiscountId", _TRN_StudentDue.DiscountId, DbType.Int64, ParameterDirection.Input),
				new Parameters("@paramUpdateBy", _TRN_StudentDue.UpdateBy, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramUpdateDate", _TRN_StudentDue.UpdateDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_TRN_StudentDue_Post", colparameters, true);
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
