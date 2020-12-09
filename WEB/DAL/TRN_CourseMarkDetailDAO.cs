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
	public class TRN_CourseMarkDetailDAO //: IDisposible
	{
		private static volatile TRN_CourseMarkDetailDAO instance;
		private static readonly object lockObj = new object();
		public static TRN_CourseMarkDetailDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new TRN_CourseMarkDetailDAO();
			}
			return instance;
		}
		public static TRN_CourseMarkDetailDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new TRN_CourseMarkDetailDAO();
						}
					}
				}
				return instance;
			}
		}

		DBExecutor dbExecutor;

		public TRN_CourseMarkDetailDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<TRN_CourseMarkDetail> Get(Int64? courseMarkDetailId = null)
		{
			try
			{
				List<TRN_CourseMarkDetail> TRN_CourseMarkDetailLst = new List<TRN_CourseMarkDetail>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramCourseMarkDetailId", courseMarkDetailId, DbType.Int64, ParameterDirection.Input)
				};
				TRN_CourseMarkDetailLst = dbExecutor.FetchData<TRN_CourseMarkDetail>(CommandType.StoredProcedure, "wsp_TRN_CourseMarkDetail_Get", colparameters);
				return TRN_CourseMarkDetailLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<TRN_CourseMarkDetail> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<TRN_CourseMarkDetail> TRN_CourseMarkDetailLst = new List<TRN_CourseMarkDetail>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				TRN_CourseMarkDetailLst = dbExecutor.FetchData<TRN_CourseMarkDetail>(CommandType.StoredProcedure, "wsp_TRN_CourseMarkDetail_GetDynamic", colparameters);
				return TRN_CourseMarkDetailLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(TRN_CourseMarkDetail _TRN_CourseMarkDetail, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[5]{
				new Parameters("@paramCourseMarkDetailId", _TRN_CourseMarkDetail.CourseMarkDetailId, DbType.Int64, ParameterDirection.Input),
				new Parameters("@paramCourseMarkId", _TRN_CourseMarkDetail.CourseMarkId, DbType.Int64, ParameterDirection.Input),
				new Parameters("@paramStudentId", _TRN_CourseMarkDetail.StudentId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramStudentMark", _TRN_CourseMarkDetail.StudentMark, DbType.Decimal, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_TRN_CourseMarkDetail_Post", colparameters, true);
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
