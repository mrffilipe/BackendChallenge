namespace BackendChallenge.Domain.Common
{
    public sealed record MotorcycleCreated(
        Guid Id,           // PK interna
        string ExternalId, // "identificador" (business key)
        int Year,
        string Model,
        string Plate,
        DateTime OccuredAt);
}
