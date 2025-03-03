﻿using Utilities;

namespace Models.OldModels
{
    public class User : TableBase
    {
        public Guid IdPointVente { get; set; }
        public string? Username { get; set; }
        public string? Photo { get; set; }
        public string? Email { get; set; }
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public string? Password { get; set; }
        public UserRole Role { get; set; }
    }
}
