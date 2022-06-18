using BRO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.IRepositories
{
    public interface IOrderBillRepository:IRepositoryAsync<OrderBill>
    {
        Task UpdateAsync(OrderBill orderBill);
    }
}
