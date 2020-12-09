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
	public class TRN_StudentDiscountDAO //: IDisposible
	{
		private static volatile TRN_StudentDiscountDAO instance;
		private static readonly object lockObj = new object();
		public static TRN_StudentDiscountDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new TRN_StudentDiscountDAO();
			}
			return instance;
		}
		public static TRN_StudentDiscountDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new TRN_StudentDiscountDAO();
						}
					}
				}
				return instance;
			}
		}

		DBExecutor dbExecutor;

		public TRN_StudentDiscountDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<TRN_StudentDiscount> Get(Int64? discountId = null)
		{
			try
			{
				List<TRN_StudentDiscount> TRN_StudentDiscountLst = new List<TRN_StudentDiscount>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramDiscountId", discountId, DbType.Int64, ParameterDirection.Input)
				};
				TRN_StudentDiscountLst = dbExecutor.FetchData<TRN_StudentDiscount>(CommandType.StoredProcedure, "wsp_TRN_StudentDiscount_Get", colparameters);
				return TRN_StudentDiscountLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<TRN_StudentDiscount> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<TRN_StudentDiscount> TRN_StudentDiscountLst = new List<TRN_StudentDiscount>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				TRN_StudentDiscountLst = dbExecutor.FetchData<TRN_StudentDiscount>(CommandType.StoredProcedure, "wsp_TRN_StudentDiscount_GetDynamic", colparameters);
				return TRN_StudentDiscountLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(TRN_StudentDiscount _TRN_StudentDiscount, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[10]{
				new Parameters("@paramDiscountId", _TRN_StudentDiscount.DiscountId, DbType.Int64, ParameterDirection.Input),
				new Parameters("@paramStudentId", _TRN_StudentDiscount.StudentId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramApplyOn", _TRN_StudentDiscount.ApplyOn, DbType.String, ParameterDirection.Input),
				new Parameters("@paramIsPercent", _TRN_StudentDiscount.IsPercent, DbType.Boolean, ParameterDirection.Input),
				new Parameters("@paramFigure", _TRN_StudentDiscount.Figure, DbType.Decimal, ParameterDirection.Input),
				new Parameters("@paramStartDate", _TRN_StudentDiscount.StartDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramEndDate", _TRN_StudentDiscount.EndDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramUpdateBy", _TRN_StudentDiscount.UpdateBy, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramUpdateDate", _TRN_StudentDiscount.UpdateDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_TRN_StudentDiscount_Post", colparameters, true);
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
