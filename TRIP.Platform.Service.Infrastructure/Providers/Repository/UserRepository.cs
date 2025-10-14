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
	public class UserRepository : Repository<User>, IUserRepository
	{
		private readonly TripDbContext _context;
		public UserRepository(TripDbContext dbContext) : base(dbContext)
		{
			_context = dbContext;
		}

		/// <summary>
		/// Method to get all users
		/// </summary>
		/// <param name="loggedUser"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<IEnumerable<User>> GetUsers(string loggedUser, CancellationToken cancellationToken)
		{
			return await this.ExecuteQueryForOtherEntities<User>(SchemeNames.Common, StoredProcedureConstants.Users_GetAll, cancellationToken);
		}

		/// <summary>
		/// Method to get logged in user detail
		/// </summary>
		/// <param name="email"></param>
		/// <param name="password"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<User> GetLoggedInUserDetail(string email, string password, CancellationToken cancellationToken)
		{
			var strParamEmail = StoredProcedureConstants.User_Email_Parameter;
			SqlParameter parameterEmail = new SqlParameter(strParamEmail, email)
			{
				SqlDbType = SqlDbType.NVarChar,
				Direction = ParameterDirection.Input
			};
			var strParamPassword = StoredProcedureConstants.User_Password_Parameter;
			SqlParameter parameterPassword = new SqlParameter(strParamPassword, password)
			{
				SqlDbType = SqlDbType.NVarChar,
				Direction = ParameterDirection.Input
			};
			List<SqlParameter> paramList = new List<SqlParameter>() { parameterEmail, parameterPassword };
			return await this.ExecuteQueryForOtherEntity<User>(SchemeNames.Common, StoredProcedureConstants.Users_GetLoggedInUser, string.Join(",", strParamEmail, strParamPassword), paramList, cancellationToken);
		}

		/// <summary>
		/// Method to save user details
		/// </summary>
		/// <param name="user"></param>
		/// <param name="loggedUser"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<bool> SaveUser(User user, string loggedUser, CancellationToken cancellationToken)
		{
			List<SqlParameter> paramList = new List<SqlParameter>();

			var strParamUser = StoredProcedureConstants.User_UserId_Parameter;
			SqlParameter parameterUser = new SqlParameter(strParamUser, user.UserId)
			{
				SqlDbType = SqlDbType.Int,
				Direction = ParameterDirection.Input
			};
			var strParamCompanyName = StoredProcedureConstants.User_CompanyName_Parameter;
			SqlParameter parameterCompanyName = new SqlParameter(strParamCompanyName, user.CompanyName)
			{
				SqlDbType = SqlDbType.NVarChar,
				Direction = ParameterDirection.Input
			};
			var strParamName = StoredProcedureConstants.User_Name_Parameter;
			SqlParameter parameterName = new SqlParameter(strParamName, user.Name)
			{
				SqlDbType = SqlDbType.NVarChar,
				Direction = ParameterDirection.Input
			};
			var strParamWebsite = StoredProcedureConstants.User_Website_Parameter;
			SqlParameter parameterWebsite = new SqlParameter(strParamWebsite, user.Website)
			{
				SqlDbType = SqlDbType.NVarChar,
				Direction = ParameterDirection.Input
			};
			var strParamEmail = StoredProcedureConstants.User_Email_Parameter;
			SqlParameter parameterEmail = new SqlParameter(strParamEmail, user.Email)
			{
				SqlDbType = SqlDbType.NVarChar,
				Direction = ParameterDirection.Input
			};
			var strParamPassword = StoredProcedureConstants.User_Password_Parameter;
			SqlParameter parameterPassword = new SqlParameter(strParamPassword, user.Password)
			{
				SqlDbType = SqlDbType.NVarChar,
				Direction = ParameterDirection.Input
			};
			var strParamContactNumber = StoredProcedureConstants.User_ContactNumber_Parameter;
			SqlParameter parameterContactNumber = new SqlParameter(strParamContactNumber, user.ContactNumber)
			{
				SqlDbType = SqlDbType.NVarChar,
				Direction = ParameterDirection.Input
			};
			var strParamAddress = StoredProcedureConstants.User_Address_Parameter;
			SqlParameter parameterAddress = new SqlParameter(strParamAddress, user.Address)
			{
				SqlDbType = SqlDbType.NVarChar,
				Direction = ParameterDirection.Input
			};
			var strParamUserTypeId = StoredProcedureConstants.User_UserTypeId_Parameter;
			SqlParameter parameterUserType = new SqlParameter(strParamUserTypeId, user.UserTypeId)
			{
				SqlDbType = SqlDbType.Int,
				Direction = ParameterDirection.Input
			};

			var strloggedUser = StoredProcedureConstants.User_LoggedUser_Parameter;
			SqlParameter parameterLoggedUser = new SqlParameter(StoredProcedureConstants.User_LoggedUser_Parameter, loggedUser);

			paramList.Add(parameterLoggedUser);
			paramList.Add(parameterCompanyName);
			paramList.Add(parameterName);
			paramList.Add(parameterWebsite);
			paramList.Add(parameterUser);
			paramList.Add(parameterEmail);
			paramList.Add(parameterPassword);
			paramList.Add(parameterContactNumber);
			paramList.Add(parameterAddress);
			paramList.Add(parameterUserType);
			return await this.ExecuteNonQuery(SchemeNames.Common, StoredProcedureConstants.User_Insert_User, string.Join(",", strParamUser, strParamCompanyName, strParamWebsite, strParamName,
				strParamEmail, strParamPassword, strParamContactNumber, strParamAddress, strParamUserTypeId, strloggedUser), paramList, cancellationToken);
		}
	}
}