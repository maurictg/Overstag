namespace Overstag.Models.Database.Meta
{
    public enum AccountType
    {
        USER, PARENT, MENTOR, ADMIN
    }

    public enum AuthType
    {
        LOGIN, API, PASSRESET
    }

    public enum ActivityType
    {
        HOME, ELSE
    }

    public enum PaymentType
    {
        IDEAL, BANK, CRYPTO
    }

    public enum PaymentStatus
    {
        OPEN, CANCELED, PENDING, AUTHORIZED, EXPIRED, FAILED, PAID 
    }
}
