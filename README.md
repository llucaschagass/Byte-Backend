# 🍴 Projeto Byte - Backend

O **Byte** é um sistema de gerenciamento de comandas e automação para restaurantes, desenvolvido como projeto de conclusão de curso para Engenharia da Computação. O nome faz um trocadilho entre o termo gastronômico (*bite* / mordida) e a unidade de dados tecnológica (**byte**), refletindo a integração entre software e hardware.

---

## 🚀 Tecnologias Utilizadas

- **Linguagem:** C# (.NET 10)
- **Framework Web:** ASP.NET Core Web API (Controllers)
- **Banco de Dados:** PostgreSQL
- **ORM:** Entity Framework Core (EF Core)
- **Containerização:** Docker & Docker Compose
- **Documentação:** Swagger (OpenAPI)

---

## 🏗️ Arquitetura e Organização

O projeto segue uma arquitetura em camadas, visando organização, manutenção e escalabilidade:

- **Controllers**  
  Portas de entrada da API, responsáveis por expor endpoints RESTful e receber as requisições do cliente.

- **Services**  
  Camada onde ficam concentradas as regras de negócio e orquestração dos fluxos da aplicação.

- **Data**  
  Responsável pela persistência dos dados, utilizando o **Entity Framework Core**, incluindo:
  - `DbContext`
  - Migrations
  - Configurações de mapeamento

- **Entities**  
  Modelagem das tabelas do banco de dados, com herança de uma entidade base comum (ex.: auditoria, identificadores, datas).

---

## 🔌 Integração com Hardware (Futuro)

O sistema foi projetado para futura integração com hardware utilizando **Arduino (C++)**. A ideia é permitir que cartões magnéticos ou **RFID** funcionem como comandas físicas, comunicando-se em tempo real com o backend para:

- Abertura de comandas
- Identificação de clientes
- Lançamento de consumos
- Fechamento e controle de pagamentos

---

## 🛠️ Como rodar o ambiente de desenvolvimento

### ✅ Pré-requisitos

- Docker Desktop instalado e em execução
- SDK do **.NET 10**
- IDE para desenvolvimento  
  👉 Recomendado: **JetBrains Rider** (mas Visual Studio ou VS Code também funcionam)

---

### ▶️ Passo a passo

#### 1️⃣ Subir o banco de dados (Infraestrutura)

Na raiz do projeto, onde se encontra o arquivo `docker-compose.yml`, execute:

```bash
docker-compose up -d
```

Isso irá iniciar o container do PostgreSQL necessário para a aplicação.

---

#### 2️⃣ Restaurar dependências do NuGet

```bash
dotnet restore
```

---

#### 3️⃣ Executar a API

```bash
dotnet run --project Byte-Backend
```

---

## 📄 Documentação da API

Após subir a aplicação, a documentação interativa estará disponível via **Swagger**, permitindo testar os endpoints diretamente pelo navegador.

Geralmente acessível em:

```
http://localhost:5000/swagger
```

*(A porta pode variar conforme configuração do projeto)*

---

## 📌 Observações

- Este projeto faz parte de um **Trabalho de Conclusão de Curso (TCC)**.
- O foco principal está na integração entre **software backend**, **banco de dados** e **automação com hardware**.
- A arquitetura foi pensada para facilitar futuras evoluções, como autenticação, multi-restaurantes e integração com dispositivos físicos.

---

🍽️ **Projeto Byte** — Tecnologia e automação servidas na medida certa.

