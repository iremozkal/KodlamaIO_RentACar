namespace Business.Constants
{
    public static class Messages
    {
        public static string AddSuccess = "(+) Insert operation is succesfully done.";
        public static string AddError = "(-) Insert operation is failed.";
        public static string UpdateSuccess = "(+) Update operation is succesfully done.";
        public static string UpdateError = "(-) Update operation is failed.";
        public static string DeleteSuccess = "(+) Delete operation is succesfully done.";
        public static string DeleteError = "(-) Delete operation is failed.";
        public static string CarNotExist = "(-) There is no such a car.";
        public static string BrandNotExist = "(-) There is no such a brand.";
        public static string ColorNotExist = "(-) There is no such a color.";
        public static string CarIsReturn = "(+) The car has been delivered.";
        public static string CarNotReturn = "(-) The car has already been rented, not been delivered yet.";
        public static string CarCountOfBrandError = "(-) The quota of this brand is full. You cannot add more cars.";
        public static string CarDescriptionAlreadyExists = "There is already a car with this description. ";
        public static string BrandLimitExceed = "(-) New cars cannot be added because the brand limit is exceeded.";
        public static string CarImageCountError = "(-) The quota of the images for this car is full. You cannot add more images.";
        public static string IncorrectFileExtension = "(-) Incorrect file extension.";
        public static string ImageNotFound = "(-) The image is not found in the folder.";
        public static string DefaultImageAdded = "(+) Default image has been added.";
        public static string ChosenImageAdded = "(+) Chosen image has been added.";
        public static string AuthorizationDenied = "(-) You are not authorized.";
        public static string SuccessfulRegister = "(+) Successful Register."; 
        public static string SuccessfulLogin = "(+) Successful Login.";
        public static string RentalNotFound = "(-) There is no such a rental record.";
        public static string CustomerNotFound = "(-) Customer not found.";
        public static string UserNotFound = "(-) User not found.";
        public static string UserAlreadyExists = "(-) User already exists.";
        public static string PasswordError = "(-) Wrong password.";
        public static string AccessTokenCreated = "(+) Access Token is created.";
        
    }
}
