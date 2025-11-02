# 📅 Sistema de Agendamento - Base Profissional

<div align="center">

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-11.0-239120?logo=c-sharp&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-MVC-512BD4?logo=dotnet&logoColor=white)
![MySQL](https://img.shields.io/badge/MySQL-8.0-4479A1?logo=mysql&logoColor=white)
![Bootstrap](https://img.shields.io/badge/Bootstrap-5.3-7952B3?logo=bootstrap&logoColor=white)

**Sistema completo de gestão de agendamentos desenvolvido com arquitetura profissional, padrões de design e tecnologias modernas.**

[🎯 Sobre o Projeto](#-sobre-o-projeto) • [🏗️ Decisões Arquiteturais](#️-decisões-arquiteturais) • [💡 Raciocínio e Escolhas](#-raciocínio-e-escolhas-técnicas) • [🚀 Funcionalidades](#-funcionalidades-implementadas) • [📊 Qualidade do Código](#-qualidade-e-métricas)

</div>

---

## 🎯 Sobre o Projeto

### O Objetivo

Desenvolvi este sistema como uma **base sólida e reutilizável** para criação de sistemas de agendamento personalizados para diferentes clientes e tipos de negócio.

A estratégia é oferecer uma solução completa que pode ser adaptada e entregue como sistema personalizado, onde cada cliente recebe sua própria instância totalmente customizada (design, nomenclaturas, regras de negócio específicas).

### Por Que Desenvolvi Desta Forma?

Após analisar diferentes abordagens, optei por criar uma base profissional ao invés de desenvolver sistemas do zero para cada cliente. As razões:

- ✅ **Eficiência**: Não recriar estrutura básica a cada projeto
- ✅ **Qualidade**: Aplicar arquitetura sólida desde o início
- ✅ **Consistência**: Padrões uniformes facilitam manutenção
- ✅ **Escalabilidade**: Base preparada para evoluir com cada cliente
- ✅ **Valor**: Entregar solução profissional em tempo otimizado

### Conceito de Negócio

Cada cliente recebe uma **instância independente e personalizada** do sistema:
- Sistema próprio com banco de dados isolado
- Personalização total (branding, nome, funcionalidades)
- Entrega completa e configurada
- Suporte e evolução conforme necessidade

---

## 🏗️ Decisões Arquiteturais

### Arquitetura em Camadas

Optei por uma **arquitetura em três camadas bem definidas**, separando claramente as responsabilidades:

```
Presentation Layer (Web)
    ↓
Business Layer (Services)  
    ↓
Data Access Layer (Repositories)
```

**Por quê esta escolha?**

Separei as camadas porque:
1. **Manutenibilidade**: Mudanças em uma camada não afetam outras
2. **Testabilidade**: Cada camada pode ser testada isoladamente
3. **Reutilização**: Lógica de negócio centralizada pode ser reutilizada
4. **Personalização**: Customizações ficam isoladas na camada de apresentação
5. **Escalabilidade**: Fácil adicionar novas funcionalidades sem impacto no core

### Design Patterns Escolhidos

#### Repository Pattern
Implementei o Repository Pattern para abstrair completamente o acesso a dados.

**Decisão**: Escolhi este padrão porque:
- Facilita futuras mudanças de banco de dados ou implementação de ORM
- Centraliza lógica de acesso a dados
- Permite mock em testes unitários
- Mantém código de negócio independente do banco

#### Dependency Injection
Utilizei Dependency Injection nativo do .NET em toda a aplicação.

**Decisão**: Esta escolha garante:
- Baixo acoplamento entre componentes
- Facilidade para criar mocks em testes
- Flexibilidade para injeção de dependências customizadas
- Manutenibilidade do código

#### Mapping Pattern
Criei camadas de mapeamento entre entidades, DTOs e ViewModels.

**Decisão**: Implementei mapeamentos porque:
- Protege entidades de domínio da camada de apresentação
- Permite personalização de apresentação sem afetar lógica de negócio
- Facilita versionamento de APIs futuras
- Mantém responsabilidades bem separadas

### Princípios SOLID Aplicados

Toda a arquitetura segue os princípios SOLID:

- **Single Responsibility**: Cada classe tem uma única responsabilidade clara
- **Open/Closed**: Sistema extensível sem modificar código existente
- **Liskov Substitution**: Interfaces implementadas corretamente
- **Interface Segregation**: Interfaces específicas e coesas
- **Dependency Inversion**: Dependências através de abstrações

**Por que isso importa?**

SOLID não é apenas teoria - aplicá-lo resultou em:
- Código mais fácil de entender e manter
- Menos bugs ao fazer mudanças
- Facilidade para adicionar novas funcionalidades
- Testes mais simples de escrever

---

## 💡 Raciocínio e Escolhas Técnicas

### Por Que .NET 8 e C# 11?

Escolhi as versões mais recentes do ecossistema .NET porque:

- **Performance**: .NET 8 oferece excelente performance
- **Recursos Modernos**: C# 11 com `required` properties, nullable reference types
- **Suporte Longo**: LTS garantindo suporte prolongado
- **Ecossistema Maduro**: Bibliotecas e ferramentas consolidadas
- **Produtividade**: Framework completo reduz tempo de desenvolvimento

### Por Que ASP.NET Core MVC?

Ao invés de Web API + Frontend separado, escolhi MVC porque:

- **Simplicidade**: Solução completa em uma única aplicação
- **Rapidez**: Desenvolvimento mais ágil para este caso de uso
- **Manutenção**: Um único projeto facilita deploy e manutenção
- **Razor Views**: Templates server-side são ideais para personalização visual por cliente

### Por Que MySQL?

Escolhi MySQL como banco de dados porque:

- **Custo**: Open source, reduzindo custos de infraestrutura para clientes
- **Compatibilidade**: Amplamente suportado em diferentes ambientes
- **Performance**: Adequado para o volume esperado
- **Familiaridade**: Conhecimento comum facilita manutenção

### Estrutura de Pastas e Organização

Organizei o projeto em projetos separados por responsabilidade:

```
Repositories/  → Acesso a dados
Services/      → Lógica de negócio
Web/           → Apresentação
```

**Raciocínio**: Esta separação permite:
- Trabalhar em uma camada sem afetar outras
- Reutilizar lógica de negócio em diferentes interfaces (web, API futura)
- Facilita identificação de onde fazer mudanças
- Melhor organização mental do código

### Algoritmo de Disponibilidade

Desenvolvi um algoritmo específico para calcular horários disponíveis:

**Desafio**: Considerar múltiplos fatores simultaneamente:
- Horários de trabalho do funcionário por dia da semana
- Agendamentos já existentes
- Duração variável do serviço
- Slots de tempo para evitar conflitos

**Solução Implementada**:
1. Busco disponibilidades do funcionário para o dia específico
2. Obtendo agendamentos existentes na mesma data
3. Calculo slots de 10 em 10 minutos dentro da janela disponível
4. Verifico conflitos com agendamentos existentes
5. Garanto que o serviço cabe completamente no slot
6. Retorno apenas horários válidos

**Por que esta abordagem?**

Esta solução é:
- **Eficiente**: Uma única query busca disponibilidades, outra busca agendamentos
- **Precisa**: Elimina conflitos e garante integridade
- **Extensível**: Pode ser adaptado para regras específicas (intervalos mínimos, etc.)
- **Performática**: Cálculo em memória após buscar dados do banco

---

## 🚀 Funcionalidades Implementadas

### Gestão de Usuários e Autenticação
- Sistema de autenticação baseado em cookies
- Controle de acesso por roles (Admin/Employee)
- Gerenciamento seguro de sessões

**Implementação**: Utilizei o sistema nativo de autenticação do ASP.NET Core, configurando cookies de forma segura e gerenciando sessões adequadamente.

### Gestão de Funcionários
- CRUD completo com validações
- Gestão de horários de disponibilidade por dia da semana
- Vinculação de funcionários a usuários do sistema

**Destaque Técnico**: Implementei relacionamento entre User e Employee, permitindo que funcionários tenham acesso ao sistema através de suas credenciais próprias.

### Gestão de Serviços
- CRUD completo de serviços
- Definição de preço e duração
- Interface intuitiva para administradores

**Extensibilidade**: A estrutura permite facilmente adicionar categorias, pacotes, combos ou outros conceitos específicos por tipo de negócio.

### Sistema de Agendamentos
- **Algoritmo inteligente de disponibilidade** (detalhado acima)
- Interface interativa de seleção de data e hora
- Validação em tempo real de conflitos
- Gestão de status (Agendado, Concluído, Cancelado)
- Filtros avançados para visualização

**Destaque**: O sistema previne automaticamente double-booking e garante que apenas horários realmente disponíveis sejam apresentados ao usuário.

### Interface e Experiência do Usuário
- Design responsivo com Bootstrap 5
- Experiência mobile-first
- Interface intuitiva e acessível
- Validações client-side e server-side

**Atenção aos Detalhes**: Implementei validações tanto no frontend (UX imediata) quanto no backend (segurança e integridade).

---

## 📊 Qualidade e Métricas

### Arquitetura

- **3 Camadas** bem definidas e separadas
- **7 Entidades** principais do domínio
- **10+ Repositories** com acesso tipado e interfaces
- **5+ Services** com lógica de negócio complexa
- **100% C# 11** utilizando recursos modernos da linguagem

### Padrões e Qualidade

- ✅ **SOLID**: Aplicado consistentemente em toda arquitetura
- ✅ **Design Patterns**: Repository, Dependency Injection, Mapping
- ✅ **Clean Code**: Código legível, bem nomeado e organizado
- ✅ **Separation of Concerns**: Responsabilidades claramente definidas
- ✅ **Null Safety**: Uso de nullable reference types do C# 11

### Processo de Desenvolvimento

Desenvolvi este projeto com foco em:

1. **Arquitetura Primeiro**: Pensei na estrutura antes de começar a codificar
2. **Padrões Desde o Início**: Apliquei padrões desde o primeiro código
3. **Extensibilidade**: Código preparado para crescer sem refatorações grandes
4. **Manutenibilidade**: Estrutura que facilita futuras modificações
5. **Qualidade**: Não apenas "funcionar", mas fazer bem feito

---

## 🎓 Competências Demonstradas

Este projeto demonstra minhas habilidades em:

### Arquitetura de Software
- Design de sistemas escaláveis e manuteníveis
- Separação de responsabilidades
- Planejamento arquitetural antes da implementação

### Padrões e Boas Práticas
- Aplicação prática de Design Patterns
- Princípios SOLID na prática
- Clean Code e código legível

### Tecnologias Modernas
- .NET 8 com recursos mais recentes
- C# 11 e suas funcionalidades modernas
- ASP.NET Core MVC completo
- MySQL e modelagem de banco de dados

### Lógica de Negócio Complexa
- Desenvolvimento de algoritmos específicos (disponibilidade)
- Validações robustas
- Prevenção de regras de negócio

### Frontend e UX
- Interface responsiva e moderna
- Validações client-side e server-side
- Experiência do usuário cuidadosa

### Pensamento Estratégico
- Visão de produto (base reutilizável)
- Otimização de processos de desenvolvimento
- Foco em qualidade e manutenibilidade

---

## 🔧 Tecnologias Utilizadas

### Backend
- **.NET 8.0** - Framework moderno e performático
- **C# 11** - Linguagem com recursos modernos
- **ASP.NET Core MVC** - Framework web robusto
- **MySQL** - Banco de dados relacional
- **MySql.Data** - Provider para MySQL

### Frontend
- **Razor Pages** - Engine de views do ASP.NET Core
- **Bootstrap 5.3** - Framework CSS responsivo
- **JavaScript (ES6+)** - Interatividade moderna
- **HTML5 & CSS3** - Semântica e estilização

### Arquitetura e Padrões
- **Dependency Injection** nativo do .NET
- **Cookie Authentication** - Autenticação segura
- **Repository Pattern** - Abstração de dados
- **Mapping Pattern** - Separação de camadas

---

## 📝 Próximos Passos e Evolução

Este projeto está em **desenvolvimento ativo** e preparado para:

- **Personalização por Cliente**: Cada instância pode ser totalmente customizada
- **Extensões**: Novas funcionalidades podem ser adicionadas conforme necessidade
- **Manutenção**: Estrutura preparada para evoluções e melhorias
- **Escalabilidade**: Arquitetura permite crescimento do sistema

A base está sólida e pronta para ser adaptada e entregue como solução completa para diferentes negócios.

---

## 💼 Valor para Clientes

### O Que Este Projeto Oferece

✅ **Solução Completa**: Sistema funcional e pronto para uso  
✅ **Arquitetura Profissional**: Base sólida que inspira confiança  
✅ **Personalização Total**: Adaptado especificamente para cada negócio  
✅ **Interface Moderna**: Design profissional e responsivo  
✅ **Manutenibilidade**: Estrutura que facilita evolução contínua  
✅ **Qualidade**: Código limpo e bem organizado  

---

<div align="center">

**Desenvolvido com foco em qualidade, arquitetura sólida e boas práticas**

Usando .NET 8, C# 11 e tecnologias modernas

</div>
