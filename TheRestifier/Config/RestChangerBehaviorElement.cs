using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace TheRestifier.Config
{
    class RestChangerBehaviorElement : BehaviorExtensionElement
    {
        private Dictionary<string, IOperationBehavior> RESTOperationBehaviors = new Dictionary<string, IOperationBehavior>();


        [ConfigurationProperty("Operations", IsDefaultCollection = true)]
        public OperationSet Operations
        {
            get
            {
                return (OperationSet)base["Operations"];
            }
        }


        public override Type BehaviorType
        {
            get { return typeof(RestChangerBehavior); }
        }

        protected override object CreateBehavior()
        {
            foreach (OperationElement op in Operations) 
            {
                string method = op.Method.ToUpper();
                if (method == "GET")
                {
                    WebGetAttribute webOpBeh = new WebGetAttribute();
                    webOpBeh.UriTemplate = op.UriTemplate;
                    webOpBeh.RequestFormat = op.RequestFormat;
                    webOpBeh.ResponseFormat = op.ResponseFormat;
                    RESTOperationBehaviors.Add(op.Name, webOpBeh);
                }
                else if (method == "POST" || method == "DELETE" || method == "PUT")
                {
                    WebInvokeAttribute webInvBeh = new WebInvokeAttribute();
                    webInvBeh.UriTemplate = op.UriTemplate;
                    webInvBeh.Method = op.Method;
                    webInvBeh.RequestFormat = op.RequestFormat;
                    webInvBeh.ResponseFormat = op.ResponseFormat;
                   
                    RESTOperationBehaviors.Add(op.Name, webInvBeh);
                }
            }
            RestChangerBehavior beh = new RestChangerBehavior(RESTOperationBehaviors);
            return beh;
        }
    }
}
