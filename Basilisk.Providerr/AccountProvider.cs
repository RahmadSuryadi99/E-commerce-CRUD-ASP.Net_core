using Basilisk.DataAccess.Models;
using Basilisk.Repository;
using Basilisk.ViewModel;
using Basilisk.ViewModel.Account;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Providerr
{
    public class AccountProvider : BaseProvider
    {
        public static AccountProvider instance = new AccountProvider();
        public static AccountProvider GetProvider() { return instance; }
        public bool IsAuthentication(LoginViewModel model)
        {
            //string hashPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);
            //return AccountRepository.GetIsAuthentication(model.Username, model.Password);
            var account = AccountRepository.GetRepository().GetSingle(model.Username);
            if (account != null)
            {
                string hashPass = account.Password;
                bool verify = BCrypt.Net.BCrypt.Verify(model.Password, hashPass);
                if (verify)
                {
                    return true;
                }
            }
            return false;
        }
        public string GetRoleName(string username)
        {
            var role = AccountRepository.GetRole(username);
            return role;
        }
        public List<GridAccountViewModel> GetIndex()
        {
            var account = AccountRepository.GetRepository().GetAll();
            var model = (from a in account
                         select new GridAccountViewModel
                         {
                             Username = a.Username,
                             Role = a.Role
                         }).ToList();
            return model;
        }
        public void SaveAccount(GridAccountViewModel model)
        {
            var accoun = new Account();
            MappingModel<Account, GridAccountViewModel>(accoun, model);
            accoun.Password = BCrypt.Net.BCrypt.HashPassword("indocyber");
            AccountRepository.GetRepository().Insert(accoun);
        }
        public List<SelectListItem> GetAllRole()
        {
            var suppliers = AccountRepository.GetRepository().GetAll();
            var result = suppliers.Select(a => new SelectListItem
            {
                Value = a.Role,
                Text = a.Role
            }).ToList();
            return result;
        }

    }
}
