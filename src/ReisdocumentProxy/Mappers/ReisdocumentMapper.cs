using Gba = HaalCentraal.ReisdocumentProxy.Generated.Gba;
using HaalCentraal.ReisdocumentProxy.Generated;
using Brp.Shared.DtoMappers.Mappers;

namespace ReisdocumentProxy.Mappers;

public static class ReisdocumentMapper
{
    
    public static Reisdocument? Map(this Gba.GbaReisdocument? source)
    {
        return source == null
            ? null
            : new Reisdocument
            {
                Reisdocumentnummer = source.Reisdocumentnummer == "........." 
                ? null 
                : source.Reisdocumentnummer,
                Soort = source.Soort.Map(),
                DatumEindeGeldigheid = source.DatumEindeGeldigheid.Map(),
                InhoudingOfVermissing = source.InhoudingOfVermissing.Map(source.InOnderzoek),
                Houder = source.Houder.Map(), 
                InOnderzoek = InOnderzoekConverter.Convert(source.InOnderzoek)
            };
    }
}

