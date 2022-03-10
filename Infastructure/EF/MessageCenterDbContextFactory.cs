using Microsoft.EntityFrameworkCore;

namespace Infastructure.EF
{
    public class MessageCenterDbContextFactory : DesignTimeFactoryBase<MessageCenterDbContext>
    {
        protected override MessageCenterDbContext CreateNewInstance(DbContextOptions<MessageCenterDbContext> options)
        {
            return new MessageCenterDbContext(options);
        }
    }
}