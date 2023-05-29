using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basilisk.DataAccess.Models;
using Basilisk.ViewModel.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Basilisk.ViewModel.Salesman
{
    public class SalesmanViewModel
    {
        public string ID { get; set; }
    
        [Required(ErrorMessage = "Wajib Diisi")]
        public string FirstName { get; set; }

        public string? LastName { get; set; }

        [Required(ErrorMessage = "Wajib Diisi")]
        public string Level { get; set; }

        [Required(ErrorMessage = "Wajib Diisi")]
        [VallidationHireDate(ErrorMessage = "HireDate")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Wajib Diisi")]
        [VallidationHireDate( ValidationType = "HireDate")]
        public DateTime HireDate { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        [VallidationHireDate(ValidationType = "phone")]
        public string Phone { get; set; }
        public string? SuperiorEmpID { get; set; }

        public string? SuperiorEmpName { get; set; } 
        
        public List<SelectListItem>? DropDownSales { get; set; } 


        //public IQueryable JumlahRegion { get; set; }

    }
}
