using Core.Entities;
using Core.Gateways.Repositories;
using Infastructure.EF;

namespace Infastructure.Gateways.Repositories
{
    public class BirthDaySentLogRepository : RepositoryBase<BirthDaySentLog>, IBirthDaySentLogRepository
    {
        public BirthDaySentLogRepository(MessageCenterDbContext dbContext) : base(dbContext)
        {
        }
    }
}