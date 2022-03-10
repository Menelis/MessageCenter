using System;
using Core.Entities.Base;
using Core.Extensions;

namespace Core.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set;}
        public DateTime DateOfBirth { get; set; }
        public DateTime EmployeeStartDate { get; set; }
        public DateTime? EmployeeEndDate { get; set; }
        public bool ItsYourBirthDay
        {
            get 
            {
                // if you have not joined the company, no email will be sent
                if(EmployeeStartDate.Date > DateTime.Now.Date) return false;
                // if you have left he company no email will be sent
                if(EmployeeEndDate != null && EmployeeEndDate.Value.Date < DateTime.Now.Date) return false;
                
                return DateOfBirth.AddYears(DateOfBirth.GetAge()).Date == DateTime.Now.Date;
            }
        }
        public string Email { get; set; }
    }
}