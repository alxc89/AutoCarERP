    namespace AutoCarERP.Application.DTOs.Cliente;

    public class ClienteReadDto
    {
        public int Codigo { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string? Telefone { get; set; }

        public string? CpfCnpj { get; set; }

        public string? Endereco { get; set; }

        public string? Email { get; set; }
    }