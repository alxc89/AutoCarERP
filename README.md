# AutoCarERP

AutoCarERP é um ERP focado em oficinas mecânicas que oferece APIs REST para gerenciar clientes, veículos, produtos/serviços e ordens de serviço. A solução está organizada em múltiplos projetos (API, Application, Core e Infra) e utiliza ASP.NET Core 9 + Entity Framework Core com PostgreSQL, autenticação/autorização baseada em ASP.NET Identity e JWT, e documentação por Swagger.

## Estrutura dos projetos

| Projeto | Descrição |
| --- | --- |
| `AutoCarERP.API` | API REST (controllers, autenticação, Swagger). |
| `AutoCarERP.Application` | DTOs, serviços de domínio, mapeadores e contratos. |
| `AutoCarERP.Core` | Entidades, repositórios e utilitários compartilhados (ex.: catálogo de permissões). |
| `AutoCarERP.Infra` | EF Core (DbContext, configurações, repositórios genéricos), Identity e auditoria. |
| `docs/` | Documentação (roadmap de segurança/autenticação, uso da API etc.). |

## Requisitos

- .NET SDK 9.0.x
- PostgreSQL 14+ (ajuste `ConnectionStrings:DefaultConnection` em `AutoCarERP.API/appsettings*.json`)
- Ferramenta `dotnet-ef` (instale com `dotnet tool install --global dotnet-ef`)

## Configuração inicial

1. **Pacotes e restauração**
   ```bash
   dotnet restore
   ```

2. **Configurar secrets**  
   - Atualize `Jwt:SigningKey` com um valor forte (64 bytes).  
   - Use [User Secrets](https://learn.microsoft.com/aspnet/core/security/app-secrets) ou variáveis de ambiente para ambientes reais.

3. **Migrations e banco**
   ```bash
   dotnet ef database drop --project AutoCarERP.Infra/AutoCarERP.Infra.csproj --startup-project AutoCarERP.API/AutoCarERP.API.csproj
   dotnet ef migrations add InitIdentity --project AutoCarERP.Infra/AutoCarERP.Infra.csproj --startup-project AutoCarERP.API/AutoCarERP.API.csproj
   dotnet ef database update --project AutoCarERP.Infra/AutoCarERP.Infra.csproj --startup-project AutoCarERP.API/AutoCarERP.API.csproj
   ```

4. **Seed inicial**  
   O `IdentitySeeder` executado no `Program.cs` cria:
   - Usuário admin: `admin@autocarerp.local` / `Admin@123`
   - Roles: `ADMIN`, `USER`
   - Claims de permissão conforme `AutoCarERP.Core/Auth/Permissions.cs`

## Executando a API

```bash
dotnet run --project AutoCarERP.API/AutoCarERP.API.csproj
```

A API expõe Swagger em `https://localhost:7200/swagger` (ou porta HTTP configurada). Todos os endpoints, exceto `/api/v1/Auth/login` e `/api/v1/Auth/refresh`, exigem JWT.

## Autenticação

- **Login**: `POST /api/v1/Auth/login` com JSON `{ "email": "...", "password": "..." }`.  
- **Token de acesso**: Bearer token retornado em `accessToken`.  
- **Refresh**: `POST /api/v1/Auth/refresh` com `{ "refreshToken": "..." }`.  
- **Registro de usuários**: `POST /api/v1/Auth/register`, restrito a `ADMIN`.

As permissões são aplicadas via policies (`Permissions.Cliente.Create`, `Permissions.OrdemDeServico.StatusChange`, etc.). Consulte `docs/api/api-usage.md` para exemplos de requisição/resposta.

## Auditoria

Operações de escrita em clientes, veículos, produtos/serviços e ordens geram registros na tabela `AuditLogs` contendo usuário, ação e payload.

## Documentação adicional

- `docs/security/autenticacao-roadmap.md`: roadmap detalhado da implementação de autenticação/autorização.
- `docs/security/autenticacao-acoes-os.md`: matriz de permissões por ação.
- `docs/api/api-usage.md`: guia de uso dos endpoints com JSON de exemplo.

## Comandos úteis

```bash
# Limpar build
dotnet clean

# Restaurar pacotes
dotnet restore

# Rodar API em modo watch
dotnet watch --project AutoCarERP.API/AutoCarERP.API.csproj run
```

## Próximos passos

- Implementar testes automatizados (fase 5.16 do roadmap).
- Configurar CI/CD e provisionamento seguro de secrets.
- Expandir o domínio (relatórios, workflows de O.S. avançados).***
