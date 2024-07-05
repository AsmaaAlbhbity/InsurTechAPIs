using InsurTech.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurTech.Core.Entities
{
    public class HealthPlanRequest:UserRequest
    {
    
        public string MedicalNetwork { get; set; }
        public decimal ClinicsCoverage { get; set; }
        public decimal HospitalizationAndSurgery { get; set; }
        public decimal OpticalCoverage { get; set; }
        public decimal DentalCoverage { get; set; }
    }
}
