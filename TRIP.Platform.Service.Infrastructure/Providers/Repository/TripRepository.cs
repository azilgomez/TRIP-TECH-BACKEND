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
		/// Method to save Trip details
		/// </summary>
		/// <param name="trip"></param>
		/// <param name="loggedUser"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<bool> SaveTrip(Trip trip, string loggedUser, CancellationToken cancellationToken)
		{
			List<SqlParameter> paramList = new List<SqlParameter>();

			var strParamTripId = StoredProcedureConstants.Trip_TripId_Parameter;
			SqlParameter parameterTripId = new SqlParameter(strParamTripId, trip.TripId)
			{
				SqlDbType = SqlDbType.Int,
				Direction = ParameterDirection.Input
			};
			var strParamTripOwner = StoredProcedureConstants.Trip_TripOwner_Parameter;
			SqlParameter parameterOwner = new SqlParameter(strParamTripOwner, trip.TripOwner)
			{
				SqlDbType = SqlDbType.NVarChar,
				Direction = ParameterDirection.Input
			};
			var strParamTripType = StoredProcedureConstants.Trip_TripType_Parameter;
			SqlParameter parameterType = new SqlParameter(strParamTripType, trip.TripType)
			{
				SqlDbType = SqlDbType.NVarChar,
				Direction = ParameterDirection.Input
			};
			var strParamTripDate = StoredProcedureConstants.Trip_TripDate_Parameter;
			SqlParameter parameterTripDate = new SqlParameter(strParamTripDate, trip.TripRequestedDate)
			{
				SqlDbType = SqlDbType.DateTime,
				Direction = ParameterDirection.Input
			};
			var strParamStartPlace = StoredProcedureConstants.Trip_StartPlace_Parameter;
			SqlParameter parameterStartPlace = new SqlParameter(strParamStartPlace, trip.StartPlace)
			{
				SqlDbType = SqlDbType.NVarChar,
				Direction = ParameterDirection.Input
			};
			var strParamEndPlace = StoredProcedureConstants.Trip_EndPlace_Parameter;
			SqlParameter parameterEndPlace = new SqlParameter(strParamEndPlace, trip.EndPlace)
			{
				SqlDbType = SqlDbType.NVarChar,
				Direction = ParameterDirection.Input
			};
			var strParamStartDate = StoredProcedureConstants.Trip_StartDate_Parameter;
			SqlParameter parameterStartDate = new SqlParameter(strParamStartDate, trip.StartDate)
			{
				SqlDbType = SqlDbType.DateTime,
				Direction = ParameterDirection.Input
			};
			var strParamRemark = StoredProcedureConstants.Trip_Remarks_Parameter;
			SqlParameter parameterRemark = new SqlParameter(strParamRemark, trip.TripRemarks)
			{
				SqlDbType = SqlDbType.NVarChar,
				Direction = ParameterDirection.Input
			};
			var strParamAssignTo = StoredProcedureConstants.Trip_AssignTo_Parameter;
			SqlParameter parameterAssignTo = new SqlParameter(strParamAssignTo, trip.AssignTo)
			{
				SqlDbType = SqlDbType.Int,
				Direction = ParameterDirection.Input
			};
			var strParamInCharge = StoredProcedureConstants.Trip_InCharge_Parameter;
			SqlParameter parameterInCharge = new SqlParameter(strParamInCharge, trip.InChargePerson)
			{
				SqlDbType = SqlDbType.Int,
				Direction = ParameterDirection.Input
			};
			var strParamStatusId = StoredProcedureConstants.Trip_Status_Parameter;
			SqlParameter parameterStatus = new SqlParameter(strParamStatusId, trip.Status)
			{
				SqlDbType = SqlDbType.Int,
				Direction = ParameterDirection.Input
			};

			var strloggedUser = StoredProcedureConstants.User_LoggedUser_Parameter;
			SqlParameter parameterLoggedUser = new SqlParameter(StoredProcedureConstants.User_LoggedUser_Parameter, loggedUser);

			paramList.Add(parameterTripId);
			paramList.Add(parameterOwner);
			paramList.Add(parameterType);
			paramList.Add(parameterStartPlace);
			paramList.Add(parameterEndPlace);
			paramList.Add(parameterStartDate);
			paramList.Add(parameterAssignTo);
			paramList.Add(parameterInCharge);
			paramList.Add(parameterRemark);
			paramList.Add(parameterStatus);
			paramList.Add(parameterLoggedUser);
			return await this.ExecuteNonQuery(SchemeNames.Common, StoredProcedureConstants.Trip_Insert_Trip, string.Join(",", strParamTripId, strParamTripType, strParamTripDate,
				strParamStartPlace, strParamEndPlace, strParamStartDate, strParamAssignTo,strParamInCharge, strParamStatusId, strParamStatusId,strParamRemark, strloggedUser), paramList, cancellationToken);
		}

		/// <summary>
		///  Method to delete trip
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
			return await this.ExecuteNonQuery(SchemeNames.Common, StoredProcedureConstants.Trip_Delete_Trip, string.Join(",", strParamTripId), paramList, cancellationToken);
		}

		/// <summary>
		/// Method to get trips
		/// </summary>
		/// <param name="loggedUser"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<IEnumerable<Trip>> GetTrips(string loggedUser, CancellationToken cancellationToken)
		{
			return await this.ExecuteQueryForOtherEntities<Trip>(SchemeNames.Common, StoredProcedureConstants.Trips_GetAll, cancellationToken);
		}

		/// <summary>
		/// Method to get Trip by Id
		/// </summary>
		/// <param name="tripId"></param>
		/// <param name="loggedUser"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<Trip> GetTripById(int tripId, string loggedUser, CancellationToken cancellationToken)
		{
			var strParamTripId = StoredProcedureConstants.Trip_TripId_Parameter;
			SqlParameter parameterTripId = new SqlParameter(strParamTripId, tripId)
			{
				SqlDbType = SqlDbType.NVarChar,
				Direction = ParameterDirection.Input
			};

			List<SqlParameter> paramList = new List<SqlParameter>() { parameterTripId };
			return await this.ExecuteQueryForOtherEntity<Trip>(SchemeNames.Common, StoredProcedureConstants.Trips_Get, string.Join(",", strParamTripId), paramList, cancellationToken);
		}
	}
}
