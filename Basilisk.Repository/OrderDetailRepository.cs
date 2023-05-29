using Basilisk.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Repository
{
    public class OrderDetailRepository : BaseRepository, IRepository<OrderDetail>
    {
        public static OrderDetailRepository instance = new OrderDetailRepository();
        public static OrderDetailRepository GetRepository() { return instance; }
        public bool Delete(object id)
        {
            try
            {
                using (var _context = new BasiliskTFContext())
                {
                    var OrderDetail = _context.OrderDetails.SingleOrDefault(a => a.Id == (long)id);
                    _context.OrderDetails.Remove(OrderDetail);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IQueryable<OrderDetail> GetAll()
        {
            try
            {
                var context = new BasiliskTFContext();
                return context.OrderDetails;
            }
            catch
            {
                throw;
            }
        }

        public OrderDetail GetSingle(object id)
        {
            try
            {
                var OrderDetail = new OrderDetail();
                using (var context = new BasiliskTFContext())
                {
                    OrderDetail = context.OrderDetails.SingleOrDefault(a => a.Id == (long)id);
                }
                return OrderDetail;
            }
            catch
            {
                throw;
            }
        }

        public bool Insert(OrderDetail model)
        {
            try
            {
                using (var _context = new BasiliskTFContext())
                {
                    //var transaction = _context.Database.BeginTransaction();
                    try
                    {
                        _context.OrderDetails.Add(model);
                        
                        int[] asa = new int[52];
                        //var tes = _context.Orders.LastOrDefault();
                        //if (tes.InvoiceNumber == "05-23-0052")
                        //{
                        //    asa[99] = 12;

                        //}
                        //throw new Exception();
                        _context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        //_context.Database.RollbackTransaction();
                        //transaction.RollbackToSavepoint("BeforeMoreBlogsss");
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(OrderDetail model)
        {
            try
            {
                using (var _context = new BasiliskTFContext())
                {
                    var OrderDetails = _context.OrderDetails.SingleOrDefault(a => a.Id == model.Id);
                    MappingModel(OrderDetails, model);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
