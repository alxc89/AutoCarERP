# Manual do Usuário - AutoCarERP

## 📋 Índice

1. [Visão Geral](#visão-geral)
2. [Acesso ao Sistema](#acesso-ao-sistema)
3. [Dashboard](#dashboard)
4. [Módulo: Clientes](#módulo-clientes)
5. [Módulo: Veículos](#módulo-veículos)
6. [Módulo: Produtos/Serviços](#módulo-produtosserviços)
7. [Módulo: Ordens de Serviço](#módulo-ordens-de-serviço)
8. [Configurações](#configurações)
9. [Gerenciamento de Usuários](#gerenciamento-de-usuários) *(Admin)*
10. [Gerenciamento de Perfis](#gerenciamento-de-perfis) *(Admin)*
11. [Perfis e Permissões](#perfis-e-permissões)
12. [Perguntas Frequentes](#perguntas-frequentes)

---

## Visão Geral

**AutoCarERP** é um sistema completo de gestão para oficinas mecânicas, oferecendo controle sobre:

- 🧑‍💼 **Clientes** - Cadastro e gestão de clientes
- 🚗 **Veículos** - Registro de veículos dos clientes
- 🛠️ **Produtos/Serviços** - Catálogo de peças e serviços
- 📄 **Ordens de Serviço** - Gestão completa de OS
- 👥 **Usuários** - Controle de acesso ao sistema
- 🔐 **Perfis** - Gerenciamento de permissões

---

## Acesso ao Sistema

### Login

1. Acesse a URL do sistema
2. Informe seu **email** e **senha**
3. Clique em **Entrar**

**Padrão de Senha:**
- Mínimo de 8 caracteres
- Deve conter letras maiúsculas e minúsculas
- Deve conter números
- Deve conter caracteres especiais (!@#$%^&*)

### Primeiro Acesso

No primeiro acesso, você será solicitado a:
1. Alterar sua senha padrão
2. Configurar suas preferências (tema, idioma)

### Esqueci Minha Senha

*Funcionalidade em desenvolvimento*

---

## Dashboard

O **Dashboard** é a tela inicial do sistema e exibe métricas importantes:

### Visualizações Disponíveis

- 📊 **Total de Clientes** - Quantidade de clientes cadastrados
- 🚗 **Total de Veículos** - Quantidade de veículos registrados
- 📄 **Ordens de Serviço** 
  - Total de OS abertas
  - OS em andamento
  - OS concluídas
- 💰 **Faturamento** - Valor total de serviços prestados

### Permissões Necessárias

- Qualquer usuário autenticado pode acessar o Dashboard
- Informações exibidas variam conforme o perfil

---

## Módulo: Clientes

Gerenciamento completo de clientes da oficina.

### Listar Clientes

**Caminho:** Menu lateral > **Clientes** > **Listar clientes**

**Funcionalidades:**
- 🔍 Buscar cliente por nome, CPF ou telefone
- 📄 Visualizar lista paginada
- ✏️ Editar dados de cliente
- 🗑️ Excluir cliente

**Informações Exibidas:**
- Nome completo
- CPF/CNPJ
- Telefone
- Email
- Endereço
- Data de cadastro

### Criar Novo Cliente

**Caminho:** Menu lateral > **Clientes** > **Novo cliente**

**Campos Obrigatórios:**
- ✅ Nome completo
- ✅ CPF/CNPJ
- ✅ Telefone

**Campos Opcionais:**
- Email
- Endereço completo
- Observações

**Passo a Passo:**
1. Clique em **Novo cliente**
2. Preencha os dados do formulário
3. Clique em **Salvar**
4. Sistema confirmará sucesso ou mostrará erros de validação

### Editar Cliente

**Caminho:** Lista de Clientes > Ações (⋮) > **Editar**

1. Localize o cliente na listagem
2. Clique no ícone de edição (✏️)
3. Altere os dados necessários
4. Clique em **Salvar**

### Excluir Cliente

**Caminho:** Lista de Clientes > Ações (⋮) > **Excluir**

⚠️ **Atenção:** 
- Não é possível excluir clientes com veículos cadastrados
- Não é possível excluir clientes com OS abertas
- A exclusão é permanente

**Passo a Passo:**
1. Localize o cliente
2. Clique no ícone de exclusão (🗑️)
3. Confirme a ação no dialog

---

## Módulo: Veículos

Registro e controle de veículos dos clientes.

### Listar Veículos

**Caminho:** Menu lateral > **Veículos** > **Listar veículos**

**Funcionalidades:**
- 🔍 Buscar por placa, modelo ou proprietário
- 📄 Visualizar lista paginada
- ✏️ Editar dados do veículo
- 🗑️ Excluir veículo

**Informações Exibidas:**
- Placa
- Modelo
- Marca
- Ano
- Cor
- Cliente proprietário
- Quilometragem (última atualização)

### Cadastrar Novo Veículo

**Caminho:** Menu lateral > **Veículos** > **Novo veículo**

**Campos Obrigatórios:**
- ✅ Placa
- ✅ Modelo
- ✅ Marca
- ✅ Ano de fabricação
- ✅ Cliente (proprietário)

**Campos Opcionais:**
- Cor
- Chassi
- Renavam
- Quilometragem atual
- Observações

**Passo a Passo:**
1. Clique em **Novo veículo**
2. Selecione o **Cliente** proprietário (busca por nome)
3. Preencha os dados do veículo
4. Clique em **Salvar**

### Editar Veículo

1. Localize o veículo na listagem
2. Clique no ícone de edição (✏️)
3. Altere os dados necessários
4. Clique em **Salvar**

### Excluir Veículo

⚠️ **Atenção:**
- Não é possível excluir veículos com OS abertas
- A exclusão é permanente

---

## Módulo: Produtos/Serviços

Catálogo de peças, produtos e serviços oferecidos.

### Listar Produtos/Serviços

**Caminho:** Menu lateral > **Produtos/Serviços** > **Listar**

**Funcionalidades:**
- 🔍 Buscar por nome ou código
- 🏷️ Filtrar por tipo (Produto ou Serviço)
- 📄 Visualizar lista paginada
- ✏️ Editar item
- 🗑️ Excluir item

**Informações Exibidas:**
- Código
- Nome/Descrição
- Tipo (Produto/Serviço)
- Preço de custo
- Preço de venda
- Estoque (para produtos)

### Cadastrar Produto/Serviço

**Caminho:** Menu lateral > **Produtos/Serviços** > **Novo**

**Campos Obrigatórios:**
- ✅ Nome/Descrição
- ✅ Tipo (Produto ou Serviço)
- ✅ Preço de venda

**Campos para Produtos:**
- Código de barras
- Estoque atual
- Estoque mínimo
- Preço de custo
- Fornecedor

**Campos para Serviços:**
- Tempo estimado (horas)
- Mão de obra incluída

**Passo a Passo:**
1. Clique em **Novo**
2. Selecione o **Tipo** (Produto ou Serviço)
3. Preencha os dados
4. Clique em **Salvar**

### Editar Produto/Serviço

1. Localize o item na listagem
2. Clique no ícone de edição (✏️)
3. Altere os dados necessários
4. Clique em **Atualizar**

### Controle de Estoque

**Para Produtos:**
- Visualize estoque atual na listagem
- Alertas visuais para itens com estoque abaixo do mínimo
- Histórico de movimentações *(em desenvolvimento)*

---

## Módulo: Ordens de Serviço

Gestão completa do fluxo de trabalho da oficina.

### Listar Ordens de Serviço

**Caminho:** Menu lateral > **Ordens de Serviço** > **Listar O.S.**

**Funcionalidades:**
- 🔍 Buscar por número, cliente ou veículo
- 🏷️ Filtrar por status:
  - 📋 Aberta
  - ⏳ Em Andamento
  - ✅ Concluída
  - ❌ Cancelada
- 📄 Visualizar lista paginada
- ✏️ Editar OS
- 📄 Visualizar detalhes

**Informações Exibidas:**
- Número da OS
- Cliente
- Veículo (placa)
- Data de abertura
- Status
- Valor total
- Técnico responsável

### Criar Nova Ordem de Serviço

**Caminho:** Menu lateral > **Ordens de Serviço** > **Nova O.S.**

**Fluxo de Criação:**

#### Passo 1: Dados Básicos
- ✅ Selecionar **Cliente**
- ✅ Selecionar **Veículo** (do cliente)
- Data de entrada (automática)
- Quilometragem atual
- Defeito reclamado

#### Passo 2: Serviços e Produtos
- Adicionar serviços a serem executados
- Adicionar produtos/peças necessários
- Definir quantidades
- Valores são calculados automaticamente

#### Passo 3: Observações
- Observações técnicas
- Observações para o cliente
- Prazo estimado de conclusão

**Passo a Passo:**
1. Clique em **Nova O.S.**
2. Selecione o **Cliente**
3. Selecione o **Veículo** do cliente
4. Preencha o defeito reclamado
5. Adicione **Serviços** necessários
6. Adicione **Produtos/Peças** necessários
7. Revise o valor total
8. Clique em **Salvar**

### Editar Ordem de Serviço

1. Localize a OS na listagem
2. Clique no ícone de edição (✏️)
3. Altere os dados necessários
4. Adicione ou remova serviços/produtos
5. Clique em **Atualizar**

⚠️ **Atenção:**
- OS concluídas não podem ser editadas
- OS canceladas não podem ser editadas

### Alterar Status da OS

**Estados possíveis:**
- 📋 **Aberta** → Aguardando início dos trabalhos
- ⏳ **Em Andamento** → Trabalhos em execução
- ✅ **Concluída** → Trabalhos finalizados
- ❌ **Cancelada** → OS cancelada

**Fluxo recomendado:**
```
Aberta → Em Andamento → Concluída
```

### Visualizar Detalhes da OS

**Informações detalhadas:**
- Dados do cliente e veículo
- Lista completa de serviços
- Lista completa de produtos/peças
- Observações técnicas
- Histórico de alterações
- Valor total discriminado

### Imprimir OS

*Funcionalidade em desenvolvimento*

---

## Configurações

**Caminho:** Menu lateral > **Configurações**

### Perfil do Usuário

Visualize e edite suas informações pessoais:
- Nome
- Email
- Avatar *(em desenvolvimento)*

### Alterar Senha

**Passo a Passo:**
1. Acesse **Configurações**
2. Clique em **Alterar Senha**
3. Digite a **senha atual**
4. Digite a **nova senha**
5. Confirme a **nova senha**
6. Clique em **Salvar**

### Preferências

Customize sua experiência:

**Tema:**
- ☀️ Modo Claro
- 🌙 Modo Escuro
- 🔄 Automático (sistema)

**Idioma:**
- 🇧🇷 Português (Brasil)
- 🇺🇸 English *(em desenvolvimento)*

**Notificações:**
- Notificar sobre OS atrasadas
- Notificar sobre estoque baixo
- Notificar sobre novos clientes

---

## Gerenciamento de Usuários

**Perfil necessário:** `ADMIN` com permissão `ViewUsers`

**Caminho:** Menu lateral > **Configurações** > **Usuários**

### Funcionalidades

O sistema permite gestão completa de usuários:

#### Listar Usuários

**Filtros disponíveis:**
- 🔍 Buscar por email ou nome
- 🏷️ Filtrar por perfil (USER, MANAGER, ADMIN, outros)
- ☑️ Incluir usuários inativos

**Informações exibidas:**
- Email
- Perfil atual
- Status (Ativo/Inativo)
- Permissões customizadas
- Ações disponíveis

#### Criar Novo Usuário

**Campos obrigatórios:**
- ✅ Email (será o username)
- ✅ Senha (mínimo 8 caracteres)
- ✅ Perfil (USER, MANAGER, ADMIN ou customizado)

**Permissões:**
- Selecione permissões adicionais além do perfil base
- Permissões organizadas por módulo
- Opção de "Marcar todas" por módulo

**Passo a Passo:**
1. Acesse **Usuários** > **Novo Usuário**
2. Digite o **Email**
3. Digite a **Senha** (8+ caracteres)
4. Selecione o **Perfil**
5. Selecione **Permissões customizadas** (opcional)
6. Clique em **Salvar**

#### Editar Usuário

**Ações disponíveis:**
1. **Alterar Perfil** - Muda o perfil base do usuário
2. **Gerenciar Permissões** - Adiciona/remove permissões específicas
3. **Desativar/Ativar** - Controla acesso sem excluir

**Gerenciar Permissões:**
- Visualize permissões atuais
- Adicione permissões extra
- Remova permissões (com cuidado!)
- Permissões são organizadas por módulo

#### Desativar Usuário

**O que acontece:**
- ✅ Usuário não consegue fazer login
- ✅ Dados são preservados
- ✅ Pode ser reativado a qualquer momento
- ❌ **Não pode desativar o último ADMIN**

**Passo a Passo:**
1. Localize o usuário
2. Clique em **Ações (⋮)** > **Desativar**
3. Confirme a ação

#### Reativar Usuário

1. Ative o filtro **Incluir inativos**
2. Localize o usuário desativado
3. Clique em **Ações (⋮)** > **Reativar**
4. Confirme a ação

#### Excluir Usuário (Permanente)

⚠️ **ATENÇÃO - Ação Irreversível!**

**Requisitos:**
- Usuário deve estar **desativado** primeiro
- Não pode ser o último ADMIN

**Passo a Passo:**
1. Desative o usuário primeiro
2. No usuário desativado, clique em **Ações (⋮)** > **Excluir Permanentemente**
3. Confirme a ação no dialog

---

## Gerenciamento de Perfis

**Perfil necessário:** `ADMIN` com permissão `ManageRoles`

**Caminho:** Menu lateral > **Configurações** > **Perfis**

### O que são Perfis?

Perfis (ou Roles) são conjuntos de permissões que definem o que um usuário pode fazer no sistema.

**Perfis do Sistema:**
- 🔴 **ADMIN** - Acesso total ao sistema
- 🟡 **MANAGER** - Gestão operacional completa
- 🟢 **USER** - Acesso básico

### Listar Perfis

**Informações exibidas:**
- Nome do perfil
- Quantidade de usuários com este perfil
- Tipo (Sistema ou Customizado)
- Ações disponíveis

### Criar Novo Perfil

**Quando criar um perfil customizado?**
- Necessidade de permissões específicas
- Separação de responsabilidades
- Controle mais granular

**Passo a Passo:**
1. Acesse **Perfis** > **Novo Perfil**
2. Digite o **Nome** do perfil (ex: "RECEPCIONISTA")
3. Selecione as **Permissões**:
   - Permissões organizadas por módulo
   - Use "Marcar todos" para módulos completos
   - Desmarque permissões não necessárias
4. Clique em **Salvar**

**Exemplo de Perfil Personalizado:**

**RECEPCIONISTA**
- ✅ ViewClientes, CreateClientes
- ✅ ViewVeiculos, CreateVeiculos  
- ✅ ViewOS, CreateOS
- ❌ DeleteClientes, DeleteVeiculos
- ❌ ViewUsers, CreateUsers

### Editar Perfil

**Ações disponíveis:**
- Adicionar/remover permissões
- Visualizar usuários com este perfil

⚠️ **Atenção:**
- Não é possível editar perfis do sistema (ADMIN, MANAGER, USER)
- Alterações afetam todos os usuários com este perfil

**Passo a Passo:**
1. Localize o perfil na listagem
2. Clique em **Editar**
3. Modifique as **Permissões**
4. Clique em **Salvar**

### Excluir Perfil

⚠️ **Requisitos:**
- Perfil não pode ser do sistema
- Nenhum usuário pode estar usando este perfil

**Passo a Passo:**
1. Remova o perfil de todos os usuários primeiro
2. Localize o perfil
3. Clique em **Excluir**
4. Confirme a ação

---

## Perfis e Permissões

### Matriz de Permissões

#### Permissões por Módulo

**Clientes:**
- `ViewClientes` - Visualizar clientes
- `CreateClientes` - Criar clientes
- `EditClientes` - Editar clientes
- `DeleteClientes` - Excluir clientes

**Veículos:**
- `ViewVeiculos` - Visualizar veículos
- `CreateVeiculos` - Criar veículos
- `EditVeiculos` - Editar veículos
- `DeleteVeiculos` - Excluir veículos

**Ordens de Serviço:**
- `ViewOS` - Visualizar OS
- `CreateOS` - Criar OS
- `EditOS` - Editar OS
- `DeleteOS` - Excluir OS
- `ApproveOS` - Aprovar OS

**Produtos/Serviços:**
- `ViewProdutos` - Visualizar produtos
- `CreateProdutos` - Criar produtos
- `EditProdutos` - Editar produtos
- `DeleteProdutos` - Excluir produtos

**Usuários:**
- `ViewUsers` - Visualizar usuários
- `CreateUsers` - Criar usuários
- `EditUsers` - Editar usuários
- `DeleteUsers` - Excluir usuários
- `ManagePermissions` - Gerenciar permissões

**Relatórios:**
- `ViewReports` - Visualizar relatórios
- `ExportReports` - Exportar relatórios

### Permissões por Perfil

| Permissão | USER | MANAGER | ADMIN |
|-----------|------|---------|-------|
| **Clientes** |
| ViewClientes | ✅ | ✅ | ✅ |
| CreateClientes | ✅ | ✅ | ✅ |
| EditClientes | ❌ | ✅ | ✅ |
| DeleteClientes | ❌ | ✅ | ✅ |
| **Veículos** |
| ViewVeiculos | ✅ | ✅ | ✅ |
| CreateVeiculos | ❌ | ✅ | ✅ |
| EditVeiculos | ❌ | ✅ | ✅ |
| DeleteVeiculos | ❌ | ✅ | ✅ |
| **Ordens de Serviço** |
| ViewOS | ✅ | ✅ | ✅ |
| CreateOS | ✅ | ✅ | ✅ |
| EditOS | ❌ | ✅ | ✅ |
| ApproveOS | ❌ | ✅ | ✅ |
| **Produtos** |
| ViewProdutos | ✅ | ✅ | ✅ |
| CreateProdutos | ❌ | ✅ | ✅ |
| EditProdutos | ❌ | ✅ | ✅ |
| DeleteProdutos | ❌ | ✅ | ✅ |
| **Usuários** |
| ViewUsers | ❌ | ❌ | ✅ |
| CreateUsers | ❌ | ❌ | ✅ |
| EditUsers | ❌ | ❌ | ✅ |
| DeleteUsers | ❌ | ❌ | ✅ |
| ManagePermissions | ❌ | ❌ | ✅ |
| **Relatórios** |
| ViewReports | ❌ | ✅ | ✅ |
| ExportReports | ❌ | ✅ | ✅ |

---

## Perguntas Frequentes

### 1. Como dar permissões extras para um usuário sem mudar o perfil?

**R:** Use a funcionalidade de **Permissões Customizadas**:
1. Acesse **Usuários** > **Editar**
2. Clique em **Gerenciar Permissões**
3. Adicione as permissões extras
4. Salve

### 2. Posso excluir um cliente com veículos cadastrados?

**R:** Não. Primeiro você precisa:
1. Excluir ou transferir os veículos
2. Depois excluir o cliente

### 3. Como desativar um usuário temporariamente?

**R:** Use a função **Desativar**:
1. Vá em **Usuários**
2. Clique em **Ações (⋮)** > **Desativar**
3. O usuário não poderá fazer login
4. Pode ser reativado a qualquer momento

### 4. Perdi a senha de administrador. E agora?

**R:** Entre em contato com o suporte técnico. Será necessário acesso direto ao banco de dados para reset.

### 5. Posso ter mais de um ADMIN?

**R:** Sim! Recomendamos ter pelo menos 2 administradores para redundância.

### 6. Como criar um perfil que só possa ver relatórios?

**R:** Crie um perfil customizado:
1. Acesse **Perfis** > **Novo Perfil**
2. Nome: "AUDITOR"
3. Selecione apenas: `ViewReports` e `ExportReports`
4. Salve

### 7. Posso alterar as permissões de um perfil do sistema?

**R:** Não. Perfis do sistema (ADMIN, MANAGER, USER) não podem ser editados. Crie um perfil customizado se precisar de permissões diferentes.

### 8. Como saber quais usuários têm um perfil específico?

**R:** Na listagem de **Perfis**, cada perfil mostra a quantidade de usuários. Clique em **Detalhes** para ver a lista completa.

### 9. O que acontece se eu excluir um perfil que está em uso?

**R:** O sistema não permite. Primeiro você precisa alterar o perfil de todos os usuários que o utilizam.

### 10. Como conceder acesso temporário a um usuário?

**R:** Use **Permissões Customizadas**:
1. Adicione as permissões temporárias
2. Quando não precisar mais, remova as permissões
3. O usuário volta ao acesso base do perfil

---

## Suporte Técnico

### Contatos

📧 **Email:** suporte@autocarerp.com  
📱 **Telefone:** (11) 9999-9999  
🌐 **Site:** www.autocarerp.com

### Horário de Atendimento

**Segunda a Sexta:** 08:00 - 18:00  
**Sábado:** 08:00 - 12:00  
**Domingo:** Fechado

---

**Versão do Manual:** 1.0  
**Última Atualização:** Dezembro 2025  
**Sistema:** AutoCarERP v1.0
