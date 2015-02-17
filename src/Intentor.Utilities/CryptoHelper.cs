/*********************************************
Intentor.Utilities
**********************************************
Copyright © 2009-2012 André "Intentor" Martins
http://intentor.com.br/
*********************************************/

using System;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Text;

namespace Intentor.Utilities {
    /// <summary>
    /// Métodos de apoio em criptografia.
    /// </summary>
    public static class CryptoHelper {
        #region Campos

        /// <summary>
        /// Chave interna de criptografia.
        /// </summary>
        private const string INTERNAL_KEY = "v7hT53o0aL";

        /// <summary>
        /// Valor utilizado para geração da criptografia juntamente da chave
        /// </summary>
        private const string SALT = "H9dY317JnAp";

        #endregion

        #region Métodos

        /// <summary>
        /// Obtém o objeto que representa o algoritmo de criptografia utilizado.
        /// </summary>
        /// <param name="key">Chave de encriptação.</param>
        /// <returns>Objeto para criptografia.</returns>
        public static SymmetricAlgorithm GetAlgorithm(string key) {
            SymmetricAlgorithm algorithm = new TripleDESCryptoServiceProvider();
            algorithm.IV = GetIV();

            //Realiza ajuste do tamanho da chave.
            if (algorithm.LegalKeySizes.Length > 0) {
                int keySize = key.Length * 8;
                int minSize = algorithm.LegalKeySizes[0].MinSize;
                int maxSize = algorithm.LegalKeySizes[0].MaxSize;
                int skipSize = algorithm.LegalKeySizes[0].SkipSize;

                if (keySize > maxSize) {
                    key = key.Substring(0, maxSize / 8);
                } else if (keySize < maxSize) {
                    int validSize = (keySize <= minSize) ? minSize : (keySize - keySize % skipSize) + skipSize;
                    if (keySize < validSize) {
                        //Preenche a chave com arteriscos para corrigir o tamanho.
                        key = key.PadRight(validSize / 8, '*');
                    }
                }
            }

            PasswordDeriveBytes finalKey = new PasswordDeriveBytes(key, ASCIIEncoding.ASCII.GetBytes(SALT));
            algorithm.Key = finalKey.GetBytes(key.Length);

            return algorithm;
        }

        /// <summary>
        /// Obtém a chave IV para criptografia.
        /// </summary>
        /// <returns>Chave IV.</returns>
        private static byte[] GetIV() {
            return new byte[] { 17, 7, 21, 36, 54, 91, 42, 3 };
        }

        /// <summary>
        /// Criptografa um texto qualquer com base em um passphrase interno da Intentor.Utilities.
        /// </summary>
        /// <param name="plainText">Texto a ser criptografado.</param>
        /// <returns>Texto criptografado.</returns>
        public static string Encrypt(string plainText) {
            return Encrypt(plainText, SALT);
        }

        /// <summary>
        /// Criptografa um texto qualquer com base em um passphrase.
        /// </summary>
        /// <param name="plainText">Texto a ser criptografado.</param>
        /// <param name="key">Chave de encriptação.</param>
        /// <returns>Texto criptografado.</returns>
        public static string Encrypt(string plainText, string key) {
            string cypherText = null;
            byte[] plainByte = ASCIIEncoding.UTF8.GetBytes(plainText);
            SymmetricAlgorithm algorithm = GetAlgorithm(key);

            //Interface de criptografia.
            ICryptoTransform encryptor = algorithm.CreateEncryptor();

            using (MemoryStream ms = new MemoryStream()) {
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write)) {
                    //Obtém os dados criptogradados.
                    cs.Write(plainByte, 0, plainByte.Length);
                    cs.FlushFinalBlock();
                    cypherText = Convert.ToBase64String(ms.ToArray());
                }
            }

            return cypherText;
        }

        /// <summary>
        /// Descriptografa uma string com base em um passphrase interno da Intentor.Utilities.
        /// </summary>
        /// <param name="cipherText">Texto a ser decriptografado</param>
        /// <exception cref="System.Security.Cryptography.CryptographicException">Chave de encriptação inválida.</exception>
        /// <returns>Texto descriptografado.</returns>
        public static string Decrypt(string cipherText) {
            return Decrypt(cipherText, SALT);
        }

        /// <summary>
        /// Descriptografa uma string com base em um passphrase.
        /// </summary>
        /// <param name="cipherText">Texto a ser decriptografado.</param>
        /// <param name="key">Chave utilizada na encriptação.</param>
        /// <exception cref="System.Security.Cryptography.CryptographicException">Chave de encriptação inválida.</exception>
        /// <returns>Texto descriptografado.</returns>
        public static string Decrypt(string cipherText, string key) {
            string plainText = null;
            SymmetricAlgorithm algorithm = GetAlgorithm(key);

            //Converte a string em Base 64 para um array de bytes.
            byte[] cryptoData = Convert.FromBase64String(cipherText);

            //Interface de criptografia.
            ICryptoTransform encryptor = algorithm.CreateDecryptor();

            using (MemoryStream ms = new MemoryStream(cryptoData, 0, cryptoData.Length)) {
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Read)) {
                    using (StreamReader sr = new StreamReader(cs, CultureHelper.EncodingUtf8)) {
                        plainText = sr.ReadToEnd();
                    }
                }
            }

            return plainText;
        }

        #endregion
    }
}
