using System;
using System.Collections.Generic;
using System.Text;

namespace SocialAnalyzer.SDK.Options
{
    public class MessageOptions
    {
        public const string SectionName = "Messages";

        public string WrongUserOrPassword { get; set; }


        public string usernameExist { get; set; }

        public string errorException { get; set; }

        public string errorSave { get; set; }

        public string errorUpdate { get; set; }



  }
}
