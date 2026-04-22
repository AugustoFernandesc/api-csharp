# MinhaApi - Employee Management System 🚀

API teste para aprendizado de C#, desenvolvida com **.NET 10** e **PostgreSQL**. O projeto utiliza padrões arquiteturais modernos para garantir escalabilidade, segurança e fácil manutenção.

## 🏗️ Arquitetura e Padrões

Este projeto foi estruturado seguindo princípios de **Clean Architecture**, separando as responsabilidades em camadas lógicas:

- **Domain:** Entidades de negócio e interfaces fundamentais.
- **Application:** Serviços (Use Cases) e ViewModels (DTOs) para tráfego de dados.
- **Infrastructure:** Implementação do **Entity Framework Core**, Mappings e Repositórios.
- **API:** Controllers e configurações de Middleware.

### Principais Padrões Utilizados:

- **Repository & Unit of Work:** Abstração da camada de dados e gerenciamento de transações atômicas.
- **Dependency Injection:** Gerenciamento de ciclo de vida (Scoped/Transient) nativo do .NET.
- **Data Transfer Object (DTO):** Proteção de entidades de domínio e exposição controlada de dados sensíveis.

## 🔒 Segurança e Integridade

- **BCrypt.Net:** Hashing de senhas para armazenamento seguro (anti-rainbow table).
- **JWT (JSON Web Token):** Autenticação e autorização via Bearer Token.
- **FluentValidation:** Camada de validação de dados desacoplada, garantindo que apenas dados válidos cheguem ao domínio.

## 🛠️ Tecnologias

- **Framework:** .NET 10
- **Database:** PostgreSQL
- **ORM:** Entity Framework Core (Code First)
- **Documentation:** Swagger (OpenAPI)
- **Validation:** FluentValidation

## ⚙️ Configuração e Execução

### 1. Banco de Dados

Atualize a `DefaultConnection` no arquivo `appsettings.json` com suas credenciais do PostgreSQL.

### 2. Migrations

Aplique o esquema ao banco de dados rodando o comando:

```bash
dotnet ef database update
```
