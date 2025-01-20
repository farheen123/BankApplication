namespace BankApp.Web.Models
{
    public class JwtTokenService
    {
        private string _token;

        public void StoreToken(string token)
        {
            _token = token;
        }

        public string GetToken()
        {
            return _token;
        }

        public bool IsTokenAvailable()
        {
            return !string.IsNullOrEmpty(_token);
        }
    }
}
