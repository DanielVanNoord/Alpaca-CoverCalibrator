using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCOM.Compatibility.Utilities
{
    public class ProfileCompat : ASCOM.Utilities.Profile, ASCOM.Compatibility.Interfaces.IProfileFull
    {
        public ProfileCompat() : base()
        {
        }
    }
}
