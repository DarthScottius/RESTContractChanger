using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace Component
{
    [ServiceContract(Namespace = "urn:DarthScottius")]
    interface ISeahawks
    {
        [OperationContract]
        //[WebInvoke(Method = "POST")]
        //[WebGet()]
        string BeatTheNiners(int pointSpread);

        [OperationContract]
        //[WebGet(UriTemplate = "BeatTheBroncos?Spread={pointSpread}")]
        string BeatTheBroncos(int pointSpread);

        [OperationContract]
      
        string WinSuperbowl();
    }

    

}
