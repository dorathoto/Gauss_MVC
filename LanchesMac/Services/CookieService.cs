namespace LanchesMac.Services;

public class CookieService : ICookieService
{
    public readonly IHttpContextAccessor _httpContextAccessor;
    Guid id;
    public CookieService(IHttpContextAccessor context)
    {
        _httpContextAccessor = context;


        if (!_httpContextAccessor.HttpContext.Request.Cookies.ContainsKey("GaussCookie"))
        {
            // Gera um GUID único
            string guid = Guid.NewGuid().ToString();

            // Cria um cookie com o GUID
            _httpContextAccessor.HttpContext.Response.Cookies.Append("GaussCookie", guid, new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.MinValue // Define a expiração (ajuste conforme necessário), no caso uma session
            });
        }
    }
    public Guid GetCookie()
    {
        return id;
    }
}
