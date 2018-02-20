using System.ComponentModel.DataAnnotations;
// ReSharper disable InconsistentNaming

namespace LockStockAuth.Auth.Models
{
    public class ExternalLoginViewModel
    {
        public string Name { get; set; }

        public string State { get; set; }
        public string Url { get; set; }
    }

    public class ParsedExternalAccessToken
    {
        public string app_id { get; set; }
        public string user_id { get; set; }
    }

    public class RegisterExternalBindingModel
    {
        public string Email { get; set; }

        public string ShareRef { get; set; }

        [Required]
        public string ExternalAccessToken { get; set; }

        public string Name { get; set; }

        [Required]
        public string Provider { get; set; }

        [Required]
        public string UserName { get; set; }
    }
}
