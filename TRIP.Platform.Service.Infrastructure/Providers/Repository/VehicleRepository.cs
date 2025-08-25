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
	public class VehicleRepository : Repository<Vehicle>, IVehicleRepository
	{
		private readonly TripDbContext _context;
		public VehicleRepository(TripDbContext dbContext) : base(dbContext)
		{
			_context = dbContext;
		}

		/// <summary>
		/// Method to save Vehicle
		/// </summary>
		/// <param name="vehicle"></param>
		/// <param name="loggedUser"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<bool> SaveVehicle(Vehicle vehicle, string loggedUser, CancellationToken cancellationToken)
		{
			List<SqlParameter> paramList = new List<SqlParameter>();

			var strParamVehicleId = StoredProcedureConstants.Vehicle_VehicleId_Parameter;
			SqlParameter parameterVehicleId = new SqlParameter(strParamVehicleId, vehicle.VehicleId)
			{
				SqlDbType = SqlDbType.Int,
				Direction = ParameterDirection.Input
			};
			var strParamName = StoredProcedureConstants.Vehicle_VehicleName_Parameter;
			SqlParameter parameterName = new SqlParameter(strParamName, vehicle.VehicleName)
			{
				SqlDbType = SqlDbType.NVarChar,
				Direction = ParameterDirection.Input
			};
			var strParamType = StoredProcedureConstants.Vehicle_VehicleType_Parameter;
			SqlParameter parameterType = new SqlParameter(strParamType, vehicle.VehicleType)
			{
				SqlDbType = SqlDbType.NVarChar,
				Direction = ParameterDirection.Input
			};
			var strParamOwner = StoredProcedureConstants.Vehicle_VehicleOwner_Parameter;
			SqlParameter parameterOwner = new SqlParameter(strParamOwner, vehicle.VehicleOwner)
			{
				SqlDbType = SqlDbType.NVarChar,
				Direction = ParameterDirection.Input
			};
			var strParamYear = StoredProcedureConstants.Vehicle_VehicleYear_Parameter;
			SqlParameter parameterYear = new SqlParameter(strParamYear, vehicle.VehicleYear)
			{
				SqlDbType = SqlDbType.NVarChar,
				Direction = ParameterDirection.Input
			};
			var strParamCapacity = StoredProcedureConstants.Vehicle_Capacity_Parameter;
			SqlParameter parameterVehicleCapcity = new SqlParameter(strParamCapacity, vehicle.Capacity)
			{
				SqlDbType = SqlDbType.Int,
				Direction = ParameterDirection.Input
			};
			var strParamAmenities = StoredProcedureConstants.Vehicle_Amenities_Parameter;
			SqlParameter parameterAmenities = new SqlParameter(strParamAmenities, vehicle.Amenities)
			{
				SqlDbType = SqlDbType.NVarChar,
				Direction = ParameterDirection.Input
			};
			var strParamStatusId = StoredProcedureConstants.Vehicle_Status_Parameter;
			SqlParameter parameterStatus = new SqlParameter(strParamStatusId, vehicle.Status)
			{
				SqlDbType = SqlDbType.Int,
				Direction = ParameterDirection.Input
			};

			var strloggedUser = StoredProcedureConstants.User_LoggedUser_Parameter;
			SqlParameter parameterLoggedUser = new SqlParameter(StoredProcedureConstants.User_LoggedUser_Parameter, loggedUser);

			paramList.Add(parameterVehicleId);
			paramList.Add(parameterName);
			paramList.Add(parameterType);
			paramList.Add(parameterOwner);
			paramList.Add(parameterYear);
			paramList.Add(parameterVehicleCapcity);
			paramList.Add(parameterAmenities);
			paramList.Add(parameterStatus);
			paramList.Add(parameterLoggedUser);
			return await this.ExecuteNonQuery(SchemeNames.Common, StoredProcedureConstants.Vehicle_Insert_Vehicle, string.Join(",", strParamVehicleId, strParamName,
				strParamType, strParamOwner, strParamYear, strParamCapacity, strParamAmenities, strParamStatusId, strloggedUser), paramList, cancellationToken);
		}

		/// <summary>
		/// Method to delete vehicle
		/// </summary>
		/// <param name="vehicleId"></param>
		/// <param name="loggedUser"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<bool> DeleteVehicle(int vehicleId, string loggedUser, CancellationToken cancellationToken)
		{
			List<SqlParameter> paramList = new List<SqlParameter>();

			var strParamVehicleId = StoredProcedureConstants.Vehicle_VehicleId_Parameter;
			SqlParameter parameterVehicleId = new SqlParameter(strParamVehicleId, vehicleId)
			{
				SqlDbType = SqlDbType.Int,
				Direction = ParameterDirection.Input
			};
			paramList.Add(parameterVehicleId);
			return await this.ExecuteNonQuery(SchemeNames.Common, StoredProcedureConstants.Vehicle_Delete_Vehicle, string.Join(",", strParamVehicleId), paramList, cancellationToken);
		}

		/// <summary>
		/// Method to get vehicles
		/// </summary>
		/// <param name="loggedUser"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<IEnumerable<Vehicle>> GetVehicles(string loggedUser, CancellationToken cancellationToken)
		{
			return await this.ExecuteQueryForOtherEntities<Vehicle>(SchemeNames.Common, StoredProcedureConstants.Vehicles_GetAll, cancellationToken);
		}

		/// <summary>
		/// Method to get Vehicle by Id
		/// </summary>
		/// <param name="vehicleId"></param>
		/// <param name="loggedUser"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<Vehicle> GetVehicleById(int vehicleId, string loggedUser, CancellationToken cancellationToken)
		{
			var strParamVehicleId = StoredProcedureConstants.Vehicle_VehicleId_Parameter;
			SqlParameter parameterVehicleId = new SqlParameter(strParamVehicleId, vehicleId)
			{
				SqlDbType = SqlDbType.NVarChar,
				Direction = ParameterDirection.Input
			};

			List<SqlParameter> paramList = new List<SqlParameter>() { parameterVehicleId };
			return await this.ExecuteQueryForOtherEntity<Vehicle>(SchemeNames.Common, StoredProcedureConstants.Vehicles_Get, string.Join(",", strParamVehicleId), paramList, cancellationToken);
		}
	}
}
