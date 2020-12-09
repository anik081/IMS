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
	public class TRN_CourseOfferDAO //: IDisposible
	{
		private static volatile TRN_CourseOfferDAO instance;
		private static readonly object lockObj = new object();
		public static TRN_CourseOfferDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new TRN_CourseOfferDAO();
			}
			return instance;
		}
		public static TRN_CourseOfferDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new TRN_CourseOfferDAO();
						}
					}
				}
				return instance;
			}
		}

		DBExecutor dbExecutor;

		public TRN_CourseOfferDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<TRN_CourseOffer> Get(Int64? courseOfferId = null)
		{
			try
			{
				List<TRN_CourseOffer> TRN_CourseOfferLst = new List<TRN_CourseOffer>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramCourseOfferId", courseOfferId, DbType.Int64, ParameterDirection.Input)
				};
				TRN_CourseOfferLst = dbExecutor.FetchData<TRN_CourseOffer>(CommandType.StoredProcedure, "wsp_TRN_CourseOffer_Get", colparameters);
				return TRN_CourseOfferLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<TRN_CourseOffer> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<TRN_CourseOffer> TRN_CourseOfferLst = new List<TRN_CourseOffer>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				TRN_CourseOfferLst = dbExecutor.FetchData<TRN_CourseOffer>(CommandType.StoredProcedure, "wsp_TRN_CourseOffer_GetDynamic", colparameters);
                

                return TRN_CourseOfferLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(TRN_CourseOffer _TRN_CourseOffer, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[11]{
				new Parameters("@paramCourseOfferId", _TRN_CourseOffer.CourseOfferId, DbType.Int64, ParameterDirection.Input),
				new Parameters("@paramCampusId", _TRN_CourseOffer.CampusId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramCourseId", _TRN_CourseOffer.CourseId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramSemesterId", _TRN_CourseOffer.SemesterId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramInstructorId", _TRN_CourseOffer.InstructorId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramTotalSeat", _TRN_CourseOffer.TotalSeat, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramScheduleSummary", _TRN_CourseOffer.ScheduleSummary, DbType.String, ParameterDirection.Input),
				new Parameters("@paramIsActive", _TRN_CourseOffer.IsActive, DbType.Boolean, ParameterDirection.Input),
				new Parameters("@paramUpdateBy", _TRN_CourseOffer.UpdateBy, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramUpdateDate", _TRN_CourseOffer.UpdateDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_TRN_CourseOffer_Post", colparameters, true);
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
