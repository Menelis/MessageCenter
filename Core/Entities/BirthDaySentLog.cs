using System;
namespace Core.Entities
{
    public class BirthDaySentLog : Base.BaseEntity
    {
        public int EmployeeId { get; set; }
        public Guid RunningYearId { get; set;}
        public RunningYear RunningYear { get; set; }
        public DateTime DateSent { get; set;} = DateTime.Now;        
    }
}