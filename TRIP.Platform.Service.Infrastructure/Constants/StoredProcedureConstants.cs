namespace TRIP.Platform.Service.Infrastructure.Constants
{
	class StoredProcedureConstants
	{
		public const string Users_GetAll = "usp_GetUsers";
		public const string Users_GetLoggedInUser = "usp_GetLoggedInUserDetails";
		public const string User_Insert_User = "usp_InsertUpdateUser";
		public const string User_UserId_Parameter = "@UserId";
		public const string User_Name_Parameter = "@Name";
		public const string User_Email_Parameter = "@Email";
		public const string User_Password_Parameter = "@Password";
		public const string User_ContactNumber_Parameter = "@ContactNumber";
		public const string User_UserTypeId_Parameter = "@UserTypeId";
		public const string User_LoggedUser_Parameter = "@LoggedUser";

		#region Vehicle
		public const string Vehicle_VehicleId_Parameter = "@VehicleId";
		public const string Vehicle_VehicleName_Parameter = "@VehicleName";
		public const string Vehicle_VehicleType_Parameter = "@VehicleType";
		public const string Vehicle_VehicleOwner_Parameter = "@VehicleOwner";
		public const string Vehicle_VehicleYear_Parameter = "@VehicleYear";
		public const string Vehicle_Capacity_Parameter = "@Capacity";
		public const string Vehicle_Amenities_Parameter = "@Amenities";
		public const string Vehicle_Status_Parameter = "@Status";
		public const string Vehicle_Insert_Vehicle = "usp_InsertUpdateVehicle";
		public const string Vehicles_GetAll = "usp_GetVehicles";
		public const string Vehicles_Get = "usp_GetVehicleById";
		public const string Vehicle_Delete_Vehicle = "usp_DeleteVehicle";
		#endregion

		#region Driver
		public const string Driver_DriverId_Parameter = "@DriverId";
		public const string Driver_DriverName_Parameter = "@DriverName";
		public const string Driver_DriverType_Parameter = "@DriverType";
		public const string Driver_Language_Parameter = "@Language";
		public const string Driver_Location_Parameter = "@Location";
		public const string Driver_Experience_Parameter = "@Experience";
		public const string Driver_ContactNumber_Parameter = "@ContactNumber";
		public const string Driver_License_Parameter = "@LicenseNumber";
		public const string Driver_Status_Parameter = "@Status";
		public const string Driver_Insert_Driver = "usp_InsertUpdateDriver";
		public const string Drivers_Get = "usp_GetDriverById";
		public const string Drivers_GetAll = "usp_GetDrivers";
		public const string Driver_Delete_Driver = "usp_DeleteDriver";
		#endregion
	}
}