
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
  git clone https://github.com/renatofreitasRR/GestaoProdutos.git
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

#### Retorna todos os produtos ativos

```http
  GET /api/Product/GetAllActivePaged
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `PageNumber`      | `int` |  Número da página |
| `PageSize`      | `int` |  Quantidade de elementos a serem retornados |
| `Search`      | `string` |  Campo de busca por descrição do produto |

#### Retorna todos os produtos ativos

```http
  GET /api/Product/GetAllInactivePaged
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `PageNumber`      | `int` |  Número da página |
| `PageSize`      | `int` |  Quantidade de elementos a serem retornados |
| `Search`      | `string` |  Campo de busca por descrição do produto |


#### Retorna um produto

```http
  GET /api/Product/GetById/${id}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Obrigatório**. O ID do produto |

#### Criar um Produto

```http
  POST /api/Product/Post
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `Description`      | `string` |**Obrigatório** Descrição do produto|
| `DueDate`      | `DateTimme` | Data de Vencimento|
| `ManufacturingDate`      | `DateTime` |Data da Fabricação do Produto|
| `SupplierCode`      | `string` |Código do Fornecedor|
| `SupplierDescription`      | `string` |Descrição do Fornecedor|
| `SupplierDocument`      | `string` |CNPJ do Fornecedor|

#### Editar um Produto

```http
  PUT /api/Product/Put
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Obrigatório**. O ID do produto |
| `Description`      | `string` |**Obrigatório** Descrição do produto|
| `DueDate`      | `DateTimme` | Data de Vencimento|
| `ManufacturingDate`      | `DateTime` |Data da Fabricação do Produto|
| `SupplierCode`      | `string` |Código do Fornecedor|
| `SupplierDescription`      | `string` |Descrição do Fornecedor|
| `SupplierDocument`      | `string` |CNPJ do Fornecedor|


#### Ativar um Produto

```http
  PATCH /api/Product/ActiveProduct
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Obrigatório**. O ID do produto |


#### Desativar um Produto

```http
  PATCH /api/Product/InactiveProduct
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Obrigatório**. O ID do produto |







