using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ServiceModel.Web;

namespace TheRestifier
{
    public class RestChangerBehavior: IEndpointBehavior
    {
        private Dictionary<string, IOperationBehavior> RESTOperationBehaviors = new Dictionary<string, IOperationBehavior>();

        public RestChangerBehavior(Dictionary<string, IOperationBehavior> restBehaviors)
        {
            this.RESTOperationBehaviors = restBehaviors;
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
            Debug.WriteLine("AddBindingParameters");
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {

        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            foreach(OperationDescription opDesc in endpoint.Contract.Operations)
            {

                IEnumerable<IOperationBehavior> webBehs =
                    (from webbehavior in opDesc.OperationBehaviors
                     where webbehavior is WebGetAttribute || webbehavior is WebInvokeAttribute
                     select webbehavior);

                if (webBehs.Count<IOperationBehavior>() == 0)
                {
                    IOperationBehavior webBeh = null;
                    if (!RESTOperationBehaviors.TryGetValue(opDesc.Name, out webBeh))
                    {
                        webBeh = new WebGetAttribute();
                        
                    }
                    opDesc.OperationBehaviors.Add(webBeh);
                }
                
            }
        }

        public void Validate(ServiceEndpoint endpoint)
        {

        }
    }
}
