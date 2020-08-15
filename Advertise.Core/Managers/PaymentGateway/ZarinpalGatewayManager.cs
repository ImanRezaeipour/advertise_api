using Advertise.Core.ZarinpalServiceReference;

namespace Advertise.Core.Managers.PaymentGateway
{
    public class ZarinpalGatewayManager : IZarinpalGatewayManager
    {
        #region Public Methods


        public PaymentGatewayImplementationServicePortTypeClient ZarinpalGateway()
        {
            System.Net.ServicePointManager.Expect100Continue = false;
            var zarinpal = new PaymentGatewayImplementationServicePortTypeClient();

            return zarinpal;
        }

        #endregion Public Methods
    }
}