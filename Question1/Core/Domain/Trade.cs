using TradeCategory.Core.Utils;

namespace TradeCategory.Core.Domain
{
    public class Trade : ITrade
    {
        public Trade(double value, string clientSector)
        {
            Value = value;

            if (string.IsNullOrEmpty(clientSector) ||
                (clientSector.ToUpper() != Constants.CLIENT_SECTOR_PUBLIC.ToUpper() &&
                 clientSector.ToUpper() != Constants.CLIENT_SECTOR_PRIVATE.ToUpper())
               )
            {
                throw new ArgumentException($"Argument must be '{Constants.CLIENT_SECTOR_PUBLIC}' or '{Constants.CLIENT_SECTOR_PRIVATE}'.", nameof(clientSector));
            }

            ClientSector = clientSector;
        }

        public double Value { get; private set; }

        public string ClientSector { get; private set; }
    }
}
