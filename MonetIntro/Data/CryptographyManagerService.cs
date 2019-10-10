using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace MonetIntro.Data
{
    public class CryptographyManagerService
    {
        private const string CertFileName = "testcert.pfx";

        private readonly X509Certificate2 cert = new X509Certificate2(File.ReadAllBytes(CertFileName), "monet");

        public async Task<byte[]> Handle(Stream file)
        {
            var data = await ReadFully(file);
            
            var envelopedCms = new EnvelopedCms(new ContentInfo(data));

            envelopedCms.Encrypt(new CmsRecipient(cert));

            var encryptedMessage = envelopedCms.Encode();

            var signedCms = new SignedCms(new ContentInfo(encryptedMessage));

            var signer = new CmsSigner(cert) {IncludeOption = X509IncludeOption.EndCertOnly};

            signedCms.ComputeSignature(signer);

            var result = signedCms.Encode();

            return result;
        }

        private static async Task<byte[]> ReadFully(Stream input)
        {
            var buffer = new byte[16 * 1024];
            await using var ms = new MemoryStream();
            int read;
            while ((read = await input.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                ms.Write(buffer, 0, read);
            }
            return ms.ToArray();
        }
    }
}
