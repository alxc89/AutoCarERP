# Guia de Uso da API AutoCarERP

Este documento descreve como consumir os endpoints principais expostos pela API (`AutoCarERP.API`). Todos os exemplos usam JSON e assumem o ambiente de desenvolvimento (`https://localhost:7200`).

## Autenticação e headers

- Adicione `Content-Type: application/json` em todas as requisições com corpo.
- Após fazer login, inclua `Authorization: Bearer {accessToken}` em todas as rotas protegidas.
- Tokens expiram conforme `Jwt:AccessTokenMinutes`; use o refresh token quando necessário.

### Fluxo de autenticação

1. **Login**
   ```
   POST /api/v1/Auth/login
   {
     "email": "admin@autocarerp.local",
     "password": "Admin@123"
   }
   ```
   **Resposta**
   ```json
   {
     "accessToken": "<jwt>",
     "accessTokenExpiresAt": "2025-01-10T15:30:00Z",
     "refreshToken": "<refresh>",
     "refreshTokenExpiresAt": "2025-01-17T15:30:00Z"
   }
   ```

2. **Refresh**
   ```
   POST /api/v1/Auth/refresh
   {
     "refreshToken": "<refresh>"
   }
   ```

3. **Registro** (somente ADMIN)
   ```
   POST /api/v1/Auth/register
   {
     "email": "novo.usuario@empresa.com",
     "password": "Senha@123",
     "role": "USER"
   }
   ```

## Clientes

| Verbo/rota | Permissão | Observações |
| --- | --- | --- |
| `POST /api/v1/Cliente/create` | `CLIENT_CREATE` | Cria cliente |
| `GET /api/v1/Cliente/get-by-cod/{cod}` | `CLIENT_READ` | Consulta por código |
| `GET /api/v1/Cliente/get-all?search=&page=1&pageSize=20` | `CLIENT_READ` | Lista paginada |
| `PUT /api/v1/Cliente/update/{cod}` | `CLIENT_CREATE` | Atualiza |
| `DELETE /api/v1/Cliente/delete/{cod}` | `CLIENT_CREATE` | Remove |

**Exemplo de criação**
```json
{
  "nome": "João da Oficina",
  "telefone": "11999990000",
  "cpfCnpj": "12345678900",
  "endereco": "Rua A, 123",
  "email": "joao@cliente.com"
}
```

## Veículos

| Verbo/rota | Permissão |
| --- | --- |
| `POST /api/v1/Veiculo/create` | `VEHICLE_CREATE` |
| `GET /api/v1/Veiculo/get-by-placa/{placa}` | `VEHICLE_READ` |
| `GET /api/v1/Veiculo/get-all` | `VEHICLE_READ` |
| `PUT /api/v1/Veiculo/update/{cod}` | `VEHICLE_CREATE` |
| `DELETE /api/v1/Veiculo/delete/{cod}` | `VEHICLE_CREATE` |

**Body (create/update)**
```json
{
  "placa": "ABC1D23",
  "marca": "Ford",
  "modelo": "Fiesta",
  "cor": "Prata",
  "ano": 2018
}
```

## Produtos/Serviços

| Verbo/rota | Permissão |
| --- | --- |
| `POST /api/v1/ProdutoServico/create` | `PRODUCT_SERVICE_CREATE` |
| `GET /api/v1/ProdutoServico/get-by-cod/{cod}` | `PRODUCT_SERVICE_READ` |
| `GET /api/v1/ProdutoServico/get-all` | `PRODUCT_SERVICE_READ` |
| `PUT /api/v1/ProdutoServico/update/{cod}` | `PRODUCT_SERVICE_CREATE` |
| `DELETE /api/v1/ProdutoServico/delete/{cod}` | `PRODUCT_SERVICE_CREATE` |

**Body (create/update)**
```json
{
  "nome": "Troca de óleo",
  "descricao": "Troca completa com filtro",
  "fornecedor": "Mobil",
  "custo": 80.0,
  "valor": 120.0
}
```

## Ordens de Serviço

| Verbo/rota | Permissão |
| --- | --- |
| `POST /api/v1/OrdemDeServico/create` | `OS_CREATE` |
| `GET /api/v1/OrdemDeServico/get-by-cod/{cod}` | `OS_READ` |
| `GET /api/v1/OrdemDeServico/get-all` | `OS_READ` |
| `PUT /api/v1/OrdemDeServico/update/{cod}` | `OS_STATUS_CHANGE` |
| `DELETE /api/v1/OrdemDeServico/delete/{cod}` | `OS_STATUS_CHANGE` |

**Body (create/update)**
```json
{
  "horaAbertura": "2025-01-10T12:00:00Z",
  "horaFechamento": null,
  "veiculoId": 1,
  "clienteId": 1,
  "produtoServicoId": 2,
  "quantidade": 1,
  "valorUnitario": 120.0,
  "valorTotal": 120.0,
  "observacao": "Solicitou revisão completa.",
  "status": "Aberta"
}
```

## Respostas de erro comuns

| Status | Motivo | Ação recomendada |
| --- | --- | --- |
| `401 Unauthorized` | Token ausente ou inválido | Faça login/refresh. |
| `403 Forbidden` | Sem permissão/claim necessária | Verifique se o usuário possui a role/permissão correta. |
| `422/400` | Validação de dados | Corrija o payload enviado. |
| `500` | Erro inesperado | Consulte logs/`AuditLogs` para detalhes. |

## Dicas

- Use o Swagger (`/swagger`) para testar rapidamente (inclua o JWT no botão `Authorize`).
- Registre sempre novos usuários via endpoint `register` com role adequada.
- Para auditoria, consulte a tabela `AuditLogs` (p.ex. via SQL) para ver histórico das operações.***
