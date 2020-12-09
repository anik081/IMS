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
	public class TRN_CourseRegistrationLogDAO //: IDisposible
	{
		private static volatile TRN_CourseRegistrationLogDAO instance;
		private static readonly object lockObj = new object();
		public static TRN_CourseRegistrationLogDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new TRN_CourseRegistrationLogDAO();
			}
			return instance;
		}
		public static TRN_CourseRegistrationLogDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new TRN_CourseRegistrationLogDAO();
						}
					}
				}
				return instance;
			}
		}

		DBExecutor dbExecutor;

		public TRN_CourseRegistrationLogDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<TRN_CourseRegistrationLog> Get(Int64? courseRegistrationLogId = null)
		{
			try
			{
				List<TRN_CourseRegistrationLog> TRN_CourseRegistrationLogLst = new List<TRN_CourseRegistrationLog>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramCourseRegistrationLogId", courseRegistrationLogId, DbType.Int64, ParameterDirection.Input)
				};
				TRN_CourseRegistrationLogLst = dbExecutor.FetchData<TRN_CourseRegistrationLog>(CommandType.StoredProcedure, "wsp_TRN_CourseRegistrationLog_Get", colparameters);
				return TRN_CourseRegistrationLogLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<TRN_CourseRegistrationLog> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<TRN_CourseRegistrationLog> TRN_CourseRegistrationLogLst = new List<TRN_CourseRegistrationLog>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				TRN_CourseRegistrationLogLst = dbExecutor.FetchData<TRN_CourseRegistrationLog>(CommandType.StoredProcedure, "wsp_TRN_CourseRegistrationLog_GetDynamic", colparameters);
				return TRN_CourseRegistrationLogLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(TRN_CourseRegistrationLog _TRN_CourseRegistrationLog, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[9]{
				new Parameters("@paramCourseRegistrationLogId", _TRN_CourseRegistrationLog.CourseRegistrationLogId, DbType.Int64, ParameterDirection.Input),
				new Parameters("@paramCourseOfferId", _TRN_CourseRegistrationLog.CourseOfferId, DbType.Int64, ParameterDirection.Input),
				new Parameters("@paramLogDate", _TRN_CourseRegistrationLog.LogDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramStudentId", _TRN_CourseRegistrationLog.StudentId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramRegStatusId", _TRN_CourseRegistrationLog.RegStatusId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramCounseledby", _TRN_CourseRegistrationLog.Counseledby, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramUpdateBy", _TRN_CourseRegistrationLog.UpdateBy, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramUpdateDate", _TRN_CourseRegistrationLog.UpdateDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_TRN_CourseRegistrationLog_Post", colparameters, true);
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
