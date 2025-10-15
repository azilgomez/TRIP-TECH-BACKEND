using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using TRIP.Platform.Service.Core.Entities;
using TRIP.Platform.Service.Core.Interfaces.Infrastructure.Repository;
using TRIP.Platform.Service.Infrastructure.Constants;
using TRIP.Platform.Service.Infrastructure.DBContext;

namespace TRIP.Platform.Service.Infrastructure.Providers.Repository
{
	public class VendorRepository : Repository<Vendor>, IVendorRepository
	{
		private readonly TripDbContext _context;
		public VendorRepository(TripDbContext dbContext) : base(dbContext)
		{
			_context = dbContext;
		}
		/// <summary>
		/// Method to delete the vendor
		/// </summary>
		/// <param name="vendorId"></param>
		/// <param name="loggedUser"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<bool> DeleteVendor(int vendorId, string loggedUser, CancellationToken cancellationToken)
		{
			List<SqlParameter> paramList = new List<SqlParameter>();

			var strParamVendorId = StoredProcedureConstants.Vendor_VendorId_Parameter;
			SqlParameter parameterVendorId = new SqlParameter(strParamVendorId, vendorId)
			{
				SqlDbType = SqlDbType.Int,
				Direction = ParameterDirection.Input
			};
			paramList.Add(parameterVendorId);
			return await this.ExecuteNonQuery(SchemeNames.Common, StoredProcedureConstants.Vendors_Delete_Vendor, string.Join(",", strParamVendorId), paramList, cancellationToken);
		}

		/// <summary>
		/// Method to get vendor by Id
		/// </summary>
		/// <param name="vendorId"></param>
		/// <param name="loggedUser"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<Vendor> GetVendorById(int vendorId, string loggedUser, CancellationToken cancellationToken)
		{
			var strParamVendorId = StoredProcedureConstants.Vendor_VendorId_Parameter;
			SqlParameter parameterVendorId = new SqlParameter(strParamVendorId, vendorId)
			{
				SqlDbType = SqlDbType.Int,
				Direction = ParameterDirection.Input
			};

			List<SqlParameter> paramList = new List<SqlParameter>() { parameterVendorId };
			return await this.ExecuteQueryForOtherEntity<Vendor>(SchemeNames.Common, StoredProcedureConstants.Vendors_Get, string.Join(",", strParamVendorId), paramList, cancellationToken);
		}

		/// <summary>
		/// Method to get all vendors
		/// </summary>
		/// <param name="loggedUser"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<IEnumerable<Vendor>> GetVendors(string loggedUser, CancellationToken cancellationToken)
		{
			return await this.ExecuteQueryForOtherEntities<Vendor>(SchemeNames.Common, StoredProcedureConstants.Vendors_GetAll, cancellationToken);
		}

		/// <summary>
		/// Method to save vendor
		/// </summary>
		/// <param name="vendor"></param>
		/// <param name="loggedUser"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<bool> SaveVendor(Vendor vendor, string loggedUser, CancellationToken cancellationToken)
		{
			List<SqlParameter> paramList = new List<SqlParameter>();

			var strParamVendorId = StoredProcedureConstants.Vendor_VendorId_Parameter;
			SqlParameter parameterVendorId = new SqlParameter(strParamVendorId, vendor.VendorId)
			{
				SqlDbType = SqlDbType.Int,
				Direction = ParameterDirection.Input
			};
			var strParamCompanyName = StoredProcedureConstants.Vendor_CompanyName_Parameter;
			SqlParameter parameterCompanyName = new SqlParameter(strParamCompanyName, vendor.CompanyName)
			{
				SqlDbType = SqlDbType.NVarChar,
				Direction = ParameterDirection.Input
			};
			var strParamName = StoredProcedureConstants.Vendor_VendorName_Parameter;
			SqlParameter parameterName = new SqlParameter(strParamName, vendor.VendorName)
			{
				SqlDbType = SqlDbType.NVarChar,
				Direction = ParameterDirection.Input
			};
			var strParamAddress = StoredProcedureConstants.Vendor_Address_Parameter;
			SqlParameter parameterAddress = new SqlParameter(strParamAddress, vendor.Address)
			{
				SqlDbType = SqlDbType.NVarChar,
				Direction = ParameterDirection.Input
			};
			var strParamEmail = StoredProcedureConstants.Vendor_Email_Parameter;
			SqlParameter parameterEmail = new SqlParameter(strParamEmail, vendor.Email)
			{
				SqlDbType = SqlDbType.NVarChar,
				Direction = ParameterDirection.Input
			};
			var strParamMobile = StoredProcedureConstants.Vendor_MobileNumber_Parameter;
			SqlParameter parameterMobile = new SqlParameter(strParamMobile, vendor.MobileNumber)
			{
				SqlDbType = SqlDbType.NVarChar,
				Direction = ParameterDirection.Input
			};
			var strParamType = StoredProcedureConstants.Vendor_Type_Parameter;
			SqlParameter parameterType = new SqlParameter(strParamType, vendor.Type)
			{
				SqlDbType = SqlDbType.NVarChar,
				Direction = ParameterDirection.Input
			};
			var strParamStatus = StoredProcedureConstants.Vendor_IsActive_Parameter;
			SqlParameter parameterStatus = new SqlParameter(strParamStatus, vendor.IsActive)
			{
				SqlDbType = SqlDbType.Bit,
				Direction = ParameterDirection.Input
			};

			var strloggedUser = StoredProcedureConstants.User_LoggedUser_Parameter;
			SqlParameter parameterLoggedUser = new SqlParameter(StoredProcedureConstants.User_LoggedUser_Parameter, loggedUser);

			paramList.Add(parameterVendorId);
			paramList.Add(parameterCompanyName);
			paramList.Add(parameterName);
			paramList.Add(parameterAddress);
			paramList.Add(parameterEmail);
			paramList.Add(parameterMobile);
			paramList.Add(parameterType);
			paramList.Add(parameterStatus);
			paramList.Add(parameterLoggedUser);
			return await this.ExecuteNonQuery(SchemeNames.Common, StoredProcedureConstants.Vendor_Insert_Vendor, string.Join(",", strParamVendorId, strParamCompanyName, strParamName,
				strParamAddress, strParamEmail, strParamMobile,  strParamType, strParamStatus, strloggedUser), paramList, cancellationToken);
		}
	}
}
