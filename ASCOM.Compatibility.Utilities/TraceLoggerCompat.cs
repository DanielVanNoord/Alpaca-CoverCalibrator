﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCOM.Compatibility.Utilities
{
    public class TraceLoggerCompat : ASCOM.Utilities.TraceLogger, ASCOM.Compatibility.Interfaces.ITraceLoggerFull
    {
        public TraceLoggerCompat() : base()
        {

        }

        public TraceLoggerCompat(string LogFileType) : base(LogFileType)
        {

        }

        public TraceLoggerCompat(string LogFileName, string LogFileType) : base(LogFileName, LogFileType)
        {

        }
    }
}
