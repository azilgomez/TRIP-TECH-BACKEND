using EY.CTP.SRED.Platform.Service.Core.Entities;
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
	public class TripRepository : Repository<Trip>, ITripRepository
	{
		private readonly TripDbContext _context;
		public TripRepository(TripDbContext dbContext) : base(dbContext)
		{
			_context = dbContext;
		}

		/// <summary>
		/// Method to delete trip
		/// </summary>
		/// <param name="tripId"></param>
		/// <param name="loggedUser"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<bool> DeleteTrip(int tripId, string loggedUser, CancellationToken cancellationToken)
		{
			List<SqlParameter> paramList = new List<SqlParameter>();

			var strParamTripId = StoredProcedureConstants.Trip_TripId_Parameter;
			SqlParameter parameterTripId = new SqlParameter(strParamTripId, tripId)
			{
				SqlDbType = SqlDbType.Int,
				Direction = ParameterDirection.Input
			};
			paramList.Add(parameterTripId);
			return await this.ExecuteNonQuery(SchemeNames.Common, StoredProcedureConstants.Trip_Delete, string.Join(",", strParamTripId), paramList, cancellationToken);
		}

		/// <summary>
		/// Method to get trip by Id
		/// </summary>
		/// <param name="tripId"></param>
		/// <param name="loggedUser"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<TripDetail> GetTripById(int tripId, string loggedUser, CancellationToken cancellationToken)
		{
			var strParamTripId = StoredProcedureConstants.Trip_TripId_Parameter;
			SqlParameter parameterTripId = new SqlParameter(strParamTripId, tripId)
			{
				SqlDbType = SqlDbType.Int,
				Direction = ParameterDirection.Input
			};

			List<SqlParameter> paramList = new List<SqlParameter>() { parameterTripId };
			return await this.ExecuteQueryForOtherEntity<TripDetail>(SchemeNames.Common, StoredProcedureConstants.Trip_Get, string.Join(",", strParamTripId), paramList, cancellationToken);
		}

		/// <summary>
		/// Method to get trip vehicle
		/// </summary>
		/// <param name="tripId"></param>
		/// <param name="loggedUser"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<IEnumerable<TripVehicle>> GetTripVehiclesById(int tripId, string loggedUser, CancellationToken cancellationToken)
		{
			var strParamTripId = StoredProcedureConstants.Trip_TripId_Parameter;
			SqlParameter parameterTripId = new SqlParameter(strParamTripId, tripId)
			{
				SqlDbType = SqlDbType.Int,
				Direction = ParameterDirection.Input
			};

			List<SqlParameter> paramList = new List<SqlParameter>() { parameterTripId };
			return await this.ExecuteQueryForOtherEntities<TripVehicle>(SchemeNames.Common, StoredProcedureConstants.Trip_Vehicle_Get, string.Join(",", strParamTripId), paramList, cancellationToken);
		}

		/// <summary>
		/// Method to get all trips
		/// </summary>
		/// <param name="loggedUser"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<IEnumerable<TripDetail>> GetTrips(string loggedUser, CancellationToken cancellationToken)
		{
			return await this.ExecuteQueryForOtherEntities<TripDetail>(SchemeNames.Common, StoredProcedureConstants.Trip_GetAll, cancellationToken);
		}

		/// <summary>
		/// Method to save trip details
		/// </summary>
		/// <param name="trip"></param>
		/// <param name="loggedUser"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<int> SaveTrip(Trip trip, string loggedUser, CancellationToken cancellationToken)
		{
			var sqlParams = new List<SqlParameter>
			{
				await this.BuildTableParameter(new StoredProcParam(){
					PARAMETER_NAME = StoredProcedureConstants.Trip_SP_Save_Param,
					USER_DEFINED_TYPE_SCHEMA = SchemeNames.Common,
					USER_DEFINED_TYPE_NAME = StoredProcedureConstants.Trip_SP_Save_Table_Type,
					PARAMETER_MODE = StoredProcedureConstants.StoredProcedure_Parameter_Mode_In},
					trip.Trips),
				await this.BuildTableParameter(new StoredProcParam(){
					PARAMETER_NAME = StoredProcedureConstants.TripVehicle_SP_Save_Param,
					USER_DEFINED_TYPE_SCHEMA = SchemeNames.Common,
					USER_DEFINED_TYPE_NAME = StoredProcedureConstants.Trip_SP_Save_VehicleTable_Type,
					PARAMETER_MODE = StoredProcedureConstants.StoredProcedure_Parameter_Mode_In},
					trip.Vehicles),
				new SqlParameter(StoredProcedureConstants.User_LoggedUser_Parameter, loggedUser),
			};
			var strParam = StoredProcedureConstants.Trip_SP_TripAdd_AllParam;

			return await this.ExecuteNonQueryWithIntOutput(SchemeNames.Common, StoredProcedureConstants.Trip_Insert_Trip, strParam, sqlParams, cancellationToken);
		}
	}
}
