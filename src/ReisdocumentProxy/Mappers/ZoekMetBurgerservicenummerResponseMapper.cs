using HC = HaalCentraal.ReisdocumentProxy.Generated;
using Gba = HaalCentraal.ReisdocumentProxy.Generated.Gba;

namespace ReisdocumentProxy.Mappers;

public static class ZoekMetBurgerservicenummerResponseMapper
{
public static HC.ZoekMetBurgerservicenummerResponse? Map(this Gba.ZoekMetBurgerservicenummerResponse? source)
    {
        return source == null
            ? null
            : new HC.ZoekMetBurgerservicenummerResponse
            {
                Reisdocumenten = source.Reisdocumenten?.Select(r => r.Map()).ToList(),
                AdditionalProperties = source.AdditionalProperties
            };
    }
}
