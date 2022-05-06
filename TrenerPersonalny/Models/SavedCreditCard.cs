using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrenerPersonalny.Models
{
    public class SavedCreditCard
    {
        public int Id { get; set; }
        public string cardNumber { get; set; }
        public string expDate { get; set; }
        public int cvv { get; set; }
        public string nameOnCard { get; set; }
    }
}
