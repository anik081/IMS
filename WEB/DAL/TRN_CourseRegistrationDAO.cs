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
	public class TRN_CourseRegistrationDAO //: IDisposible
	{
		private static volatile TRN_CourseRegistrationDAO instance;
		private static readonly object lockObj = new object();
		public static TRN_CourseRegistrationDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new TRN_CourseRegistrationDAO();
			}
			return instance;
		}
		public static TRN_CourseRegistrationDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new TRN_CourseRegistrationDAO();
						}
					}
				}
				return instance;
			}
		}

		DBExecutor dbExecutor;

		public TRN_CourseRegistrationDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<TRN_CourseRegistration> Get(Int64? courseRegistrationId = null)
		{
			try
			{
				List<TRN_CourseRegistration> TRN_CourseRegistrationLst = new List<TRN_CourseRegistration>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramCourseRegistrationId", courseRegistrationId, DbType.Int64, ParameterDirection.Input)
				};
				TRN_CourseRegistrationLst = dbExecutor.FetchData<TRN_CourseRegistration>(CommandType.StoredProcedure, "wsp_TRN_CourseRegistration_Get", colparameters);
				return TRN_CourseRegistrationLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<TRN_CourseRegistration> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<TRN_CourseRegistration> TRN_CourseRegistrationLst = new List<TRN_CourseRegistration>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				TRN_CourseRegistrationLst = dbExecutor.FetchData<TRN_CourseRegistration>(CommandType.StoredProcedure, "wsp_TRN_CourseRegistration_GetDynamic", colparameters);
				return TRN_CourseRegistrationLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(TRN_CourseRegistration _TRN_CourseRegistration, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[10]{
				new Parameters("@paramCourseRegistrationId", _TRN_CourseRegistration.CourseRegistrationId, DbType.Int64, ParameterDirection.Input),
				new Parameters("@paramCourseOfferId", _TRN_CourseRegistration.CourseOfferId, DbType.Int64, ParameterDirection.Input),
				new Parameters("@paramRegistrationDate", _TRN_CourseRegistration.RegistrationDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramStudentId", _TRN_CourseRegistration.StudentId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramRegStatusId", _TRN_CourseRegistration.RegStatusId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramCounseledby", _TRN_CourseRegistration.Counseledby, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramRemarks", _TRN_CourseRegistration.Remarks, DbType.String, ParameterDirection.Input),
				new Parameters("@paramUpdateBy", _TRN_CourseRegistration.UpdateBy, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramUpdateDate", _TRN_CourseRegistration.UpdateDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_TRN_CourseRegistration_Post", colparameters, true);
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
