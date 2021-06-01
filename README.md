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

## Coisas para se melhorar no projeto

- Utilização de um servidor de identidade (IdentityServer4)
	- Criptografia de senha
	- controle de acessos
	- permissionamento
	- etc
- Fila/Mensageria (Devido a falta de tempo as integrações tiveram que ser em REST)
- Fluxo assincrono ao realizar o pedido de resgate (Criar uma estrutura que permitisse o processamento de um pedido de forma assincrona levaria mais tempo que o disponivel)
- Logs com persistencia em algum storage
- Controle de exibição de erros e tratamento de exceções
- gRPC/Melhor configuração do Ocelot
- Swagger no ApiGateway
- Testes Unitários
