# PB_api

# Configurando o SQL Server

Neste projeto, utilizei docker para o SQL Server.
Você pode encontrar a imagem que utilizei [aqui](https://hub.docker.com/_/microsoft-mssql-server)

Ou 

Apenas execute o comando no terminal
`docker pull mcr.microsoft.com/mssql/server`

Para iniciar a imagem, execute o seguinte comando no terminal
`docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=1q2w3e4r!@#$' -p 1433:1433 -d mcr.microsoft.com/mssql/server`

Com isso você terá o sql server sendo executado no Docker com o usuário **sa** e senha **1q2w3e4r!@#$**