using Gba = HaalCentraal.ReisdocumentProxy.Generated.Gba;
using HaalCentraal.ReisdocumentProxy.Generated;
using Brp.Shared.DtoMappers.Mappers;

namespace ReisdocumentProxy.Mappers;

public static class ReisdocumenthouderMapper
{
    
    public static Reisdocumenthouder? Map(this Gba.GbaReisdocumenthouder? source)
    {
        if (source == null) return null;

        bool? geheimhoudingPersoonsgegevens = (source.GeheimhoudingPersoonsgegevens.GetValueOrDefault() != 0) 
            ? true 
            : null;
        return new Reisdocumenthouder
            {
                Burgerservicenummer = source.Burgerservicenummer,
                GeheimhoudingPersoonsgegevens = geheimhoudingPersoonsgegevens,
                InOnderzoek = source.InOnderzoek.Map(),
                OpschortingBijhouding = source.OpschortingBijhouding.Map(),
                AdditionalProperties = source.AdditionalProperties
            };
    }
}

