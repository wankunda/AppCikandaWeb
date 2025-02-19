namespace ModelsServices.Utilities
{
    public enum Monnaie
    {
        CDF,
        USD,
    }

    public enum UserRole
    {
        Admin,
        User
    }

    public enum EtatVente
    {
        Credit,
        Comptant,
        Perte,
        Moisissure
    }

    public enum EtatCommand
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
