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
	public class TRN_StudentPaymentDAO //: IDisposible
	{
		private static volatile TRN_StudentPaymentDAO instance;
		private static readonly object lockObj = new object();
		public static TRN_StudentPaymentDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new TRN_StudentPaymentDAO();
			}
			return instance;
		}
		public static TRN_StudentPaymentDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new TRN_StudentPaymentDAO();
						}
					}
				}
				return instance;
			}
		}

		DBExecutor dbExecutor;

		public TRN_StudentPaymentDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<TRN_StudentPayment> Get(Int64? paymentId = null)
		{
			try
			{
				List<TRN_StudentPayment> TRN_StudentPaymentLst = new List<TRN_StudentPayment>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramPaymentId", paymentId, DbType.Int64, ParameterDirection.Input)
				};
				TRN_StudentPaymentLst = dbExecutor.FetchData<TRN_StudentPayment>(CommandType.StoredProcedure, "wsp_TRN_StudentPayment_Get", colparameters);
				return TRN_StudentPaymentLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<TRN_StudentPayment> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<TRN_StudentPayment> TRN_StudentPaymentLst = new List<TRN_StudentPayment>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				TRN_StudentPaymentLst = dbExecutor.FetchData<TRN_StudentPayment>(CommandType.StoredProcedure, "wsp_TRN_StudentPayment_GetDynamic", colparameters);
				return TRN_StudentPaymentLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(TRN_StudentPayment _TRN_StudentPayment, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[17]{
				new Parameters("@paramPaymentId", _TRN_StudentPayment.PaymentId, DbType.Int64, ParameterDirection.Input),
				new Parameters("@paramStudentId", _TRN_StudentPayment.StudentId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramPaymentPurpose", _TRN_StudentPayment.PaymentPurpose, DbType.String, ParameterDirection.Input),
				new Parameters("@paramReceivedById", _TRN_StudentPayment.ReceivedById, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramReceiptNo", _TRN_StudentPayment.ReceiptNo, DbType.String, ParameterDirection.Input),
				new Parameters("@paramAmount", _TRN_StudentPayment.Amount, DbType.Decimal, ParameterDirection.Input),
				new Parameters("@paramPreviousDue", _TRN_StudentPayment.PreviousDue, DbType.Decimal, ParameterDirection.Input),
				new Parameters("@paramPayMethodId", _TRN_StudentPayment.PayMethodId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramPayMethodNo", _TRN_StudentPayment.PayMethodNo, DbType.String, ParameterDirection.Input),
				new Parameters("@paramUpdateBy", _TRN_StudentPayment.UpdateBy, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramBank", _TRN_StudentPayment.Bank, DbType.String, ParameterDirection.Input),
				new Parameters("@paramBranch", _TRN_StudentPayment.Branch, DbType.String, ParameterDirection.Input),
				new Parameters("@paramChequeNo", _TRN_StudentPayment.ChequeNo, DbType.String, ParameterDirection.Input),
				new Parameters("@paramPaymentChequeDate", _TRN_StudentPayment.PaymentChequeDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramUpdateDate", _TRN_StudentPayment.UpdateDate, DbType.DateTime, ParameterDirection.Input),
                new Parameters("@paramPaymentDate", _TRN_StudentPayment.PaymentDate, DbType.DateTime, ParameterDirection.Input),
                new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_TRN_StudentPayment_Post", colparameters, true);
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
