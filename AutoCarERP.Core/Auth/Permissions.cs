namespace AutoCarERP.Core.Auth;

public static class Permissions
{
    public const string ClaimType = "perm";

    public static class Cliente
    {
        public const string Create = "CLIENT_CREATE";
        public const string Read = "CLIENT_READ";
    }

    public static class Veiculo
    {
        public const string Create = "VEHICLE_CREATE";
        public const string Read = "VEHICLE_READ";
    }

    public static class ProdutoServico
    {
        public const string Create = "PRODUCT_SERVICE_CREATE";
        public const string Read = "PRODUCT_SERVICE_READ";
    }

    public static class OrdemDeServico
    {
        public const string Create = "OS_CREATE";
        public const string Read = "OS_READ";
        public const string ItemAdd = "OS_ITEM_ADD";
        public const string StatusChange = "OS_STATUS_CHANGE";
        public const string Finalize = "OS_FINALIZE";
        public const string PaymentUpdate = "OS_PAYMENT_UPDATE";
    }

    public static class Relatorio
    {
        public const string Generate = "REPORT_GENERATE";
    }

    public static IReadOnlyList<string> All =>
    [
        Cliente.Create,
        Cliente.Read,
        Veiculo.Create,
        Veiculo.Read,
        ProdutoServico.Create,
        ProdutoServico.Read,
        OrdemDeServico.Create,
        OrdemDeServico.Read,
        OrdemDeServico.ItemAdd,
        OrdemDeServico.StatusChange,
        OrdemDeServico.Finalize,
        OrdemDeServico.PaymentUpdate,
        Relatorio.Generate
    ];
}
