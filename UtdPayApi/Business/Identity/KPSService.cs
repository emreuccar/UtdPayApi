using FunTranslator.Data.Models;
using KPSPublic;
using static KPSPublic.KPSPublicSoapClient;

namespace UtdPayApi.Business.Identity
{
    public class KPSService
    {
        public bool ValidateIdentity(CustomerRequestModel customerRequestModel)
        {
            var kPSPublicSoapClient = new KPSPublicSoapClient(EndpointConfiguration.KPSPublicSoap);
           var response = kPSPublicSoapClient.TCKimlikNoDogrulaAsync(
                customerRequestModel.IdentityNo,
                customerRequestModel.Name,
                customerRequestModel.Surname,
                customerRequestModel.BirthDate.Year
                );

            return response.Result.Body.TCKimlikNoDogrulaResult;
        }
    }
}
