using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.ViewModel.Cart
{
    public class CartProductViewModel
    {
        public long IdCard { get; set; }
        public long Id { get; set; }
        public string NamaProduk { get; set; }
        public decimal Harga { get; set; }
        public int Quantity { get; set; }
        public bool Checked { get; set; }
        public decimal TotalHarga { get; set; }
    }
}
