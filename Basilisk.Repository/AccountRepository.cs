using Basilisk.DataAccess.Models;
using Basilisk.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Repository
{
    public class AccountRepository : BaseRepository, IRepository<Account>
    {
        public static string GetRole(string username)
        {
            using (var context = new BasiliskTFContext())
            {
                var account= context.Accounts.SingleOrDefault(a => a.Username == username);
                return account.Role;
            }
        }
        public static bool  GetIsAuthentication(string username , string password)
        {
            using (var context = new BasiliskTFContext())
            {
               
                var accountByUsername = context.Accounts.SingleOrDefault(a => a.Username == username && a.Password == password);
                if (accountByUsername != null)
                {
                    return true;

                }
                else
                {
                    return false;
                }
            }
        }

        private static IRepository<Account> instance = new AccountRepository();
        public static IRepository<Account> GetRepository()
        {
            return instance;
        }
        public bool Delete(object id)
        {
            try
            {
                using (var _context = new BasiliskTFContext())
                {
                    var Account = _context.Accounts.SingleOrDefault(a => a.Username == (string)id);
                    _context.Accounts.Remove(Account);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IQueryable<Account> GetAll()
        {
            var context = new BasiliskTFContext();

            return context.Accounts;
        }

        public Account GetSingle(object id)
        {
            var Account = new Account();
            using (var context = new BasiliskTFContext())
            {
                Account = context.Accounts.SingleOrDefault(a => a.Username == (string)id);
            }
            return Account;
        }

        public bool Insert(Account model)
        {
            try
            {
                using (var _context = new BasiliskTFContext())
                {
                    _context.Accounts.Add(model);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Account model)
        {
            try
            {
                using (var _context = new BasiliskTFContext())
                {
                    var Account = _context.Accounts.SingleOrDefault(a => a.Username == model.Username);
                    MappingModel<Account, Account>(Account, model);
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
