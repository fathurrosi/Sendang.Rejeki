using DataAccessLayer;
using System;
using System.Collections.Generic;

namespace DataObject
{

    public class PurchaseOrderNotes : IDataMapper<PurchaseOrderNotes>
    {
        //[Column("PurchaseOrderCode",100)] 
        public string PurchaseOrderCode { get; set; }

        //[Column("Sequence")]
        public Int32? Sequence { get; set; }

        //[Column("Note",200)] 
        public string Note { get; set; }


        public List<PurchaseOrderNotes> DatasetToDto(System.Data.DataSet ds)
        {
            List<PurchaseOrderNotes> results = new List<PurchaseOrderNotes>();
            if (ds != null)
            {
                System.Data.DataTable tb = ds.Tables[0];
                foreach (System.Data.DataRow dr in tb.Rows)
                {
                    var helper = new PurchaseOrderNotes();
                    helper.PurchaseOrderCode = string.Format("{0}", dr["PurchaseOrderCode"]);
                    helper.Sequence = Convert.ToInt32(dr["Sequence"]);
                    helper.Note = string.Format("{0}", dr["Note"]);
                    results.Add(helper);
                }
            }

            return results;
        }


        public PurchaseOrderNotes Map(System.Data.IDataReader reader)
        {
            var helper = new PurchaseOrderNotes();
            helper.PurchaseOrderCode = string.Format("{0}", reader["PurchaseOrderCode"]);
            helper.Sequence = Convert.ToInt32(reader["Sequence"]);
            helper.Note = string.Format("{0}", reader["Note"]);

            return helper;
        }
		

}

//Completion time: 2025-11-16T10:29:16.9914276+07:00

}
