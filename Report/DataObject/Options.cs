using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;

namespace DataObject
{

    //public class Options : IDataMapper<Options>
    //{
    //    //[Key()]
    //    //[Column("ValueMember",20)] 
    //    public string ValueMember { get; set; }

    //    //[Column("DisplayMember",100)] 
    //    public string DisplayMember { get; set; }

    //    //[Key()]
    //    //[Column("Name",20)] 
    //    public string Name { get; set; }

    //    //[Column("CreatedBy",50)] 
    //    public string CreatedBy { get; set; }

    //    //[Column("CreatedDate")]
    //    public DateTime? CreatedDate { get; set; }

    //    //[Column("ModifiedBy",50)] 
    //    public string ModifiedBy { get; set; }

    //    //[Column("ModifiedDate")]
    //    public DateTime? ModifiedDate { get; set; }

    //    public string Description { get; set; }
    //    public List<Options> DatasetToDto(System.Data.DataSet ds)
    //    {
    //        List<Options> results = new List<Options>();
    //        if (ds != null)
    //        {
    //            System.Data.DataTable tb = ds.Tables[0];
    //            foreach (System.Data.DataRow dr in tb.Rows)
    //            {
    //                var helper = new Options();
    //                helper.ValueMember = string.Format("{0}", dr["ValueMember"]);
    //                helper.DisplayMember = string.Format("{0}", dr["DisplayMember"]);
    //                helper.Name = string.Format("{0}", dr["Name"]);
    //                helper.CreatedBy = string.Format("{0}", dr["CreatedBy"]);
    //                helper.CreatedDate = (dr["CreatedDate"] != null) ? Convert.ToDateTime(dr["CreatedDate"]) : new DateTime(1900, 1, 1);
    //                helper.ModifiedBy = string.Format("{0}", dr["ModifiedBy"]);
    //                helper.ModifiedDate = (dr["ModifiedDate"] != null) ? Convert.ToDateTime(dr["ModifiedDate"]) : new DateTime(1900, 1, 1);
    //                results.Add(helper);
    //            }
    //        }

    //        return results;
    //    }

    //    public Options Map(System.Data.IDataReader reader)
    //    {
    //        var helper = new Options();
    //        helper.ValueMember = string.Format("{0}", reader["ValueMember"]);
    //        helper.DisplayMember = string.Format("{0}", reader["DisplayMember"]);
    //        helper.Name = string.Format("{0}", reader["Name"]);
    //        helper.CreatedBy = string.Format("{0}", reader["CreatedBy"]);
    //        helper.CreatedDate = (reader["CreatedDate"] is System.DBNull) ? (DateTime?)null : Convert.ToDateTime(reader["CreatedDate"]);
    //        helper.ModifiedBy = string.Format("{0}", reader["ModifiedBy"]);
    //        helper.ModifiedDate = (reader["ModifiedDate"] is System.DBNull) ? (DateTime?)null : Convert.ToDateTime(reader["ModifiedDate"]);

    //        return helper;
    //    }
    //}


    public class Options : IDataMapper<Options>
    {
        //[Key()]
        //[Column("ValueMember",20)] 
        public string ValueMember { get; set; }

        //[Column("DisplayMember",100)] 
        public string DisplayMember { get; set; }

        //[Key()]
        //[Column("Name",20)] 
        public string Name { get; set; }

        //[Column("CreatedBy",50)] 
        public string CreatedBy { get; set; }

        //[Column("CreatedDate")]
        public DateTime? CreatedDate { get; set; }

        //[Column("ModifiedBy",50)] 
        public string ModifiedBy { get; set; }

        //[Column("ModifiedDate")]
        public DateTime? ModifiedDate { get; set; }

        //[Column("Description",500)] 
        public string Description { get; set; }


        public List<Options> DatasetToDto(System.Data.DataSet ds)
        {
            List<Options> results = new List<Options>();
            if (ds != null)
            {
                System.Data.DataTable tb = ds.Tables[0];
                foreach (System.Data.DataRow dr in tb.Rows)
                {
                    var helper = new Options();
                    helper.ValueMember = string.Format("{0}", dr["ValueMember"]);
                    helper.DisplayMember = string.Format("{0}", dr["DisplayMember"]);
                    helper.Name = string.Format("{0}", dr["Name"]);
                    helper.CreatedBy = string.Format("{0}", dr["CreatedBy"]);
                    helper.CreatedDate = (dr["CreatedDate"] != null) ? Convert.ToDateTime(dr["CreatedDate"]) : new DateTime(1900, 1, 1);
                    helper.ModifiedBy = string.Format("{0}", dr["ModifiedBy"]);
                    helper.ModifiedDate = (dr["ModifiedDate"] != null) ? Convert.ToDateTime(dr["ModifiedDate"]) : new DateTime(1900, 1, 1);
                    helper.Description = string.Format("{0}", dr["Description"]);
                    results.Add(helper);
                }
            }

            return results;
        }


        public Options Map(System.Data.IDataReader reader)
        {
            var helper = new Options();
            helper.ValueMember = string.Format("{0}", reader["ValueMember"]);
            helper.DisplayMember = string.Format("{0}", reader["DisplayMember"]);
            helper.Name = string.Format("{0}", reader["Name"]);
            helper.CreatedBy = string.Format("{0}", reader["CreatedBy"]);
            helper.CreatedDate = (reader["CreatedDate"] is System.DBNull) ? (DateTime?)null : Convert.ToDateTime(reader["CreatedDate"]);
            helper.ModifiedBy = string.Format("{0}", reader["ModifiedBy"]);
            helper.ModifiedDate = (reader["ModifiedDate"] is System.DBNull) ? (DateTime?)null : Convert.ToDateTime(reader["ModifiedDate"]);
            helper.Description = string.Format("{0}", reader["Description"]);

            return helper;
        }
		

}

//Completion time: 2025-11-15T21:24:12.9345790+07:00

}
