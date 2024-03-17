namespace PasswordManager.KeyVaults.Domain.Operations
{
    public sealed class OperationDataConstants
    {
        #region CreateSecurityKey
        public static string CreateSecurityKeySecretKey = "createSecurityKeySecretKey";
        public static string CreateSecurityKeyObjectId = "createSecurityKeyObjectId";
        #endregion

        #region UpdateSecurityKey
        public static string UpdateSecurityKeySecretKey = "updateSecurityKeySecretKey";
        public static string UpdateSecurityKeyObjectId = "updateSecurityKeyObjectId";
        #endregion

        #region DeleteSecurityKey
        public static string DeleteSecurityKeyObjectId = "deleteSecurityKeyObjectId";
        public static string DeleteSecurityKeySecretKey = "deleteSecurityKeySecret";
        #endregion
    }
}
