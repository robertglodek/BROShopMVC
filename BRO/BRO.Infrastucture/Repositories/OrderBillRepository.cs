using BRO.Domain.Entities;
using BRO.Domain.IRepositories;
using BRO.Infrastructure.Repositories;
using BRO.Infrastucture.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Infrastucture.Repositories
{
    public class OrderBillRepository : RepositoryAsync<OrderBill>, IOrderBillRepository
    {

        private readonly ApplicationDbContext _db;
        public OrderBillRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        

    }
}
