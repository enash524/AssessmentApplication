using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AssessmentApplication.DataContracts;

namespace AssessmentApplication.DAL
{
    public interface ISalesOrderDM
    {
        #region Public Methods

        Task<List<SalesOrderHeader>> GetAllSalesOrderHeaderAsync(string customerName, DateTime? dueDateEnd, DateTime? dueDateStart, DateTime? orderDateEnd, DateTime? orderDateStart, DateTime? shipDateEnd, DateTime? shipDateStart);

        Task<List<SalesOrderDetail>> GetSalesOrderDetailAsync(int salesOrderId);

        #endregion Public Methods
    }
}
