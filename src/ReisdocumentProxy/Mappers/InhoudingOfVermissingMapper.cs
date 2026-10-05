using HC = HaalCentraal.ReisdocumentProxy.Generated;
using Gba = HaalCentraal.ReisdocumentProxy.Generated.Gba;

namespace ReisdocumentProxy.Mappers;

public static class InhoudingOfVermissingMapper
{
 public static HC.InhoudingOfVermissing? Map(this Gba.GbaInhoudingOfVermissing? source)
    {
        return source == null
            ? null
            : new HC.InhoudingOfVermissing
            {
                Datum = source.Datum.Map(),
                Aanduiding = source.Aanduiding.Map(),
                AdditionalProperties = source.AdditionalProperties
            };
    }
}
