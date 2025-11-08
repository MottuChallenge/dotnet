## 👥 INTEGRANTES DO GRUPO

- RM559064 - Pedro Henrique dos Santos
- RM556182 - Vinícius de Oliveira Coutinho
- RM557992 - Thiago Thomaz Sales Conceição

---

## 🎯 PROBLEMA A SER RESOLVIDO

A Mottu enfrenta dificuldades para localizar e gerenciar com precisão as motos estacionadas em seus pátios. O processo atual é manual, sujeito a erros e impacta negativamente a eficiência operacional e o controle de ativos.

---

# 🏍️ Mottu Challenge - Gestão de Pátio e Setores

Este projeto implementa um sistema de **gestão de pátio (Yard)**, **setores (Sector)** e **vagas (Spots)** para organização e alocação de motos.  
O objetivo é permitir que filiais da Mottu consigam estruturar seus pátios em setores e, automaticamente, gerar as vagas disponíveis para as motos.

Desenvolveremos uma API RESTful para registrar, atualizar e consultar a localização das motos em tempo real nos pátios da Mottu. O sistema permitirá:

- Cadastro e atualização de motos, pátios, seções e filiais.
- Consulta rápida da localização de cada moto.
- Integração com cameras e IA para verificar se um setor esta cheio e com base nisso aconselhar a criar outros setores ou mudar de patio as motos que chegaram com esse modelo especifico, tambem para localizar uma moto especifica
- Facilidade de integração com outros sistemas internos da Mottu.
- Tera um sistema alerta onde quando uma moto estiver perto de sua revisão avisara a um prestador de serviço da mottu para adicionar essa moto a um setor de revisão

Essa solução trará mais agilidade, precisão e controle para a operação, reduzindo erros e otimizando o uso dos recursos.

---

## 📌 Domínio

- **Yard (Pátio)**  
  Representa um espaço físico de uma filial, que pode conter múltiplos setores.  
  Cada pátio possui dimensões e restrições de coordenadas.

- **Sector (Setor)**  
  Representa uma área dentro de um pátio.  
  É definido por pontos (polígono), e a partir dele são geradas vagas (spots).  
  O sistema valida se o setor:
  - Está contido dentro do pátio.  
  - Não se sobrepõe a outros setores do mesmo pátio.  

- **Spot (Vaga)**  
  Representa uma vaga de moto dentro de um setor.  
  Por padrão, cada vaga ocupa um espaço de **2m x 2m**.
  Exemplo: um setor de 10m x 10m comporta 25 vagas.
  
- **Motorcycle (Motocicleta)**
  A motocicleta é a principal entidade do negócio, pois é o objeto que precisa ser cadastrado, alocado e movimentado dentro dos setores e pátios. Todas as operações de gestão convergem para ela.

---

## 🏗️ Justificativa da Arquitetura  

O sistema foi desenvolvido utilizando **.NET 8 com arquitetura em camadas (Clean Architecture/DDD)**, separando responsabilidades entre:  

- **Domain** → contém as entidades centrais do negócio (Motorcycle, Yard, Sector).  
- **Application** → contém os casos de uso, DTOs e interfaces de repositórios.  
- **Infrastructure** → implementação da persistência (Entity Framework Core).  
- **API** → camada de apresentação, responsável pela exposição dos endpoints via controllers RESTful.  

Essa organização garante:  
- **Escalabilidade** → fácil manutenção e extensão do projeto.  
- **Testabilidade** → com a separação clara de responsabilidades, os testes de unidade e integração são simplificados.  
- **Flexibilidade** → é possível trocar o banco de dados ou tecnologia de infraestrutura sem alterar as regras de negócio.  

---

## ⚙️ Instruções de Execução

### Pré-requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)

**Nota**: O EF Core CLI será instalado automaticamente no passo 2 das instruções.

### Passos
1. Clone o repositório:
   ```bash
     git clone https://github.com/MottuChallenge/dotnet.git
   ```
2. Instale o EF Core CLI globalmente (se ainda não tiver):
   ```bash
     dotnet tool install --global dotnet-ef
   ```
3. Entre na pasta do projeto e rode o comando do docker compose para subir um banco mysql no docker:
   ```bash
     cd .\dotnet\
     docker-compose up -d
   ```
4. Rode o comando do database update para lançar as migrations no banco:
   ```bash
     dotnet ef database update --startup-project MottuChallenge.Api --project MottuChallenge.Infrastructure
   ```
4. Se quiser deixar o banco populado com alguns registros, use uma das opções abaixo:

   **Opção A - Usando Docker (recomendado):**
   ```bash
   docker exec -i mysql mysql -u user_test -puser_password MottuGridDb < .\mysql-init\init.sql
   ```

   **Opção B - Copiando arquivo para o container:**
   ```bash
   docker cp .\mysql-init\init.sql mysql:/tmp/init.sql
   docker exec mysql mysql -u user_test -puser_password MottuGridDb -e "source /tmp/init.sql"
   ```

   **Opção C - Se tiver cliente MySQL instalado:**
   ```bash
   mysql -h 127.0.0.1 -P 3307 -u user_test -p MottuGridDb < .\mysql-init\init.sql
   # aqui vai pedir para colocar a senha: user_password
   ```
5. Rode o programa:
   ```bash
    dotnet run --project MottuChallenge.Api
   # Abra a url no navegador http://localhost:5006/swagger/index.html
   ``` 

---

## Testes

POST /api/yards
Content-Type: application/json

```json
{
  "name": "Pátio Central",
  "cep": "01311300",
  "number": "100",
  "points": [
    { "pointOrder": 1, "x": 0, "y": 0 },
    { "pointOrder": 2, "x": 0, "y": 50 },
    { "pointOrder": 3, "x": 50, "y": 50 },
    { "pointOrder": 4, "x": 50, "y": 0 }
  ]
}

```
Aqui ele usa a api do via cep para buscar o endereço da pessoa

PUT /api/yards/{id}
Content-Type: application/json

```json
{
  "name": "Pátio Central Renovado"
}
```

DELETE /api/yards/{id}


POST /api/sectors_type
Content-Type: application/json

```json
{
  "name": "Estacionamento"
}
```

Tem validação se ja existe sector_type com esse nome

POST /api/sectors
Content-Type: application/json

```json
{
  "yardId": "id gerado quando cria o yard",
  "sectorTypeId": "id gerado quando cria sectorType",
  "points": [
    {
      "pointOrder": 1,
      "x": 1,
      "y": 1
    },
    {
      "pointOrder": 2,
      "x": 1,
      "y": 10
    },
    {
      "pointOrder": 3,
      "x": 10,
      "y": 10
    },
    {
      "pointOrder": 4,
      "x": 10,
      "y": 1
    }
  ]
}
```

com isso sera gerado o maximo de vagas disponiveis para a dimensão do setor, aqui tem validação se o setor cabe dentro do patio ou se ja tem um setor cadastrado nesse lugar.

PUT /api/sectors/{id}
Content-Type: application/json

```json
{
  "sectorTypeId": "9a0d3c5a-5eab-4b2b-a28a-12b9df312345"
}
```

DELETE /api/sectors/{id}

POST /api/motorcycles
Content-Type: application/json

```json
{
  "model": "Honda CG 160",
  "engineType": 0,
  "plate": "ABC-1234",
  "lastRevisionDate": "2025-09-20T00:00:00",
  "spotId": "c0a80123-4567-890a-bcde-f1234567890a"
}
```

GET /api/motorcycles?page=1&pageSize=10

PUT /api/motorcycles/{id}
Content-Type: application/json

```json
{
  "model": "Honda CG 160 Titan",
  "engineType": 0,
  "plate": "ABC-1234",
  "lastRevisionDate": "2025-09-22T00:00:00",
  "spotId": "c0a80123-4567-890a-bcde-f1234567890a"
}
```

DELETE /api/motorcycles/{id}

---

## 🔐 Autenticação - Employee (register / login)

O sistema possui endpoints para registro e autenticação de funcionários (Employees). Eles são expostos no controller `AuthController` e estão versionados como v1.

- Registrar (POST): `/api/v1/auth/register`
  - Body: `CreateEmployeeRequest`
  - Campos: `Name`, `Email`, `YardId` (Guid), `Password`

Exemplo de body para registro:

```json
{
  "Name": "João Silva",
  "Email": "joao@example.com",
  "YardId": "c56a4180-65aa-42ec-a945-5fd21dec0538",
  "Password": "StrongPassword123"
}
```

- Login (POST): `/api/v1/auth/login`
  - Body: `LoginRequest` com `Email` e `Password`
  - Resposta: string contendo o token JWT (200 OK) ou `401` em credenciais inválidas

Exemplo de body para login:

```json
{
  "Email": "joao@example.com",
  "Password": "StrongPassword123"
}
```

Uso do token JWT
- O endpoint de login retorna um token JWT como texto. Para chamar endpoints protegidos (quando aplicável), adicione o header HTTP:

```
Authorization: Bearer <token_aqui>
```

- No Swagger UI você pode usar o botão "Authorize" (se habilitado) e colar `Bearer <token>` para testar rotas autenticadas.

Notas:
- O controller de autenticação está em `api/v1/auth` (versão 1). A API de ML (recomendação) usa versão 2: `/api/v2/ml/recommend-spot`.

## 🧠 ML - Recomendação de vaga (recommend-spot)

Adicionado um endpoint de recomendação que, dado um setor e uma data de revisão, sugere a melhor vaga (spot) e um score de confiança.

- Rota (POST): `/api/v2/ml/recommend-spot`
- Body: `RecommendSpotRequest` com os campos abaixo.

Exemplo de body (use no Swagger ou via curl). O formato usa ISO-8601 para a data e `Guid` para o `SectorId`.

```json
{
  "ReviewDate": "2025-11-08T10:00:00Z",
  "SectorId": "123f846a-b2b2-11f0-a6f4-aa8c626e8990"
}
```

Também funciona com keys em camelCase (o JSON binder do .NET lida com ambas):

```json
{
  "reviewDate": "2025-11-08T10:00:00Z",
  "sectorId": "123f846a-b2b2-11f0-a6f4-aa8c626e8990"
}
```

Exemplo de resposta (200 OK):

```json
{
  "spotId": "c0a80123-4567-890a-bcde-f1234567890a",
  "score": 0.85
}
```

Notas rápidas:
- O endpoint está versionado como v2 (veja a rota /api/v2/).
- Se não houver recomendação disponível será retornado `404` com a mensagem "No recommendation available".
- Para testar localmente abra o Swagger (normalmente em `http://localhost:5006/swagger/index.html`) e use a rota `POST /api/v2/ml/recommend-spot`.

Se quiser que eu acrescente exemplos curl prontos ou mais opções de datas (ex.: apenas data sem hora, datas futuras/passadas), eu preparo os comandos tbm.




  
