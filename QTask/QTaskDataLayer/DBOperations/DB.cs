using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTaskDataLayer.DBOperations
{
	public class DB
	{
		SqlConnection conn;
		IConfiguration Config;
		string ConnectionString = string.Empty;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public DB(IConfiguration _config)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		{
			Config = _config;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
			ConnectionString = Config.GetConnectionString("QTask").ToString();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
		}

		public DataSet getDataFromDBToDataSet(string ProcName, SqlParameter[] param)
		{
			DataSet ds = new DataSet();
#pragma warning disable CS0168 // Variable is declared but never used
			try
			{
				conn = new SqlConnection(ConnectionString);
				if (conn.State == ConnectionState.Closed)
					conn.Open();

				SqlCommand cmd = conn.CreateCommand();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = ProcName;
				if (param != null)
					cmd.Parameters.AddRange(param);

				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(ds);
			}
			catch (Exception ex)
			{
				throw;
			}
			finally
			{
				if (conn.State == ConnectionState.Open)
					conn.Close();
			}
#pragma warning restore CS0168 // Variable is declared but never used
			return ds;
		}

		public int InsertData(string ProcName, SqlParameter[] param)
		{
			int Result = 0;
#pragma warning disable CS0168 // Variable is declared but never used
			try
			{
				conn = new SqlConnection(ConnectionString);
				if (conn.State == ConnectionState.Closed)
					conn.Open();

				SqlCommand cmd = conn.CreateCommand();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = ProcName;
				cmd.Parameters.AddRange(param);

				Result = cmd.ExecuteNonQuery();

			}
			catch (Exception ex)
			{
				throw;
			}
			finally
			{
				if (conn.State == ConnectionState.Open)
					conn.Close();
			}
#pragma warning restore CS0168 // Variable is declared but never used
			return Result;
		}

		public int DeleteData(string ProcName, SqlParameter[] param)
		{
			int Result = 0;
			try
			{
				conn = new SqlConnection(ConnectionString);
				if (conn.State == ConnectionState.Closed)
					conn.Open();

				SqlCommand cmd = conn.CreateCommand();

				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = ProcName;
				cmd.Parameters.AddRange(param);

				Result = cmd.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (conn.State == ConnectionState.Open)
					conn.Close();
			}

			return Result;
		}
	}
}
