using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HumanResources_Application.Models.VMs.CompanyVMS
{
    [DataContract]
    public class CompanyChartVM
    {
        
        public CompanyChartVM(string label, double y)
        {
            this.Label = label;
            this.Y = y;
        }

     
        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "label")]
        public string Label = "";

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "y")]
        
        public Nullable<double> Y = null;
       
    }
}
