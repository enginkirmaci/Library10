using Library10.Core.BaseClasses;
using System;
using System.Threading.Tasks;
using Windows.System;

namespace Library10.Core.UI
{
    public class StoreService : IStoreService
    {
        private const string PUBLISHERAPPSURL = "ms-windows-store:search?publisher={0}";
        private const string RATEREVIEWURL = "ms-windows-store:reviewapp?appid=";

        async public Task<bool> OtherApps()
        {
            return await Launcher.LaunchUriAsync(new Uri(string.Format(PUBLISHERAPPSURL, BaseSettings.General.CompanyName)));
        }

        async public Task<bool> RateReview()
        {
            return await Launcher.LaunchUriAsync(new Uri(RATEREVIEWURL + BaseSettings.General.AppMarketId));
        }
    }
}