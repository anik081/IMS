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
	public class LU_ProgramDAO //: IDisposible
	{
		private static volatile LU_ProgramDAO instance;
		private static readonly object lockObj = new object();
		public static LU_ProgramDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new LU_ProgramDAO();
			}
			return instance;
		}
		public static LU_ProgramDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new LU_ProgramDAO();
						}
					}
				}
				return instance;
			}
		}

		DBExecutor dbExecutor;

		public LU_ProgramDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<LU_Program> Get(Int32? programId = null)
		{
			try
			{
				List<LU_Program> LU_ProgramLst = new List<LU_Program>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramProgramId", programId, DbType.Int32, ParameterDirection.Input)
				};
				LU_ProgramLst = dbExecutor.FetchData<LU_Program>(CommandType.StoredProcedure, "wsp_LU_Program_Get", colparameters);
				return LU_ProgramLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<LU_Program> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<LU_Program> LU_ProgramLst = new List<LU_Program>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				LU_ProgramLst = dbExecutor.FetchData<LU_Program>(CommandType.StoredProcedure, "wsp_LU_Program_GetDynamic", colparameters);
				return LU_ProgramLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(LU_Program _LU_Program, string transactionType)
		{
			string ret = string.Empty;
			try
			{
                _LU_Program.InstituteId = 1;
               // _LU_Program.MinCreditPerSemester = 1;
               // _LU_Program.MaxCreditPerSemester = 1;

                Parameters[] colparameters = new Parameters[13]{
				new Parameters("@paramProgramId", _LU_Program.ProgramId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramInstituteId", _LU_Program.InstituteId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramProgramHeadId", _LU_Program.ProgramHeadId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramProgramTitle", _LU_Program.ProgramTitle, DbType.String, ParameterDirection.Input),
				new Parameters("@paramProgramCode", _LU_Program.ProgramCode, DbType.String, ParameterDirection.Input),
				new Parameters("@paramTotalCredit", _LU_Program.TotalCredit, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramDurationInMonth", _LU_Program.DurationInMonth, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramMinCreditPerSemester", _LU_Program.MinCreditPerSemester, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramMaxCreditPerSemester", _LU_Program.MaxCreditPerSemester, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramIsActive", _LU_Program.IsActive, DbType.Boolean, ParameterDirection.Input),
				new Parameters("@paramUpdateBy", _LU_Program.UpdateBy, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramUpdateDate", _LU_Program.UpdateDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_LU_Program_Post", colparameters, true);
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
