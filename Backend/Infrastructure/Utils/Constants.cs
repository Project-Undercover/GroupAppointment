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
        public const int VerificationCodeLength = 5;
        public const int CodeExpirationTimeInMin = 6;
        public const string FireBaseURL = "https://fcm.googleapis.com/fcm/";
        public const string FirebaseKey = "AAAAi1P0S-c:APA91bG4qOOeoUVJC-4HvdQcceequiJS4q6JujzgXi1P3HvfT4_vt3MSJh8aVa617zUfQ7xiq0u8WuxTRrP750TS7Ta2rXMJl8ivzPQDDlPlOMNLSDh95emTuT3O7pOvCnVVYIL-cB2Q";
        
        //public const string GoogleApiKey = "AIzaSyD_d5xbS_WhZxLOIHekh5JOYWgk-QD7AZA";
        //public const string GoogleGeocodeUrl = "https://maps.googleapis.com/maps/api/geocode/";
    }
}
