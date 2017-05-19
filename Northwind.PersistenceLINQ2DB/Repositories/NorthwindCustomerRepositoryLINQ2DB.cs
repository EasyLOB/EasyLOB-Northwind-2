using Northwind.Data;
using EasyLOB.Persistence;
using System.Linq;

namespace Northwind.Persistence
{
    public class NorthwindCustomerRepositoryLINQ2DB : NorthwindGenericRepositoryLINQ2DB<Customer>
    {
        #region Methods

        public NorthwindCustomerRepositoryLINQ2DB(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override IQueryable<Customer> Join(IQueryable<Customer> query)
        {
            return
                from customer in query
                select new Customer
                {
                    CustomerId = customer.CustomerId,
                    CompanyName = customer.CompanyName,
                    ContactName = customer.ContactName,
                    ContactTitle = customer.ContactTitle,
                    Address = customer.Address,
                    City = customer.City,
                    Region = customer.Region,
                    PostalCode = customer.PostalCode,
                    Country = customer.Country,
                    Phone = customer.Phone,
                    Fax = customer.Fax
                };
        }

        #endregion Methods
    }
}

