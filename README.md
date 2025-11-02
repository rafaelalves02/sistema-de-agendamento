# 📅 Sistema de Agendamento

<div align="center">

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-11.0-239120?logo=c-sharp&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-MVC-512BD4?logo=dotnet&logoColor=white)
![MySQL](https://img.shields.io/badge/MySQL-8.0-4479A1?logo=mysql&logoColor=white)
![Bootstrap](https://img.shields.io/badge/Bootstrap-5.3-7952B3?logo=bootstrap&logoColor=white)

**Sistema completo de gestão de agendamentos desenvolvido com arquitetura em camadas, padrões de design e tecnologias modernas.**

[🚀 Features](#-principais-funcionalidades) • [🏗️ Arquitetura](#️-arquitetura-e-padrões) • [💻 Tecnologias](#-tecnologias) • [📦 Como Executar](#-como-executar) • [📚 Documentação](#-estrutura-do-projeto)

</div>

---

## 📋 Sobre o Projeto

Sistema web completo para gestão de agendamentos de serviços, desenvolvido com **arquitetura em camadas**, implementando **Design Patterns** profissionais e utilizando as **mais recentes tecnologias do ecossistema .NET**.

O projeto foi desenvolvido com foco em:
- ✅ **Código limpo e manutenível**
- ✅ **Separação de responsabilidades**
- ✅ **Escalabilidade e performance**
- ✅ **Boas práticas de desenvolvimento**
- ✅ **Interface responsiva e moderna**

---

## 🚀 Principais Funcionalidades

### 👤 Gestão de Usuários e Autenticação
- Sistema de autenticação baseado em cookies
- Controle de acesso por roles (Admin/Employee)
- Gerenciamento seguro de sessões

### 👷 Gestão de Funcionários
- Cadastro completo de funcionários
- Gestão de horários de disponibilidade por dia da semana
- Vinculação de funcionários a usuários do sistema
- Visualização e edição de informações

### 🛎️ Gestão de Serviços
- CRUD completo de serviços
- Definição de preço e duração por serviço
- Interface intuitiva para administradores

### 📅 Sistema de Agendamentos
- **Algoritmo inteligente de disponibilidade**: calcula automaticamente os horários disponíveis considerando:
  - Horários de trabalho do funcionário
  - Agendamentos já existentes
  - Duração do serviço solicitado
  - Conflitos de horário
- Interface interativa de seleção de data e hora
- Validação em tempo real de conflitos
- Gestão de status (Agendado, Concluído, Cancelado)

### 🔍 Filtros Avançados
- Filtro por data, serviço, funcionário e status
- Busca por telefone do cliente
- Visualização completa de agendamentos com cards informativos

### 🎨 Interface Moderna
- Design responsivo com Bootstrap 5
- Experiência mobile-first
- Interface intuitiva e acessível
- Feedback visual em tempo real

---

## 🏗️ Arquitetura e Padrões

### Arquitetura em Camadas

```
┌─────────────────────────────────────┐
│   Presentation Layer (Web)          │
│   - Controllers                     │
│   - Views                           │
│   - ViewModels                      │
└──────────────┬──────────────────────┘
               │
┌──────────────▼──────────────────────┐
│   Business Layer (Services)        │
│   - Business Logic                 │
│   - Validation                      │
│   - Mapping                         │
└──────────────┬──────────────────────┘
               │
┌──────────────▼──────────────────────┐
│   Data Access Layer (Repositories)  │
│   - Database Access                 │
│   - Entities                        │
└─────────────────────────────────────┘
```

### Design Patterns Implementados

- 🏭 **Repository Pattern**: Abstração completa do acesso a dados
- 🔌 **Dependency Injection**: Inversão de dependências para melhor testabilidade
- 🗺️ **Mapping Pattern**: Separação entre entidades de domínio e DTOs/ViewModels
- 📐 **Separation of Concerns**: Responsabilidades bem definidas em cada camada
- 🔄 **Interface Segregation**: Interfaces específicas para cada responsabilidade

### Princípios SOLID

- ✅ **Single Responsibility**: Cada classe tem uma única responsabilidade
- ✅ **Open/Closed**: Extensível sem modificar código existente
- ✅ **Liskov Substitution**: Interfaces implementadas corretamente
- ✅ **Interface Segregation**: Interfaces específicas e coesas
- ✅ **Dependency Inversion**: Dependências através de abstrações

---

## 💻 Tecnologias

### Backend
- **.NET 8.0** - Framework moderno e performático
- **C# 11** - Linguagem com recursos modernos (required properties, nullable reference types)
- **ASP.NET Core MVC** - Framework web robusto
- **MySQL** - Banco de dados relacional
- **MySql.Data** - Provider para MySQL

### Frontend
- **Razor Pages** - Engine de views do ASP.NET Core
- **Bootstrap 5.3** - Framework CSS responsivo
- **JavaScript (ES6+)** - Interatividade moderna
- **HTML5 & CSS3** - Semântica e estilização

### Ferramentas e Conceitos
- **Dependency Injection** nativo do .NET
- **Cookie Authentication** - Autenticação segura
- **Async/Await** - Programação assíncrona
- **LINQ** - Consultas tipadas
- **Enums** - Tipos seguros
- **Nullable Reference Types** - Segurança de tipos

---

## 📦 Como Executar

### Pré-requisitos

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [MySQL Server](https://dev.mysql.com/downloads/mysql/) 8.0 ou superior
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/)

### Passo a Passo

1. **Clone o repositório**
   ```bash
   git clone https://github.com/seu-usuario/sistema-de-agendamento.git
   cd sistema-de-agendamento
   ```

2. **Configure o Banco de Dados**
   
   Execute o script SQL para criar o schema e as tabelas:
   ```bash
   mysql -u root -p < SistemaDeAgendamento/create_database.sql
   ```

3. **Configure a Connection String**
   
   Edite o arquivo `appsettings.json` ou `appsettings.Development.json`:
   ```json
   {
     "ConnectionStrings": {
       "SistemaDeAgendamentoConnectionString": "Server=localhost;Database=sistema_de_agendamento;Uid=root;Pwd=sua_senha;"
     }
   }
   ```

4. **Restore as Dependências**
   ```bash
   dotnet restore
   ```

5. **Execute o Projeto**
   ```bash
   cd SistemaDeAgendamento/SistemaDeAgendamento.Web
   dotnet run
   ```

6. **Acesse a Aplicação**
   
   Abra seu navegador em: `https://localhost:5001` ou `http://localhost:5000`

### Credenciais Padrão

- **Usuário**: `admin`
- **Senha**: `admin123`
- **Role**: Admin

---

## 📚 Estrutura do Projeto

```
SistemaDeAgendamento/
│
├── SistemaDeAgendamento.Web/           # Camada de Apresentação
│   ├── Controllers/                     # Controladores MVC
│   ├── Views/                           # Views Razor
│   ├── Models/                          # ViewModels
│   ├── Mappings/                        # Mapeamentos Web → Services
│   └── wwwroot/                         # Arquivos estáticos
│
├── SistemaDeAgendamento.Services/       # Camada de Negócio
│   ├── Models/                          # DTOs e Requests/Results
│   ├── Mappings/                        # Mapeamentos Services → Entities
│   └── [Business Services]              # Lógica de negócio
│
├── SistemaDeAgendamento.Repositories/   # Camada de Dados
│   ├── Entities/                        # Entidades do domínio
│   └── [Repositories]                   # Acesso a dados
│
└── create_database.sql                  # Script de criação do banco
```

---

## 🎯 Destaques Técnicos

### Algoritmo de Disponibilidade

O sistema implementa um algoritmo sofisticado que calcula os horários disponíveis considerando múltiplos fatores:

```csharp
public IList<TimeSpan> GetAvailableSlots(int employeeId, DateTime date, int serviceDuration)
{
    // 1. Busca disponibilidades do funcionário para o dia da semana
    // 2. Obtém agendamentos existentes para o funcionário na data
    // 3. Calcula slots de 10 em 10 minutos
    // 4. Verifica conflitos com agendamentos existentes
    // 5. Garante que o serviço cabe no horário disponível
    // 6. Retorna apenas slots válidos
}
```

### Validações Robustas

- Validação de conflitos de horário
- Verificação de disponibilidade do funcionário
- Validação de datas futuras
- Verificação de duplicidade de clientes

### Mapeamento Entre Camadas

Sistema completo de mapeamento que garante que:
- Entidades de domínio não são expostas na camada de apresentação
- ViewModels são específicos para cada necessidade
- DTOs são usados para comunicação entre camadas

---

## 🚧 Em Desenvolvimento

- [ ] Testes unitários e de integração
- [ ] Implementação de logging estruturado
- [ ] Transações para operações críticas
- [ ] Paginação em listagens
- [ ] Cache para otimização de performance
- [ ] API REST para integrações
- [ ] Documentação Swagger/OpenAPI

---

## 📊 Métricas do Projeto

- **3 Camadas** arquiteturais bem definidas
- **7 Entidades** principais do domínio
- **10+ Repositories** com acesso tipado
- **5+ Services** com lógica de negócio complexa
- **100% C# 11** com recursos modernos
- **Arquitetura** escalável e manutenível

---

## 🎓 O Que Este Projeto Demonstra

### Para Recrutadores

Este projeto demonstra competência em:

✅ **Arquitetura de Software**: Design de sistemas escaláveis e manuteníveis  
✅ **Design Patterns**: Aplicação prática de padrões profissionais  
✅ **Clean Code**: Código limpo, legível e bem organizado  
✅ **Tecnologias Modernas**: Uso de .NET 8 e C# 11 com recursos avançados  
✅ **Lógica de Negócio Complexa**: Algoritmos para gerenciamento de disponibilidade  
✅ **Frontend Responsivo**: Interface moderna e acessível  
✅ **Boas Práticas**: Separation of Concerns, SOLID, DRY  
✅ **Database Design**: Modelagem relacional bem estruturada  

---

## 📝 Licença

Este projeto é um portfólio educacional e está disponível para análise.

---

## 👨‍💻 Autor

Desenvolvido com foco em qualidade, boas práticas e tecnologias modernas.

---

<div align="center">

**Desenvolvido com ❤️ usando .NET 8 e C#**

⭐ Se este projeto te ajudou, considere dar uma estrela!

</div>

