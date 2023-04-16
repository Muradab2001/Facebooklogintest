namespace MultiShop.ViewModels
{
    public class ExternalLoginViewModel
    {
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
        public string ReturnUrl { get; set; }
    }

}
