namespace InstituteManagement.Helper
{
    public class RegionAPI
    {
        public HttpClient Initial()
        {
            var Client = new HttpClient();
            Client.BaseAddress = new Uri("http://regionapi.sircltech.com/");
            return Client;

        }
    }
}
