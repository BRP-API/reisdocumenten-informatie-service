using Gba = HaalCentraal.ReisdocumentProxy.Generated.Gba;
using HaalCentraal.ReisdocumentProxy.Generated;
using Brp.Shared.DtoMappers.BrpApiDtos;
using Brp.Shared.DtoMappers.CommonDtos;
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
                Soort = MapWaardetabel(source.Soort),
                DatumEindeGeldigheid = MapDatum(source.DatumEindeGeldigheid),
                InhoudingOfVermissing = source.InhoudingOfVermissing.Map(source.InOnderzoek),
                Houder = source.Houder.Map(), 
                InOnderzoek = InOnderzoekConverter.Convert(source.InOnderzoek)
            };
    }

    private static Waardetabel? MapWaardetabel(Waardetabel? waardetabel)
    {
        return waardetabel?.Code == ".."
            ? null
            : waardetabel.Map();
    }

    private static AbstractDatum? MapDatum(string? datum)
    => datum == null ? null : datum.Map();
}

