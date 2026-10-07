using Gba = HaalCentraal.ReisdocumentProxy.Generated.Gba;
using HaalCentraal.ReisdocumentProxy.Generated;
using Brp.Shared.DtoMappers.Mappers;

namespace ReisdocumentProxy.Mappers;

public static class ReisdocumentMapper
{
    
    public static Reisdocument? Map(this Gba.GbaReisdocument? source)
    {
        if (source == null) return null;

        var reisdocumentnummer = source.Reisdocumentnummer == "........." ? null : source.Reisdocumentnummer; 
        return  new Reisdocument
            {
                Reisdocumentnummer = reisdocumentnummer,
                Soort = source.Soort.Map(),
                DatumEindeGeldigheid = source.DatumEindeGeldigheid.Map(),
                InhoudingOfVermissing = source.InhoudingOfVermissing.Map(source.InOnderzoek),
                Houder = source.Houder.Map(), 
                InOnderzoek = InOnderzoekConverter.Convert(source.InOnderzoek)
            };
    }
}

