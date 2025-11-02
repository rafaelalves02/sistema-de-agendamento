# 📅 Sistema de Agendamento - Base Personalizável

<div align="center">

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-11.0-239120?logo=c-sharp&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-MVC-512BD4?logo=dotnet&logoColor=white)
![MySQL](https://img.shields.io/badge/MySQL-8.0-4479A1?logo=mysql&logoColor=white)
![Bootstrap](https://img.shields.io/badge/Bootstrap-5.3-7952B3?logo=bootstrap&logoColor=white)

**Base sólida e profissional para criação de sistemas de agendamento personalizados por cliente.**

Arquitetura pronta para ser adaptada e entregue como solução completa para cada negócio específico.

[🎯 Visão Geral](#-visão-geral) • [🏗️ Arquitetura](#️-arquitetura-e-padrões) • [🔧 Personalização](#-estratégia-de-personalização) • [💻 Tecnologias](#-tecnologias) • [📦 Como Usar](#-como-usar-esta-base)

</div>

---

## 🎯 Visão Geral

### O que é este projeto?

Este é um **projeto base/template** desenvolvido para ser usado como fundação na criação de sistemas de agendamento personalizados para diferentes clientes e tipos de negócio.

### Conceito: Base Reutilizável para Entregas Personalizadas

A proposta é simples e eficaz:

1. **Base Sólida**: Este projeto contém toda a estrutura, arquitetura e funcionalidades core de um sistema de agendamento profissional
2. **Apresentação ao Cliente**: Você demonstra o sistema base, explicando como ele pode otimizar a rotina do negócio
3. **Personalização Completa**: Você realiza todas as adaptações necessárias:
   - Design e branding específico do cliente
   - Nome e identidade visual do sistema
   - Funcionalidades ajustadas ao tipo de negócio
   - Regras de negócio específicas
4. **Entrega Pronta**: O cliente recebe um sistema completo, totalmente personalizado para seu negócio

### Modelo de Negócio

Cada cliente recebe sua **própria instância personalizada** do sistema:

- ✅ **Sistema Independente**: Cada cliente tem seu próprio banco de dados e instalação
- ✅ **Personalização Total**: Design, nome, funcionalidades adaptadas ao negócio específico
- ✅ **Sem Compartilhamento**: Dados e configurações isolados por cliente
- ✅ **Entrega Completa**: Sistema pronto para uso, configurado e personalizado

### Exemplo Prático

**Cenário**: Uma barbearia precisa de um sistema de agendamento

1. Você apresenta a base, demonstrando funcionalidades
2. Cliente aprova e solicita ajustes (cores da marca, nome "Barbershop Pro", etc.)
3. Você personaliza:
   - Tema visual com cores da barbearia
   - Nome do sistema no cabeçalho
   - Ajustes nas nomenclaturas (ex: "Barbeiros" ao invés de "Funcionários")
   - Regras específicas se necessário
4. Entrega o sistema completo e personalizado para a barbearia

### Vantagens desta Abordagem

✅ **Desenvolvimento Rápido**: Não começar do zero para cada cliente  
✅ **Economia de Tempo**: Foco na personalização, não na estrutura base  
✅ **Qualidade Garantida**: Arquitetura profissional desde o início  
✅ **Consistência**: Padrões uniformes, facilitando manutenção futura  
✅ **Escalabilidade**: Base preparada para crescer com o cliente  
✅ **Flexibilidade**: Fácil adaptação para diferentes tipos de negócio  

---

## 🚀 Funcionalidades Core (Base)

### 👤 Gestão de Usuários e Autenticação
- Sistema de autenticação baseado em cookies
- Controle de acesso por roles (Admin/Employee)
- Gerenciamento seguro de sessões
- Pronto para extensão com novos perfis conforme necessário

### 👷 Gestão de Funcionários
- Cadastro completo de funcionários
- Gestão de horários de disponibilidade por dia da semana
- Vinculação de funcionários a usuários do sistema
- CRUD completo com validações robustas

### 🛎️ Gestão de Serviços
- CRUD completo de serviços
- Definição de preço e duração por serviço
- Interface intuitiva para administradores
- Base extensível para categorias, pacotes, combos, etc.

### 📅 Sistema de Agendamentos
- **Algoritmo inteligente de disponibilidade** - calcula automaticamente os horários disponíveis considerando:
  - Horários de trabalho do funcionário
  - Agendamentos já existentes
  - Duração do serviço solicitado
  - Conflitos de horário em tempo real
- Interface interativa de seleção de data e hora
- Validação de conflitos em tempo real
- Gestão de status (Agendado, Concluído, Cancelado)
- Filtros avançados para visualização

### 🔍 Recursos Adicionais
- Filtro por data, serviço, funcionário e status
- Busca por telefone do cliente
- Visualização completa de agendamentos com cards informativos
- Interface responsiva e moderna

### 🎨 Interface Moderna
- Design responsivo com Bootstrap 5
- Experiência mobile-first
- Interface intuitiva e acessível
- Fácil customização visual (cores, logos, fontes)

---

## 🏗️ Arquitetura e Padrões

### Arquitetura em Camadas

```
┌─────────────────────────────────────┐
│   Presentation Layer (Web)          │
│   - Controllers                     │
│   - Views                           │
│   - ViewModels                      │
│   - Mappings                        │
└──────────────┬──────────────────────┘
               │
┌──────────────▼──────────────────────┐
│   Business Layer (Services)        │
│   - Business Logic                 │
│   - Validation                      │
│   - Mappings                        │
│   - DTOs/Results                    │
└──────────────┬──────────────────────┘
               │
┌──────────────▼──────────────────────┐
│   Data Access Layer (Repositories)  │
│   - Database Access                 │
│   - Entities                        │
│   - Interfaces                      │
└─────────────────────────────────────┘
```

### Design Patterns Implementados

- 🏭 **Repository Pattern**: Abstração completa do acesso a dados - facilita manutenção e extensões
- 🔌 **Dependency Injection**: Inversão de dependências para melhor testabilidade e flexibilidade
- 🗺️ **Mapping Pattern**: Separação entre entidades de domínio e DTOs/ViewModels
- 📐 **Separation of Concerns**: Responsabilidades bem definidas em cada camada
- 🔄 **Interface Segregation**: Interfaces específicas para cada responsabilidade
- 🏛️ **Base Repository Pattern**: Reutilização de código comum entre repositories

### Princípios SOLID

A arquitetura segue rigorosamente os princípios SOLID, garantindo:

- ✅ **Single Responsibility**: Cada classe tem uma única responsabilidade clara
- ✅ **Open/Closed**: Extensível sem modificar código existente - ideal para personalizações
- ✅ **Liskov Substitution**: Interfaces implementadas corretamente
- ✅ **Interface Segregation**: Interfaces específicas e coesas
- ✅ **Dependency Inversion**: Dependências através de abstrações

### Por que esta Arquitetura Funciona para Personalização?

1. **Modularidade**: Camadas bem separadas facilitam personalização por cliente
2. **Extensibilidade**: Padrão Open/Closed permite adicionar features sem quebrar o core
3. **Manutenibilidade**: Mudanças isoladas em uma camada não afetam outras
4. **Flexibilidade**: ViewModels e DTOs permitem customizações na apresentação sem tocar no core
5. **Profissionalismo**: Arquitetura sólida inspira confiança nos clientes

---

## 🔧 Estratégia de Personalização

### O que Pode Ser Personalizado?

#### 1. **Identidade Visual (Branding)**
- ✅ Cores primárias e secundárias
- ✅ Logos e imagens
- ✅ Fontes e tipografia
- ✅ Estilos de botões e componentes
- ✅ Layout e disposição de elementos

**Onde customizar**: Views (`Views/`), arquivos CSS em `wwwroot/css/`, variáveis Bootstrap

#### 2. **Nomenclaturas e Textos**
- ✅ Nome do sistema (títulos, cabeçalhos)
- ✅ Termos específicos do negócio (ex: "Barbeiros" vs "Funcionários", "Cortes" vs "Serviços")
- ✅ Mensagens e labels
- ✅ Descrições e ajuda contextual

**Onde customizar**: Views (`Views/`), ViewModels (`Models/`), textos em português

#### 3. **Regras de Negócio**
- ✅ Validações específicas (ex: horários permitidos, dias da semana)
- ✅ Cálculos personalizados (descontos, pacotes)
- ✅ Fluxos de aprovação
- ✅ Integrações com sistemas externos

**Onde customizar**: Services (`Services/`), validações nos controllers

#### 4. **Funcionalidades Adicionais**
- ✅ Novos módulos por cliente
- ✅ Relatórios específicos
- ✅ Integrações (pagamentos, WhatsApp, email)
- ✅ Features exclusivas do negócio

**Onde adicionar**: Novos Controllers, Services, Views, Entidades no banco

### Processo de Personalização Recomendado

```bash
# 1. Criar cópia da base para o cliente
cp -r sistema-de-agendamento barbearia-xyz-pro

# 2. Configurar ambiente específico
cd barbearia-xyz-pro

# 3. Personalizar configurações
# - appsettings.json (connection string, configurações)
# - Nome do projeto (opcional, mas recomendado)

# 4. Personalizar identidade visual
# - Modificar cores em wwwroot/css/
# - Adicionar logos em wwwroot/
# - Ajustar Views com branding

# 5. Ajustar nomenclaturas
# - Atualizar textos nas Views
# - Modificar labels e títulos
# - Ajustar mensagens

# 6. Configurar banco de dados específico
# - Criar banco único para o cliente
# - Executar create_database.sql
# - Configurar connection string

# 7. Testar personalização
# - Validar todas as funcionalidades
# - Verificar responsividade
# - Testar regras de negócio

# 8. Deploy da instância personalizada
```

### Boas Práticas para Personalização

✅ **Documente mudanças**: Anote o que foi alterado por cliente para facilitar futuras atualizações  
✅ **Mantenha o core**: Evite modificar arquitetura core sem necessidade  
✅ **Use configuração**: Prefira appsettings.json para configurações específicas  
✅ **Teste após mudanças**: Sempre valide após personalizações  
✅ **Backup antes**: Mantenha backup da base original  

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
- **Bootstrap 5.3** - Framework CSS responsivo (fácil customização)
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

## 📦 Como Usar Esta Base

### Cenário: Criar Sistema Personalizado para um Cliente

#### Passo a Passo

1. **Clone ou baixe a base**
   ```bash
   git clone https://github.com/seu-usuario/sistema-de-agendamento.git cliente-xyz-agendamento
   cd cliente-xyz-agendamento
   ```

2. **Renomeie o projeto (opcional, mas recomendado)**
   - Renomeie pastas se necessário
   - Atualize namespaces se fizer sentido para o cliente
   - Ajuste nome da solution

3. **Configure ambiente específico**
   - Instale .NET 8.0 SDK
   - Configure MySQL
   - Crie banco de dados específico para o cliente

4. **Personalize identidade visual**
   - Modifique cores em `wwwroot/css/`
   - Adicione logos e imagens em `wwwroot/`
   - Ajuste Views com branding do cliente

5. **Ajuste nomenclaturas**
   - Atualize textos nas Views (`Views/`)
   - Modifique labels e mensagens
   - Ajuste termos para o negócio do cliente

6. **Configure banco de dados**
   - Crie banco específico
   - Execute `create_database.sql`
   - Configure connection string em `appsettings.json`

7. **Teste e valide**
   - Teste todas funcionalidades
   - Valide responsividade
   - Verifique regras de negócio

8. **Deploy da instância**
   - Configure ambiente de produção
   - Faça deploy da instância personalizada
   - Entregue ao cliente

### Executar Localmente para Demonstração

#### Pré-requisitos

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [MySQL Server](https://dev.mysql.com/downloads/mysql/) 8.0 ou superior
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/)

#### Passo a Passo Rápido

1. **Clone o repositório**
   ```bash
   git clone https://github.com/seu-usuario/sistema-de-agendamento.git
   cd sistema-de-agendamento
   ```

2. **Configure o Banco de Dados**
   ```bash
   mysql -u root -p < SistemaDeAgendamento/create_database.sql
   ```

3. **Configure a Connection String**
   
   Edite `appsettings.json` ou `appsettings.Development.json`:
   ```json
   {
     "ConnectionStrings": {
       "SistemaDeAgendamentoConnectionString": "Server=localhost;Database=sistema_de_agendamento;Uid=root;Pwd=sua_senha;"
     }
   }
   ```

4. **Restore e Execute**
   ```bash
   dotnet restore
   cd SistemaDeAgendamento/SistemaDeAgendamento.Web
   dotnet run
   ```

5. **Acesse a Aplicação**
   - `https://localhost:5001` ou `http://localhost:5000`
   - **Usuário**: `admin` | **Senha**: `admin123`

---

## 📚 Estrutura do Projeto

```
SistemaDeAgendamento/
│
├── SistemaDeAgendamento.Web/           # Camada de Apresentação
│   ├── Controllers/                     # Controladores MVC
│   │   ├── AppointmentController.cs
│   │   ├── EmployeeController.cs
│   │   ├── ServiceController.cs
│   │   └── UserController.cs
│   ├── Views/                           # Views Razor (fácil customização)
│   │   ├── Appointment/
│   │   ├── Employee/
│   │   ├── Service/
│   │   └── Shared/
│   ├── Models/                          # ViewModels
│   ├── Mappings/                        # Mapeamentos Web → Services
│   └── wwwroot/                         # Arquivos estáticos (CSS, JS, imagens)
│
├── SistemaDeAgendamento.Services/       # Camada de Negócio
│   ├── Models/                          # DTOs e Requests/Results
│   │   ├── Appointment/
│   │   ├── Employee/
│   │   ├── Service/
│   │   └── User/
│   ├── Mappings/                        # Mapeamentos Services → Entities
│   └── [Business Services]             # Lógica de negócio
│       ├── AppointmentService.cs
│       ├── EmployeeService.cs
│       ├── ServiceService.cs
│       └── UserService.cs
│
├── SistemaDeAgendamento.Repositories/   # Camada de Dados
│   ├── Entities/                        # Entidades do domínio
│   │   ├── Appointment.cs
│   │   ├── Employee.cs
│   │   ├── Service.cs
│   │   └── User.cs
│   └── [Repositories]                   # Acesso a dados
│       ├── AppointmentRepository.cs
│       ├── EmployeeRepository.cs
│       ├── ServiceRepository.cs
│       └── BaseRepository.cs
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
    var availabilities = _availabilityRepository.GetAvailabilitiesByEmployeeAndWeekday(employeeId, weekday);
    
    // 2. Obtém agendamentos existentes para o funcionário na data
    var appointments = _appointmentRepository.GetAppointmentsByEmployeeAndDate(employeeId, date);
    
    // 3. Calcula slots de 10 em 10 minutos
    // 4. Verifica conflitos com agendamentos existentes
    // 5. Garante que o serviço cabe no horário disponível
    // 6. Retorna apenas slots válidos
}
```

Este algoritmo pode ser **estendido ou customizado** para regras específicas de cada cliente.

### Validações Robustas

- Validação de conflitos de horário
- Verificação de disponibilidade do funcionário
- Validação de datas futuras
- Verificação de duplicidade de clientes
- Base preparada para novas validações específicas

### Mapeamento Entre Camadas

Sistema completo de mapeamento que garante:
- Entidades de domínio não são expostas na camada de apresentação
- ViewModels são específicos para cada necessidade
- DTOs são usados para comunicação entre camadas
- Facilita personalizações sem impactar o core

---

## 📊 Métricas e Qualidade

### Arquitetura

- **3 Camadas** arquiteturais bem definidas
- **7 Entidades** principais do domínio
- **10+ Repositories** com acesso tipado
- **5+ Services** com lógica de negócio complexa
- **100% C# 11** com recursos modernos
- **SOLID** aplicado consistentemente

### Código

- **Clean Code**: Código limpo e legível
- **Design Patterns**: Padrões profissionais aplicados
- **Separation of Concerns**: Responsabilidades bem definidas
- **Extensibilidade**: Preparado para crescimento
- **Manutenibilidade**: Estrutura que facilita manutenção

---

## 🎓 Valor para Desenvolvedores e Clientes

### Para Desenvolvedores

Esta base oferece:

✅ **Economia de Tempo**: Não começar do zero a cada projeto  
✅ **Arquitetura Profissional**: Estrutura sólida e testada  
✅ **Base Sólida**: Funcionalidades core já implementadas  
✅ **Flexibilidade**: Fácil adaptação para diferentes negócios  
✅ **Manutenibilidade**: Código organizado facilita manutenção  
✅ **Escalabilidade**: Preparado para crescimento  

### Para Clientes

✅ **Solução Completa**: Sistema pronto para uso  
✅ **Personalização Total**: Adaptado especificamente para o negócio  
✅ **Interface Moderna**: Design profissional e responsivo  
✅ **Funcionalidades Core**: Tudo que é necessário para gestão de agendamentos  
✅ **Suporte Contínuo**: Base sólida facilita manutenção e evolução  

---

## 🤔 Avaliação da Arquitetura

### ✅ Pontos Fortes (Já Implementados)

1. **Arquitetura em Camadas**: Separação clara facilita personalização
2. **Repository Pattern**: Abstração permite manutenção e extensões sem complexidade
3. **Dependency Injection**: Facilita testes e injeção de dependências customizadas
4. **Mapping Pattern**: ViewModels e DTOs isolam personalizações da camada de apresentação
5. **Interfaces bem definidas**: Permitem extensão sem quebrar compatibilidade
6. **Connection String configurável**: Cada instância pode ter seu próprio banco
7. **Separation of Concerns**: Facilita identificar onde fazer personalizações

### 🎯 Estrutura Ideal para Personalização

A arquitetura atual é **excelente** para este modelo de negócio:

✅ **Camadas Separadas**: Personalizações visuais ficam na camada Web  
✅ **Services Isolados**: Regras de negócio customizadas podem ser adicionadas sem afetar o core  
✅ **Repositories Abstratos**: Facilita extensões de dados se necessário  
✅ **Configuração Externa**: appsettings permite isolamento por instância  
✅ **ViewModels Flexíveis**: Fácil adaptação de apresentação por cliente  

### 📈 Conclusão da Avaliação

**A arquitetura está perfeitamente alinhada** com o objetivo de ser uma base personalizável:

✅ Separação de responsabilidades facilita personalização sem quebrar o core  
✅ Padrões aplicados são ideais para reutilização e extensão  
✅ Estrutura permite evolução e customização por cliente  
✅ Configurações externas permitem isolamento completo por instância  
✅ Código limpo e organizado facilita trabalho de personalização  

**O caminho está correto!** A arquitetura atual fornece uma base sólida e profissional, ideal para ser adaptada e entregue como solução personalizada para cada cliente.

---

## 📝 Licença

Este projeto serve como base/template para desenvolvimento de sistemas de agendamento personalizados.

---

<div align="center">

**🚀 Base Profissional para Sistemas de Agendamento**

Desenvolvido com arquitetura sólida usando .NET 8 e C#

---

**Transforme esta base em soluções personalizadas para seus clientes!**

</div>
