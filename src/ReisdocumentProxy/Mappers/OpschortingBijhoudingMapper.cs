using Brp.Shared.DtoMappers.BrpApiDtos;
using HC = HaalCentraal.ReisdocumentProxy.Generated;
using Gba = HaalCentraal.ReisdocumentProxy.Generated.Gba;

namespace ReisdocumentProxy.Mappers;

public static class OpschortingBijhoudingMapper
{
    //  public static OpschortingBijhouding? Map(this Gba.GbaOpschortingBijhouding source)
    // {
    //       return source == null
    //         ? null
    //         : new OpschortingBijhouding
    //         {
    //             Datum = source.Datum?.Map(),
    //             Reden = source.Reden?.Map()
    //         };
    // }

     public static HC.OpschortingBijhouding? Map(this Gba.GbaOpschortingBijhouding? source)
    {
        return source == null
            ? null
            : new HC.OpschortingBijhouding
            {
                Datum = source.Datum.Map(),
                Reden = source.Reden.Map(),
                AdditionalProperties = source.AdditionalProperties
            };
    }
}

