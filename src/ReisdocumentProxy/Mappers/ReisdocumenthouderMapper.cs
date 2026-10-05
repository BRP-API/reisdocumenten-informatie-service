
using Gba = HaalCentraal.ReisdocumentProxy.Generated.Gba;
using HaalCentraal.ReisdocumentProxy.Generated;

namespace ReisdocumentProxy.Mappers;

public static class ReisdocumenthouderMapper
{
    
    public static Reisdocumenthouder? Map(this Gba.GbaReisdocumenthouder? source)
    {
        return source == null
            ? null
            : new Reisdocumenthouder
            {
                Burgerservicenummer = source.Burgerservicenummer,
                GeheimhoudingPersoonsgegevens = source.GeheimhoudingPersoonsgegevens.GetValueOrDefault() != 0,
                InOnderzoek = source.InOnderzoek.Map(),
                OpschortingBijhouding = source.OpschortingBijhouding.Map(),
                AdditionalProperties = source.AdditionalProperties
            };
    }
}

