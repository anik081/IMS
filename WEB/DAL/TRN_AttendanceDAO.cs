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
	public class TRN_AttendanceDAO //: IDisposible
	{
		private static volatile TRN_AttendanceDAO instance;
		private static readonly object lockObj = new object();
		public static TRN_AttendanceDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new TRN_AttendanceDAO();
			}
			return instance;
		}
		public static TRN_AttendanceDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new TRN_AttendanceDAO();
						}
					}
				}
				return instance;
			}
		}

		DBExecutor dbExecutor;

		public TRN_AttendanceDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<TRN_Attendance> Get(Int64? attendanceId = null)
		{
			try
			{
				List<TRN_Attendance> TRN_AttendanceLst = new List<TRN_Attendance>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramAttendanceId", attendanceId, DbType.Int64, ParameterDirection.Input)
				};
				TRN_AttendanceLst = dbExecutor.FetchData<TRN_Attendance>(CommandType.StoredProcedure, "wsp_TRN_Attendance_Get", colparameters);
				return TRN_AttendanceLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<TRN_Attendance> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<TRN_Attendance> TRN_AttendanceLst = new List<TRN_Attendance>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				TRN_AttendanceLst = dbExecutor.FetchData<TRN_Attendance>(CommandType.StoredProcedure, "wsp_TRN_Attendance_GetDynamic", colparameters);
				return TRN_AttendanceLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(TRN_Attendance _TRN_Attendance, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[9]{
				new Parameters("@paramAttendanceId", _TRN_Attendance.AttendanceId, DbType.Int64, ParameterDirection.Input),
				new Parameters("@paramCourseOfferId", _TRN_Attendance.CourseOfferId, DbType.Int64, ParameterDirection.Input),
				new Parameters("@paramAttendanceDate", _TRN_Attendance.AttendanceDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramAttendanceTypeId", _TRN_Attendance.AttendanceTypeId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramStudentId", _TRN_Attendance.StudentId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramIsAttend", _TRN_Attendance.IsAttend, DbType.Boolean, ParameterDirection.Input),
				new Parameters("@paramUpdateBy", _TRN_Attendance.UpdateBy, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramUpdateDate", _TRN_Attendance.UpdateDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_TRN_Attendance_Post", colparameters, true);
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
