using System;
namespace EFMC.Service.Common.Constants
{
    // Domain of MessageResult.json
    public class MessageDomainConstant
    {
        public static readonly string ROLE = "Role";
        public static readonly string USER = "User";
        public static readonly string PHARMACY = "Pharmacy";
        public static readonly string CONSIGNMENT = "Consignment";
        public static readonly string INDUSTRY = "Industry";
        public static readonly string DRUG = "Drug";
    }
    // Mapping message Role
    public class MessageRoleConstant
    {
        #region Error
        public static readonly string ADD_ROLE_FAILED = "E001";
        public static readonly string GET_ROLES_FAILED = "E002";
        #endregion

        #region Success
        public static readonly string ADD_ROLE_SUCCESS = "S001";
        public static readonly string GET_ROLES_SUCCESS = "S002";
        #endregion
    }
    // Mapping message User
    public class MessageUserConstant
    {
        #region Error
        public static readonly string NOT_FOUND_USER = "E101";
        public static readonly string NOT_ACTIVATE_USER = "E102";
        public static readonly string WRONG_PASSWORD = "E103";
        public static readonly string NOT_FOUND_STATUS = "E104";
        public static readonly string INVALID_STATUS = "E105";
        #endregion

        #region Success
        public static readonly string LOG_IN_SUCCESS = "S101";
        public static readonly string LOG_OUT_SUCCESS = "S102";
        public static readonly string REGISTER_SUCCESS = "S103";
        public static readonly string GET_INFORMATION_SUCCESS = "S104";
        #endregion
    }
    public class MessagePharmacyConstant
    {
        #region Error
        public static readonly string NOT_EXISTED = "E201";
        public static readonly string CREATE_FAILED = "E202";
        public static readonly string IS_EXISTED_USER = "E203";
        #endregion

        #region Success
        public static readonly string CREATE_SUCCESS = "S201";
        public static readonly string GET_PHARMACIES_SUCCESS = "S202";
        public static readonly string UPDATE_SUCCESS = "S203";
        #endregion
    }
    public class MessageConsignmentConstant
    {
        #region Error
        public static readonly string NOT_EXISTED = "E301";
        #endregion

        #region Success
        public static readonly string CREATE_SUCCESS = "S301";
        public static readonly string GET_ALL_SUCCESS = "S302";
        #endregion
    }
    public class MessageIndustryConstant
    {
        #region Error
        public static readonly string NOT_EXISTED = "E401";
        #endregion

        #region Success
        public static readonly string CREATE_SUCCESS = "S401";
        public static readonly string GET_ALL_SUCCESS = "S402";
        #endregion
    }
    public class MessageDrugConstant
    {
        #region Error
        public static readonly string NOT_EXISTED = "E501";
        #endregion

        #region Success
        #endregion
    }
}
