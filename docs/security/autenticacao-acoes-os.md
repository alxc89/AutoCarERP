# Autenticação e Autorização por Ação – Sistema de Ordem de Serviço

## 1. Perfis de Acesso

- **Usuário (USER)**
  - Opera o dia a dia do sistema (atendimento, oficina, etc.).
- **Administrador (ADMIN)**
  - Gerencia cadastros, configurações e tem acesso ampliado.

Todas as ações abaixo exigem **autenticação** (login válido) de `USER` ou `ADMIN`.

---

## 2. Regras Gerais

- Toda requisição deve vir com **sessão/token válido**.
- Ações são liberadas por **permissão**, associada a cada perfil.
- `ADMIN` pode ter **todas** as permissões; `USER` apenas as necessárias ao trabalho.
- Toda ação que altera dados deve gerar **registro de auditoria**:
  - Usuário que executou
  - Data/hora
  - Ação
  - Registro afetado (ex.: ClienteId, OsId)

---

## 3. Ações por Tipo

### 3.1. Cadastrar Clientes

- **Descrição:** criar um novo cliente.
- **Quem pode:**
  - `USER`  
  - `ADMIN`
- **Permissão sugerida:** `CLIENT_CREATE`
- **Observações:**
  - Registrar quem criou o cliente.

---

### 3.2. Consultar Clientes

- **Descrição:** listar e ver detalhes de clientes.
- **Quem pode:**
  - `USER`  
  - `ADMIN`
- **Permissão sugerida:** `CLIENT_READ`

---

### 3.3. Cadastrar Veículos

- **Descrição:** criar um veículo vinculado a um cliente.
- **Quem pode:**
  - `USER`  
  - `ADMIN`
- **Permissão sugerida:** `VEHICLE_CREATE`

---

### 3.4. Consultar Veículos

- **Descrição:** listar e ver detalhes de veículos.
- **Quem pode:**
  - `USER`  
  - `ADMIN`
- **Permissão sugerida:** `VEHICLE_READ`

---

### 3.5. Cadastrar Produtos/Serviços

- **Descrição:** criar itens de produto/serviço usados nas O.S.
- **Quem pode:**
  - `ADMIN` (recomendado)
- **Permissão sugerida:** `PRODUCT_SERVICE_CREATE`

---

### 3.6. Consultar Produtos/Serviços

- **Descrição:** listar produtos/serviços para uso nas O.S.
- **Quem pode:**
  - `USER`  
  - `ADMIN`
- **Permissão sugerida:** `PRODUCT_SERVICE_READ`

---

### 3.7. Abrir / Cadastrar Ordem de Serviço

- **Descrição:** criar uma nova O.S. para um cliente/veículo.
- **Quem pode:**
  - `USER`  
  - `ADMIN`
- **Permissão sugerida:** `OS_CREATE`

---

### 3.8. Consultar Ordem de Serviço

- **Descrição:** listar e ver detalhes das O.S.
- **Quem pode:**
  - `USER`  
  - `ADMIN`
- **Permissão sugerida:** `OS_READ`

---

### 3.9. Adicionar Produtos/Serviços à O.S.

- **Descrição:** incluir itens (produtos/serviços) em uma O.S. já aberta.
- **Quem pode:**
  - `USER`  
  - `ADMIN`
- **Permissão sugerida:** `OS_ITEM_ADD`
- **Observações:**
  - Apenas se a O.S. estiver em status que permita edição (ex.: Aberta, Em execução).

---

### 3.10. Registrar Baixa no Pagamento da O.S.

> **Obs.:** Mantida a ação do diagrama, mas sem separar papel “financeiro”.

- **Descrição:** marcar pagamento da O.S. (total ou parcial).
- **Quem pode:**
  - `USER` (se a empresa permitir)  
  - `ADMIN`
- **Permissão sugerida:** `OS_PAYMENT_UPDATE`
- **Observações:**
  - Deve registrar valor, data e forma de pagamento.
  - Ideal que empresas mais rígidas usem apenas `ADMIN` aqui.

---

### 3.11. Alterar Status da O.S.

- **Descrição:** mudar o status da O.S. (Aberta, Em execução, Aguardando, Finalizada, etc.).
- **Quem pode:**
  - `USER`  
  - `ADMIN`
- **Permissão sugerida:** `OS_STATUS_CHANGE`
- **Observações:**
  - Validar se a transição é permitida (ex.: não sair de Aberta direto para Finalizada, se o fluxo não permitir).

---

### 3.12. Finalizar Ordem de Serviço

- **Descrição:** concluir a O.S. e marcá-la como “Finalizada”.
- **Quem pode:**
  - `USER` (responsável pela O.S.)  
  - `ADMIN`
- **Permissão sugerida:** `OS_FINALIZE`
- **Observações:**
  - Em muitas empresas, apenas `ADMIN` pode finalizar se não houver baixa financeira registrada.

---

### 3.13. Gerar Relatórios

- **Descrição:** gerar relatórios (O.S. por período, clientes, veículos, etc.).
- **Quem pode:**
  - `USER` (quando necessário)  
  - `ADMIN`
- **Permissão sugerida:** `REPORT_GENERATE`

---

## 4. Resumo das Permissões

Sugestão de mapeamento rápido:

- Clientes  
  - `CLIENT_CREATE`, `CLIENT_READ`
- Veículos  
  - `VEHICLE_CREATE`, `VEHICLE_READ`
- Produtos/Serviços  
  - `PRODUCT_SERVICE_CREATE`, `PRODUCT_SERVICE_READ`
- Ordem de Serviço  
  - `OS_CREATE`, `OS_READ`, `OS_ITEM_ADD`, `OS_STATUS_CHANGE`, `OS_FINALIZE`, `OS_PAYMENT_UPDATE`
- Relatórios  
  - `REPORT_GENERATE`

Cada perfil (USER, ADMIN) deve receber o conjunto de permissões adequado à função que exerce na empresa.
