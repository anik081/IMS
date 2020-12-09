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
	public class TRN_CourseMarkDAO //: IDisposible
	{
		private static volatile TRN_CourseMarkDAO instance;
		private static readonly object lockObj = new object();
		public static TRN_CourseMarkDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new TRN_CourseMarkDAO();
			}
			return instance;
		}
		public static TRN_CourseMarkDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new TRN_CourseMarkDAO();
						}
					}
				}
				return instance;
			}
		}

		DBExecutor dbExecutor;

		public TRN_CourseMarkDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<TRN_CourseMark> Get(Int64? courseMarkId = null)
		{
			try
			{
				List<TRN_CourseMark> TRN_CourseMarkLst = new List<TRN_CourseMark>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramCourseMarkId", courseMarkId, DbType.Int64, ParameterDirection.Input)
				};
				TRN_CourseMarkLst = dbExecutor.FetchData<TRN_CourseMark>(CommandType.StoredProcedure, "wsp_TRN_CourseMark_Get", colparameters);
				return TRN_CourseMarkLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<TRN_CourseMark> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<TRN_CourseMark> TRN_CourseMarkLst = new List<TRN_CourseMark>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				TRN_CourseMarkLst = dbExecutor.FetchData<TRN_CourseMark>(CommandType.StoredProcedure, "wsp_TRN_CourseMark_GetDynamic", colparameters);
				return TRN_CourseMarkLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(TRN_CourseMark _TRN_CourseMark, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[7]{
				new Parameters("@paramCourseMarkId", _TRN_CourseMark.CourseMarkId, DbType.Int64, ParameterDirection.Input),
				new Parameters("@paramCourseOfferId", _TRN_CourseMark.CourseOfferId, DbType.Int64, ParameterDirection.Input),
				new Parameters("@paramMarkTypeId", _TRN_CourseMark.MarkTypeId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramMarkingDate", _TRN_CourseMark.MarkingDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramUpdateBy", _TRN_CourseMark.UpdateBy, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramUpdateDate", _TRN_CourseMark.UpdateDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_TRN_CourseMark_Post", colparameters, true);
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
