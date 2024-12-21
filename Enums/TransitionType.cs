using System.ComponentModel;

namespace FinancesApi.Enums
{
    public enum TransitionType
    {
        [Description("Entrada de dinheiro.")]
        Inflow = 0,
        [Description("Saída de dinheiro.")]
        OutFlow = 1
    }
}
