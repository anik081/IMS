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
	public class LU_CourseDAO //: IDisposible
	{
		private static volatile LU_CourseDAO instance;
		private static readonly object lockObj = new object();
		public static LU_CourseDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new LU_CourseDAO();
			}
			return instance;
		}
		public static LU_CourseDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new LU_CourseDAO();
						}
					}
				}
				return instance;
			}
		}

		DBExecutor dbExecutor;

		public LU_CourseDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<LU_Course> Get(Int32? courseId = null)
		{
			try
			{
				List<LU_Course> LU_CourseLst = new List<LU_Course>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramCourseId", courseId, DbType.Int32, ParameterDirection.Input)
				};
				LU_CourseLst = dbExecutor.FetchData<LU_Course>(CommandType.StoredProcedure, "wsp_LU_Course_Get", colparameters);
				return LU_CourseLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<LU_Course> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<LU_Course> LU_CourseLst = new List<LU_Course>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				LU_CourseLst = dbExecutor.FetchData<LU_Course>(CommandType.StoredProcedure, "wsp_LU_Course_GetDynamic", colparameters);
				return LU_CourseLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(LU_Course _LU_Course, string transactionType)
		{
			string ret = string.Empty;
			try
			{
                _LU_Course.InstituteId = 1;

                Parameters[] colparameters = new Parameters[10]{
				new Parameters("@paramCourseId", _LU_Course.CourseId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramInstituteId", _LU_Course.InstituteId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramCourseCode", _LU_Course.CourseCode, DbType.String, ParameterDirection.Input),
				new Parameters("@paramCourseTitle", _LU_Course.CourseTitle, DbType.String, ParameterDirection.Input),
				new Parameters("@paramCredits", _LU_Course.Credits, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramInstructorId", _LU_Course.InstructorId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramIsActive", _LU_Course.IsActive, DbType.Boolean, ParameterDirection.Input),
				new Parameters("@paramUpdateBy", _LU_Course.UpdateBy, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramUpdateDate", _LU_Course.UpdateDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_LU_Course_Post", colparameters, true);
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
