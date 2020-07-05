using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCOM.Compatibility.Utilities
{
    public class ProfileCompat :  ASCOM.Compatibility.Interfaces.IProfile
    {
        public string DriverID
        {
            get;
            set;
        } = string.Empty;
        string DeviceType = string.Empty;
        public ProfileCompat(string driverID, string deviceType)
        {
            DriverID = driverID;
            DeviceType = deviceType;
        }

        public void DeleteValue(string Name)
        {
            using(ASCOM.Utilities.Profile profile = new ASCOM.Utilities.Profile())
            {
                profile.DeviceType = DeviceType;
                profile.DeleteValue(DriverID, Name);
            }
        }

        public string GetValue(string Name)
        {
            using (ASCOM.Utilities.Profile profile = new ASCOM.Utilities.Profile())
            {
                profile.DeviceType = DeviceType;
                return profile.GetValue(DriverID, Name);
            }
        }

        public string GetValue(string Name, string DefaultValue)
        {
            using (ASCOM.Utilities.Profile profile = new ASCOM.Utilities.Profile())
            {
                profile.DeviceType = DeviceType;
                return profile.GetValue(DriverID, Name, string.Empty, DefaultValue);
            }
        }

        public List<string> Values()
        {
            using (ASCOM.Utilities.Profile profile = new ASCOM.Utilities.Profile())
            {
                profile.DeviceType = DeviceType;

                List<string> retValues = new List<string>();
                var values = profile.Values(DriverID);

                foreach(var value in values)
                {
                    retValues.Add(value.ToString());
                }

                return retValues;
            }
        }

        public void WriteValue(string Name, string Value)
        {
            using (ASCOM.Utilities.Profile profile = new ASCOM.Utilities.Profile())
            {
                profile.DeviceType = DeviceType;
                profile.WriteValue(DriverID, Name, Value);
            }
        }
    }
}
