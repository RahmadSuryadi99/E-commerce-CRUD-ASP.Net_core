using Basilisk.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Repository
{
    public class TransactionRepository : BaseRepository
    {
        public static void OrderProduct(Order order, List<OrderDetail> orderDetail, List<Cart> listCart)
        {
            using (var context = new BasiliskTFContext())
            {
                using var transact = context.Database.BeginTransaction();
                try
                {
                    context.Orders.Add(order);
                    foreach (var cart in listCart)
                    {
                        var product = context.Products.SingleOrDefault(p => p.Id == cart.ProductId);
                        product.Stock -= cart.Quantity;
                        context.Carts.Remove(cart);
                    }
                    foreach (var orDet in orderDetail)
                    {
                        context.OrderDetails.Add(orDet);
                    }

                    context.SaveChanges();
                    transact.Commit();
                }
                catch (Exception ex)
                {
                    transact.Rollback();
                }
            }
        }
    }
}

