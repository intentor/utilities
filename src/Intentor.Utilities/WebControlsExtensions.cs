/*********************************************
Intentor.Utilities
**********************************************
Copyright © 2009-2012 André "Intentor" Martins
http://intentor.com.br/
*********************************************/

using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Intentor.Utilities {
    /// <summary>
    /// Métodos para apoio na utilização de webcontrols utilizados como extensão.
    /// </summary>
    public static class WebControlsExtensions {
        /// <summary>
        /// Preenche um ListControl com dados oriundos de um <see cref="DataTable"/>, inserindo um item vazio.
        /// </summary>
        /// <param name="obj">Controle a ser preenchido.</param>
        /// <param name="dt">Objeto com os dados a serem inseridos.</param>
        /// <param name="textField">Nome do campo de <paramref name="dt"/> que contém o texto dos itens.</param>
        /// <param name="valueField">Nome do campo de <paramref name="dt"/> que contém o valor dos itens.</param>
        /// <remarks>O conteúdo do controle sempre é limpo antes da inserção dos novos itens.</remarks>
        public static void Fill(this ListControl obj,
            DataTable dt,
            string textField,
            string valueField) {
            obj.Fill(dt, textField, valueField, true);
        }

        /// <summary>
        /// Preenche um ListControl com dados oriundos de um <see cref="DataTable"/>.
        /// </summary>
        /// <param name="obj">Controle a ser preenchido.</param>
        /// <param name="dt">Objeto com os dados a serem inseridos.</param>
        /// <param name="textField">Nome do campo de <paramref name="dt"/> que contém o texto dos itens.</param>
        /// <param name="valueField">Nome do campo de <paramref name="dt"/> que contém o valor dos itens.</param>
        /// <param name="insertEmptyItem">Insere um item vazio com texto "---" e valor nulo.</param>
        /// <remarks>O conteúdo do controle sempre é limpo antes da inserção dos novos itens.</remarks>
        public static void Fill(this ListControl obj,
            DataTable dt,
            string textField,
            string valueField,
            bool insertEmptyItem) {
            string emptyItemText = (insertEmptyItem ? "---" : String.Empty);
            obj.Fill(dt, textField, valueField, emptyItemText, String.Empty);
        }

        /// <summary>
        /// Preenche um ListControl com dados oriundos de um <see cref="DataTable"/>.
        /// </summary>
        /// <param name="obj">Controle a ser preenchido.</param>
        /// <param name="dt">Objeto com os dados a serem inseridos.</param>
        /// <param name="textField">Nome do campo de <paramref name="dt"/> que contém o texto dos itens.</param>
        /// <param name="valueField">Nome do campo de <paramref name="dt"/> que contém o valor dos itens.</param>
        /// <param name="emptyItemText">Texto para o campo vazio, o qual é sempre incluído antes dos items de <paramref name="list"/>.</param>
        /// <param name="emptyItemValue">Valor do campo vazio.</param>
        /// <remarks>O conteúdo do controle sempre é limpo antes da inserção dos novos itens.</remarks>
        public static void Fill(this ListControl obj,
            DataTable dt,
            string textField,
            string valueField,
            string emptyItemText,
            string emptyItemValue) {
            obj.Items.Clear();

            if (!emptyItemText.IsNullOrEmpty()) {
                obj.Items.Add(new ListItem(emptyItemText, emptyItemValue));
            }

            foreach (DataRow drw in dt.Rows) {
                ListItem li = new ListItem(drw[textField].ToString(),
                    drw[valueField].ToString());

                obj.Items.Add(li);
            }
        }

        /// <summary>
        /// Preenche um ListControl com dados oriundos de uma lista de objetos do tipo <typeparamref name="T"/>, inserindo um item vazio.
        /// </summary>
        /// <typeparam name="T">Tipo do objeto da lista.</typeparam>
        /// <param name="obj">Controle a ser preenchido.</param>
        /// <param name="list">Lista de objetos do tipo <typeparamref name="T"/>.</param>
        /// <param name="textProperty">Nome da propriedade dos objetos em <paramref name="list"/> que contém contém o texto dos itens.</param>
        /// <param name="valueProperty">Nome da propriedade dos objetos em <paramref name="list"/> que contém o valor dos itens.</param>
        /// <remarks> O conteúdo do controle sempre é limpo antes da inserção dos novos itens.</remarks>
        public static void Fill<T>(this ListControl obj,
            List<T> list,
            string textProperty,
            string valueProperty) {
            obj.Fill<T>(list, textProperty, valueProperty, true);
        }

        /// <summary>
        /// Preenche um ListControl com dados oriundos de uma lista de objetos do tipo <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Tipo do objeto da lista.</typeparam>
        /// <param name="obj">Controle a ser preenchido.</param>
        /// <param name="list">Lista de objetos do tipo <typeparamref name="T"/>.</param>
        /// <param name="textProperty">Nome da propriedade dos objetos em <paramref name="list"/> que contém contém o texto dos itens.</param>
        /// <param name="valueProperty">Nome da propriedade dos objetos em <paramref name="list"/> que contém o valor dos itens.</param>
        /// <param name="insertEmptyItem">Insere um item vazio com texto "---" e valor nulo.</param>
        /// <remarks> O conteúdo do controle sempre é limpo antes da inserção dos novos itens.</remarks>
        public static void Fill<T>(this ListControl obj,
            List<T> list,
            string textProperty,
            string valueProperty,
            bool insertEmptyItem) {
            string emptyItemText = (insertEmptyItem ? "---" : String.Empty);
            obj.Fill<T>(list, textProperty, valueProperty, emptyItemText, String.Empty);
        }

        /// <summary>
        /// Preenche um ListControl com dados oriundos de uma lista de objetos do tipo <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Tipo do objeto da lista.</typeparam>
        /// <param name="obj">Controle a ser preenchido.</param>
        /// <param name="list">Lista de objetos do tipo <typeparamref name="T"/>.</param>
        /// <param name="textProperty">Nome da propriedade dos objetos em <paramref name="list"/> que contém contém o texto dos itens.</param>
        /// <param name="valueProperty">Nome da propriedade dos objetos em <paramref name="list"/> que contém o valor dos itens.</param>
        /// <param name="emptyItemText">Texto para o campo vazio, o qual é sempre incluído antes dos items de <paramref name="list"/>.</param>
        /// <param name="emptyItemValue">Valor do campo vazio.</param>
        /// <remarks> O conteúdo do controle sempre é limpo antes da inserção dos novos itens.</remarks>
        public static void Fill<T>(this ListControl obj,
            List<T> list,
            string textProperty,
            string valueProperty,
            string emptyItemText,
            string emptyItemValue) {
            Type objType = typeof(T);
            obj.Items.Clear();

            if (!emptyItemText.IsNullOrEmpty()) {
                obj.Items.Add(new ListItem(emptyItemText, emptyItemValue));
            }

            foreach (T item in list) {
                ListItem li = new ListItem(
                    objType.GetProperty(textProperty).GetValue(item, null).ToString(),
                    objType.GetProperty(valueProperty).GetValue(item, null).ToString());

                obj.Items.Add(li);
            }
        }

        /// <summary>
        /// Seleciona um item de um ListControl com base com base em seu texto.
        /// </summary>
        /// <param name="obj">Controle a ter o item selecionado.</param>
        /// <param name="text">Texto a ser pesquisado.</param>
        public static void SelectItemByText(this ListControl obj, string text) {
            for (int i = 0; i < obj.Items.Count; i++) {
                if (obj.Items[i].Text == text) {
                    obj.SelectedIndex = i;
                    break;
                }
            }
        }

        /// <summary>
        /// Seleciona um item de um ListControl com base com base em seu valor.
        /// </summary>
        /// <param name="obj">Controle a ter o item selecionado.</param>
        /// <param name="value">Valor a ser pesquisado.</param>
        public static void SelectItemByValue(this ListControl obj, string value) {
            for (int i = 0; i < obj.Items.Count; i++) {
                if (obj.Items[i].Value == value) {
                    obj.SelectedIndex = i;
                    break;
                }
            }
        }

        /// <summary>
        /// Insere um item indicando valor vazio (text = "---" e value = "").
        /// </summary>
        /// <param name="obj">Controle a ser preenchido.</param>
        public static void InsertEmptyItem(this ListControl obj) {
            obj.Items.Add(new ListItem("---", ""));
        }

        /// <summary>
        /// Coloca controles de uma coleção em estado de somente leitura.
        /// </summary>
        /// <param name="control">Controle a ter seus controles avaliados.</param>
        /// <remarks>
        /// Controles analisados:
        /// - TextBox;
        /// - ListControl;
        /// - CheckBox;
        /// - RadioButton;
        /// - Button;
        /// - LinkButton.
        /// </remarks>
        public static void PutControlsInReadOnlyState(this Control control) {
            WebControlsHelper.PutControlsInReadOnlyState(control.Controls);
        }

        /// <summary>
        /// Retira controles de uma coleção do estado de somente leitura.
        /// </summary>
        /// <param name="control">Controle a ter seus controles avaliados.</param>
        /// <remarks>
        /// Controles analisados:
        /// - TextBox;
        /// - ListControl;
        /// - CheckBox;
        /// - RadioButton;
        /// - Button;
        /// - LinkButton.
        /// </remarks>
        public static void ChangeControlsInReadOnlyState(this Control control) {
            WebControlsHelper.ChangeControlsInReadOnlyState(control.Controls);
        }

        /// <summary>
        /// Define um ValidationGroup para todos os validators presentes em um UserControl.
        /// </summary>
        /// <param name="control">Controle a ser avaliado.</param>
        /// <param name="validationGroup">ValidationGroup a ser definido.</param>
        public static void SetValidationGroup(this UserControl uc, string validationGroup) {
            if (uc.Controls.Count > 0) WebControlsHelper.SetValidationGroup(uc.Controls, validationGroup);
        }

        #region EnableValidators

        /// <summary>
        /// Habilita ou desabilita os validators presentes em uma coleção de controles.
        /// </summary>
        /// <param name="control">Controle a ser avaliado.</param>
        public static void EnableValidators(this UserControl control) {
            WebControlsHelper.EnableValidators(control.Controls, true, String.Empty);
        }

        /// <summary>
        /// Habilita ou desabilita os validators presentes em uma coleção de controles.
        /// </summary>
        /// <param name="control">Controle a ser avaliado.</param>
        public static void EnableValidators(this Page control) {
            WebControlsHelper.EnableValidators(control.Controls, true, String.Empty);
        }

        /// <summary>
        /// Habilita ou desabilita os validators presentes em uma coleção de controles.
        /// </summary>
        /// <param name="control">Controle a ser avaliado.</param>
        /// <param name="enable">Indica se se deve habilitar ou desabilitar os controles.</param>
        public static void EnableValidators(this UserControl control
            , bool enable) {
            WebControlsHelper.EnableValidators(control.Controls, enable, String.Empty);
        }

        /// <summary>
        /// Habilita ou desabilita os validators presentes em uma coleção de controles.
        /// </summary>
        /// <param name="control">Controle a ser avaliado.</param>
        /// <param name="enable">Indica se se deve habilitar ou desabilitar os controles.</param>
        public static void EnableValidators(this Page control
            , bool enable) {
            WebControlsHelper.EnableValidators(control.Controls, enable, String.Empty);
        }

        /// <summary>
        /// Habilita ou desabilita os validators presentes em uma coleção de controles.
        /// </summary>
        /// <param name="control">Controle a ser avaliado.</param>
        /// <param name="enable">Indica se se deve habilitar ou desabilitar os controles.</param>
        /// <param name="validationGroup">ValidationGroup a ser definido.</param>
        public static void EnableValidators(this UserControl control
            , bool enable
            , string validationGroup) {
            WebControlsHelper.EnableValidators(control.Controls, enable, validationGroup);
        }

        /// <summary>
        /// Habilita ou desabilita os validators presentes em uma coleção de controles.
        /// </summary>
        /// <param name="control">Controle a ser avaliado.</param>
        /// <param name="enable">Indica se se deve habilitar ou desabilitar os controles.</param>
        /// <param name="validationGroup">ValidationGroup a ser definido.</param>
        public static void EnableValidators(this Page control
            , bool enable
            , string validationGroup) {
            WebControlsHelper.EnableValidators(control.Controls, enable, validationGroup);
        }

        /// <summary>
        /// Habilita ou desabilita os validators presentes em uma coleção de controles.
        /// </summary>
        /// <param name="control">Controle a ser avaliado.</param>
        /// <param name="enable">Indica se se deve habilitar ou desabilitar os controles.</param>
        /// <param name="validationGroup">ValidationGroup a ser definido.</param>
        /// <param name="onlyOnEmptyValidationGroups">Somente aplica o ValidationGroup nos validators que não tiverem validators definidos.</param>
        public static void EnableValidators(this UserControl control
            , bool enable
            , string validationGroup
            , bool onlyOnEmptyValidationGroups) {
            WebControlsHelper.EnableValidators(control.Controls, enable, validationGroup, onlyOnEmptyValidationGroups);
        }

        /// <summary>
        /// Habilita ou desabilita os validators presentes em uma coleção de controles.
        /// </summary>
        /// <param name="control">Controle a ser avaliado.</param>
        /// <param name="enable">Indica se se deve habilitar ou desabilitar os controles.</param>
        /// <param name="validationGroup">ValidationGroup a ser definido.</param>
        /// <param name="onlyOnEmptyValidationGroups">Somente aplica o ValidationGroup nos validators que não tiverem validators definidos.</param>
        public static void EnableValidators(this Page control
            , bool enable
            , string validationGroup
            , bool onlyOnEmptyValidationGroups) {
            WebControlsHelper.EnableValidators(control.Controls, enable, validationGroup, onlyOnEmptyValidationGroups);
        }

        #endregion
    }
}
