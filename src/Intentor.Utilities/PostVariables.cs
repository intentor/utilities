using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intentor.Utilities {
    /// <summary>
    /// Post variables collection.
    /// </summary>
    public class PostVariables {
        /// <summary>Post variables.</summary>
        private Dictionary<string, string> _variables;

        /// <summary>
        /// Construtor.
        /// </summary>
        public PostVariables()
            : this(String.Empty, true) {
        }

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="variables">Post variables on the format key=value&key=value.</param>
        public PostVariables(string variables)
            : this(variables, true) {
        }

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="variables">Post variables on the format key=value&key=value.</param>
        /// <param name="escape">Indicates if the values should be escaped by default.</param>
        public PostVariables(string variables, bool escape) {
            _variables = new Dictionary<string, string>();
            this.Escape = escape;

            if (!variables.IsNullOrEmpty()) {
                var dic = variables.Split('&');

                foreach (var d in dic) {
                    var valuePair = d.Split('=');
                    this.Add(valuePair[0], valuePair[1]);
                }
            }
        }

        /// <summary>
        /// Post variables.
        /// </summary>
        public Dictionary<string, string> Variables {
            get { return _variables; }
        }

        /// <summary>
        /// Indicates if all the values should be URL escaped.
        /// </summary>
        public bool Escape { get; set; }

        /// <summary>
        /// Adds the specified key and value to the dictionary.
        /// </summary>
        /// <param name="key">Kay name.</param>
        /// <param name="value">Key value.</param>
        public new void Add(string key, object value) {
            this.Add(key, value, null);
        }

        /// <summary>
        /// Adds the specified key and value to the dictionary.
        /// </summary>
        /// <param name="key">Kay name.</param>
        /// <param name="value">Key value.</param>
        /// <param name="escape">Indicates if the value needs to be URL escaped.</param>
        public void Add(string key, object value, bool? escape) {
            var strVal = value.ToString();

            if (escape.HasValue) {
                if (escape.Value) {
                    strVal = Uri.EscapeUriString(strVal);
                }
            } else if (this.Escape) {
                strVal = Uri.EscapeUriString(strVal);
            }

            _variables.Add(key, strVal);
        }

        /// <summary>
        /// Gets the post data in a single string.
        /// </summary>
        /// <returns>Post data.</returns>
        public override string ToString() {
            var sb = new StringBuilder();

            foreach (var pair in _variables) {
                sb.Append(pair.Key).Append("=").Append(pair.Value).Append('&');
            }

            var res = sb.ToString().TrimEnd('&');

            return res;
        }
    }
}
