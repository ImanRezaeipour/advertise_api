using Advertise.Core.ZarinpalServiceReference;

namespace Advertise.Core.Managers.PaymentGateway
{
    public interface IZarinpalGatewayManager
    {
        PaymentGatewayImplementationServicePortTypeClient ZarinpalGateway();
    }
}