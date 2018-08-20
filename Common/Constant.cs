using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public static class Constant
    {
        public const string SecretKey = "$#@^$*&#@^$JHGJGFJ*$YWI((&#@(&#(^(@%(#^%()#";
        public const string SecretSercurityKey = "%$^%^$#*%^*@fggfgg0979(545)782-123456789-helloXinhp";
    }

    public struct LogFormat
    {
        // Constants Log Formats
        public const string ControllerStart = "Start {0} - WebAPI";
        public const string ControllerEnd = "End {0} - WebAPI";

        public const string ServiceStart = "Start {0} - DAL";
        public const string ServiceEnd = "End {0} - DAL";
    }
}
