/*********************************************
Intentor.Utilities
**********************************************
Copyright � 2009-2012 Andr� "Intentor" Martins
http://intentor.com.br/
*********************************************/

using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Intentor.Utilities {
    /// <summary>
    /// M�todos diversos para formata��o de dados.
    /// </summary>
    public static class FormatHelper {
        #region Documentos/padr�es

        /// <summary>
        /// Realiza formata��o de CPF.
        /// </summary>
        /// <param name="cpf">String contendo o CPF a ser formatado.</param>
        /// <remarks>O CPF informado deve possuir 11 caracteres.</remarks>
        /// <returns>CPF formatado.</returns>
        public static string Cpf(string cpf) {
            cpf = FormatHelper.GetNumbers(cpf);

            if (cpf != "") {
                if (cpf.Length < 11) {
                    cpf = Convert.ToInt64(cpf).ToString("00000000000");
                }

                cpf = cpf.Substring(0, 3) + "." + cpf.Substring(3, 3) + "." + cpf.Substring(6, 3) + "-" + cpf.Substring(9, 2);
            }

            return cpf;
        }

        /// <summary>
        /// Realiza formata��o de CNPJ.
        /// </summary>
        /// <param name="cnpj">String contendo o CNPJ a ser formatado.</param>
        /// <remarks>O CNPJ informado deve possuir 14 caracteres.</remarks>
        /// <returns>CNPJ formatado.</returns>
        public static string Cnpj(string cnpj) {
            cnpj = FormatHelper.GetNumbers(cnpj);

            if (cnpj != "") {
                if (cnpj.Length < 14) {
                    cnpj = Convert.ToInt64(cnpj).ToString("00000000000000");
                }

                cnpj = cnpj.Substring(0, 2) + "." + cnpj.Substring(2, 3) + "." + cnpj.Substring(5, 3) + "/" + cnpj.Substring(8, 4) + "-" + cnpj.Substring(12, 2);
            }

            return cnpj;
        }

        /// <summary>
        /// Realiza formata��o de CPF/CNPJ.
        /// </summary>
        /// <param name="cpfCnpj">String contendo o CPF/CNPJ a ser formatado.</param>
        /// <remarks>O CPF informado deve possuir 11 caracteres e o CNPJ deve possuir 14 caracteres.
        /// </remarks><returns>CPF/CNPJ formatado.</returns>
        public static string CpfCnpj(string cpfCnpj) {
            if (cpfCnpj.Length <= 11) {
                cpfCnpj = FormatHelper.Cpf(cpfCnpj);
            } else {
                cpfCnpj = FormatHelper.Cnpj(cpfCnpj);
            }

            return cpfCnpj;
        }

        /// <summary>
        /// Realiza formata��o de CEP.
        /// </summary>
        /// <param name="cep">String contendo o CEP a ser formatado.</param>
        /// <returns>CEP formatado.</returns>
        public static string Cep(string cep) {
            cep = FormatHelper.GetNumbers(cep);

            if (cep.Length < 8) {
                cep = Convert.ToInt64(cep).ToString("00000000");
            }

            cep = cep.Substring(0, 5) + "-" + cep.Substring(5, 3);

            return cep;
        }

        /// <summary>
        /// Realiza formata��o do N�mero do Telefone.
        /// </summary>
        /// <param name="tel">String contendo o N�mero do Telefone a ser formatado.</param>
        /// <remarks>O n�mero de telefone deve possuir 10 n�meros (DDD + telefone) e ser� retornado no formato (##) ####-####.</remarks>
        /// <returns>N�mero do Telefone formatado.</returns>
        public static string Telephone(string tel) {
            tel = FormatHelper.GetNumbers(tel);

            if (tel != "") {
                if (tel.Length < 10) {
                    tel = Convert.ToInt64(tel).ToString("0000000000");
                }

                tel = "(" + tel.Substring(0, 2) + ") " + tel.Substring(2, 4) + "-" + tel.Substring(6, 4);
            }

            return tel;
        }

        #endregion

        #region Apoio

        /// <summary>
        /// Troca o casing de todas as propriedades de um objeto.
        /// </summary>
        /// <param name="obj">Objeto a ser analisado.</param>
        /// <param name="toLower">Indica��o de coloca��o em mai�scula ou min�scula.</param>
        public static void ChangeCasing(object obj, bool toLower) {
            Type objType = obj.GetType();

            PropertyInfo[] properties = objType.GetProperties();

            foreach (PropertyInfo property in properties) {
                if (property.PropertyType.Name.Equals("String")) {
                    string value = property.GetValue(obj, null).ToString();

                    if (toLower) value = value.ToLower();
                    else value.ToUpper();

                    property.SetValue(obj, value, null);
                }
            }
        }

        /// <summary>
        /// Obt�m apenas os n�meros que comp�em uma string.
        /// </summary>
        /// <param name="s">String contendo os n�meros a serem extra�dos.</param>
        /// <returns>N�meros que comp�em uma string.</returns>
        public static string GetNumbers(string s) {
            return Regex.Replace(s, @"\D", "");
        }

        /// <summary>
        /// Substitui caracteres com acento por sua vers�o sem sinal em uma string.
        /// </summary>
        /// <param name="s">String a ser avaliada.</param>
        /// <returns>String formatada.</returns>
        public static string ReplaceAccents(string s) {
            string normalizedString = s.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < normalizedString.Length; i++) {
                char c = normalizedString[i];
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    stringBuilder.Append(c);
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Substitui caracteres especiais e com acento por sua vers�o similar em uma string.
        /// </summary>
        /// <param name="s">String a ser avaliada.</param>
        /// <returns>String formatada.</returns>
        public static string ReplaceSpecialChars(string s) {
            string normalized = s.Normalize(NormalizationForm.FormD);

            StringBuilder resultBuilder = new StringBuilder();
            foreach (var character in normalized) {
                UnicodeCategory category = CharUnicodeInfo.GetUnicodeCategory(character);
                if (category == UnicodeCategory.LowercaseLetter
                    || category == UnicodeCategory.UppercaseLetter
                    || category == UnicodeCategory.DecimalDigitNumber
                    || category == UnicodeCategory.SpaceSeparator)
                    resultBuilder.Append(character);
            }

            return resultBuilder.ToString();
        }

        /// <summary>
        /// Formata uma string para uso em URL.
        /// </summary>
        /// <param name="s">String a ser avaliada.</param>
        /// <returns>String formatada.</returns>
        public static string FormatForUrl(string s) {
            return Regex.Replace(ReplaceSpecialChars(s), @"\s+", "-").ToLower();
        }

        /// <summary>
        /// Preenche uma string com zeros (0) � esquerda at� que o tamanho m�ximo definido seja atingido.
        /// </summary>
        /// <param name="s">String base.</param>
        /// <param name="size">Tamanho m�ximo da string.</param>
        /// <returns>String com zeros � esquerda caso esta n�o possua o tamanho m�ximo definido.</returns>
        public static string FillStringWithZeros(string s, int size) {
            s = s.Trim();

            /* Verifica se o tamanho da string � maior que o tamanho total 
             * estipulado, o que causa a truncagem do conte�do.
             */
            if (s.Length > size) {
                s = s.Substring(0, size);
            } else {
                //Acresce zerios (0) � esquerda da string.
                int len = s.Length;

                if (size > len) {
                    for (int i = 0; i < (size - len); i++) {
                        s = "0" + s;
                    }
                }
            }
            return s;
        }

        /// <summary>
        /// Preenche uma string com espa�os vazios � direita at� que o tamanho m�ximo definido seja atingido.
        /// </summary>
        /// <param name="s">String base.</param>
        /// <param name="size">Tamanho m�ximo da string.</param>
        /// <returns>String com espa�os � direita.</returns>
        public static string FillStringWithSpaces(string s, int size) {
            return FormatHelper.FillStringWithSpaces(s, size, true);
        }

        /// <summary>
        /// Preenche uma string com espa�os vazios at� que o tamanho m�ximo definido seja atingido.
        /// </summary>
        /// <param name="s">String base.</param>
        /// <param name="size">Tamanho m�ximo da string. </param>
        /// <param name="insertOnLeft">Identifica se os espa�os devem ser inseridos � direita da string. Sendo <see langword="false"/>, os espa�os ser�o inseridos � esquerda.</param>
        /// <returns>String com espa�os.</returns>
        public static string FillStringWithSpaces(string s, int size, bool insertOnLeft) {
            s = s.Trim();

            /* Verifica se o tamanho da string � maior que o tamanho total 
             * estipulado, o que causa a truncagem do conte�do.
             */
            if (s.Length > size) {
                s = s.Substring(0, size);
            } else {
                //Acresce espa�os na string, coforme posi��o definida pelo par�metro "insertOnLeft".
                int len = s.Length;

                if (size > len) {
                    for (int i = 0; i < (size - len); i++) {
                        if (insertOnLeft) {
                            s += " ";
                        } else {
                            s = " " + s;
                        }
                    }
                }
            }

            return s;
        }

        #endregion
    }
}
