using HaalCentraal.ReisdocumentProxy.Generated;
using Gba = HaalCentraal.ReisdocumentProxy.Generated.Gba;

namespace ReisdocumentProxy.Mappers;

public static class InOnderzoekConverter
{
    
     public static ReisdocumentInOnderzoek? Convert(this Gba.GbaInOnderzoek source)
    {
        return source?.AanduidingGegevensInOnderzoek switch
        {
            "120000" or
            "123500" => new ReisdocumentInOnderzoek
            {
                DatumEindeGeldigheid = true,
                Reisdocumentnummer = true,
                Soort = true,
                DatumIngangOnderzoek = source?.DatumIngangOnderzoek?.Map()
            },
            "123510" => new ReisdocumentInOnderzoek
            {
                Soort = true,
                DatumIngangOnderzoek = source?.DatumIngangOnderzoek?.Map()
            },
            "123520" => new ReisdocumentInOnderzoek
            {
                Reisdocumentnummer= true,
                DatumIngangOnderzoek = source?.DatumIngangOnderzoek?.Map()
            },
            "123550" => new ReisdocumentInOnderzoek
            {
                DatumEindeGeldigheid = true,
                DatumIngangOnderzoek = source?.DatumIngangOnderzoek?.Map()
            },
            _ => null
        };
    }
}

