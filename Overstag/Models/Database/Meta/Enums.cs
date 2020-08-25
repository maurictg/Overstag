namespace Overstag.Models.Database.Meta
{
    public enum AccountType : byte
    {
        USER, PARENT, MENTOR, ADMIN
    }

    public enum AuthType : byte
    {
        LOGIN, API, PASSRESET, TWOFACTORLOGIN
    }

    public enum ActivityType : byte
    {
        HOME, ELSE
    }

    public enum PaymentType : byte
    {
        IDEAL, BANK, CRYPTO
    }

    public enum PaymentStatus : byte
    {
        OPEN, CANCELED, PENDING, AUTHORIZED, EXPIRED, FAILED, PAID 
    }
}
