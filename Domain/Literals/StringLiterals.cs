using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Literals
{
    public class StringLiterals
    {
        /***** STATUS CODES *****/
        public const string StatusCode_Success = "00";
        public const string StatusCode_UserAccountNotFound = "01";
        public const string StatusCode_TokenNullValue = "02";
        public const string StatusCode_BadRequest = "03";
        public const string StatusCode_Unauthorized = "04";
        public const string StatusCode_PartialContent = "05";
        public const string StatusCode_Failure = "06";
        public const string StatusCode_DatabaseConnectionTimeout = "07";
        public const string StatusCode_StoredProcedureError = "08";
        public const string StatusCode_ExceptionError = "09";
        public const string StatusCode_DatabaseConnectionError = "10";
        public const string StatusCode_DirectoryNotFound = "11";
        public const string StatusCode_FilesCountMismatch = "12";
        public const string StatusCode_FilesNotFound = "13";
        public const string StatusCode_FailedInputValidation = "14";
        public const string StatusCode_RoleAssignmentFailed = "15";
        public const string StatusCode_UserCreationFailed = "16";
        public const string StatusCode_UserEmailNotConfirmed = "17";
        public const string StatusCode_LoginFailed = "18";
        public const string StatusCode_UserNotFound = "19";
        public const string StatusCode_PasswordResetFailed = "20";
        public const string StatusCode_WrongPassword = "21";
        public const string StatusCode_UnableToRemovePassword = "22";
        public const string StatusCode_FailedToAddNewPassword = "23";
        public const string StatusCode_UserEmailAlreadyConfirmed = "24";
        public const string StatusCode_ConfirtmationLinkExpired = "25";
        public const string StatusCode_FailedToGenerateConfirmationToken = "26";

        /***** STATUS MESSAGES *****/
        public const string StatusMessage_Success = "Request Successful.";
        public const string StatusMessage_SuccessEmailConfirmation = "Email Confirmation Successful.";
        public const string StatusMessage_Failure = "Request Failed";
        public const string StatusMessage_Duplicate = "Failed: Duplicate RequestID";
        public const string StatusMessage_DirectoryNotFound = "Directory not found.";
        public const string StatusMessage_FilesNotFound = "Files not found.";
        public const string StatusMessage_RoleAssignmentFailed = "Role assignment failed";
        public const string StatusMessage_WrongInput = "Wrong Input Supplied.";
        public const string StatusMessage_UserCreationFailed = "User creation failed";
        public const string StatusMessage_UserEmailNotConfirmed = "Please confirm email";
        public const string StatusMessage_UserEmailAlreadyConfirmed = "Please confirm email";
        public const string StatusMessage_FailedToGenerateConfirmationToken = "Failed to Generate Confirmation Token";
        public const string StatusMessage_ConfirmationMailSent = "Confirmation link sent to your email";
        public const string StatusMessage_UserNotFound = "User not found.";
        public const string StatusMessage_PasswordResetFailed = "Password reset failed.";
        public const string StatusMessage_ConfirtmationLinkExpired = "Confirmation token expired.";
        public const string StatusMessage_FailedToAddNewPassword = "Failed to Add New Password for User.";
        public const string StatusMessage_WrongPassword = "Wrong Password.";
        public const string StatusMessage_UnableToRemovePassword = "Unable to remove password for user.";
        public const string StatusMessage_LoginFailed = "Login failed";
        public const string StatusMessage_FilesCountMismatch = "Path and file count mismatch.";
        public const string StatusMessage_UnknownError = "Unknown Error Occured while Performing this Action.";
        public const string StatusMessage_TokenNullValue = "Authorization Token Value is Null";
        public const string StatusMessage_BadRequest = "Required request parameter is Invalid / Missing";
        public const string StatusMessage_Unauthorized = "Authentication Token is Unauthorized";
        public const string StatusMessage_DatabaseConnectionTimeout = "Database Connection Timeout";
        public const string StatusMessage_StoredProcedureError = "Stored Procedured Execution Failed";
        public const string StatusMessage_ExceptionError = "An Exception Occured";
        public const string StatusMessage_DatabaseConnectionError = "Database Connection Error";
        public const string StatusMessage_AccountNameNotFound = "Merchant Details Not Found";
        public const string StatusMessage_TransactionNotFound = "Transaction Information Not Found";
        public const string StatusMessage_AuthenticationFailue = "Unable to Authenticate Merchant, Please Try Again Later!";
        public const string StatusMessage_InputFailure = "Header Value 'Merchant ID' Contains Disallowed Special Characters";
        public const string StatusMessage_DateFailure = "Date Format Supplied does not Match Server Date, Format expected is {serverDateFormat}";
        public const string StatusMessage_TransactionExceeding = "Maximumn Number of Days to Request Transaction History Per Time Exceeded";
        public const string StatusMessage_IncorrectCredentials = "Incorrect Login Credentials Provided";
    }
}
