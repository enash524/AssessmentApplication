using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using AssessmentApplication.DataContracts;

namespace AssessmentApplication.DAL
{
	public class SalesOrderDM : ISalesOrderDM
	{
		#region Public Methods

		public async Task<List<SalesOrderHeader>> GetAllSalesOrderHeaderAsync(string customerName, DateTime? dueDateEnd, DateTime? dueDateStart, DateTime? orderDateEnd, DateTime? orderDateStart, DateTime? shipDateEnd, DateTime? shipDateStart)
		{
			string cs = GetConnectionString();
			List<SalesOrderHeader> sales = new List<SalesOrderHeader>();

			using (SqlConnection conn = new SqlConnection(cs))
			{
				await conn.OpenAsync();

				using (SqlCommand cmd = new SqlCommand())
				{
					cmd.CommandText = "Sales.uspSearchSalesOrderHeader";
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Connection = conn;
					cmd.Parameters.AddRange(new[]
					{
						new SqlParameter("@orderDateStart", orderDateStart),
						new SqlParameter("@orderDateEnd", orderDateEnd),
						new SqlParameter("@dueDateStart", dueDateStart),
						new SqlParameter("@dueDateEnd", dueDateEnd),
						new SqlParameter("@shipDateStart", shipDateStart),
						new SqlParameter("@shipDateEnd", shipDateEnd),
						new SqlParameter("@customerName", customerName)
					});

					using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
					{
						if (dr.IsClosed)
							return null;

						while (await dr.ReadAsync())
						{
							SalesOrderHeader item = new SalesOrderHeader
							{
								SalesOrderId = dr.GetInt32(dr.GetOrdinal("SalesOrderID")),
								AccountNumber = dr["AccountNumber"] as string,
								SubTotal = dr.GetDecimal(dr.GetOrdinal("SubTotal")),
								TaxAmt = dr.GetDecimal(dr.GetOrdinal("TaxAmt")),
								Freight = dr.GetDecimal(dr.GetOrdinal("Freight")),
								TotalDue = dr.GetDecimal(dr.GetOrdinal("TotalDue")),
								ShipToAddress = new Address
								{
									AddressId = dr.GetInt32(dr.GetOrdinal("AddressID")),
									AddressLine1 = dr["AddressLine1"] as string,
									AddressLine2 = dr["AddressLine2"] as string,
									City = dr["City"] as string,
									StateProvinceCode = dr["StateProvinceCode"] as string,
									PostalCode = dr["PostalCode"] as string
								},
								Person = new Person
								{
									BusinessEntityId = dr.GetInt32(dr.GetOrdinal("BusinessEntityID")),
									Title = dr["Title"] as string,
									FirstName = dr["FirstName"] as string,
									MiddleName = dr["MiddleName"] as string,
									LastName = dr["LastName"] as string,
									Suffix = dr["Suffix"] as string
								},
								ShipMethod = new ShipMethod
								{
									ShipMethodId = dr.GetInt32(dr.GetOrdinal("ShipMethodID")),
									Name = dr["Name"] as string
								}
							};

							sales.Add(item);
						}
					}
				}
			}

			return sales;
		}

		public async Task<List<SalesOrderDetail>> GetSalesOrderDetailAsync(int salesOrderId)
		{
			string cs = GetConnectionString();
			List<SalesOrderDetail> details = new List<SalesOrderDetail>();

			using (SqlConnection conn = new SqlConnection(cs))
			{
				await conn.OpenAsync();

				using (SqlCommand cmd = new SqlCommand())
				{
					cmd.CommandText = "Sales.uspGetSalesOrderDetail";
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Connection = conn;
					cmd.Parameters.AddWithValue("@salesOrderId", salesOrderId);

					using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
					{
						if (dr.IsClosed)
							return null;

						while (await dr.ReadAsync())
						{
							SalesOrderDetail item = new SalesOrderDetail
							{
								Name = dr.GetString(dr.GetOrdinal("Name")),
								ProductNumber = dr.GetString(dr.GetOrdinal("ProductNumber")),
								OrderQty = dr.GetInt16(dr.GetOrdinal("OrderQty")),
								UnitPrice = dr.GetDecimal(dr.GetOrdinal("UnitPrice")),
								UnitPriceDiscount = dr.GetDecimal(dr.GetOrdinal("UnitPriceDiscount")),
								LineTotal = dr.GetDecimal(dr.GetOrdinal("LineTotal"))
							};

							details.Add(item);
						}
					}
				}
			}

			return details;
		}

		#endregion Public Methods

		#region Private Methods

		private string GetConnectionString()
		{
			return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
		}

		#endregion Private Methods
	}
}