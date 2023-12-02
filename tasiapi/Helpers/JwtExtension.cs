namespace tasiapi.Helpers
{
    public static class JwtExtension
    {
        public static void AddAplicationError(this HttpResponse response,string message)
        {
            response.Headers.Add("Aplication Error", message);
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.Headers.Add("Access-Control-Expose-Header", "Application-Error");
        }
    }
}
