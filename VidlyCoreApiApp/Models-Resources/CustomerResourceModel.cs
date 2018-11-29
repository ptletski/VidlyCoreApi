using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using VidlyCoreApp.Models;

namespace VidlyCoreApiApp.ResourceModels
{
    public class CustomerResourceModel : CommonResourceModel
    {
        public CustomerResourceModel() : base()
        {
        }

        public List<Customer> Customers
        {
            get
            {
                try
                {
                    var customers = _dbContext.Customers;

                    if (customers == null)
                    {
                        return null;
                    }

                    if (customers.Any() == false)
                    {
                        return null;
                    }

                    var customerList = customers.ToList();

                    return customerList;
                }
                catch (Exception exception)
                {
                    Debug.Assert(false, "Failure Gathering Customers");
                    Debug.Assert(false, exception.Message);

                    throw new ResourceFindAllException(exception.Message);
                }
            }
        }

        public Customer Find(int id)
        {
            try
            {
                Customer customer = null; 

                customer = _dbContext.Customers.Single(c => c.CustomerId == id);

                return customer;
            }
            catch (Exception e)
            {
                Debug.Assert(false, "Failure Finding Customer by Id");
                Debug.Assert(false, e.Message);

                throw new ResourceFindException(e.Message);
            }
        }

        public Customer Add(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            try
            {   // Create new customer with a system defined Customer ID.
                Customer target = new Customer
                {
                    Address = customer.Address,
                    BirthDate = customer.BirthDate,
                    City = customer.City,
                    IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter,
                    MembershipTypeId = customer.MembershipTypeId,
                    Name = customer.Name,
                    State = customer.State
                };

                // Save customer
                _dbContext.Customers.Add(customer);
                _dbContext.SaveChanges();

                target.CustomerId = _dbContext.RetrieveLastAutoIncrementKey(VidlyDbContext.CustomersTable);

                return target;
            }
            catch (Exception exception)
            {
                Debug.Assert(false, "Failure Adding Customer");
                Debug.Assert(false, exception.Message);

                throw new ResourceAddException(exception.Message);
            }
        }

        public bool Update(Customer customerUpdate)
        {
            if (customerUpdate == null)
            {
                throw new ArgumentNullException(nameof(customerUpdate));
            }

            try
            {
                bool isUpdated = false; 
                var existingCustomer = _dbContext.Customers.Find(customerUpdate.CustomerId);

                if (existingCustomer != null)
                {
                    _dbContext.Entry(existingCustomer).CurrentValues.SetValues(customerUpdate); // !!
                    _dbContext.SaveChanges();

                    isUpdated = true;
                }

                return isUpdated;
            }
            catch (Exception exception)
            {
                Debug.Assert(false, $"Failure Updating Customer {customerUpdate.CustomerId}");
                Debug.Assert(false, exception.Message);

                throw new ResourceUpdateException(exception.Message);
            }
        }

        public bool Delete(int customerId)
        {
            try
            {
                bool isDeleted = false; 
                var customer = _dbContext.Customers.Find(customerId);

                if (customer != null)
                {
                    _dbContext.Customers.Remove(customer);
                    _dbContext.SaveChanges();

                    isDeleted = true;
                }

                return isDeleted;
            }
            catch (Exception exception)
            {
                Debug.Assert(false, $"Failure Deleting Customer With ID: {customerId}");
                Debug.Assert(false, exception.Message);

                throw new ResourceDeleteException(exception.Message);
            }
        }
    }
}
