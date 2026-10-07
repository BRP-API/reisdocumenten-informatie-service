using Gba = HaalCentraal.ReisdocumentProxy.Generated.Gba;
using HaalCentraal.ReisdocumentProxy.Generated;

namespace ReisdocumentProxy.Mappers;

public static class ReisdocumenthouderInOnderzoekMapper
{

    public static ReisdocumenthouderInOnderzoek? Map(this Gba.GbaInOnderzoek source)
    {
           return source?.AanduidingGegevensInOnderzoek switch
        {
            "010000" or
            "010100" or
            "010120" => new ReisdocumenthouderInOnderzoek
            {
                Burgerservicenummer = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek?.Map()
            },
            _ => null
        };
    }   
}

