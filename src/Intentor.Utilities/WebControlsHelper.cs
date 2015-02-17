/*********************************************
Intentor.Utilities
**********************************************
Copyright � 2009-2012 Andr� "Intentor" Martins
http://intentor.com.br/
*********************************************/

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Intentor.Utilities {
    /// <summary>
    /// M�todos para apoio na utiliza��o de webcontrols.
    /// </summary>
    public static class WebControlsHelper {
        /// <summary>
        /// Coloca controles de uma cole��o em estado de somente leitura.
        /// </summary>
        /// <param name="collection">Cole��o de controles a ser avaliada.</param>
        /// <remarks>
        /// Controles analisados:
        /// - TextBox;
        /// - ListControl;
        /// - CheckBox;
        /// - RadioButton;
        /// - Button;
        /// - LinkButton.
        /// </remarks>
        public static void PutControlsInReadOnlyState(ControlCollection collection) {
            foreach (Control c in collection) {
                if (c.Controls.Count > 0) {
                    PutControlsInReadOnlyState(c.Controls);
                } else {
                    if (c is TextBox) {
                        ((TextBox)c).ReadOnly = true;
                    } else if (c is ListControl ||
                          c is CheckBox ||
                          c is RadioButton ||
                          c is Button ||
                          c is LinkButton) {
                        ((WebControl)c).Enabled = false;
                    }
                }
            }
        }

        /// <summary>
        /// Retira controles de uma cole��o do estado de somente leitura.
        /// </summary>
        /// <param name="collection">Cole��o de controles a ser avaliada.</param>
        /// <remarks>
        /// Controles analisados:
        /// - TextBox;
        /// - ListControl;
        /// - CheckBox;
        /// - RadioButton;
        /// - Button;
        /// - LinkButton.
        /// </remarks>
        public static void ChangeControlsInReadOnlyState(ControlCollection collection) {
            foreach (Control c in collection) {
                if (c.Controls.Count > 0) {
                    ChangeControlsInReadOnlyState(c.Controls);
                } else {
                    if (c is TextBox) {
                        ((TextBox)c).ReadOnly = false;
                    } else if (c is ListControl ||
                          c is CheckBox ||
                          c is RadioButton ||
                          c is Button ||
                          c is LinkButton) {
                        ((WebControl)c).Enabled = true;
                    }
                }
            }
        }

        /// <summary>
        /// Define um ValidationGroup para todos os validators presentes em um controle.
        /// </summary>
        /// <param name="collection">Cole��o de controles a ser avaliada.</param>
        /// <param name="validationGroup">ValidationGroup a ser definido.</param>
        public static void SetValidationGroup(ControlCollection collection, string validationGroup) {
            foreach (Control c in collection) {
                if (c.Controls.Count > 0) SetValidationGroup(c.Controls, validationGroup);
                else if (c is BaseValidator) ((BaseValidator)c).ValidationGroup = validationGroup;
            }
        }

        /// <summary>
        /// Move itens selecionados de uma <see cref="ListBox"/> para outra.
        /// </summary>
        /// <param name="isFromSource">Indica se a fonte da sele��o � <paramref name="from"/>. Caso contr�rio, ser� <paramref name="to"/>.</param>
        /// <param name="fromBase"></param>
        /// <param name="toBase"></param>
        public static void MoveListItems(bool isSourceFrom, ListBox from, ListBox to) {
            ListBox lstFrom, lstTo;

            lstFrom = (isSourceFrom ? from : to);
            lstTo = (isSourceFrom ? to : from);

            for (int i = 0; i < lstFrom.Items.Count; i++) {
                var item = lstFrom.Items[i];
                if (item.Selected) {
                    lstFrom.Items.Remove(item);
                    lstTo.Items.Add(item);
                    i--;
                }
            }
        }

        #region EnableValidators

        /// <summary>
        /// Habilita ou desabilita os validators presentes em uma  cole��o de controles.
        /// </summary>
        /// <param name="collection">Cole��o de controles a ser avaliada.</param>
        public static void EnableValidators(ControlCollection collection) {
            EnableValidators(collection, true, String.Empty, false);
        }

        /// <summary>
        /// Habilita ou desabilita os validators presentes em uma cole��o de controles.
        /// </summary>
        /// <param name="collection">Cole��o de controles a ser avaliada.</param>
        /// <param name="enable">Indica se se deve habilitar ou desabilitar os controles.</param>
        public static void EnableValidators(ControlCollection collection
            , bool enable) {
            EnableValidators(collection, enable, String.Empty, false);
        }

        /// <summary>
        /// Habilita ou desabilita os validators presentes em uma cole��o de controles.
        /// </summary>
        /// <param name="collection">Cole��o de controles a ser avaliada.</param>
        /// <param name="enable">Indica se se deve habilitar ou desabilitar os controles.</param>
        /// <param name="validationGroup">ValidationGroup a ser definido.</param>
        public static void EnableValidators(ControlCollection collection
            , bool enable
            , string validationGroup) {
            EnableValidators(collection, enable, validationGroup, false);
        }

        /// <summary>
        /// Habilita ou desabilita os validators presentes em uma cole��o de controles.
        /// </summary>
        /// <param name="collection">Cole��o de controles a ser avaliada.</param>
        /// <param name="enable">Indica se se deve habilitar ou desabilitar os controles.</param>
        /// <param name="validationGroup">ValidationGroup a ser definido.</param>
        /// <param name="onlyOnEmptyValidationGroups"> Somente aplica o ValidationGroup nos validators que n�o tiverem validators definidos.</param>
        public static void EnableValidators(ControlCollection collection
            , bool enable
            , string validationGroup
            , bool onlyOnEmptyValidationGroups) {
            foreach (Control c in collection) {
                if (c.Controls.Count > 0) EnableValidators(c.Controls, enable, validationGroup);
                else if (c is BaseValidator) {
                    BaseValidator v = (BaseValidator)c;

                    if (onlyOnEmptyValidationGroups && String.IsNullOrEmpty(v.ValidationGroup))
                        v.Enabled = enable;
                    else if (!onlyOnEmptyValidationGroups) {
                        if (String.IsNullOrEmpty(validationGroup) ||
                            v.ValidationGroup == validationGroup)
                            v.Enabled = enable;
                    }
                }
            }
        }

        #endregion
    }
}
