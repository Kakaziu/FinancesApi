using System.ComponentModel;

namespace FinancesApi.Enums
{
    public enum TransactionType
    {
        [Description("Entrada de caixa")]
        Inflow = 0,
        [Description("Saída de caixa")]
        OutFlow = 1
    }
}
