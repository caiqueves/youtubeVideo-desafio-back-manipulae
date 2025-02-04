# YouTube Video Project

Este projeto permite interagir com a API do YouTube para realizar tarefas como baixar vídeos ou buscar informações. Para rodá-lo corretamente em sua máquina, siga as instruções abaixo.

## Pré-requisitos

Antes de rodar o projeto, você precisa ter os seguintes itens instalados:

- [Visual Studio](https://visualstudio.microsoft.com/) (recomendado para desenvolvimento em .NET)
- [SDK do .NET](https://dotnet.microsoft.com/download) (para rodar e compilar o projeto)
- Conta no [Google Cloud Platform](https://console.cloud.google.com/) para gerar uma chave da API do YouTube.

## Passo 1: Obter a API Key do YouTube

Para que o projeto funcione, é necessário configurar uma chave de API do YouTube:

1. Acesse o [Google Cloud Console](https://console.cloud.google.com/).
2. Crie um novo projeto ou selecione um projeto existente.
3. Ative a **YouTube Data API v3** no seu projeto.
4. Crie credenciais do tipo **API Key**.
5. Copie a chave gerada, pois você precisará dela para configurar o projeto localmente.

## Passo 2: Configurar a API Key no Projeto

1. No projeto, localize o arquivo variaveis.bat para definir a API Key que precisa ser inserida. 
   
   @echo off
:: Definir variáveis de ambiente
set api_key="COLOCA AQUI API KEY"

:: Pausa para visualizar a saída antes de fechar
pause

## Passo 3: Baixar as Dependencias do Nuget

Abrir a raiz do projeto no cmd e executar o comando

dotnet restore

## Passo 4: Executar o projeto

Após configurar a chave da API e baixar as dependências, você pode rodar o projeto localmente com o seguinte comando:

dotnet run
