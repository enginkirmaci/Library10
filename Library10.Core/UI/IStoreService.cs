using System;
using System.Threading.Tasks;

namespace Library10.Core.UI
{
    public interface IStoreService
    {
        Task<bool> RateReview();

        Task<bool> OtherApps();
    }
}