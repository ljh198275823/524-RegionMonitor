using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace LJH.GeneralLibrary
{
    public static class RSASignHelper
    {
        public static byte[] HashAndSignBytes(byte[] DataToSign, string Key)
        {
            try
            {
                // Create a new instance of RSACryptoServiceProvider using the  key from RSAParameters.  
                RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();
                RSAalg.FromXmlString(Key);
                // Hash and sign the data. Pass a new instance of SHA1CryptoServiceProvider to specify the use of SHA1 for hashing.
                return RSAalg.SignData(DataToSign, new SHA1CryptoServiceProvider());
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return null;
            }
        }

        public static bool VerifySignedHash(byte[] DataToVerify, byte[] SignedData, string Key)
        {
            try
            {
                // Create a new instance of RSACryptoServiceProvider using the  key from RSAParameters.
                RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();
                RSAalg.FromXmlString(Key);
                // Verify the data using the signature.  Pass a new instance of SHA1CryptoServiceProvider to specify the use of SHA1 for hashing.
                return RSAalg.VerifyData(DataToVerify, new SHA1CryptoServiceProvider(), SignedData);

            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return false;
            }
        }
    }
}
