# 🧩 Backend Challenge

Este projeto foi desenvolvido como parte de um **teste técnico para vaga de desenvolvedor backend**, com foco em **arquitetura limpa (Clean Architecture)**, princípios de **DDD (Domain-Driven Design)** e uso de **mensageria assíncrona**.

A aplicação expõe uma **API** para gerenciar **motos, entregadores e locações**, com **armazenamento de imagens em S3** e **notificações de eventos via RabbitMQ**.

---

## 🏗️ Arquitetura da Solução

A solução segue princípios de **Clean Architecture** + **DDD** e, em alguns pontos, características de **Arquitetura Hexagonal** (separação clara de portas/adapters). Buscamos **baixo acoplamento**, **alta coesão** e **independência entre camadas**.

```
BackendChallenge
├── BackendChallenge.API            → Camada de entrada (Controllers/HTTP)
├── BackendChallenge.Application    → Casos de uso, DTOs, mapeadores, contratos
├── BackendChallenge.Domain         → Entidades, enums, interfaces de repositório, base do domínio
├── BackendChallenge.Infrastructure → EF Core (PostgreSQL), Repositórios, RabbitMQ, S3
├── BackendChallenge.Worker         → Serviço em background que consome eventos e persiste notificações
└── docker-compose.yml              → Orquestração de containers
```

### 🧠 Camadas (resumo)

* **API**: exposição de endpoints HTTP e orquestração dos casos de uso.
* **Application**: *use cases* (ex.: cadastrar moto, buscar, locação, etc.), DTOs, mappers, contratos/serviços.
* **Domain**: regras de negócio puras (entidades, enums e interfaces) sem dependências de infraestrutura.
* **Infrastructure**: implementação concreta de persistência (PostgreSQL/EF Core), mensageria (RabbitMQ) e storage (AWS S3).
* **Worker**: processo separado que consome eventos do RabbitMQ e persiste notificações no banco.

---

## 📂 Estrutura de Pastas (espelhando o projeto)

### `BackendChallenge.API`

```
BackendChallenge.API
├── Controllers/
├── Interfaces/
├── appsettings.json
├── Dockerfile
└── Program.cs
```

### `BackendChallenge.Application`

```
BackendChallenge.Application
├── Dependencies/
├── Common/
├── Services/
└── UseCases/
    ├── DeliveryPerson/
    │   ├── Dtos/
    │   ├── Mappers/
    │   ├── Queries/
    │   ├── RegisterDeliveryPerson/
    │   └── UpdateYourDriversLicensePhoto/
    ├── Motorcycle/
    │   ├── AdminRegisterMotorcycle/
    │   ├── AdminRemovesMotorcycleById/
    │   ├── AdminSearchesForMotorcycleById/
    │   ├── AdminSearchesForMotorcycleByPlate/
    │   ├── AdminUpdatesMotorcyclePlate/
    │   ├── Dtos/
    │   ├── Mappers/
    │   └── Queries/
    └── MotorcycleRental/
        ├── Dtos/
        ├── Mappers/
        ├── Queries/
        ├── RegisterMotorcycleRental/
        ├── SearcheForMotorcycleRentalById/
        └── UpdateReturnDate/
```

### `BackendChallenge.Domain`

```
BackendChallenge.Domain
├── Dependencies/
├── Common/
│   ├── Events/
│   └── BaseEntity.cs
├── Entities/
├── Enums/
├── Repositories/
└── DomainClassDiagram.cd
```

### `BackendChallenge.Infrastructure`

```
BackendChallenge.Infrastructure
├── Dependencies/
├── Configurations/
├── Extensions/
├── Migrations/
├── Persistence/
│   ├── Mappings/
│   ├── Repositories/
│   └── ApplicationDbContext.cs
├── Services/
└── InfrastructureClassDiagram.cd
```

### `BackendChallenge.Worker`

```
BackendChallenge.Worker
├── Dockerfile
├── Program.cs
└── Worker.cs
```

---

## ⚙️ Tecnologias Utilizadas

| Componente         | Tecnologia            | Justificativa                                                                                      |
| ------------------ | --------------------- | -------------------------------------------------------------------------------------------------- |
| **Banco de Dados** | PostgreSQL            | Banco relacional robusto, open-source, excelente para transações e com suporte sólido no EF Core.  |
| **Mensageria**     | RabbitMQ              | Broker leve e confiável para comunicação assíncrona (ex.: evento “moto cadastrada”).               |
| **Storage**        | AWS S3                | Armazenamento de objetos escalável e durável; ideal para CNHs (não armazenamos binários no banco). |
| **ORM**            | Entity Framework Core | Produtividade + flexibilidade para consultas e mapeamento.                                         |
| **.NET 8 / C# 12** | Plataforma principal  | Maturidade, performance e recursos modernos de linguagem/plataforma.                               |

---

## 🚀 Como Executar o Projeto

### 🧰 Pré-requisitos

* [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
* [Docker](https://www.docker.com/) e [Docker Compose](https://docs.docker.com/compose/)
* Opcional: cliente SQL (DBeaver/HeidiSQL) para inspecionar o banco

---

### 🪣 1) Clonar o repositório

```bash
git clone https://github.com/mrffilipe/BackendChallenge.git
cd BackendChallenge
```

---

### ⚙️ 2) Restaurar os pacotes

```bash
dotnet restore
```

---

### 🧾 3) Criar o arquivo `.env`

Crie um arquivo `.env` **no mesmo diretório do `docker-compose.yml`** com o conteúdo abaixo.
Você pode **usar exatamente este modelo** e **alterar apenas as variáveis da AWS** (as demais podem ficar como estão):

```bash
# -----------------------
# Geral
# -----------------------
ASPNETCORE_ENVIRONMENT=Development
DOCKER_REGISTRY=
COMPOSE_PROJECT_NAME=backendchallenge

# -----------------------
# API
# -----------------------
API_HTTP_PORT=5000

# -----------------------
# PostgreSQL
# -----------------------
POSTGRES_HOST=postgres
POSTGRES_HOST_PORT=5432
POSTGRES_PORT=5432
POSTGRES_DB=backendchallenge
POSTGRES_USER=app
POSTGRES_PASSWORD=app

CONNECTIONSTRINGS__DEFAULTCONNECTION=Host=${POSTGRES_HOST};Port=${POSTGRES_PORT};Database=${POSTGRES_DB};Username=${POSTGRES_USER};Password=${POSTGRES_PASSWORD}

# -----------------------
# RabbitMQ
# -----------------------
RABBITMQ_HOST=rabbitmq
RABBITMQ_PORT=5672
RABBITMQ_MGMT_PORT=15672
RABBITMQ_USER=guest
RABBITMQ_PASSWORD=guest

RABBIT__CONNECTION=amqp://${RABBITMQ_USER}:${RABBITMQ_PASSWORD}@${RABBITMQ_HOST}:${RABBITMQ_PORT}

# -----------------------
# AWS S3
# -----------------------
AWS_ACCESS_KEY_ID=REPLACE_ME
AWS_SECRET_ACCESS_KEY=REPLACE_ME
AWS_REGION=sa-east-1
S3_BUCKET_NAME=backendchallenge-cnh
S3_BUCKET_PREFIX=cnh/
S3_SERVICE_URL=
S3_FORCE_PATH_STYLE=false
S3_USE_ACCELERATE=false
```

---

### 🐳 4) Subir os containers

```bash
docker compose up -d --build
```

Isso iniciará os serviços:

* **API** (.NET 8)
* **Worker** (.NET 8)
* **PostgreSQL**
* **RabbitMQ**

> RabbitMQ Management UI: [http://localhost:15672](http://localhost:15672)
> Login: `guest` / `guest`

---

### 🗃️ 5) Migrations (observação)

A aplicação utiliza **Entity Framework Core**.

> **Quando a API sobe e o Postgres já está rodando via Docker Compose, a própria API aplica as migrations automaticamente.**

---

### 🧠 6) Testar a API

**Swagger**:

```
http://localhost:5000/swagger
```

**Coleções e documentação adicional**: veja o diretório **`/docs`** do repositório:

* **Modelagem do sistema** (diagramas/ER/fluxos);
* **Arquivo `.json` do Insomnia** com todas as requisições para testar os endpoints.

---

## 📬 Eventos e Mensageria

* Ao cadastrar uma moto (`POST /motos`), a API publica o evento **`motorcycle.created.v1`** no RabbitMQ.
* O **Worker** consome o evento e:

  * Se **`Year == 2024`**, cria uma **notificação** no banco;
  * Caso contrário, faz ACK e segue.

Isso promove **desacoplamento** e **processamento assíncrono** entre contextos.

---

## 🧱 Decisões de Arquitetura (resumo)

* **PostgreSQL**: estabilidade, transações e ótimo suporte no EF Core.
* **RabbitMQ**: comunicação assíncrona simples, confiável e com eco-sistema maduro no .NET.
* **AWS S3**: armazenamento de objetos externo (CNHs), alta durabilidade/escalabilidade e SDK oficial para .NET.

---

## ✅ Próximos Passos / Testes Unitários (sugestões)

Para elevar ainda mais a qualidade:

1. **Testes de Application (use cases):**

   * Mockar repositórios e `IFileStorage`/`IMessageBus` para validar **regras de negócio** sem tocar em DB ou rede.
   * Casos:

     * Cadastro de moto/entregador com validações de unicidade;
     * Upload de CNH: rejeição de formatos não-PNG/BMP;
     * Locação: CNH diferente de **A/A+B** deve falhar; data de início = D+1; coerência do plano;
     * Cálculo de **multa** (planos 7/15) e **acréscimo** (R$50/dia extra).

2. **Testes de Domain (entidades):**

   * Já foram adicionados **testes unitários** para as principais entidades da camada de domínio (`Motorcycle`, `DeliveryPerson`, `MotorcycleRental`, `Notification` e `BaseEntity`), cobrindo construtores, invariantes e métodos de comportamento (como `UpdatePlate` e validação de CNH).
   * Recomenda-se expandir gradualmente os cenários e adicionar novos testes à medida que o domínio evoluir.

3. **Testes de Infra (integração):**

   * Próximo passo recomendado: criar **testes de integração** para validar repositórios e adapters.
   * Utilizar banco em memória ou contêiner efêmero (ex.: `Testcontainers`) para testar repositórios EF.
   * Smoke tests para S3 (quando possível, apontando para **LocalStack** ou **MinIO** em ambiente local).

4. **Ferramentas sugeridas:**

   * **xUnit** (ou NUnit/MSTest)
   * **FluentAssertions**
   * **Moq** (ou NSubstitute)
   * **Bogus** (dados fake)

---

## 🧩 Conclusão

O projeto reflete boas práticas de **arquitetura limpa** e **escala sustentável**: responsabilidades bem definidas, mensageria desacoplada, storage externo para arquivos e banco relacional para consistência. A solução está pronta para evoluir, com pontos claros para inclusão de **testes automatizados** e novas funcionalidades.

---

### 🧑‍💻 Autor

Desenvolvido por **Filipe**
Contato: [LinkedIn](https://www.linkedin.com/in/mrffilipe/)

