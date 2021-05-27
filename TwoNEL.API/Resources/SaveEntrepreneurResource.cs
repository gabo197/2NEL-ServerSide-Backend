using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwoNEL.API.Resources
{
    public class SaveEntrepreneurResource : SaveProfileResource
    {
        public SaveEnterpriseResource Enterprise { get; set; }
    }
}
