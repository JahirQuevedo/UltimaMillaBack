using QRCoder;

namespace ALOG.InfraestructuraReportes
{
    /// <summary>
    /// Clase para generar codigos de barra.
    /// </summary>
    public class GeneraCodigoBarra
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public GeneraCodigoBarra()
        {

        }

        public string CodigoQRImageBase64(string contenidoCodigo, int pixeles)
        {
            try
            {
                QRCodeData qrCodeData;
                using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                {
                    qrCodeData = qrGenerator.CreateQrCode(contenidoCodigo, QRCodeGenerator.ECCLevel.Q);
                }

                var imgType = Base64QRCode.ImageType.Png;

                var qrCode = new Base64QRCode(qrCodeData);

                string qrCodeImageAsBase64 = qrCode.GetGraphic(pixeles, SixLabors.ImageSharp.Color.Black, SixLabors.ImageSharp.Color.White, true, imgType);

                return qrCodeImageAsBase64;
            }
            catch
            {
                throw;
            }
        }



    }
}
