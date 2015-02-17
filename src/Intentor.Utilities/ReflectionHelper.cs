/*********************************************
Intentor.Utilities
**********************************************
Copyright � 2009-2012 Andr� "Intentor" Martins
http://intentor.com.br/
*********************************************/

using System;
using System.Linq;
using System.Reflection;

namespace Intentor.Utilities {
    /// <summary>
    /// Apoio na execu��o de atividades de Reflection.
    /// </summary>
    public static class ReflectionHelper {
        /// <summary>
        /// Cria inst�ncia para um tipo a partir de seu nome.
        /// </summary>
        /// <param name="typeName">Nome do tipo a ser instanciado.</param>
        /// <param name="assemblyName">Nome do assembly.</param>
        /// <param name="args">Vetor e objetos que coincidem em n�mero, ordem e tipo os par�metros do construtor da classe a ser criada.</param>
        /// <returns>Objeto instanciado.</returns>
        public static object CreateInstance(string typeName, string assemblyName) {
            return CreateInstance(typeName, assemblyName, null);
        }

        /// <summary>
        /// Cria inst�ncia para um tipo a partir de seu nome.
        /// </summary>
        /// <param name="typeName">Nome do tipo a ser instanciado.</param>
        /// <param name="assemblyName">Nome do assembly.</param>
        /// <param name="args">Vetor e objetos que coincidem em n�mero, ordem e tipo os par�metros do construtor da classe a ser criada.</param>
        /// <returns>Objeto instanciado.</returns>
        public static object CreateInstance(string typeName, string assemblyName, params object[] args) {
            Type t = ReflectionHelper.GetTypeFromAssembly(typeName, assemblyName);

            if (args == null || args.Length == 0)
                return Activator.CreateInstance(t);
            else
                return Activator.CreateInstance(t, args);
        }

        /// <summary>
        /// Realiza c�pia de valores de propriedades entre dois objetos.
        /// </summary>
        /// <param name="source">Objeto fonte das propriedades.</param>
        /// <param name="target">Objeto para o qual os valores das propriedades ser�o copiados.</param>
        /// <remarks>Ambos os objetos devem possuir propriedades de mesmo nome.</remarks>
        public static void CopyPropertyValue(object source, object target) {
            //Obt�m objetos PropertyInfo que representam as propriedades de ambos os objetos.
            PropertyInfo[] sourcePropertiesInfo = source.GetType().GetProperties();
            PropertyInfo[] toPropertiesInfo = target.GetType().GetProperties();

            /* Realiza itera��o entre as propriedades do objeto fonte
             * para pesquisa no objeto destino da exist�ncia da propriedade
             * e posterior tranfer�ncia dos valores.
             */
            foreach (PropertyInfo pfs in sourcePropertiesInfo) {
                /* Pesquisa entre as propriedades de destino se h� uma
                 * propriedade de mesmo nome daquela manipulada pela itera��o.
                 */
                PropertyInfo pft = toPropertiesInfo.FirstOrDefault(p => p.Name.Equals(pfs.Name));
                //Caso exista, copia o valor da propriedade fonte para a destino.
                if (pft != null) pft.SetValue(target, pfs.GetValue(source, null), null);
            }
        }

        /// <summary>
        /// Obt�m o valor de uma propriedade de um objeto.
        /// </summary>
        /// <param name="obj">Objeto a ter o valor de uma propriedade obtida.</param>
        /// <param name="propertyName">Nome da propriedade a ser obtida.</param>
        /// <returns>Valor da propriedade ou <see langword="null"/> caso nenhum valor seja encontrado.</returns>
        public static object GetPropertyValue(object obj, string propertyName) {
            object propertyValue = null;

            PropertyInfo[] propertiesInfo = obj.GetType().GetProperties();
            PropertyInfo pi = propertiesInfo.FirstOrDefault(p => p.Name.Equals(propertyName));
            if (pi != null) propertyValue = pi.GetValue(obj, null);

            return propertyValue;
        }

        /// <summary>
        /// Alterada o valor de uma propriedade de um objeto.
        /// </summary>
        /// <param name="obj">Objeto a ter o valor de uma propriedade alterada.</param>
        /// <param name="propertyName">Nome da propriedade a ter seu valor alterado.</param>
        /// <param name="propertyValue">Valor a ser inserido na propriedade.</param>
        public static void SetPropertyValue(object obj, string propertyName, object propertyValue) {
            PropertyInfo[] propertiesInfo = obj.GetType().GetProperties();
            PropertyInfo pi = propertiesInfo.FirstOrDefault(p => p.Name.Equals(propertyName));
            if (pi != null) pi.SetValue(obj, propertyValue, null);
        }

        /// <summary>
        /// Obt�m um objeto <see cref="System.Type"/> de um assembly j� carregado ou, caso n�o esteja carregado, carrega-o para posterior obten��o do tipo.
        /// </summary>
        /// <param name="typeName">Nome do tipo.</param>
        /// <param name="assemblyName">Nome completo do assembly.</param>
        /// <remarks>Vers�o original em NHibernate-1.2.1.GA (parcialmente modificada).</remarks>
        /// <returns>Objeto <see cref="System.Type"/>.</returns>
        public static Type GetTypeFromAssembly(string typeName, string assemblyName) {
            return ReflectionHelper.GetTypeFromAssembly(new AssemblyQualifiedTypeName(typeName, assemblyName));
        }

        /// <summary>
        /// Obt�m um objeto <see cref="System.Type"/> de um assembly j� carregado ou, caso n�o esteja carregado, carrega-o para posterior obten��o do tipo.
        /// </summary>
        /// <param name="name">Objeto <see cref="AssemblyQualifiedTypeName" /> que representa o nome do assembly.</param>
        /// <remarks>Vers�o original em NHibernate-1.2.1.GA (parcialmente modificada).</remarks>
        /// <returns>Objeto <see cref="System.Type"/>.</returns>
        public static Type GetTypeFromAssembly(AssemblyQualifiedTypeName name) {
            return ReflectionHelper.GetTypeFromAssembly(name, true);
        }

        /// <summary>
        /// Obt�m um objeto <see cref="System.Type"/> de um assembly j� carregado ou, caso n�o esteja carregado, carrega-o para posterior obten��o do tipo.
        /// </summary>
        /// <param name="name">Objeto <see cref="AssemblyQualifiedTypeName" /> que representa o nome do assembly.</param>
        /// <param name="throwOnError">Identifica se uma exce��o deve ser gerado caso o tipo n�o seja encontrado.</param>
        /// <remarks>Vers�o original em NHibernate-1.2.1.GA (parcialmente modificada).</remarks>
        /// <returns>Objeto <see cref="System.Type"/>.</returns>
        public static Type GetTypeFromAssembly(AssemblyQualifiedTypeName name, bool throwOnError) {
            try {
                //Tenta obter o tipo de um assembly j� carregado.
                System.Type type = System.Type.GetType(name.ToString());

                //Caso seja obtido, retorna o tipo.
                if (type != null) {
                    return type;
                }

                //Verifica se h� um assembly especifiado.
                if (String.IsNullOrEmpty(name.Assembly)) {
                    string message = "N�o foi poss�vel carregar o tipo " + name + ". Poss�vel causa: o nome do assembly n�o foi especificado.";

                    if (throwOnError) throw new TypeLoadException(message);
                }

                //Carrega o assembly.
                Assembly assembly = Assembly.Load(name.Assembly);

                //Verifica se um assembly foi carregado.
                if (assembly == null) {
                    string message = "N�o foi poss�vel carregar o assembly " + name + ".";

                    if (throwOnError) throw new TypeLoadException(message);
                }

                //Obt�m o tipo solicitado.
                type = assembly.GetType(name.Type, throwOnError);

                //Verifica se o tipo foi obtido.
                if (type == null) {
                    string message = "N�o foi poss�vel carregar o tipo " + name + ".";

                    if (throwOnError) throw new TypeLoadException(message);
                }

                return type;
            } catch (Exception e) {
                throw e;
            }
        }

        /// <summary>
        /// Obt�m todos os tipos de um determinado namespace de um determinado assembly.
        /// </summary>
        /// <param name="assembly">Assembly do qual os dados ser�o obtidos.</param>
        /// <param name="namespaceToSearch">Namespace do qual os dados ser�o obtidos.</param>
        /// <returns>Tipos encontrados.</returns>
        public static Type[] GetTypesInNamespace(Assembly assembly, string namespaceToSearch) {
            return assembly.GetTypes().Where(t => String.Equals(t.Namespace, namespaceToSearch, StringComparison.Ordinal)).ToArray();
        }
    }
}
