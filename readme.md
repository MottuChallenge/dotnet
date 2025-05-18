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

## 📌 OBSERVAÇÕES FINAIS

O projeto foi estruturado para facilitar a manutenção, escalabilidade e integração com tecnologias futuras, como IA. O uso de padrões modernos garante robustez e flexibilidade para atender às necessidades da Mottu, promovendo maior controle e eficiência operacional.
