using HC = HaalCentraal.ReisdocumentProxy.Generated;
using Gba = HaalCentraal.ReisdocumentProxy.Generated.Gba;
using Brp.Shared.DtoMappers.Mappers;

namespace ReisdocumentProxy.Mappers;

public static class InhoudingOfVermissingMapper
{
    
    public static HC.InhoudingOfVermissing? Map(this Gba.GbaInhoudingOfVermissing? source, Gba.GbaInOnderzoek inOnderzoek)
    {
        return source == null
            ? null
            : new HC.InhoudingOfVermissing
            {
                Datum = source.Datum.Map(),
                Aanduiding = source.Aanduiding.Map(),
                InOnderzoek = inOnderzoek.ConvertToInhoudingOfVermissingInOnderzoek()
            };
    }
}
