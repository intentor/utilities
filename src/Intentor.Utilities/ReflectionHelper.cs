/*********************************************
Intentor.Utilities
**********************************************
Copyright © 2009-2012 André "Intentor" Martins
http://intentor.com.br/
*********************************************/

using System;
using System.Linq;
using System.Reflection;

namespace Intentor.Utilities {
    /// <summary>
    /// Apoio na execução de atividades de Reflection.
    /// </summary>
    public static class ReflectionHelper {
        /// <summary>
        /// Cria instância para um tipo a partir de seu nome.
        /// </summary>
        /// <param name="typeName">Nome do tipo a ser instanciado.</param>
        /// <param name="assemblyName">Nome do assembly.</param>
        /// <param name="args">Vetor e objetos que coincidem em número, ordem e tipo os parâmetros do construtor da classe a ser criada.</param>
        /// <returns>Objeto instanciado.</returns>
        public static object CreateInstance(string typeName, string assemblyName) {
            return CreateInstance(typeName, assemblyName, null);
        }

        /// <summary>
        /// Cria instância para um tipo a partir de seu nome.
        /// </summary>
        /// <param name="typeName">Nome do tipo a ser instanciado.</param>
        /// <param name="assemblyName">Nome do assembly.</param>
        /// <param name="args">Vetor e objetos que coincidem em número, ordem e tipo os parâmetros do construtor da classe a ser criada.</param>
        /// <returns>Objeto instanciado.</returns>
        public static object CreateInstance(string typeName, string assemblyName, params object[] args) {
            Type t = ReflectionHelper.GetTypeFromAssembly(typeName, assemblyName);

            if (args == null || args.Length == 0)
                return Activator.CreateInstance(t);
            else
                return Activator.CreateInstance(t, args);
        }

        /// <summary>
        /// Realiza cópia de valores de propriedades entre dois objetos.
        /// </summary>
        /// <param name="source">Objeto fonte das propriedades.</param>
        /// <param name="target">Objeto para o qual os valores das propriedades serão copiados.</param>
        /// <remarks>Ambos os objetos devem possuir propriedades de mesmo nome.</remarks>
        public static void CopyPropertyValue(object source, object target) {
            //Obtém objetos PropertyInfo que representam as propriedades de ambos os objetos.
            PropertyInfo[] sourcePropertiesInfo = source.GetType().GetProperties();
            PropertyInfo[] toPropertiesInfo = target.GetType().GetProperties();

            /* Realiza iteração entre as propriedades do objeto fonte
             * para pesquisa no objeto destino da existência da propriedade
             * e posterior tranferência dos valores.
             */
            foreach (PropertyInfo pfs in sourcePropertiesInfo) {
                /* Pesquisa entre as propriedades de destino se há uma
                 * propriedade de mesmo nome daquela manipulada pela iteração.
                 */
                PropertyInfo pft = toPropertiesInfo.FirstOrDefault(p => p.Name.Equals(pfs.Name));
                //Caso exista, copia o valor da propriedade fonte para a destino.
                if (pft != null) pft.SetValue(target, pfs.GetValue(source, null), null);
            }
        }

        /// <summary>
        /// Obtém o valor de uma propriedade de um objeto.
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
        /// Obtém um objeto <see cref="System.Type"/> de um assembly já carregado ou, caso não esteja carregado, carrega-o para posterior obtenção do tipo.
        /// </summary>
        /// <param name="typeName">Nome do tipo.</param>
        /// <param name="assemblyName">Nome completo do assembly.</param>
        /// <remarks>Versão original em NHibernate-1.2.1.GA (parcialmente modificada).</remarks>
        /// <returns>Objeto <see cref="System.Type"/>.</returns>
        public static Type GetTypeFromAssembly(string typeName, string assemblyName) {
            return ReflectionHelper.GetTypeFromAssembly(new AssemblyQualifiedTypeName(typeName, assemblyName));
        }

        /// <summary>
        /// Obtém um objeto <see cref="System.Type"/> de um assembly já carregado ou, caso não esteja carregado, carrega-o para posterior obtenção do tipo.
        /// </summary>
        /// <param name="name">Objeto <see cref="AssemblyQualifiedTypeName" /> que representa o nome do assembly.</param>
        /// <remarks>Versão original em NHibernate-1.2.1.GA (parcialmente modificada).</remarks>
        /// <returns>Objeto <see cref="System.Type"/>.</returns>
        public static Type GetTypeFromAssembly(AssemblyQualifiedTypeName name) {
            return ReflectionHelper.GetTypeFromAssembly(name, true);
        }

        /// <summary>
        /// Obtém um objeto <see cref="System.Type"/> de um assembly já carregado ou, caso não esteja carregado, carrega-o para posterior obtenção do tipo.
        /// </summary>
        /// <param name="name">Objeto <see cref="AssemblyQualifiedTypeName" /> que representa o nome do assembly.</param>
        /// <param name="throwOnError">Identifica se uma exceção deve ser gerado caso o tipo não seja encontrado.</param>
        /// <remarks>Versão original em NHibernate-1.2.1.GA (parcialmente modificada).</remarks>
        /// <returns>Objeto <see cref="System.Type"/>.</returns>
        public static Type GetTypeFromAssembly(AssemblyQualifiedTypeName name, bool throwOnError) {
            try {
                //Tenta obter o tipo de um assembly já carregado.
                System.Type type = System.Type.GetType(name.ToString());

                //Caso seja obtido, retorna o tipo.
                if (type != null) {
                    return type;
                }

                //Verifica se há um assembly especifiado.
                if (String.IsNullOrEmpty(name.Assembly)) {
                    string message = "Não foi possível carregar o tipo " + name + ". Possível causa: o nome do assembly não foi especificado.";

                    if (throwOnError) throw new TypeLoadException(message);
                }

                //Carrega o assembly.
                Assembly assembly = Assembly.Load(name.Assembly);

                //Verifica se um assembly foi carregado.
                if (assembly == null) {
                    string message = "Não foi possível carregar o assembly " + name + ".";

                    if (throwOnError) throw new TypeLoadException(message);
                }

                //Obtém o tipo solicitado.
                type = assembly.GetType(name.Type, throwOnError);

                //Verifica se o tipo foi obtido.
                if (type == null) {
                    string message = "Não foi possível carregar o tipo " + name + ".";

                    if (throwOnError) throw new TypeLoadException(message);
                }

                return type;
            } catch (Exception e) {
                throw e;
            }
        }

        /// <summary>
        /// Obtém todos os tipos de um determinado namespace de um determinado assembly.
        /// </summary>
        /// <param name="assembly">Assembly do qual os dados serão obtidos.</param>
        /// <param name="namespaceToSearch">Namespace do qual os dados serão obtidos.</param>
        /// <returns>Tipos encontrados.</returns>
        public static Type[] GetTypesInNamespace(Assembly assembly, string namespaceToSearch) {
            return assembly.GetTypes().Where(t => String.Equals(t.Namespace, namespaceToSearch, StringComparison.Ordinal)).ToArray();
        }
    }
}
