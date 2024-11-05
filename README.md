# ApiProject

## Tecnologias
Projeto de teste construído com .net core 8 / SqlServer 2019 / Docker

Feito utilizando `CodeFirst DDD contém testes e boas práticas de desenvolvimento.`

Consiste em um sistema para controlar o fluxo de caixa, com lançamentos de operações e conciliação diária com o relatório dos mesmos.

## Executando
Para executar a aplicação basta usar o docker-compose -d

## Escolha das tecnologias
- SqlServer foi utilizado para termos integridade dos dados com os clientes/produtos/vendedores.

``
Os dados das tabelas que não fazem parte do domínio (clientes/produtos/vendedores) devem ser carregados através de messageria, ou seja, teriamos outros serviços que fazem o CRUD destes dados e receberiamos as atualizações com os dados basicos utilizados.
``

## Outras tecnologias que poderiam ser usadas
- Fluent Validation
- AutoMapper
- Hangfire
- Cache dos dados das tabelas auxiliares (clientes/produtos/vendedores)
- Autenticação e autorização `Token JWT`
- Messageria ou NoSQL para armazenar as operações e depois elas serem processadas
 > `Isto garantiria não perder as operações mesmo em momentos de sobrecarga, mas aumentaria a complexida inclusive para quem for utilizar o serviço, pois caso a operação demorasse mais de 3s para responder iriamos enviar um token (GUID) para poder confirmar o status da operação.`

## Processos
### Operações
/v1/Operations (POST, DELETE)

> Foram criadas as operações de Adicionar e remover

#### *Validações por:*

* Produto
* Vendedor
* Client `pode ser nulo`
* Valor
* Tipo da Operação `Credito = 1, Debito = 2`
* Data

### Conciliação
- Para a consolidação dos dados apenas criei um endpoint para isto, mas poderiamos usar ``HangFire`` para agendar um horário para execução e pode ser um outro serviço exlusivo para isto ou um outro `POD` apenas para isto
- /v1/Balance/Consolidate
  > usado para fazer a consolidação das operações `faz a consolidação de todos os dias desde a última vez que foi feita, na primeira execução faz apenas do dia anterior e do dia atual.`

- Para os relatórios temos alguns endpoints
  A data nestes endpoints deve ser informada no formato `dd-MM-yyyy`
> [!NOTE]
> Faltou incluir a paginação nos endpoints com listas.
  
- /v1/Balance/{date}
  > Retorna os dados consolidados geral sem as operações 
- /v1/Balance/{date}/ByClients
  > Retorna os dados consolidados com as operações dos clientes 

- /v1/Balance/{date}/ByProducts
  > Retorna os dados consolidados com as operações por produtos 

- /v1/Balance/{date}/BySellers
  > Retorna os dados consolidados com as operações por vendedores

