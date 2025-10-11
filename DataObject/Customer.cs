using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataObject
{

    public class Customer : IDataMapper<Customer>
    {
        public Customer() { }
        public Customer(int id, string fullname)
        {
            this.ID = id;
            this.FullName = fullname;
        }

        public Customer(int id, string fullname, string code)
        {
            this.ID = id;
            this.FullName = fullname;
            this.Code = code;
        }

        public int ID { get; set; }

        public string Code { get; set; }

        public string FullName { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }        

        public Customer Map(System.Data.IDataReader reader)
        {
            Customer obj = new Customer();
            obj.ID = Convert.ToInt32(reader["ID"]);
            obj.FullName = reader["FullName"].ToString();
            obj.Address = reader["Address"].ToString();
            obj.Phone = reader["Phone"].ToString();
            try
            {
                obj.Code = reader["Code"].ToString();
            }
            catch (Exception)
            {
            }

            return obj;
        }
    }

}
