using HC = HaalCentraal.ReisdocumentProxy.Generated;
using Gba = HaalCentraal.ReisdocumentProxy.Generated.Gba;

namespace ReisdocumentProxy.Mappers;

public static class RaadpleegMetReisdocumentnummerResponseMapper
{
public static HC.RaadpleegMetReisdocumentnummerResponse? Map(this Gba.RaadpleegMetReisdocumentnummerResponse? source)
    {
        return source == null
            ? null
            : new HC.RaadpleegMetReisdocumentnummerResponse
            {
                Reisdocumenten = source.Reisdocumenten?.Select(r => r.Map()).ToList()
            };
    }
}
