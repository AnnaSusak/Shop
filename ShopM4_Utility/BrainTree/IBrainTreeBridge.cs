using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Braintree;

namespace ShopM4_Utility.BrainTree
{
    public interface IBrainTreeBridge
    {
        IBraintreeGateway CreateGateWay();
        IBraintreeGateway GetGateWay();
    }
}
