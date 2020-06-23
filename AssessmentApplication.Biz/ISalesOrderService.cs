using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AssessmentApplication.DataContracts;

namespace AssessmentApplication.Biz
{
    public interface ISalesOrderService
    {
        #region Public Methods

        Task<List<SalesOrderHeader>> GetAllSalesOrderHeaderAsync(string customerName, DateTime? dueDateEnd, DateTime? dueDateStart, DateTime? orderDateEnd, DateTime? orderDateStart, DateTime? shipDateEnd, DateTime? shipDateStart);

        Task<List<SalesOrderDetail>> GetSalesOrderDetailAsync(int salesOrderId);

        #endregion Public Methods
    }
}