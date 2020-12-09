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
	public class TRN_CourseOfferScheduleDAO //: IDisposible
	{
		private static volatile TRN_CourseOfferScheduleDAO instance;
		private static readonly object lockObj = new object();
		public static TRN_CourseOfferScheduleDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new TRN_CourseOfferScheduleDAO();
			}
			return instance;
		}
		public static TRN_CourseOfferScheduleDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new TRN_CourseOfferScheduleDAO();
						}
					}
				}
				return instance;
			}
		}

		DBExecutor dbExecutor;

		public TRN_CourseOfferScheduleDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<TRN_CourseOfferSchedule> Get(Int64? courseOfferScheduleId = null)
		{
			try
			{
				List<TRN_CourseOfferSchedule> TRN_CourseOfferScheduleLst = new List<TRN_CourseOfferSchedule>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramCourseOfferScheduleId", courseOfferScheduleId, DbType.Int64, ParameterDirection.Input)
				};
				TRN_CourseOfferScheduleLst = dbExecutor.FetchData<TRN_CourseOfferSchedule>(CommandType.StoredProcedure, "wsp_TRN_CourseOfferSchedule_Get", colparameters);
				return TRN_CourseOfferScheduleLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<TRN_CourseOfferSchedule> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<TRN_CourseOfferSchedule> TRN_CourseOfferScheduleLst = new List<TRN_CourseOfferSchedule>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				TRN_CourseOfferScheduleLst = dbExecutor.FetchData<TRN_CourseOfferSchedule>(CommandType.StoredProcedure, "wsp_TRN_CourseOfferSchedule_GetDynamic", colparameters);
				return TRN_CourseOfferScheduleLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(TRN_CourseOfferSchedule _TRN_CourseOfferSchedule, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[6]{
				new Parameters("@paramCourseOfferScheduleId", _TRN_CourseOfferSchedule.CourseOfferScheduleId, DbType.Int64, ParameterDirection.Input),
				new Parameters("@paramCourseOfferId", _TRN_CourseOfferSchedule.CourseOfferId, DbType.Int64, ParameterDirection.Input),
				new Parameters("@paramDayName", _TRN_CourseOfferSchedule.DayName, DbType.String, ParameterDirection.Input),
				new Parameters("@paramStartTime", _TRN_CourseOfferSchedule.StartTime, DbType.String, ParameterDirection.Input),
				new Parameters("@paramEndTime", _TRN_CourseOfferSchedule.EndTime, DbType.String, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_TRN_CourseOfferSchedule_Post", colparameters, true);
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
