# MyPoints (Micro-Services)
para rodar o projeto: 
  1. Instalar docker e docker-compose
  2. Rodar os comandos no console (Powershell no Windows)
  
  ```console
  docker-compose build
  docker-compose up
  ```
## Collection do Postman para facilitar as requisições
https://www.getpostman.com/collections/65f4d509c604224cd4af
- Sequência para efetuar resgate de produtos
	- Cadastrar Usuario ```POST - http://localhost:3000/api/identity/User```
	- Cadastrar endereço (caso queira utilizar endereço padrão) ```POST http://localhost:3000/api/identity/User/Address```
	- Adicionar recarga ```POST - http://localhost:3000/api/Account/Transaction/Recharge```
	- Listar Produtos ```GET - http://localhost:3000/api/Catalog/Product/Available```
	- Realizar Resgate ```POST - http://localhost:3000/api/Catalog/Order```	
	- Visualizar Pedidos ```GET - http://localhost:3000/api/Catalog/Order```
	- Reprocessar Pedido ```POST - http://localhost:3000/api/Catalog/Order/Reprocess``` --reprocessamento da transação (caso não tenha saldo/endereço cadastrado)
	- Visualizar saldo/extrato ```GET - http://localhost:3000/api/Account/Transaction```



## Coisas para se melhorar no projeto

- [ ] Utilização de um servidor de identidade (IdentityServer4)
	- [ ] Criptografia de senha
	- [ ] controle de acessos
	- [ ] permissionamento
	- [ ] etc
- [x] Pedido com status de entrega
- [x] Fila/Mensageria 
- [x] Fluxo assincrono ao realizar o pedido de resgate (Criar uma estrutura que permitisse o processamento de um pedido de forma assincrona levaria mais tempo que o disponivel)
	- [x]  Receber o pedido
	- [x]  Validar dados
	- [x]  Realizar cobrança
	- [x]  Aprovar Resgate
	- [x] Liberar para entrega
- [ ] Separar Cada Handler em uma microservice
- [ ] Logs com persistencia em algum storage
- [ ] Controle de exibição de erros e tratamento de exceções
- [ ] gRPC/Melhor configuração do Ocelot
- [x] Swagger nos Micro serviços
- [ ] Swagger no ApiGateway
- [ ] Testes Unitários
	- [x] Identity
	- [ ] Catalog
	- [ ] Account
