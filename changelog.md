# Changelog for Intentor.Utilities

Changelog details in Brazilian Portuguese.

## 12.10.2.1750 (02/10/2012)
- Acr�scimo do envio de cole��o de vari�veis de POST em WebAccessHelper para permitir escape de par�metros;
- Altera��o do padr�o de caracteres de JsonHelper para UTF8.

## 12.1.12.1357 (12/01/2012)
- Acr�scimo da classe JsonHelper para apoio ao uso de JSON (apenas para .net 4.0);
- Acr�scimo dos m�todos GetAsByte e GetAsStream em WebAccessHelper;
- Ajustes nos m�todos Get de WebAccessHelper;
- Retirada do par�metro relativeURL dos m�todos POST de WebAccessHelper.

## 12.1.6.1103 (06/01/2012)
- Acr�scimo do m�todo RemoveAccents em FormatHelper;
- Acr�scimo do m�todo ReplaceSpecialChars em FormatHelper;
- Acr�scimo do m�todo FormatForUrl em FormatHelper.

## 11.12.6.1819 (06/12/2011)
- Acr�scimo do m�todo GetTypesInNamespace em ReflectionHelper;
- Acr�scimo do m�todo MoveListItems em WebControlsHelper.

## 11.10.11.1412 (11/10/2011)
- Acr�scimo de sobrecarga no m�todo Fill de WebControlsExtensions para permitir a entrada de valor e texto de itens vazios;
- Retirada do m�todo InsertEmptyItem.
	
## 11.8.20.2122 (20/08/2011)
- Acr�scimo do m�todo GetValueOrNull.

## 11.8.1.1330 (01/08/2011)
- Troca do nome do m�todo HttpRequest da classe WebAccessHelper para Post;
- Acr�scimo do m�todo Get na classe WebAccessHelper.

## 11.2.10.1000 (10/02/2011)
- Compila��o de vers�o para o .Net 4.0 Full Profile.

## 10.10.25.1130 (25/10/2010)
- Cria��o da classe FormatExtensions, contendo os extension methods de FormatHelper;
- Cria��o da classe WebControlsExtensions, contendo os extension methods de WebControlsHelper;
- Renomea��o da classe ClientSupport para ClientHelper;
- Renomea��o da classe Constants para AppConstants;
- Renomea��o da classe CryptoSupport para CryptoHelper, bem como de seus m�todos;
- Renomea��o da classe CultureSettings para CultureHelper;
- Renomea��o da classe ReflecionHelper para ReflectionHelper;
- Renomea��o da classe ServerSupport para ServerHelper;
- Renomea��o da classe SmtpAccess para SmtpHelper;
- Renomea��o da classe WebAccess para WebAccessHelper;
- Exclus�o da classes CultureSettings;
- Exclus�o das classes de Filters;
- Exclus�o das classes de handlers;
- Realoca��o das constantes em CultureSettings para CultureHelper;
- Organiza��o da classe FormatHelper, com exclus�o de m�todos obsoletos;
- Acr�scimo de possibilidade de defini��o de credenciais durante envio de e-mails na classe SmtpHelper;
- Atualiza��o e padroniza��o de coment�rios.

## 10.4.1.1521 (01/04/2010)
- Altera��o do nome da classe ReflexionHelper para ReflecionHelper;
- Altera��o do m�todo CreateInstance da classe ReflecionHelper para permitir envio de par�metros de construtor quando da cria��o de inst�ncias de objetos.