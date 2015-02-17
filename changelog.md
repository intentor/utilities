# Changelog for Intentor.Utilities

Changelog details in Brazilian Portuguese.

## 12.10.2.1750 (02/10/2012)
- Acréscimo do envio de coleção de variáveis de POST em WebAccessHelper para permitir escape de parâmetros;
- Alteração do padrão de caracteres de JsonHelper para UTF8.

## 12.1.12.1357 (12/01/2012)
- Acréscimo da classe JsonHelper para apoio ao uso de JSON (apenas para .net 4.0);
- Acréscimo dos métodos GetAsByte e GetAsStream em WebAccessHelper;
- Ajustes nos métodos Get de WebAccessHelper;
- Retirada do parâmetro relativeURL dos métodos POST de WebAccessHelper.

## 12.1.6.1103 (06/01/2012)
- Acréscimo do método RemoveAccents em FormatHelper;
- Acréscimo do método ReplaceSpecialChars em FormatHelper;
- Acréscimo do método FormatForUrl em FormatHelper.

## 11.12.6.1819 (06/12/2011)
- Acréscimo do método GetTypesInNamespace em ReflectionHelper;
- Acréscimo do método MoveListItems em WebControlsHelper.

## 11.10.11.1412 (11/10/2011)
- Acréscimo de sobrecarga no método Fill de WebControlsExtensions para permitir a entrada de valor e texto de itens vazios;
- Retirada do método InsertEmptyItem.
	
## 11.8.20.2122 (20/08/2011)
- Acréscimo do método GetValueOrNull.

## 11.8.1.1330 (01/08/2011)
- Troca do nome do método HttpRequest da classe WebAccessHelper para Post;
- Acréscimo do método Get na classe WebAccessHelper.

## 11.2.10.1000 (10/02/2011)
- Compilação de versão para o .Net 4.0 Full Profile.

## 10.10.25.1130 (25/10/2010)
- Criação da classe FormatExtensions, contendo os extension methods de FormatHelper;
- Criação da classe WebControlsExtensions, contendo os extension methods de WebControlsHelper;
- Renomeação da classe ClientSupport para ClientHelper;
- Renomeação da classe Constants para AppConstants;
- Renomeação da classe CryptoSupport para CryptoHelper, bem como de seus métodos;
- Renomeação da classe CultureSettings para CultureHelper;
- Renomeação da classe ReflecionHelper para ReflectionHelper;
- Renomeação da classe ServerSupport para ServerHelper;
- Renomeação da classe SmtpAccess para SmtpHelper;
- Renomeação da classe WebAccess para WebAccessHelper;
- Exclusão da classes CultureSettings;
- Exclusão das classes de Filters;
- Exclusão das classes de handlers;
- Realocação das constantes em CultureSettings para CultureHelper;
- Organização da classe FormatHelper, com exclusão de métodos obsoletos;
- Acréscimo de possibilidade de definição de credenciais durante envio de e-mails na classe SmtpHelper;
- Atualização e padronização de comentários.

## 10.4.1.1521 (01/04/2010)
- Alteração do nome da classe ReflexionHelper para ReflecionHelper;
- Alteração do método CreateInstance da classe ReflecionHelper para permitir envio de parâmetros de construtor quando da criação de instâncias de objetos.