/*********************************************
Intentor.Utilities
**********************************************
Copyright © 2009-2012 André "Intentor" Martins
http://intentor.com.br/
*********************************************/

using System;

namespace Intentor.Utilities {
    /// <summary>
    /// Métodos diversos de validação utilizados como extensão.
    /// </summary>
    public static class ValidationExtensions {
        /// <summary>
        /// Verifica se um determinado objeto é nulo.
        /// </summary>
        /// <param name="obj">Objeto a ser verificado.</param>
        /// <returns>Valor booleano indicando se o objeto é nulo.</returns>
        public static bool IsNullOrDbNull(this object obj) {
            return ValidationHelper.IsNullOrDbNull(obj);
        }

        /// <summary>
        /// Verifica se um determinado objeto é do tipo numérico.
        /// </summary>
        /// <param name="obj">Objeto a ser verificado.</param>
        /// <returns>Valor booleano indicando se o objeto é um número.</returns>
        public static bool IsNullOrEmpty(this string obj) {
            return String.IsNullOrEmpty(obj);
        }

        /// <summary>
        /// Verifica se um determinado objeto é do tipo numérico.
        /// </summary>
        /// <param name="obj">Objeto a ser verificado.</param>
        /// <returns>Valor booleano indicando se o objeto é nulo.</returns>
        public static bool IsNumber(this object obj) {
            double Dummy = 0;

            return double.TryParse(obj.ToString(),
                System.Globalization.NumberStyles.Any, null, out Dummy);
        }

        /// <summary>
        /// Realiza a validação de uma string avaliando se seu formato é de uma data.
        /// </summary>
        /// <param name="obj">String a ser validada.</param>
        /// <returns>Valor booleano indicando se a string é uma data.</returns>
        public static bool IsDate(this string obj) {
            return ValidationHelper.IsDate(obj);
        }

        /// <summary>
        /// Realiza a validação de uma string avaliando se seu formato é de uma data.
        /// </summary>
        /// <param name="obj">String a ser validada.</param>
        /// <param name="formatProvider">Provedor que define o formato de data a ser utilizado.</param>
        /// <returns>Valor booleano indicando se a string é uma data.</returns>
        public static bool IsDate(this string obj, IFormatProvider formatProvider) {
            return ValidationHelper.IsDate(obj, formatProvider);
        }

        /// <summary>
        /// Verifica se uma string possui dígitos.
        /// </summary>
        /// <param name="obj">String a ser validada.</param>
        /// <returns>Valor booleano indicando se a string possui números.</returns>
        public static bool HasNumbers(this string obj) {
            return ValidationHelper.HasNumbers(obj);
        }

        /// <summary>
        /// Realiza validação de um número de CPF.
        /// </summary>
        /// <param name="obj">Número do CPF, informado apenas com seus dígitos.</param>
        /// <returns>Valro booleano indicando se o CPF é válido.</returns>
        public static bool IsCpf(this string obj) {
            return ValidationHelper.IsCpf(obj);
        }

        /// <summary>
        /// Realiza validação de um número de CNPJ.
        /// </summary>
        /// <param name="obj">Número do CNPJ, informado apenas com seus dígitos.</param>
        /// <returns>Valro booleano indicando se o CNPJ é válido.</returns>
        public static bool IsCnpj(this string obj) {
            return ValidationHelper.IsCnpj(obj);
        }

        /// <summary>
        /// Realiza validação de um número de CPF/CNPJ.
        /// </summary>
        /// <param name="obj">Número do CPF ou CNPJ, informado apenas com seus dígitos.</param>
        /// <returns>Valro booleano indicando se o CNPJ é válido.</returns>
        public static bool IsCpfOrCnpj(this string obj) {
            return ValidationHelper.IsCpfOrCnpj(obj);
        }

        /// <summary>
        /// Obtém o valor de um objeto Nullable ou <see cref="DBNull.Value"/> caso o objeto seja nulo.
        /// </summary>
        /// <typeparam name="T">Tipo do objeto a ser verificado.</typeparam>
        /// <param name="obj">Objeto a ser verificado.</param>
        /// <returns>Objeto contendo ou o valor do objeto ou <see cref="DBNull.Value"/>.</returns>
        public static object GetValueOrDbNull<T>(this Nullable<T> obj)
            where T : struct {
            object res = DBNull.Value;

            if (obj.HasValue) res = obj.Value;

            return res;
        }

        /// <summary>
        /// Obtém uma string ou <see cref="null"/> caso esta esteja vazia.
        /// </summary>
        /// <typeparam name="T">Tipo do objeto a ser verificado.</typeparam>
        /// <param name="str">String a ser verificado.</param>
        /// <returns>Objeto contendo ou o valor da string ou <see cref="null"/>.</returns>
        public static string GetValueOrNull(this string str) {
            string res = null;

            if (!str.IsNullOrEmpty()) res = str;

            return res;
        }

        /// <summary>
        /// Obtém o valor de uma string ou <see cref="DBNull.Value"/> caso a string seja vazia ou nula.
        /// </summary>
        /// <param name="str">String a ser verificada.</param>
        /// <returns>Objeto contendo ou o valor da string ou <see cref="DBNull.Value"/>.</returns>
        public static object GetValueOrDbNull(this string str) {
            object res = DBNull.Value;

            if (!String.IsNullOrEmpty(str)) res = str;

            return res;
        }

        /// <summary>
        /// Obtém o valor de uma string convertido para um tipo específico ou <see langword="null"/> caso a string esteja vazia.
        /// </summary>
        /// <typeparam name="T">Tipo para ser convertido.</typeparam>
        /// <param name="source">String a ser convertida.</param>
        /// <returns>Objeto contendo ou o valor da string ou <see langword="null"/>.</returns>
        public static Nullable<T> GetNullableObjectFor<T>(this string source)
            where T : struct, IConvertible {
            Nullable<T> res = null;

            if (!String.IsNullOrEmpty(source)) res = source.Parse<T>();

            return res;
        }

        /// <summary>
        /// Realiza conversão de uma string para o tipo <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Tipo a ser convertida a string.</typeparam>
        /// <param name="source">String fonte para conversão.</param>
        /// <returns>Objeto <typeparamref name="T"/>.</returns>
        public static T Parse<T>(this object source)
            where T : IConvertible {
            return (T)Convert.ChangeType(source, typeof(T));
        }

        /// <summary>
        /// Realiza conversão de um objeto para o tipo <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Tipo a ser convertido o objeto.</typeparam>
        /// <param name="source">Objeto fonte para conversão.</param>
        /// <param name="provider">Provedor de formatação.</param>
        /// <returns>Objeto <typeparamref name="T"/>.</returns>
        public static T Parse<T>(this object source, IFormatProvider provider)
            where T : IConvertible {
            return (T)Convert.ChangeType(source, typeof(T), provider);
        }
    }
}
