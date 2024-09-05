<h1 align="center"> Global5 Solution </h1>

## 🎯 Sobre o projeto
 App web feito em .NET 8 com Blazor wasm e minimal API utilizando Dapper para acesso à dados. 
 Os endpoints da API chamam stored procedures do banco de dados para a minipulação de dados.

## 🔨 Funcionalidades do projeto

 O App lista materiais, fonrcedores, estoque e gera relatórios.

## 🛠️ Abrir e rodar o projeto

 Para rodar o projeto é necessário a versão 8 do .NET SDK ou posterior, encontrada em <href> https://dotnet.microsoft.com/en-us/download/dotnet/8.0 </href>
Após baixar o projeto ou clonar o repositório, basta acessar a Pasta Global5.Api pelo terminal e executar o comando 'dotnet run' para rodar a api, a URL
da API será mostrada no terminal. Repetir o mesmo processo com a pasta Globl5.Web, que irá subir o frontend. Feito isso, é só acessar a URL do front-end 
que será mostrada no terminal também. O terminal irá mostrar as URLs corretas de cada projeto.
<br>
É necessário criar o banco de dados manualmente no SQL Server com o script disponibilizado no projeto e editar a string de conexão com o IP do banco de dados.
<br>
 As configurações das URLs usadas para rodar a solução estão presentes no arquivo appsettings.json na pasta Global5.Api,
porém, checar as URLs do terminal para acessar a api e o frontend.  
<br>

## 📚 Documentação e arquitetura

 Ao subir a API e acessar a URL, o usuário será direcionado ao browser com a mensagem "OK" de health-check.
A Api está documentada no Swagger, que pode ser acessada colocando /swagger no final da URL do localhost. <br>

 Um pouco sobre a arquitetura do projeto, escolhi criar uma class library chamada Global5.Core que define os padrões e comportamentos de requests e responses.
Por meio de handlers implementados no core, a API mapeia e implementa seus endpoints e define como vai lidar com cada requisição, além de estabelecer as relações e gerenciar o banco de dados com EntityFramework. 

<br>
Já o frontend é feito com blazor wasm e componentes do MudBlazor, implementa os handlers do Core com um HttpClient que faz todas as requisições para a API, que retorna por sua vez as respostas padronizadas.
