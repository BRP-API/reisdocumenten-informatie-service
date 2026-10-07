namespace Brp.Shared.DtoMappers.Mappers;

public static class WaardetabelMapper
{
    public static CommonDtos.Waardetabel? Map(this CommonDtos.Waardetabel? waardetabel)
    {
        return waardetabel == null || waardetabel.Code == ".."
            ? null
            : new CommonDtos.Waardetabel
            {
                Code = waardetabel.Code,
                Omschrijving = waardetabel.Omschrijving
            };
    }
}
