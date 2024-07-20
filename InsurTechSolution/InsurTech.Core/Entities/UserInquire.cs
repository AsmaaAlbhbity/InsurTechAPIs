using InsurTech.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurTech.Core.Entities
{
    public class UserInquire:BaseEntity
    {
        public string Email { get; set; }
        public string Content { get; set; }   
        public DateTime Date { get; set; }
        public bool IsRead { get; set; }
    }
}
  