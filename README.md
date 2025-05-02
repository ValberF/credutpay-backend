# Credutpay - Backend

Este repositório contém o backend do projeto **Credutpay**, desenvolvido com ASP.NET Core e utilizando PostgreSQL como banco de dados.

---

## Pré-requisitos

- [.NET SDK 8.0 ou superior](https://dotnet.microsoft.com/en-us/download)
- [Docker e Docker Compose](https://www.docker.com/products/docker-desktop)
- [Visual Studio 2022 ou superior (opcional)](https://visualstudio.microsoft.com/)
- Ferramentas do Entity Framework (se for usar via terminal):
  ```bash
  dotnet tool install --global dotnet-ef
  ```

---

## Passo a passo para rodar o projeto

### 1. Subir o banco de dados com Docker

Na raiz do projeto (onde está localizado o arquivo `docker-compose.yml`), execute:

```bash
docker compose up -d
```

Isso iniciará um container com o PostgreSQL.

---

### 2. Aplicar as migrations no banco de dados

O comando precisa ser executado a partir do projeto `Credutpay.Infra.Data`.

#### Usando o Console do Gerenciador de Pacotes no Visual Studio:

Certifique-se de que:
- O projeto de inicialização seja `Credutpay.API`
- O projeto padrão seja `Credutpay.Infra.Data`

E execute:

```powershell
Update-Database
```

#### Usando o terminal:

Navegue até o diretório do projeto de infraestrutura:

```bash
cd credutpay-backend/Credutpay.Infra.Data
dotnet ef database update
```

---

### 3. Executar a aplicação

#### Pelo Visual Studio:

- Defina o projeto `Credutpay.API` como projeto de inicialização.
- Pressione **F5** ou clique em **Start**.

#### Pelo terminal:

```bash
cd credutpay-backend/Credutpay.API
dotnet run
```

---

## Estrutura do Projeto

```
Solução 'Credutpay'
├── Domain
│   ├── Credutpay.Domain.Core
│   └── Credutpay.Domain.Registrations
├── Infra
│   ├── Credutpay.Infra.Core
│   ├── Credutpay.Infra.Data           # Migrations e contexto do EF Core
│   └── Credutpay.Infra.IoC            # Injeção de dependências
├── Services
│   └── Credutpay.API                  # Projeto principal da API
├── docker-compose                     # Configuração do PostgreSQL
```

---

## Configuração do PostgreSQL (Docker)

- **Imagem**: `postgres:latest`
- **Container name**: `Credutpay`
- **Porta externa**: `5433`
- **Porta interna (container)**: `5432`
- **Usuário**: `credutpay_user`
- **Senha**: `credutpay_password`
- **Banco de dados**: `Credutpay`

### Volume persistente:

- Nome: `pgdata`
- Caminho interno: `/var/lib/postgresql/data`

---

## String de Conexão (appsettings.json)

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=Credutpay;Username=credutpay_user;Password=credutpay_password;Port=5433;"
}
```

---

## Resetar o ambiente

Caso deseje limpar os dados e containers:

```bash
docker compose down -v
```

---

## Usuários de Teste

A aplicação já vem com alguns usuários pré-cadastrados no banco de dados após rodar as migrations e o UpdateDatabase. Você pode utilizá-los para testes.

### Credenciais

| Nome               | E-mail                | Senha     | Tipo       |
|--------------------|------------------------|-----------|------------|
| Administrador      | admin@credutpay.com    | 12345678  | User       |
| Lojista Silva      | lojista@credutpay.com  | 12345678  | User       |
| João Usuário       | joao@credutpay.com     | 12345678  | User       |
| Maria Usuária      | maria@credutpay.com    | 12345678  | User       |

---

## Endpoints da API

A API oferece os seguintes endpoints, acessíveis via Swagger ou ferramentas como Postman:

### Login

| Método | Rota         | Descrição               |
|--------|--------------|--------------------------|
| POST   | /api/login   | Realiza o login do usuário e retorna um token JWT. |

---

### User (CRUD de Usuários)

| Método | Rota        | Descrição                            |
|--------|-------------|----------------------------------------|
| GET    | /api/user   | Lista os usuários cadastrados.        |
| POST   | /api/user   | Cria um novo usuário.                 |
| PUT    | /api/user   | Atualiza os dados de um usuário.      |
| DELETE | /api/user   | Remove logicamente um usuário.        |

---

### Wallet (Carteira do Usuário)

| Método | Rota                        | Descrição                                                                 |
|--------|-----------------------------|---------------------------------------------------------------------------|
| GET    | /api/wallet                 | Retorna os dados da carteira do usuário autenticado.                      |
| GET    | /api/wallet/transfer        | Lista todas as transações da carteira (envios e recebimentos).            |
| POST   | /api/wallet/addfunds        | Adiciona saldo à carteira do usuário.                                     |

---

### WalletTransaction (Transferência entre Carteiras)

| Método | Rota                         | Descrição                                      |
|--------|------------------------------|------------------------------------------------|
| POST   | /api/wallet-transaction      | Realiza a transferência entre carteiras.       |

---

> Todos os endpoints (exceto `/api/login`) exigem autenticação via Bearer Token JWT.

---
