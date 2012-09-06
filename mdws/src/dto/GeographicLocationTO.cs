#region CopyrightHeader
//
//  Copyright by Contributors
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//         http://www.apache.org/licenses/LICENSE-2.0.txt
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
//
#endregion

using System;
using System.Data;
using System.Configuration;
using gov.va.medora.mdo;

namespace gov.va.medora.mdws.dto
{
    public class GeographicLocationTO : AbstractTO
    {
        public string zipcode;
        public string zipcodeType;
        public string cityName;
        public string cityType;
        public string countyName;
        public string countyFips;
        public string stateName;
        public string stateAbbreviation;
        public string stateFips;
        public string msaCode;
        public string areaCode;
        public string timeZone;
        public int utc;
        public bool daylightSavings;
        public double latitude;
        public double longitude;

        public GeographicLocationTO() { }

        public GeographicLocationTO(GeographicLocation mdo)
        {
            this.zipcode = mdo.Zipcode;
            this.zipcodeType = mdo.ZipcodeType;
            this.cityName = mdo.CityName;
            this.cityType = mdo.CityType;
            this.countyName = mdo.CountyName;
            this.countyFips = mdo.CountyFips;
            this.stateName = mdo.StateName;
            this.stateAbbreviation = mdo.StateAbbreviation;
            this.stateFips = mdo.StateFips;
            this.msaCode = mdo.MsaCode;
            this.areaCode = mdo.AreaCode;
            this.timeZone = mdo.TimeZone;
            this.utc = mdo.Utc;
            this.daylightSavings = mdo.DaylightSavings;
            this.latitude = mdo.Latitude;
            this.longitude = mdo.Longitude;
        }
    }
}
