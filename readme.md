# IDEIA DO PROJETO - CP2 - ADVANCED BUSINESS DEVELOPMENT WITH .NET

Este documento apresenta a proposta do grupo para o projeto de CP2, baseado no desafio da Mottu.

---

## 👥 INTEGRANTES DO GRUPO

- RM559064 - Pedro Henrique dos Santos
- RM556182 - Vinícius de Oliveira Coutinho
- RM557992 - Thiago Thomaz Sales Conceição

---

## 📘 TÍTULO DO PROJETO

MOTTU GRID

---

## 🎯 PROBLEMA A SER RESOLVIDO

A Mottu enfrenta dificuldades para localizar e gerenciar com precisão as motos estacionadas em seus pátios. O processo atual é manual, sujeito a erros e impacta negativamente a eficiência operacional e o controle de ativos.

---

## 💡 SOLUÇÃO PROPOSTA

Desenvolveremos uma API RESTful para registrar, atualizar e consultar a localização das motos em tempo real nos pátios da Mottu. O sistema permitirá:

- Cadastro e atualização de motos, pátios, seções e filiais.
- Consulta rápida da localização de cada moto.
- Integração com cameras e IA para verificar se um setor esta cheio e com base nisso aconselhar a criar outros setores ou mudar de patio as motos que chegaram com esse modelo especifico, tambem para localizar uma moto especifica
- Facilidade de integração com outros sistemas internos da Mottu.
- Tera um sistema alerta onde quando uma moto estiver perto de sua revisão avisara a um prestador de serviço da mottu para adicionar essa moto a um setor de revisão

Essa solução trará mais agilidade, precisão e controle para a operação, reduzindo erros e otimizando o uso dos recursos.

---

## 📐 ENTIDADES PRINCIPAIS

- **Moto**: placa, modelo, tipo de motor, status, seção, última revisão.
- **Pátio**: nome, área, endereço, filial associada.
- **Seção**: cor, área, pátio associado.
- **Filial**: nome, CNPJ, telefone, e-mail, endereço, pátios associados.
- **Endereço**: rua, número, bairro, cidade, estado, CEP, país.

---

## 🛠 TECNOLOGIAS E ESTRUTURA

- .NET 8
- Entity Framework Core com Oracle
- Swagger/OpenAPI para documentação
- Clean Architecture
- DTOs para comunicação entre camadas
- MappingConfig para mapeamento de entidades
- Migrations para versionamento do banco de dados

---

## Endpoints Principais

### Yards (Pátios)
| Método | Rota               | Descrição                       |
|--------|--------------------|---------------------------------|
| GET    | /api/yards         | Lista todos os pátios           |
| GET    | /api/yards/{id}    | Busca pátio por ID              |
| POST   | /api/yards         | Cria um novo pátio              |
| PUT    | /api/yards/{id}    | Atualiza um pátio existente     |
| DELETE | /api/yards/{id}    | Remove um pátio                 |

---

### Sections (Setores)
| Método | Rota                 | Descrição                     |
|--------|----------------------|-------------------------------|
| GET    | /api/sections        | Lista todos os setores        |
| GET    | /api/sections/{id}   | Busca setor por ID            |
| POST   | /api/sections        | Cria um novo setor            |
| PUT    | /api/sections/{id}   | Atualiza um setor existente   |
| DELETE | /api/sections/{id}   | Remove um setor               |

---

### Motorcycles (Motocicletas)
| Método | Rota                   | Descrição                   |
|--------|------------------------|-----------------------------|
| GET    | /api/motorcycles       | Lista todas as motos        |
| GET    | /api/motorcycles/{id}  | Busca moto por ID           |
| POST   | /api/motorcycles       | Cadastra uma nova moto      |
| PUT    | /api/motorcycles/{id}  | Atualiza dados da moto      |
| DELETE | /api/motorcycles/{id}  | Remove uma moto             |

---

### Logs
| Método | Rota              | Descrição                        |
|--------|-------------------|----------------------------------|
| GET    | /api/logs         | Lista todos os logs              |
| GET    | /api/logs/{id}    | Busca log por ID                 |
| POST   | /api/logs         | Cria um novo log                 |
| PUT    | /api/logs/{id}    | Atualiza dados de um Log         |
| DELETE | /api/logs/{id}    | Remove um log                    |

---

### Branches
| Método | Rota              | Descrição                        |
|--------|-------------------|----------------------------------|
| GET    | /api/branches         | lista de todas as filiais    |
| GET    | /api/branches/{id}    | Busca filial por ID          |
| POST   | /api/branches         | Cria uma nova filial         |
| PUT    | /api/branches/{id}    | Atualiza dados de uma filial |
| DELETE | /api/branches/{id}    | Remove uma filial            |

---

## 🚀 COMO RODAR O PROJETO

Abra o projeto no Visual Studio 

### 1. Configurar a string de conexão

No arquivo `appsettings.json`, configure a string de conexão para o banco Oracle na seção `ConnectionStrings`:

```json
"ConnectionStrings": {
  "Oracle": "Data Source=oracle.fiap.com.br:1521/orcl;User ID=SEU_USUARIO;Password=SUA_SENHA;"
}
```
### 2. Criar Tabelas no Banco
No seu Terminal de um comando que é Update-Database.
Com esse comando seu banco ira criar as tabelas automaticamente.

### 3. Rodar o Projeto e abrir o swagger no Navegador

Depois é so rodar o projeto e abrir o endereço do swagger no seu navegador
Endereço Normalmente é esse https://localhost:5001/swagger mas se não estiver nesse olhe a porta que seu Visual Studio abriu.

---

## 🚀 COMO RODAR O PROJETO VIA DOCKER

### 1. Baixar o Docker se não tiver

Baixe o Docker Desktop e instale em sua máquina.

### 2. Criar Container

Crie um container com o seguinte comando:

```bash
	docker run -d --name mottu-grid -p 8080:80 pedrohenrique32/mottu-grid-dotnet
```

---

## Script Azure CLI

1 - az group create -l eastus -n rg-vm-challenge
 
2 - az vm create --resource-group rg-vm-challenge --name vm-challenge --image Canonical:ubuntu-24_04-lts:minimal:24.04.202505020 --size Standard_B2s --admin-username admin_fiap --admin-password admin_fiap@123

4 - az network nsg rule create --resource-group rg-vm-challenge --nsg-name vm-challengeNSG --name port_8080 --protocol tcp --priority 1010 --destination-port-range 8080
 
5 - az network nsg rule create --resource-group rg-vm-challenge --nsg-name vm-challengeNSG --name port_80 --protocol tcp --priority 1020 --destination-port-range 80
 
6 - Instalação do Docker na máquina Linux

<pre><code>sudo apt-get update
	
sudo apt-get install ca-certificates curl 
	
sudo install -m 0755 -d /etc/apt/keyrings 
	
sudo curl -fsSL https://download.docker.com/linux/ubuntu/gpg -o /etc/apt/keyrings/docker.asc 
	
sudo chmod a+r /etc/apt/keyrings/docker.asc
echo \
"deb [arch=$(dpkg --print-architecture) signed-by=/etc/apt/keyrings/docker.asc] https://download.docker.com/linux/ubuntu \ 
$(. /etc/os-release && echo \"${UBUNTU_CODENAME:-$VERSION_CODENAME}\") stable" | \ 
sudo tee /etc/apt/sources.list.d/docker.list > /dev/null
	
sudo apt-get update

sudo apt-get install docker-ce docker-ce-cli containerd.io docker-buildx-plugin docker-compose-plugin

sudo usermod -aG docker $USER

newgrp docker
	
</code></pre>

7 - rodar o projeto

```bash
docker run --name mottu -d -p 8080:80 pedrohenrique32/mottu-grid-dotnet
```

## 📌 OBSERVAÇÕES FINAIS

O projeto foi estruturado para facilitar a manutenção, escalabilidade e integração com tecnologias futuras, como IA. O uso de padrões modernos garante robustez e flexibilidade para atender às necessidades da Mottu, promovendo maior controle e eficiência operacional.
