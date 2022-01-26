using System.Threading.Tasks;
using AzCost.Models;

namespace AzCost.Repositories
{
    internal interface IRepository
    {
        Task SaveRgInfoAsync(ResourceGroupInfo rgInfo);
    }
}