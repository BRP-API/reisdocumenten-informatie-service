using HaalCentraal.ReisdocumentProxy.Generated;
using Gba = HaalCentraal.ReisdocumentProxy.Generated.Gba;

namespace ReisdocumentProxy.Mappers;

public static class InhoudingOfVermissingInOnderzoekConverter
{
    
    public static InhoudingOfVermissingInOnderzoek? ConvertToInhoudingOfVermissingInOnderzoek(this Gba.GbaInOnderzoek source)
    {
        return source?.AanduidingGegevensInOnderzoek switch
        {
            "120000" or
            "123500" => new InhoudingOfVermissingInOnderzoek
            {
                Aanduiding = true,
                Datum = true,
                DatumIngangOnderzoek = source?.DatumIngangOnderzoek?.Map()
            },
            "123560" => new InhoudingOfVermissingInOnderzoek
            {
                Datum = true,
                DatumIngangOnderzoek = source?.DatumIngangOnderzoek?.Map()
            },
            "123570" => new InhoudingOfVermissingInOnderzoek
            {
                Aanduiding = true,
                DatumIngangOnderzoek = source?.DatumIngangOnderzoek?.Map()
            },
            _ => null
        };
    }
}
