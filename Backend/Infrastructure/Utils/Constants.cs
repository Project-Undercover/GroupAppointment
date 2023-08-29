using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Utils
{
    public class Constants
    {
        public const string jwtSecret = "FNSIKMTZ0NRR4SKPE1R8F6LLTM@GAZJ99==";
        public static int jwtExpirationDays = 1;
        public static double jwtExpirationHours = 24;


        // Mail
        public const int MailPort = 587;
        public const string MailHost = "smtp.office365.comz";
        public const string EmailPassword = "Codit12ma!@z";
        public const string EmailAddress = "support@codit.co.ilz";

        //Passowrd Hash
        public const int SaltSize = 20;
        public const int HashSize = 24;
        public const string HashPrefix = "$NOTMYHASHMAN$V3NEW$";


        // System Authentication
        public const bool TwoFactorAuthEnabled = true;
        public const int VerificationAttempts = 10;
        public const int VerificationCodeDuration = 2;
        public const int CodeExpirationTimeInMin = 6;
    }
}
