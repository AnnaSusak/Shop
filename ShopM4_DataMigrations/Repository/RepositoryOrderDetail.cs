using ShopM4_DataMigrations.Data;
using ShopM4_DataMigrations.Repository.IRepository;
using ShopM4_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopM4_DataMigrations.Repository
{
    public class RepositoryOrderDetail : Repository<OrderDetail>, IRepositoryOrderDetail
    {
        public RepositoryOrderDetail(ApplicationDbContext db) : base(db) { }

        public void Update(OrderDetail obj)
        {
            db.OrderDetail.Update(obj);
        }
    }
}
