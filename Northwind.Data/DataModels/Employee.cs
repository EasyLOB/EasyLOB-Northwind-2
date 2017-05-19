using EasyLOB.Data;
using EasyLOB.Library;
using System;
using System.Collections.Generic;

namespace Northwind.Data
{
    public partial class Employee : ZDataBase
    {        
        #region Properties
        
        public virtual int EmployeeId { get; set; }
        
        public virtual string LastName { get; set; }
        
        public virtual string FirstName { get; set; }
        
        public virtual string Title { get; set; }
        
        public virtual string TitleOfCourtesy { get; set; }
        
        public virtual DateTime? BirthDate { get; set; }
        
        public virtual DateTime? HireDate { get; set; }
        
        public virtual string Address { get; set; }
        
        public virtual string City { get; set; }
        
        public virtual string Region { get; set; }
        
        public virtual string PostalCode { get; set; }
        
        public virtual string Country { get; set; }
        
        public virtual string HomePhone { get; set; }
        
        public virtual string Extension { get; set; }
        
        public virtual byte[] Photo { get; set; }
        
        public virtual string Notes { get; set; }
        
        private int? _reportsTo;
        
        public virtual int? ReportsTo
        {
            get { return this.EmployeeReportsTo == null ? _reportsTo : this.EmployeeReportsTo.EmployeeId; }
            set
            {
                _reportsTo = value;
                EmployeeReportsTo = null;
            }
        }
        
        public virtual string PhotoPath { get; set; }

        #endregion Properties

        #region Associations (FK)

        public virtual Employee EmployeeReportsTo { get; set; } // ReportsTo

        #endregion Associations FK

        #region Collections (PK)

        public virtual IList<Employee> Employees { get; }

        public virtual IList<EmployeeTerritory> EmployeeTerritories { get; }

        public virtual IList<Order> Orders { get; }

        #endregion Collections (PK)

        #region Methods
        
        public Employee()
        {            
            EmployeeId = LibraryDefaults.Default_Int32;
            LastName = LibraryDefaults.Default_String;
            FirstName = LibraryDefaults.Default_String;
            Title = null;
            TitleOfCourtesy = null;
            BirthDate = null;
            HireDate = null;
            Address = null;
            City = null;
            Region = null;
            PostalCode = null;
            Country = null;
            HomePhone = null;
            Extension = null;
            Photo = null;
            Notes = null;
            ReportsTo = null;
            PhotoPath = null;

            //EmployeeReportsTo = new Employee();

            Employees = new List<Employee>();
            EmployeeTerritories = new List<EmployeeTerritory>();
            Orders = new List<Order>();
    
            OnConstructor();
        }

        public Employee(int employeeId)
            : this()
        {            
            EmployeeId = employeeId;
        }

        public Employee(
            int employeeId,
            string lastName,
            string firstName,
            string title = null,
            string titleOfCourtesy = null,
            DateTime? birthDate = null,
            DateTime? hireDate = null,
            string address = null,
            string city = null,
            string region = null,
            string postalCode = null,
            string country = null,
            string homePhone = null,
            string extension = null,
            byte[] photo = null,
            string notes = null,
            int? reportsTo = null,
            string photoPath = null
        )
            : this()
        {
            EmployeeId = employeeId;
            LastName = lastName;
            FirstName = firstName;
            Title = title;
            TitleOfCourtesy = titleOfCourtesy;
            BirthDate = birthDate;
            HireDate = hireDate;
            Address = address;
            City = city;
            Region = region;
            PostalCode = postalCode;
            Country = country;
            HomePhone = homePhone;
            Extension = extension;
            Photo = photo;
            Notes = notes;
            ReportsTo = reportsTo;
            PhotoPath = photoPath;
        }

        public override object[] GetId()
        {
            return new object[] { EmployeeId };
        }

        public override void SetId(object[] ids)
        {
            if (ids != null && ids[0] != null)
            {
                EmployeeId = DataHelper.IdToInt32(ids[0]);
            }
        }

        #endregion Methods
    }
}
