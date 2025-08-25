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
	public class DriverRepository : Repository<Driver>, IDriverRepository
	{
		private readonly TripDbContext _context;
		public DriverRepository(TripDbContext dbContext) : base(dbContext)
		{
			_context = dbContext;
		}

		/// <summary>
		/// Method to save Driver
		/// </summary>
		/// <param name="driver"></param>
		/// <param name="loggedUser"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<bool> SaveDriver(Driver driver, string loggedUser, CancellationToken cancellationToken)
		{
			List<SqlParameter> paramList = new List<SqlParameter>();

			var strParamDriverId = StoredProcedureConstants.Driver_DriverId_Parameter;
			SqlParameter parameterDriverId = new SqlParameter(strParamDriverId, driver.DriverId)
			{
				SqlDbType = SqlDbType.Int,
				Direction = ParameterDirection.Input
			};
			var strParamType = StoredProcedureConstants.Driver_DriverType_Parameter;
			SqlParameter parameterType = new SqlParameter(strParamType, driver.DriverType)
			{
				SqlDbType = SqlDbType.NVarChar,
				Direction = ParameterDirection.Input
			};
			var strParamName = StoredProcedureConstants.Driver_DriverName_Parameter;
			SqlParameter parameterName = new SqlParameter(strParamName, driver.DriverName)
			{
				SqlDbType = SqlDbType.NVarChar,
				Direction = ParameterDirection.Input
			};
			var strParamLanguage = StoredProcedureConstants.Driver_Language_Parameter;
			SqlParameter parameterLanguage = new SqlParameter(strParamLanguage, driver.Language)
			{
				SqlDbType = SqlDbType.NVarChar,
				Direction = ParameterDirection.Input
			};
			var strParamLocation = StoredProcedureConstants.Driver_Location_Parameter;
			SqlParameter parameterLocation = new SqlParameter(strParamLocation, driver.Location)
			{
				SqlDbType = SqlDbType.NVarChar,
				Direction = ParameterDirection.Input
			};
			var strParamExperience = StoredProcedureConstants.Driver_Experience_Parameter;
			SqlParameter parameterExperience = new SqlParameter(strParamExperience, driver.Experience)
			{
				SqlDbType = SqlDbType.NVarChar,
				Direction = ParameterDirection.Input
			};
			var strParamLicense = StoredProcedureConstants.Driver_License_Parameter;
			SqlParameter parameterLicense = new SqlParameter(strParamLicense, driver.LicenseNumber)
			{
				SqlDbType = SqlDbType.NVarChar,
				Direction = ParameterDirection.Input
			};

			var strParamContactNumber = StoredProcedureConstants.Driver_ContactNumber_Parameter;
			SqlParameter parameterContactNumber = new SqlParameter(strParamContactNumber, driver.ContactNumber)
			{
				SqlDbType = SqlDbType.NVarChar,
				Direction = ParameterDirection.Input
			};
			var strParamStatus = StoredProcedureConstants.Driver_Status_Parameter;
			SqlParameter parameterStatus = new SqlParameter(strParamStatus, driver.Status)
			{
				SqlDbType = SqlDbType.Bit,
				Direction = ParameterDirection.Input
			};
			var strloggedUser = StoredProcedureConstants.User_LoggedUser_Parameter;
			SqlParameter parameterLoggedUser = new SqlParameter(StoredProcedureConstants.User_LoggedUser_Parameter, loggedUser);

			paramList.Add(parameterDriverId);
			paramList.Add(parameterType);
			paramList.Add(parameterName);
			paramList.Add(parameterLanguage);
			paramList.Add(parameterLocation);
			paramList.Add(parameterExperience);
			paramList.Add(parameterLicense);
			paramList.Add(parameterContactNumber);
			paramList.Add(parameterStatus);
			paramList.Add(parameterLoggedUser);
			return await this.ExecuteNonQuery(SchemeNames.Common, StoredProcedureConstants.Driver_Insert_Driver, string.Join(",", strParamDriverId, strParamType, strParamName,
				strParamLanguage, strParamLocation, strParamExperience, strParamLicense, strParamContactNumber, strParamStatus, strloggedUser), paramList, cancellationToken);
		}

		/// <summary>
		/// Method to delete driver
		/// </summary>
		/// <param name="driverId"></param>
		/// <param name="loggedUser"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<bool> DeleteDriver(int driverId, string loggedUser, CancellationToken cancellationToken)
		{
			List<SqlParameter> paramList = new List<SqlParameter>();

			var strParamDriverId = StoredProcedureConstants.Driver_DriverId_Parameter;
			SqlParameter parameterDriverId = new SqlParameter(strParamDriverId, driverId)
			{
				SqlDbType = SqlDbType.Int,
				Direction = ParameterDirection.Input
			};
			paramList.Add(parameterDriverId);
			return await this.ExecuteNonQuery(SchemeNames.Common, StoredProcedureConstants.Driver_Delete_Driver, string.Join(",", strParamDriverId), paramList, cancellationToken);
		}

		/// <summary>
		/// Method to get drivers
		/// </summary>
		/// <param name="loggedUser"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<IEnumerable<Driver>> GetDrivers(string loggedUser, CancellationToken cancellationToken)
		{
			return await this.ExecuteQueryForOtherEntities<Driver>(SchemeNames.Common, StoredProcedureConstants.Drivers_GetAll, cancellationToken);
		}

		/// <summary>
		/// Method to get Driver by Id
		/// </summary>
		/// <param name="driverId"></param>
		/// <param name="loggedUser"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<Driver> GetDriverById(int driverId, string loggedUser, CancellationToken cancellationToken)
		{
			var strParamDriverId = StoredProcedureConstants.Driver_DriverId_Parameter;
			SqlParameter parameterDriverId = new SqlParameter(strParamDriverId, driverId)
			{
				SqlDbType = SqlDbType.NVarChar,
				Direction = ParameterDirection.Input
			};

			List<SqlParameter> paramList = new List<SqlParameter>() { parameterDriverId };
			return await this.ExecuteQueryForOtherEntity<Driver>(SchemeNames.Common, StoredProcedureConstants.Drivers_Get, string.Join(",", strParamDriverId), paramList, cancellationToken);
		}
	}
}
