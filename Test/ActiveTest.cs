using MembershopTest.Page;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembershopTest.Test
{
    public class ActiveTest : BaseTest
    {
        [TestCase("1", "5", "13", "05", TestName = "13 km in 1 hour 5 min is 5 km/hour")]
        public static void Test13KmIn1Hour5Min(string hour, string minutes, string distance, string resultMin)
        {
            _activePage.NavigateToPage();
            //_activePage.CloseAdvertisement();
            _activePage.AcceptCookies();
            _activePage.InsertRunningHours(hour);
            _activePage.InsertRunningMinutes(minutes);
            _activePage.InsertRunningDistance(distance);
            _activePage.SelectRunningDistanceKm();
            _activePage.SelectRunningSpeedPerHourTypeKmPerHour();
            _activePage.CountPacePerHour();
            _activePage.ValidatePaceResult(resultMin);
            _activePage.Reset();
        }
    }
}
