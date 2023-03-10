using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDatabase.Models
{
    public class CandidateAddress
    {
        public int CandidateAddressId { get; set; }
        public string AddressLine { get; set; }
        public string? State { get; set; }
        public string? Province { get; set; }
        public string City { get; set; }
        public int PostalCode { get; set; }
        public virtual Candidate Candidate { get; set; }
        
    }
}
