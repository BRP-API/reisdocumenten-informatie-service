using Brp.Shared.DtoMappers.CommonDtos;
using Gba = HaalCentraal.ReisdocumentProxy.Generated.Gba;

namespace ReisdocumentProxy.Mappers;

public static class WaardetabelMapper
{
     public static Waardetabel? Map(this Gba.Waardetabel source)
    {
        return source == null
            ? null
            : new Waardetabel
            {
                Code = source.Code,
                Omschrijving = source.Omschrijving
            };
    }
}

