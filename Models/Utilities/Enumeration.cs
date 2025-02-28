namespace Utilities
{
    public enum Monnaie
    {
        CDF,
        USD,
    }

    public enum UserRole
    {
        None,
        Administrateur,
        Simple_User,
        Visitor,
        Servante,
        Other
    }

    public enum StatutVente
    {
        Credit,
        Comptant,
        Perte,
        Moisissure
    }

    public enum StatutCommand
    {
        Encours,
        Clôturée,
        Suspendue,
        Annulée
    }

    public enum TypeResponse
    {
        Normal,
        Info,
        Success,
        Warning,
        Error
    }
}
