using System;
using System.Collections.Generic;
using System.Text;

namespace Alpaca.CoverCalibrator
{
    internal static class ServerManager
    {
        private static uint transactionID = 0;

        internal static uint ServerTransactionID
        {
            get
            {
                return transactionID++;
            }
        }
    }
}
