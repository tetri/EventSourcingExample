# Event Sourcing Example with ASP.NET Core and SQLite

Este repositório contém um exemplo de implementação de Event Sourcing usando ASP.NET Core e SQLite. O projeto demonstra como criar, armazenar e recuperar eventos relacionados a contas bancárias, além de calcular o saldo atual de uma conta.

## Visão Geral

Event Sourcing é um padrão de design onde as mudanças no estado de um aplicativo são armazenadas como uma sequência de eventos. Este exemplo mostra como implementar Event Sourcing em uma aplicação ASP.NET Core com um banco de dados SQLite.

## Funcionalidades

- Criação de contas bancárias
- Depósito de dinheiro
- Saque de dinheiro
- Recuperação do saldo atual da conta
- Persistência de eventos em banco de dados SQLite

## Tecnologias Utilizadas

- [.NET 8](https://dotnet.microsoft.com/)
- [ASP.NET Core](https://docs.microsoft.com/aspnet/core)
- [Entity Framework Core](https://docs.microsoft.com/ef/core)
- [SQLite](https://www.sqlite.org/index.html)
- [Swagger](https://swagger.io/)

## Estrutura do Projeto

- `Controllers`: Contém os controladores da API
- `Events`: Define os eventos utilizados no Event Sourcing
- `Aggregates`: Define os agregados (entidades) do domínio
- `Repositories`: Contém a lógica para armazenamento e recuperação dos eventos
- `Data`: Configurações do Entity Framework Core

## Configuração do Projeto

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQLite](https://www.sqlite.org/download.html)

### Clonar o Repositório

```bash
git clone https://github.com/seu-usuario/event-sourcing-example.git
cd event-sourcing-example
```

### Configurar o Banco de Dados

No arquivo `appsettings.json`, configure a string de conexão para o SQLite:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=eventsourcing.db"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### Executar Migrações

Para garantir que o banco de dados e as tabelas necessárias sejam criados, execute as migrações:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### Executar o Projeto

Para executar a aplicação:

```bash
dotnet run
```

A aplicação estará disponível em `https://localhost:5001` e a interface do Swagger pode ser acessada em `https://localhost:5001/swagger`.

## Endpoints da API

### Criar Conta

```http
POST /api/BankAccount
```

Corpo da Requisição:

```json
{
  "accountId": "GUID"
}
```

### Depositar Dinheiro

```http
POST /api/BankAccount/{accountId}/deposit
```

Corpo da Requisição:

```json
{
  "amount": 100.0
}
```

### Sacar Dinheiro

```http
POST /api/BankAccount/{accountId}/withdraw
```

Corpo da Requisição:

```json
{
  "amount": 50.0
}
```

### Obter Saldo da Conta

```http
GET /api/BankAccount/{accountId}/balance
```

Resposta:

```json
{
  "accountId": "GUID",
  "balance": 50.0
}
```

## Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues ou enviar pull requests.

## Licença

Este projeto está licenciado sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.
