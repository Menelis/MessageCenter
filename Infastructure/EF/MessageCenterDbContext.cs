using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infastructure.EF
{
    public class MessageCenterDbContext : DbContext
    {
        public MessageCenterDbContext(DbContextOptions<MessageCenterDbContext> options) : base(options){}

        public DbSet<RunningYear> RunningYears { get; set; }
        public DbSet<BirthDaySentLog> BirthDaySentLogs { get; set; }
    }
}