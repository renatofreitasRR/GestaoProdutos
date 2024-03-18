
# Gestão de Produtos

Sistema para gestão de produtos


## Stack utilizada

**Back-end:** .NET 8, Docker

**Banco de dados:** SQL Server


## Uso de Tecnologias

- FluentValidation para validação dos métodos de POST e PUT
- Documentação dos End-Points com Swagger
- XUnit para testes unitários



## Rodando localmente

Clone o projeto

```bash
  git clone https://github.com/renatofreitasRR/PortfolioInvestimentos.git
```

Abra o Projeto com o Visual Studio

Selecione o Docker Compose como Projeto de Inicialização

Inicie o servidor




## Documentação Entidades

### Product

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `Id`      | `int` |Código do Produto|
| `Description`      | `string` |Descrição do produto|
| `IsActive`      | `bool` | Produto está ou não ativo |
| `QuantityAvailable`      | `int` |Quantidade disponível |
| `DueDate`      | `DateTimme` | Data de Vencimento|
| `ManufacturingDate`      | `DateTime` |Data da Fabricação do Produto|
| `SupplierCode`      | `string` |Código do Fornecedor|
| `SupplierDescription`      | `string` |Descrição do Fornecedor|
| `SupplierDocument`      | `string` |CNPJ do Fornecedor|





## Documentação da API


### Retorno
#### Toda end-point tem o retorno padrão no formato: 

```http
  {
    Data: object,
    Errors: List<string>
  }
```

### Exceções
#### Para toda exceção retornamos no mesmo formato

```http
  {
    Data: null,
    Errors: List<string>
  }
```

### Products

#### Retorna todos os produtos
Acesso: Manager, Client

```http
  GET /api/Product/GetAllPaged
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `PageNumber`      | `int` |  Número da página |
| `PageSize`      | `int` |  Quantidade de elementos a serem retornados |


#### Retorna um produto
Acesso: Manager, Client

```http
  GET /api/Product/GetById/${id}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `string` | **Obrigatório**. O ID do produto |

#### Criar um Produto
Acesso: Manager
```http
  POST /api/Product/Post
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `Name`      | `string` | **Obrigatório**. Nome do produto|
| `Type`      | `ProductType` | **Obrigatório**. Tipo do produto |
| `Value`      | `decimal` | **Obrigatório**. Valor do produto |
| `QuantityAvailable`      | `int` | **Obrigatório**. Quantidade disponível |
| `DueDate`      | `DateTimme` | **Obrigatório**.  Data de Vencimento|

#### Editar um Produto
Acesso: Manager

```http
  PUT /api/Product/Put
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `Id`      | `int` | **Obrigatório**. Id do Produto|
| `Name`      | `string` | **Obrigatório**. Nome do produto|
| `Type`      | `ProductType` | **Obrigatório**. Tipo do produto |
| `Value`      | `decimal` | **Obrigatório**. Valor do produto |
| `QuantityAvailable`      | `int` | **Obrigatório**. Quantidade disponível |
| `DueDate`      | `DateTimme` | **Obrigatório**.  Data de Vencimento|
| `IsActive`      | `bool` | **Obrigatório**.  Produto está ativo|







