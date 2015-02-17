/*********************************************
Intentor.Utilities
**********************************************
Copyright © 2009-2012 André "Intentor" Martins
http://intentor.com.br/
*********************************************/

using System;

namespace Intentor.Utilities {
    /// <summary>
    ///Métodos diversos de validação.
    /// </summary>
    public static class ValidationHelper {
        #region Métodos

        /// <summary>
        /// Verifica se um determinado objeto é nulo.
        /// </summary>
        /// <param name="obj">Objeto a ser verificado.</param>
        /// <returns>Valor booleano indicando se o objeto é nulo.</returns>
        public static bool IsNullOrDbNull(object obj) {
            return (obj == null || obj == DBNull.Value);
        }

        /// <summary>
        /// Verifica se um determinado objeto é do tipo numérico.
        /// </summary>
        /// <param name="obj">Objeto a ser verificado.</param>
        /// <returns>Valor booleano indicando se o objeto é um número.</returns>
        public static bool IsNumber(object obj) {
            double Dummy = 0;

            return double.TryParse(obj.ToString(),
                System.Globalization.NumberStyles.Any, null, out Dummy);
        }

        /// <summary>
        /// Realiza a validação de uma string avaliando se seu formato é de uma data.
        /// </summary>
        /// <param name="date">String a ser validada.</param>
        /// <returns>Valor booleano indicando se a string é uma data.</returns>
        public static bool IsDate(string date) {
            return ValidationHelper.IsDate(date,
                CultureHelper.CulturePtBr);
        }

        /// <summary>
        /// Realiza a validação de uma string avaliando se seu formato é de uma data.
        /// </summary>
        /// <param name="date">String a ser validada.</param>
        /// <param name="formatProvider">Provedor que define o formato de data a ser utilizado.</param>
        /// <returns>Valor booleano indicando se a string é uma data.</returns>
        public static bool IsDate(string date, IFormatProvider formatProvider) {
            DateTime dtResult;

            return DateTime.TryParse(date
                , formatProvider
                , System.Globalization.DateTimeStyles.None
                , out dtResult);
        }

        /// <summary>
        /// Verifica se uma string possui dígitos.
        /// </summary>
        /// <param name="s">String a ser validada.</param>
        /// <returns>Valor booleano indicando se a string possui números.</returns>
        public static bool HasNumbers(string s) {
            bool isValid = false;

            foreach (char c in s) {
                if (Char.IsDigit(c)) {
                    isValid = true;
                    break;
                }
            }

            return isValid;
        }

        /// <summary>
        /// Realiza validação de um número de CPF.
        /// </summary>
        /// <param name="cpf">Número do CPF, informado apenas com seus dígitos.</param>
        /// <returns>Valro booleano indicando se o CPF é válido.</returns>
        public static bool IsCpf(string cpf) {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            cpf = cpf.Replace(".", "").Replace("-", "").Trim();

            if (cpf.Length != 11)
                return false;

            tempCpf = cpf.Substring(0, 9);
            soma = 0;
            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            tempCpf = tempCpf + digito;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }

        /// <summary>
        /// Realiza validação de um número de CNPJ.
        /// </summary>
        /// <param name="cnpj">Número do CNPJ, informado apenas com seus dígitos.</param>
        /// <returns>Valro booleano indicando se o CNPJ é válido.</returns>
        public static bool IsCnpj(string cnpj) {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;

            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "").Trim();

            if (cnpj.Length != 14)
                return false;

            tempCnpj = cnpj.Substring(0, 12);

            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cnpj.EndsWith(digito);
        }

        /// <summary>
        /// Realiza validação de um número de CPF/CNPJ.
        /// </summary>
        /// <param name="cpfCnpj">Número do CPF ou CNPJ, informado apenas com seus dígitos.</param>
        /// <returns>Valro booleano indicando se o CNPJ é válido.</returns>
        public static bool IsCpfOrCnpj(string cpfCnpj) {
            return (ValidationHelper.IsCpf(cpfCnpj) || ValidationHelper.IsCnpj(cpfCnpj));
        }

        #endregion
    }
}
