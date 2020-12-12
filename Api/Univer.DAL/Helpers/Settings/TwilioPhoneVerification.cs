using System;
using System.Collections.Generic;
using System.Text;

namespace Univer.DAL.Helpers.Settings
{
    public class TwilioPhoneVerification
    {
        public string AccountSid { get; set; }
        public string AuthToken { get; set; }
        public string PhoneNumberToSendSMS { get; set; }
    }
}
