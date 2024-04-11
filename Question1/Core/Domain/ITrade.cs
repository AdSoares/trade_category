namespace TradeCategory.Core.Domain
{
    public interface ITrade
    {
        double Value { get; }
        string ClientSector { get; }
    }
}
