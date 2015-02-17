/*********************************************
Intentor.Utilities
**********************************************
Copyright � 2009-2012 Andr� "Intentor" Martins
http://intentor.com.br/

Vers�o original por Kevin McFarlane
http://www.codeproject.com/KB/cs/designbycontract.aspx

Vers�o adaptada por Billy McCafferty
http://www.codeproject.com/KB/architecture/NHibernateBestPractices.aspx

Informa��es adicionais:
http://archive.eiffel.com/doc/manuals/technology/contract/
**********************************************/

using System;
using System.Diagnostics;

namespace Intentor.Utilities {
    #region Check

    /// <summary>
    /// Realiza valida��o de contratos 
    /// </summary>
    /// <remarks>
    /// O exemplo abaixo exemplifica o uso do m�todo Require:
    /// 
    /// <code>
    ///     public void Test(int x)
    ///     {
    /// 	        try
    /// 	        {
    ///			        Check.Require(x > 1, "x deve ser maior que 1");
    ///		        }
    ///		        catch (System.Exception ex)
    ///		        {
    ///			        Console.WriteLine(ex.ToString());
    ///		        }
    ///	        }
    /// </code>
    ///
    /// Pode-se realizar a sa�da tamb�m diretamente para um Trace listener. 
    /// Pode-se, por exemplo inserir
    /// 
    /// <code>
    ///     Trace.Listeners.Clear();
    ///     Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
    /// </code>
    /// 
    /// ou realizar-se sa�da diretamente para um arquivo ou log de eventos.
    /// 
    /// (Nota: para clientes ASP.NET em Release somente o tratamento de exce��es
    /// � poss�vel)
    /// </remarks>
    public static class Check {
        #region Campos

        /// <summary>
        /// Identifica se se deve utilizar exce��es durante as valida��es. Por padr�o � utilizado.
        /// </summary>
        private static bool useAssertions = false;

        #endregion

        #region Propriedades

        /// <summary>
        /// Identifica se se deve utilizar exce��es durante as valida��es. Por padr�o � utilizado.
        /// </summary>
        private static bool UseExceptions {
            get {
                return !useAssertions;
            }
        }

        /// <summary>
        /// Identifica se se deve utilizar trace asserts ao inv�s de exce��es. Por padr�o n�o � utilizado.
        /// </summary>
        public static bool UseAssertions {
            get {
                return useAssertions;
            }
            set {
                useAssertions = value;
            }
        }

        #endregion

        #region M�todos

        #region Precondition

        /// <summary>
        /// Precondition check - obriga��es contratuais antes da chamada a um m�todo ou propriedade.
        /// </summary>
        /// <remarks>
        /// Falha na checagem de pr�-condi��es identifica problemas oriundos
        /// do cliente, aquele que chama o procedimetno que executa a valida��o
        /// de contrato.
        /// </remarks>
        /// <param name="assertion">Condi��o a ser avaliada.</param>
        public static void Require(bool assertion) {
            if (UseExceptions) {
                if (!assertion) throw new PreconditionException("Precondition failed.");
            } else {
                Trace.Assert(assertion, "Precondition failed.");
            }
        }

        /// <summary>
        /// Precondition check - obriga��es contratuais antes da chamada a
        /// um m�todo ou propriedade.
        /// </summary>
        /// <remarks>
        /// Falha na checagem de pr�-condi��es identifica problemas oriundos
        /// do cliente, aquele que chama o procedimetno que executa a valida��o
        /// de contrato.
        /// </remarks>
        /// <param name="assertion">Condi��o a ser avaliada.</param>
        /// <param name="message">Mensagem a ser exibida caso o contrato n�o seja seguido.</param>
        public static void Require(bool assertion, string message) {
            if (UseExceptions) {
                if (!assertion) throw new PreconditionException(message);
            } else {
                Trace.Assert(assertion, "Precondition: " + message);
            }
        }

        /// <summary>
        /// Precondition check - obriga��es contratuais antes da chamada a
        /// um m�todo ou propriedade.
        /// </summary>
        /// <remarks>
        /// Falha na checagem de pr�-condi��es identifica problemas oriundos
        /// do cliente, aquele que chama o procedimetno que executa a valida��o
        /// de contrato.
        /// </remarks>
        /// <param name="assertion">Condi��o a ser avaliada.</param>
        /// <param name="message">Mensagem a ser exibida caso o contrato n�o seja seguido.</param>
        /// <param name="inner">Exce��o relacionada.</param>
        public static void Require(bool assertion, string message, Exception inner) {
            if (UseExceptions) {
                if (!assertion) throw new PreconditionException(message, inner);
            } else {
                Trace.Assert(assertion, "Precondition: " + message);
            }
        }

        #endregion

        #region Postcondition

        /// <summary>
        /// Postcondition check. - garante que obriga��es contratuais
        /// foram cumpridas ap�s execu��o de procedimentos.
        /// </summary>
        /// <remarks>
        /// Falha na checagem de p�s-condi��es pode identificar um problema (bug)
        /// na rotina que solicitou tal valida��o contratual.
        /// </remarks>
        /// <param name="assertion">Condi��o a ser avaliada.</param>
        public static void Ensure(bool assertion) {
            if (UseExceptions) {
                if (!assertion) throw new PostconditionException("Postcondition failed.");
            } else {
                Trace.Assert(assertion, "Postcondition failed.");
            }
        }

        /// <summary>
        /// Postcondition check. - garante que obriga��es contratuais
        /// foram cumpridas ap�s execu��o de procedimentos.
        /// </summary>
        /// <remarks>
        /// Falha na checagem de p�s-condi��es pode identificar um problema (bug)
        /// na rotina que solicitou tal valida��o contratual.
        /// </remarks>
        /// <param name="assertion">Condi��o a ser avaliada.</param>
        /// <param name="message">Mensagem a ser exibida caso o contrato n�o seja seguido.</param>
        public static void Ensure(bool assertion, string message) {
            if (UseExceptions) {
                if (!assertion) throw new PostconditionException(message);
            } else {
                Trace.Assert(assertion, "Postcondition: " + message);
            }
        }

        /// <summary>
        /// Postcondition check. - garante que obriga��es contratuais
        /// foram cumpridas ap�s execu��o de procedimentos.
        /// </summary>
        /// <remarks>
        /// Falha na checagem de p�s-condi��es pode identificar um problema (bug)
        /// na rotina que solicitou tal valida��o contratual.
        /// </remarks>
        /// <param name="assertion">Condi��o a ser avaliada.</param>
        /// <param name="message">Mensagem a ser exibida caso o contrato n�o seja seguido.</param>
        /// <param name="inner">Exce��o relacionada.</param>
        public static void Ensure(bool assertion, string message, Exception inner) {
            if (UseExceptions) {
                if (!assertion) throw new PostconditionException(message, inner);
            } else {
                Trace.Assert(assertion, "Postcondition: " + message);
            }
        }

        #endregion

        #region Invariant check

        /// <summary>
        /// Invariant check - cl�usula geral que se aplica a um conjunto de
        /// contratos definidos em uma classe.
        /// </summary>
        /// <param name="assertion">Condi��o a ser avaliada.</param>
        public static void Invariant(bool assertion) {
            if (UseExceptions) {
                if (!assertion) throw new InvariantException("Invariant failed.");
            } else {
                Trace.Assert(assertion, "Invariant failed.");
            }
        }

        /// <summary>
        /// Invariant check - cl�usula geral que se aplica a um conjunto de
        /// contratos definidos em uma classe.
        /// </summary>
        /// <param name="assertion">Condi��o a ser avaliada.</param>
        /// <param name="message">Mensagem a ser exibida caso o contrato n�o seja seguido.</param>
        public static void Invariant(bool assertion, string message) {
            if (UseExceptions) {
                if (!assertion) throw new InvariantException(message);
            } else {
                Trace.Assert(assertion, "Invariant: " + message);
            }
        }

        /// <summary>
        /// Invariant check - cl�usula geral que se aplica a um conjunto de
        /// contratos definidos em uma classe.
        /// </summary>
        /// <param name="assertion">Condi��o a ser avaliada.</param>
        /// <param name="message">Mensagem a ser exibida caso o contrato n�o seja seguido.</param>
        /// <param name="inner">Exce��o relacionada.</param>
        public static void Invariant(bool assertion, string message, Exception inner) {
            if (UseExceptions) {
                if (!assertion) throw new InvariantException(message, inner);
            } else {
                Trace.Assert(assertion, "Invariant: " + message);
            }
        }

        #endregion

        #region Assertion check

        /// <summary>
        /// Assertion check - avalia��o de contratos gen�rico, sem uma
        /// defini��o expl�cita.
        /// </summary>
        /// <param name="assertion">Condi��o a ser avaliada.</param>
        public static void Assert(bool assertion) {
            if (UseExceptions) {
                if (!assertion) throw new AssertionException("Assertion failed.");
            } else {
                Trace.Assert(assertion, "Assertion failed.");
            }
        }

        /// <summary>
        /// Assertion check - avalia��o de contratos gen�rico, sem uma
        /// defini��o expl�cita.
        /// </summary>
        /// <param name="assertion">Condi��o a ser avaliada.</param>
        /// <param name="message">Mensagem a ser exibida caso o contrato n�o seja seguido.</param>
        public static void Assert(bool assertion, string message) {
            if (UseExceptions) {
                if (!assertion) throw new AssertionException(message);
            } else {
                Trace.Assert(assertion, "Assertion: " + message);
            }
        }

        /// <summary>
        /// Assertion check - avalia��o de contratos gen�rico, sem uma
        /// defini��o expl�cita.
        /// </summary>
        /// <param name="assertion">Condi��o a ser avaliada.</param>
        /// <param name="message">Mensagem a ser exibida caso o contrato n�o seja seguido.</param>
        /// <param name="inner">Exce��o relacionada.</param>
        public static void Assert(bool assertion, string message, Exception inner) {
            if (UseExceptions) {
                if (!assertion) throw new AssertionException(message, inner);
            } else {
                Trace.Assert(assertion, "Assertion: " + message);
            }
        }

        #endregion

        #endregion
    }

    #endregion

    #region Exce��es

    /// <summary>
    /// Exce��o disparada quando um contrato falha.
    /// </summary>
    public class DesignByContractException : ApplicationException {
        /// <summary>
        /// DesignByContract Exception.
        /// </summary>
        protected DesignByContractException() { }
        /// <summary>
        /// DesignByContract Exception.
        /// </summary>
        protected DesignByContractException(string message) : base(message) { }
        /// <summary>
        /// DesignByContract Exception.
        /// </summary>
        protected DesignByContractException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Exce��o disparada quando uma pr�-condi��o falha.
    /// </summary>
    public class PreconditionException : DesignByContractException {
        /// <summary>
        /// Precondition Exception.
        /// </summary>
        public PreconditionException() { }
        /// <summary>
        /// Precondition Exception.
        /// </summary>
        public PreconditionException(string message) : base(message) { }
        /// <summary>
        /// Precondition Exception.
        /// </summary>
        public PreconditionException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Exce��o disparada quando uma p�s-condi��o falha.
    /// </summary>
    public class PostconditionException : DesignByContractException {
        /// <summary>
        /// Postcondition Exception.
        /// </summary>
        public PostconditionException() { }
        /// <summary>
        /// Postcondition Exception.
        /// </summary>
        public PostconditionException(string message) : base(message) { }
        /// <summary>
        /// Postcondition Exception.
        /// </summary>
        public PostconditionException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Exce��o disparada quando uma falha invariant ocorre.
    /// </summary>
    public class InvariantException : DesignByContractException {
        /// <summary>
        /// Invariant Exception.
        /// </summary>
        public InvariantException() { }
        /// <summary>
        /// Invariant Exception.
        /// </summary>
        public InvariantException(string message) : base(message) { }
        /// <summary>
        /// Invariant Exception.
        /// </summary>
        public InvariantException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Exce��o disparada quando uma assertiva falha.
    /// </summary>
    public class AssertionException : DesignByContractException {
        /// <summary>
        /// Assertion Exception.
        /// </summary>
        public AssertionException() { }
        /// <summary>
        /// Assertion Exception.
        /// </summary>
        public AssertionException(string message) : base(message) { }
        /// <summary>
        /// Assertion Exception.
        /// </summary>
        public AssertionException(string message, Exception inner) : base(message, inner) { }
    }

    #endregion
}
