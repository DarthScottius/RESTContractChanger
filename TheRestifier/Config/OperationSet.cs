using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRestifier.Config
{
    public class OperationSet : ConfigurationElementCollection
    {

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMap;
            }
        }

        protected override string ElementName
        {
            get
            {
                return "Operation";
            }
        }


        protected override ConfigurationElement CreateNewElement()
        {
            return new OperationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((OperationElement)element).Name;
        }
    }
}
