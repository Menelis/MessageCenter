using Core.Entities;
using Core.Gateways.Repositories;
using Infastructure.EF;

namespace Infastructure.Gateways.Repositories
{
    public class RunningYearRepository : RepositoryBase<RunningYear>, IRunningYearRepository
    {
        public RunningYearRepository(MessageCenterDbContext dbContext) : base(dbContext)
        {
        }
    }
}