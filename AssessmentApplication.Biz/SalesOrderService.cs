using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AssessmentApplication.DAL;
using AssessmentApplication.DataContracts;

namespace AssessmentApplication.Biz
{
    public class SalesOrderService : ISalesOrderService
    {
        #region Private Fields

        private readonly Lazy<ISalesOrderDM> _dm;

        #endregion Private Fields

        #region Public Constructors

        public SalesOrderService()
        {
            _dm = new Lazy<ISalesOrderDM>(() => new SalesOrderDM());
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<List<SalesOrderHeader>> GetAllSalesOrderHeaderAsync(string customerName, DateTime? dueDateEnd, DateTime? dueDateStart, DateTime? orderDateEnd, DateTime? orderDateStart, DateTime? shipDateEnd, DateTime? shipDateStart)
        {
            return await _dm.Value.GetAllSalesOrderHeaderAsync(customerName, dueDateEnd, dueDateStart, orderDateEnd, orderDateStart, shipDateEnd, shipDateStart);
        }

        public async Task<List<SalesOrderDetail>> GetSalesOrderDetailAsync(int salesOrderId)
        {
            return await _dm.Value.GetSalesOrderDetailAsync(salesOrderId);
        }

        #endregion Public Methods
    }
}