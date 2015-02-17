/*********************************************
Intentor.Utilities
**********************************************
Copyright © 2009-2012 André "Intentor" Martins
http://intentor.com.br/
*********************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Intentor.Utilities {
    /// <summary>
    /// Métodos para apoio no uso de JSON.
    /// </summary>
    /// <remarks>Código original: http://stackoverflow.com/questions/1212344/parse-json-in-c-sharp.</remarks>
    public static class JsonHelper {
        /// <summary>
        /// Realiza deserialização de uma string em JSON para um objeto.
        /// </summary>
        /// <typeparam name="T">Tipo do objeto.</typeparam>
        /// <param name="json">JSON em formato string.</param>
        /// <remarks>Marque a classe a ser deserializada com [DataContract] e as propriedades com [DataMember].</remarks>
        /// <returns>Objeto deserializado.</returns>
        public static T Deserialise<T>(string json) {
            T obj = Activator.CreateInstance<T>();

            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json))) {
                var serializer = new DataContractJsonSerializer(obj.GetType());
                obj = (T)serializer.ReadObject(ms);
            }

            return obj;
        }
    }
}
