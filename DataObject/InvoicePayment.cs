using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;

namespace DataObject
{
public class InvoicePayment : IDataMapper<InvoicePayment>
{
	//[Key(true)]
	//[Column("PaymentID")]
	public Int32 PaymentID { get; set; }

	//[Key()]
	//[Column("InvoiceID")]
	public Int32 InvoiceID { get; set; }

	//[Column("PaymentDate")]
	public DateTime PaymentDate { get; set; }

	//[Column("TotalPayment")]
	public decimal TotalPayment { get; set; }

	//[Column("TotalReturn")]
	public decimal TotalReturn { get; set; }

	//[Column("PaymentAmount")]
	public decimal PaymentAmount { get; set; }

	//[Column("CreatedBy",50)] 
	public string CreatedBy { get; set; }

	//[Column("CreatedDate")]
	public DateTime? CreatedDate { get; set; }

	//[Column("ModifiedBy",50)] 
	public string ModifiedBy { get; set; }

	//[Column("ModifiedDate")]
	public DateTime? ModifiedDate { get; set; }


	public List<InvoicePayment> DatasetToDto(System.Data.DataSet ds)
	{
	    List<InvoicePayment> results = new List<InvoicePayment>();
	    if (ds != null)
	    {
	        System.Data.DataTable tb = ds.Tables[0];
	        foreach (System.Data.DataRow dr in tb.Rows)
	        {
	            var helper = new InvoicePayment();
			helper.PaymentID= (dr["PaymentID"] != null) ? Convert.ToInt32(dr["PaymentID"]) :0;
			helper.InvoiceID= (dr["InvoiceID"] != null) ? Convert.ToInt32(dr["InvoiceID"]) :0;
			helper.PaymentDate= Convert.ToDateTime(dr["PaymentDate"]);
			helper.TotalPayment= Convert.ToDecimal(dr["TotalPayment"]);
			helper.TotalReturn= (dr["TotalReturn"] != null) ? Convert.ToDecimal(dr["TotalReturn"]) :0;	
			helper.PaymentAmount= (dr["PaymentAmount"] != null) ? Convert.ToDecimal(dr["PaymentAmount"]) :0;	
			helper.CreatedBy= string.Format("{0}", dr["CreatedBy"]);
			helper.CreatedDate= (dr["CreatedDate"] != null) ? Convert.ToDateTime(dr["CreatedDate"]) : new DateTime(1900, 1, 1);	
			helper.ModifiedBy= string.Format("{0}", dr["ModifiedBy"]);
			helper.ModifiedDate= (dr["ModifiedDate"] != null) ? Convert.ToDateTime(dr["ModifiedDate"]) : new DateTime(1900, 1, 1);	
			results.Add(helper);
	        }
	    }
	
	    return results;
	}
	
	
	public InvoicePayment Map(System.Data.IDataReader reader)
	{
	    var helper = new InvoicePayment();
		helper.PaymentID= (reader["PaymentID"] is System.DBNull ) ? 0: Convert.ToInt32(reader["PaymentID"]);
		helper.InvoiceID= (reader["InvoiceID"] is System.DBNull ) ? 0: Convert.ToInt32(reader["InvoiceID"]);
		helper.PaymentDate= Convert.ToDateTime(reader["PaymentDate"]);
		helper.TotalPayment= Convert.ToDecimal(reader["TotalPayment"]);
		helper.TotalReturn= (reader["TotalReturn"] is System.DBNull) ? 0: Convert.ToDecimal(reader["TotalReturn"]);	
		helper.PaymentAmount= (reader["PaymentAmount"] is System.DBNull) ? 0: Convert.ToDecimal(reader["PaymentAmount"]);	
		helper.CreatedBy= string.Format("{0}",reader["CreatedBy"]);
		helper.CreatedDate= (reader["CreatedDate"] is System.DBNull) ? (DateTime?)null :Convert.ToDateTime(reader["CreatedDate"]);	
		helper.ModifiedBy= string.Format("{0}",reader["ModifiedBy"]);
		helper.ModifiedDate= (reader["ModifiedDate"] is System.DBNull) ? (DateTime?)null :Convert.ToDateTime(reader["ModifiedDate"]);	

	    return helper;
	}
		

}


}
