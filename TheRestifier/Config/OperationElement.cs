using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace TheRestifier.Config
{
    public class OperationElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get
            {
                return (string)this["name"];
            }
            set
            {
                this["name"] = value;
            }
        }

        [ConfigurationProperty("method", DefaultValue = "GET")]
        public string Method
        {
            get
            {
                return (string)this["method"];
            }
            set
            {
                this["method"] = value;
            }
        }


        [ConfigurationProperty("RequestFormat")]
        public WebMessageFormat RequestFormat
        {
            get
            {
                return (WebMessageFormat)this["RequestFormat"];
            }
            set
            {
                this["RequestFormat"] = value;
            }
        }


        [ConfigurationProperty("ResponseFormat")]
        public WebMessageFormat ResponseFormat
        {
            get
            {
                return (WebMessageFormat)this["ResponseFormat"];
            }
            set
            {
                this["ResponseFormat"] = value;
            }
        }

        [ConfigurationProperty("UriTemplate")]
        public string UriTemplate
        {
            get
            {
                return (string)this["UriTemplate"];
            }
            set
            {
                this["UriTemplate"] = value;
            }
        }
    }
}
