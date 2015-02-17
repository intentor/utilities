/*********************************************
Intentor.Utilities
**********************************************
Copyright © 2009-2012 André "Intentor" Martins
http://intentor.com.br/

Versão original por Kevin McFarlane
http://www.codeproject.com/KB/cs/designbycontract.aspx

Versão adaptada por Billy McCafferty
http://www.codeproject.com/KB/architecture/NHibernateBestPractices.aspx

Informações adicionais:
http://archive.eiffel.com/doc/manuals/technology/contract/
**********************************************/

using System;
using System.Diagnostics;

namespace Intentor.Utilities {
    #region Check

    /// <summary>
    /// Realiza validação de contratos 
    /// </summary>
    /// <remarks>
    /// O exemplo abaixo exemplifica o uso do método Require:
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
    /// Pode-se realizar a saída também diretamente para um Trace listener. 
    /// Pode-se, por exemplo inserir
    /// 
    /// <code>
    ///     Trace.Listeners.Clear();
    ///     Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
    /// </code>
    /// 
    /// ou realizar-se saída diretamente para um arquivo ou log de eventos.
    /// 
    /// (Nota: para clientes ASP.NET em Release somente o tratamento de exceções
    /// é possível)
    /// </remarks>
    public static class Check {
        #region Campos

        /// <summary>
        /// Identifica se se deve utilizar exceções durante as validações. Por padrão é utilizado.
        /// </summary>
        private static bool useAssertions = false;

        #endregion

        #region Propriedades

        /// <summary>
        /// Identifica se se deve utilizar exceções durante as validações. Por padrão é utilizado.
        /// </summary>
        private static bool UseExceptions {
            get {
                return !useAssertions;
            }
        }

        /// <summary>
        /// Identifica se se deve utilizar trace asserts ao invés de exceções. Por padrão não é utilizado.
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

        #region Métodos

        #region Precondition

        /// <summary>
        /// Precondition check - obrigações contratuais antes da chamada a um método ou propriedade.
        /// </summary>
        /// <remarks>
        /// Falha na checagem de pré-condições identifica problemas oriundos
        /// do cliente, aquele que chama o procedimetno que executa a validação
        /// de contrato.
        /// </remarks>
        /// <param name="assertion">Condição a ser avaliada.</param>
        public static void Require(bool assertion) {
            if (UseExceptions) {
                if (!assertion) throw new PreconditionException("Precondition failed.");
            } else {
                Trace.Assert(assertion, "Precondition failed.");
            }
        }

        /// <summary>
        /// Precondition check - obrigações contratuais antes da chamada a
        /// um método ou propriedade.
        /// </summary>
        /// <remarks>
        /// Falha na checagem de pré-condições identifica problemas oriundos
        /// do cliente, aquele que chama o procedimetno que executa a validação
        /// de contrato.
        /// </remarks>
        /// <param name="assertion">Condição a ser avaliada.</param>
        /// <param name="message">Mensagem a ser exibida caso o contrato não seja seguido.</param>
        public static void Require(bool assertion, string message) {
            if (UseExceptions) {
                if (!assertion) throw new PreconditionException(message);
            } else {
                Trace.Assert(assertion, "Precondition: " + message);
            }
        }

        /// <summary>
        /// Precondition check - obrigações contratuais antes da chamada a
        /// um método ou propriedade.
        /// </summary>
        /// <remarks>
        /// Falha na checagem de pré-condições identifica problemas oriundos
        /// do cliente, aquele que chama o procedimetno que executa a validação
        /// de contrato.
        /// </remarks>
        /// <param name="assertion">Condição a ser avaliada.</param>
        /// <param name="message">Mensagem a ser exibida caso o contrato não seja seguido.</param>
        /// <param name="inner">Exceção relacionada.</param>
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
        /// Postcondition check. - garante que obrigações contratuais
        /// foram cumpridas após execução de procedimentos.
        /// </summary>
        /// <remarks>
        /// Falha na checagem de pós-condições pode identificar um problema (bug)
        /// na rotina que solicitou tal validação contratual.
        /// </remarks>
        /// <param name="assertion">Condição a ser avaliada.</param>
        public static void Ensure(bool assertion) {
            if (UseExceptions) {
                if (!assertion) throw new PostconditionException("Postcondition failed.");
            } else {
                Trace.Assert(assertion, "Postcondition failed.");
            }
        }

        /// <summary>
        /// Postcondition check. - garante que obrigações contratuais
        /// foram cumpridas após execução de procedimentos.
        /// </summary>
        /// <remarks>
        /// Falha na checagem de pós-condições pode identificar um problema (bug)
        /// na rotina que solicitou tal validação contratual.
        /// </remarks>
        /// <param name="assertion">Condição a ser avaliada.</param>
        /// <param name="message">Mensagem a ser exibida caso o contrato não seja seguido.</param>
        public static void Ensure(bool assertion, string message) {
            if (UseExceptions) {
                if (!assertion) throw new PostconditionException(message);
            } else {
                Trace.Assert(assertion, "Postcondition: " + message);
            }
        }

        /// <summary>
        /// Postcondition check. - garante que obrigações contratuais
        /// foram cumpridas após execução de procedimentos.
        /// </summary>
        /// <remarks>
        /// Falha na checagem de pós-condições pode identificar um problema (bug)
        /// na rotina que solicitou tal validação contratual.
        /// </remarks>
        /// <param name="assertion">Condição a ser avaliada.</param>
        /// <param name="message">Mensagem a ser exibida caso o contrato não seja seguido.</param>
        /// <param name="inner">Exceção relacionada.</param>
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
        /// Invariant check - cláusula geral que se aplica a um conjunto de
        /// contratos definidos em uma classe.
        /// </summary>
        /// <param name="assertion">Condição a ser avaliada.</param>
        public static void Invariant(bool assertion) {
            if (UseExceptions) {
                if (!assertion) throw new InvariantException("Invariant failed.");
            } else {
                Trace.Assert(assertion, "Invariant failed.");
            }
        }

        /// <summary>
        /// Invariant check - cláusula geral que se aplica a um conjunto de
        /// contratos definidos em uma classe.
        /// </summary>
        /// <param name="assertion">Condição a ser avaliada.</param>
        /// <param name="message">Mensagem a ser exibida caso o contrato não seja seguido.</param>
        public static void Invariant(bool assertion, string message) {
            if (UseExceptions) {
                if (!assertion) throw new InvariantException(message);
            } else {
                Trace.Assert(assertion, "Invariant: " + message);
            }
        }

        /// <summary>
        /// Invariant check - cláusula geral que se aplica a um conjunto de
        /// contratos definidos em uma classe.
        /// </summary>
        /// <param name="assertion">Condição a ser avaliada.</param>
        /// <param name="message">Mensagem a ser exibida caso o contrato não seja seguido.</param>
        /// <param name="inner">Exceção relacionada.</param>
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
        /// Assertion check - avaliação de contratos genérico, sem uma
        /// definição explícita.
        /// </summary>
        /// <param name="assertion">Condição a ser avaliada.</param>
        public static void Assert(bool assertion) {
            if (UseExceptions) {
                if (!assertion) throw new AssertionException("Assertion failed.");
            } else {
                Trace.Assert(assertion, "Assertion failed.");
            }
        }

        /// <summary>
        /// Assertion check - avaliação de contratos genérico, sem uma
        /// definição explícita.
        /// </summary>
        /// <param name="assertion">Condição a ser avaliada.</param>
        /// <param name="message">Mensagem a ser exibida caso o contrato não seja seguido.</param>
        public static void Assert(bool assertion, string message) {
            if (UseExceptions) {
                if (!assertion) throw new AssertionException(message);
            } else {
                Trace.Assert(assertion, "Assertion: " + message);
            }
        }

        /// <summary>
        /// Assertion check - avaliação de contratos genérico, sem uma
        /// definição explícita.
        /// </summary>
        /// <param name="assertion">Condição a ser avaliada.</param>
        /// <param name="message">Mensagem a ser exibida caso o contrato não seja seguido.</param>
        /// <param name="inner">Exceção relacionada.</param>
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

    #region Exceções

    /// <summary>
    /// Exceção disparada quando um contrato falha.
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
    /// Exceção disparada quando uma pré-condição falha.
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
    /// Exceção disparada quando uma pós-condição falha.
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
    /// Exceção disparada quando uma falha invariant ocorre.
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
    /// Exceção disparada quando uma assertiva falha.
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
