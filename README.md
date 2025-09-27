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
- Banco de dados MySQL
- [EF Core CLI](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)

### Passos
1. Clone o repositório:
   ```bash
   git clone https://github.com/MottuChallenge/dotnet.git
   ```
   Abra a solução
2. Entre na pasta do MottuChallenge.Api e rode o comando 
  ```bash
  docker-compose up -d
  ```
   esse comando subira um banco mysql no docker
3. Abra o Package menager console que fica no tools NuGet Package Menage e rode esse comando
   ```bash
   Update-Database -Project MottuChallenge.Infrastructure -StartupProject MottuChallenge.Api
   ```
4. Depois de dar um Update-Database rode esse comando no terminal
   ```bash
   mysql -h 127.0.0.1 -P 3306 -u root -p MottuGridDb < .\mysql-init\init.sq
   ```
   para isso voce deve estar na pasta MottuChallenge.Api
   Esse comando irá popular o banco inicialmente
5. Start o Programa

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




  
