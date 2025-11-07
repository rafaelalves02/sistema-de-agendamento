# Configuração do Banco de Dados

Este documento descreve como configurar o banco de dados para o Sistema de Agendamento, suportando tanto **MySQL** quanto **SQL Server**.

## Índice

- [Visão Geral](#visão-geral)
- [Configuração do Provider](#configuração-do-provider)
- [Variáveis de Ambiente](#variáveis-de-ambiente)
  - [MySQL](#mysql)
  - [SQL Server](#sql-server)
- [Exemplos de Configuração](#exemplos-de-configuração)
  - [Windows](#windows)
  - [Linux/Mac](#linuxmac)
  - [Visual Studio / Rider](#visual-studio--rider)
  - [Docker](#docker)
- [Validação](#validação)
- [Troubleshooting](#troubleshooting)

## Visão Geral

O sistema suporta dois providers de banco de dados:

- **MySQL** (padrão)
- **SQL Server**

As connection strings são armazenadas exclusivamente em **variáveis de ambiente** por questões de segurança, enquanto o provider do banco de dados pode ser configurado no arquivo `appsettings.json`.

## Configuração do Provider

O provider do banco de dados é configurado no arquivo `appsettings.json` através da propriedade `DatabaseProvider`:

```json
{
  "DatabaseProvider": "mysql"
}
```

Valores aceitos:
- `"mysql"` - Para usar MySQL (padrão se não especificado)
- `"sqlserver"` - Para usar SQL Server

## Variáveis de Ambiente

As connection strings devem ser configuradas como variáveis de ambiente. O nome das variáveis depende do provider escolhido.

### MySQL

**Nome da Variável:** `SISTEMA_AGENDAMENTO_MYSQL_CONNECTION_STRING`

**Formato da Connection String:**
```
Server=localhost;Database=nome_do_banco;Uid=usuario;Pwd=senha;Port=3306;
```

**Exemplo:**
```
Server=localhost;Database=sistema_agendamento;Uid=root;Pwd=minhasenha;Port=3306;
```

**Parâmetros comuns:**
- `Server` - Endereço do servidor (ex: localhost, 192.168.1.100)
- `Database` - Nome do banco de dados
- `Uid` - Usuário do banco de dados
- `Pwd` - Senha do banco de dados
- `Port` - Porta (padrão: 3306)
- `SslMode` - Modo SSL (ex: None, Required, Preferred)

### SQL Server

**Nome da Variável:** `SISTEMA_AGENDAMENTO_SQLSERVER_CONNECTION_STRING`

**Formato da Connection String:**
```
Server=localhost;Database=nome_do_banco;User Id=usuario;Password=senha;TrustServerCertificate=True;
```

**Exemplo:**
```
Server=localhost;Database=sistema_agendamento;User Id=sa;Password=minhasenha;TrustServerCertificate=True;
```

**Parâmetros comuns:**
- `Server` - Endereço do servidor (ex: localhost, localhost\\SQLEXPRESS, 192.168.1.100)
- `Database` - Nome do banco de dados
- `User Id` - Usuário do banco de dados
- `Password` - Senha do banco de dados
- `TrustServerCertificate` - Confiar no certificado do servidor (True/False)
- `Integrated Security` - Usar autenticação do Windows (True/False)
- `Encrypt` - Criptografar conexão (True/False)

## Exemplos de Configuração

### Windows

#### PowerShell (Temporário - apenas para a sessão atual)

```powershell
# Para MySQL
$env:SISTEMA_AGENDAMENTO_MYSQL_CONNECTION_STRING="Server=localhost;Database=sistema_agendamento;Uid=root;Pwd=senha123;Port=3306;"

# Para SQL Server
$env:SISTEMA_AGENDAMENTO_SQLSERVER_CONNECTION_STRING="Server=localhost;Database=sistema_agendamento;User Id=sa;Password=senha123;TrustServerCertificate=True;"

# Configurar o provider (opcional, padrão é mysql)
$env:DatabaseProvider="mysql"
```

#### PowerShell (Permanente - para o usuário)

```powershell
# Para MySQL
[System.Environment]::SetEnvironmentVariable("SISTEMA_AGENDAMENTO_MYSQL_CONNECTION_STRING", "Server=localhost;Database=sistema_agendamento;Uid=root;Pwd=senha123;Port=3306;", "User")

# Para SQL Server
[System.Environment]::SetEnvironmentVariable("SISTEMA_AGENDAMENTO_SQLSERVER_CONNECTION_STRING", "Server=localhost;Database=sistema_agendamento;User Id=sa;Password=senha123;TrustServerCertificate=True;", "User")

# Reinicie o terminal/IDE após configurar
```

#### CMD (Prompt de Comando)

```cmd
REM Para MySQL
setx SISTEMA_AGENDAMENTO_MYSQL_CONNECTION_STRING "Server=localhost;Database=sistema_agendamento;Uid=root;Pwd=senha123;Port=3306;"

REM Para SQL Server
setx SISTEMA_AGENDAMENTO_SQLSERVER_CONNECTION_STRING "Server=localhost;Database=sistema_agendamento;User Id=sa;Password=senha123;TrustServerCertificate=True;"

REM Reinicie o terminal/IDE após configurar
```

#### Interface Gráfica do Windows

1. Pressione `Win + R` e digite `sysdm.cpl`
2. Vá na aba **Avançado**
3. Clique em **Variáveis de Ambiente**
4. Em **Variáveis do usuário**, clique em **Novo**
5. Nome: `SISTEMA_AGENDAMENTO_MYSQL_CONNECTION_STRING`
6. Valor: `Server=localhost;Database=sistema_agendamento;Uid=root;Pwd=senha123;Port=3306;`
7. Clique em **OK** e reinicie o IDE/terminal

### Linux/Mac

#### Bash/Zsh (Temporário - apenas para a sessão atual)

```bash
# Para MySQL
export SISTEMA_AGENDAMENTO_MYSQL_CONNECTION_STRING="Server=localhost;Database=sistema_agendamento;Uid=root;Pwd=senha123;Port=3306;"

# Para SQL Server
export SISTEMA_AGENDAMENTO_SQLSERVER_CONNECTION_STRING="Server=localhost;Database=sistema_agendamento;User Id=sa;Password=senha123;TrustServerCertificate=True;"

# Configurar o provider (opcional)
export DatabaseProvider="mysql"
```

#### Bash/Zsh (Permanente)

Adicione as linhas acima no arquivo `~/.bashrc` ou `~/.zshrc`:

```bash
# Abrir o arquivo de configuração
nano ~/.bashrc  # ou ~/.zshrc

# Adicionar as variáveis
export SISTEMA_AGENDAMENTO_MYSQL_CONNECTION_STRING="Server=localhost;Database=sistema_agendamento;Uid=root;Pwd=senha123;Port=3306;"

# Recarregar o arquivo
source ~/.bashrc  # ou source ~/.zshrc
```

### Visual Studio / Rider

#### Visual Studio (launchSettings.json)

No arquivo `Properties/launchSettings.json`, adicione as variáveis no perfil de execução:

```json
{
  "profiles": {
    "SistemaDeAgendamento.Web": {
      "commandName": "Project",
      "environmentVariables": {
        "SISTEMA_AGENDAMENTO_MYSQL_CONNECTION_STRING": "Server=localhost;Database=sistema_agendamento;Uid=root;Pwd=senha123;Port=3306;",
        "DatabaseProvider": "mysql"
      }
    }
  }
}
```

#### JetBrains Rider

1. Vá em **Run** > **Edit Configurations**
2. Selecione a configuração do projeto
3. Em **Environment variables**, clique no ícone de edição
4. Adicione as variáveis:
   - Nome: `SISTEMA_AGENDAMENTO_MYSQL_CONNECTION_STRING`
   - Valor: `Server=localhost;Database=sistema_agendamento;Uid=root;Pwd=senha123;Port=3306;`
5. Clique em **OK**

### Docker

#### docker-compose.yml

```yaml
version: '3.8'

services:
  sistema-agendamento-web:
    build: .
    environment:
      - SISTEMA_AGENDAMENTO_MYSQL_CONNECTION_STRING=Server=mysql_db;Database=sistema_agendamento;Uid=root;Pwd=senha123;Port=3306;
      - DatabaseProvider=mysql
    depends_on:
      - mysql_db

  mysql_db:
    image: mysql:8.0
    environment:
      MYSQL_ROOT_PASSWORD: senha123
      MYSQL_DATABASE: sistema_agendamento
    ports:
      - "3306:3306"
```

#### Dockerfile

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# ... outras instruções ...

# As variáveis de ambiente podem ser passadas via docker run -e ou docker-compose.yml
ENV DatabaseProvider=mysql
```

#### Docker Run

```bash
docker run -e SISTEMA_AGENDAMENTO_MYSQL_CONNECTION_STRING="Server=host.docker.internal;Database=sistema_agendamento;Uid=root;Pwd=senha123;Port=3306;" \
           -e DatabaseProvider="mysql" \
           sistema-agendamento-web
```

## Validação

Ao iniciar a aplicação, o sistema valida automaticamente se as configurações estão corretas:

1. **Provider configurado?** - Verifica se `DatabaseProvider` está definido (padrão: mysql)
2. **Connection string configurada?** - Verifica se a variável de ambiente correspondente está definida

Se alguma validação falhar, a aplicação lançará uma exceção com uma mensagem clara indicando o que precisa ser configurado.

### Exemplo de Erro

```
InvalidOperationException: ConnectionString para MySQL não configurada. 
Configure a variável de ambiente 'SISTEMA_AGENDAMENTO_MYSQL_CONNECTION_STRING'
```

## Troubleshooting

### Problema: Variável de ambiente não é reconhecida

**Solução:**
1. Verifique se a variável foi configurada corretamente
2. **Reinicie o IDE/terminal** após configurar variáveis permanentes
3. No Windows, certifique-se de usar `setx` para variáveis permanentes (não `set`)
4. Verifique se não há espaços extras ou caracteres especiais no nome da variável

### Problema: Connection string inválida

**Solução:**
1. Verifique se todos os parâmetros estão corretos (Server, Database, User, Password)
2. Teste a connection string usando uma ferramenta de cliente de banco de dados
3. Verifique se o servidor de banco de dados está acessível
4. Para SQL Server, certifique-se de incluir `TrustServerCertificate=True` se necessário

### Problema: Provider não encontrado

**Solução:**
1. Verifique se o valor em `DatabaseProvider` está correto: `"mysql"` ou `"sqlserver"` (minúsculas)
2. Certifique-se de que o arquivo `appsettings.json` está no formato JSON válido

### Problema: Erro de conexão ao banco de dados

**Solução:**
1. Verifique se o servidor de banco de dados está rodando
2. Verifique se as credenciais estão corretas
3. Verifique se o firewall permite conexões na porta do banco de dados
4. Para MySQL, verifique se o usuário tem permissões adequadas
5. Para SQL Server, verifique se a autenticação está configurada corretamente (SQL Server ou Windows)

### Verificar variáveis de ambiente configuradas

#### Windows (PowerShell)
```powershell
Get-ChildItem Env: | Where-Object Name -like "*SISTEMA_AGENDAMENTO*"
```

#### Windows (CMD)
```cmd
set | findstr SISTEMA_AGENDAMENTO
```

#### Linux/Mac
```bash
env | grep SISTEMA_AGENDAMENTO
```

## Resumo

1. Configure `DatabaseProvider` no `appsettings.json` (opcional, padrão é `mysql`)
2. Configure a variável de ambiente apropriada:
   - MySQL: `SISTEMA_AGENDAMENTO_MYSQL_CONNECTION_STRING`
   - SQL Server: `SISTEMA_AGENDAMENTO_SQLSERVER_CONNECTION_STRING`
3. Reinicie o IDE/terminal se necessário
4. Execute a aplicação

## Segurança

⚠️ **Importante:**
- Nunca commite connection strings em repositórios Git
- Use variáveis de ambiente ou serviços de gerenciamento de segredos
- Em produção, considere usar Azure Key Vault, AWS Secrets Manager ou similar
- O arquivo `appsettings.json` pode ser versionado, mas não deve conter informações sensíveis

## Suporte

Para mais informações, consulte:
- [Documentação MySQL Connector](https://dev.mysql.com/doc/connector-net/en/)
- [Documentação Microsoft.Data.SqlClient](https://learn.microsoft.com/en-us/dotnet/api/system.data.sqlclient)
- [Connection Strings Reference](https://www.connectionstrings.com/)

