using EY.CTP.SRED.Platform.Service.Core.Entities;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
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

		public Task<bool> DeleteTrip(int tripId, string loggedUser, CancellationToken cancellationToken)
		{
			throw new System.NotImplementedException();
		}

		public Task<Trip> GetTripById(int tripId, string loggedUser, CancellationToken cancellationToken)
		{
			throw new System.NotImplementedException();
		}

		public Task<IEnumerable<Trip>> GetTrips(string loggedUser, CancellationToken cancellationToken)
		{
			throw new System.NotImplementedException();
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

			// Fix: Use ExecuteNonQueryWithIntOutput instead of ExecuteQueryForOtherEntities<int>  
			return await this.ExecuteNonQueryWithIntOutput(SchemeNames.Common, StoredProcedureConstants.Trip_Insert_Trip, strParam, sqlParams, cancellationToken);
		}
	}
}
