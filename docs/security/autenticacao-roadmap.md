# Plano de Implementação – Autenticação e Autorização

Este documento lista as tarefas e dependências necessárias para atender os requisitos de autenticação/autorização definidos em `docs/security/autenticacao-acoes-os.md`.

---

## 1. Dependências e Instalações (ASP.NET Core Identity)

| Projeto                      | Pacote / Recurso                                                           | Comando/Observação                                                                                           |
|------------------------------|----------------------------------------------------------------------------|--------------------------------------------------------------------------------------------------------------|
| `AutoCarERP.Infra`           | `Microsoft.AspNetCore.Identity.EntityFrameworkCore`                       | `dotnet add AutoCarERP.Infra package Microsoft.AspNetCore.Identity.EntityFrameworkCore`                      |
| `AutoCarERP.Infra`           | `Microsoft.EntityFrameworkCore.Tools` (se ainda não referenciado)         | `dotnet add AutoCarERP.Infra package Microsoft.EntityFrameworkCore.Tools`                                    |
| `AutoCarERP.API`             | `Microsoft.AspNetCore.Authentication.JwtBearer`                           | `dotnet add AutoCarERP.API package Microsoft.AspNetCore.Authentication.JwtBearer`                            |
| `AutoCarERP.API`             | `Microsoft.AspNetCore.Identity.UI` (útil para scaffolding/recursos padrão)| `dotnet add AutoCarERP.API package Microsoft.AspNetCore.Identity.UI`                                         |
| `AutoCarERP.API`             | `Swashbuckle.AspNetCore` (já instalado, manter para documentar JWT)       | Nenhuma ação (já presente)                                                                                   |
| **Ferramentas externas**     | OpenSSL (ou similar) para gerar chaves/secret JWT                         | `sudo apt install openssl` ou usar secrets manager do ambiente                                               |

> **Observações**  
> - Ajuste as strings de conexão no `appsettings` para incluir as novas tabelas de Identity.  
> - Configure User Secrets ou variáveis de ambiente para armazenar o `Jwt:Issuer`, `Jwt:Audience` e `Jwt:SigningKey`.

---

## 2. Roadmap de Tarefas

### Fase 1 – Modelo de Identidade e Persistência
1. Configurar o ASP.NET Core Identity no `AppDbContext` (herdar de `IdentityDbContext` ou adicionar `Identity` com `AddIdentityCore`), garantindo a criação das tabelas (`AspNetUsers`, `AspNetRoles`, `AspNetUserRoles`, `AspNetRoleClaims` etc.) no projeto `AutoCarERP.Infra`.
2. Adicionar migrations e atualizar o banco com as novas tabelas.
3. Popular seed inicial usando os managers do Identity (`RoleManager`, `UserManager`): criar usuário administrador, roles `ADMIN`/`USER` e claims de permissão (`CLIENT_*`, `VEHICLE_*`, `PRODUCT_SERVICE_*`, `OS_*`, `REPORT_GENERATE`).

### Fase 2 – Serviços de Autenticação
4. Implementar endpoints de registro (opcional), login e refresh token em `AutoCarERP.API` (ex.: `AuthController`) consumindo `UserManager`/`SignInManager`.
5. Gerar JWT contendo `sub`, `role` e claims `perm`. Armazenar refresh tokens (tabela/persistência simples) caso necessário.
6. Configurar `AddAuthentication().AddJwtBearer()` e `AddAuthorization()` no `Program.cs`, lendo os valores de configuração seguros.

### Fase 3 – Contexto do Usuário e Auditoria
7. Criar `IUserContext` (no projeto Application) e implementação que lê do `IHttpContextAccessor` + `UserManager`/claims do Identity.
8. Implementar serviço de auditoria (`IAuditLogger`) e tabela `AuditLogs` para registrar ação, usuário, data/hora e entidade afetada.
9. Atualizar services (`ClienteService`, `VeiculoService`, etc.) para registrar auditoria nas operações de escrita relevantes.

### Fase 4 – Policies e Proteção de Endpoints
10. Centralizar o catálogo de permissões em uma classe (`Permissions`) no projeto Application/Core.
11. Registrar policies com base nessas permissões (`options.AddPolicy(Permissions.Client.Create, policy => ... )`).
12. Decorar controllers com `[Authorize(Policy = Permissions.Client.Create)]` etc., alinhando cada rota à matriz fornecida.
13. Atualizar o Swagger para exigir token JWT (SecurityDefinition `Bearer` + `[Authorize]`).

### Fase 5 – Hardening e Operação
14. Implementar regras de senha/lockout, expiração de token, refresh tokens revogáveis e (opcional) MFA.
15. Criar documentação operacional: como provisionar usuários/roles, rotinas de rotação de chaves JWT, backup da tabela de auditoria.
16. Adicionar testes de integração cobrindo fluxos de login, autorização negada e acesso permitido com permissões corretas.

---

## 3. Entregáveis Esperados
- Migrations aplicadas com tabelas de identidade e auditoria.
- Endpoints de autenticação funcionando e documentados (Swagger).
- Controllers protegidos por policies coerentes com o documento de requisitos.
- Logs de auditoria gerados para operações críticas.
- Guia operacional descrevendo como administrar usuários, permissões e chaves de segurança.

Seguindo as fases em ordem, você constrói primeiro a base de usuários/permissões, depois implementa o fluxo de autenticação, expõe o contexto de usuário para auditoria e, por fim, protege todas as rotas com as policies adequadas.***
