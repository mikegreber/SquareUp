using System.Security.Claims;
using System.Text.Json;

namespace SquareUp.Server.Services;

public static class Authorize
{
    public static IEnumerable<Claim> GetClaims(HttpRequest request)
    {
        var authorization = request.Headers.Authorization;
        if (authorization.Count == 0) return new List<Claim>();
        
        var claims = ParseClaimsFromJwt(authorization.First());

        return claims;
    }

    public static int GetUserId(this HttpRequest request)
    {
        var claim = GetClaims(request).FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
        if (claim == null) return 0;

        return int.Parse(claim);
    }

    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        var claims = keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));

        return claims;

    }
    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2:
                base64 += "==";
                break;
            case 3:
                base64 += "=";
                break;
        }

        return Convert.FromBase64String(base64);
    }
}

// public async Task<ActionResult<ServiceResponse<GroupInfo>>> GetGroupsInfo(int id)
// {
//     var request = Request.Headers.Authorization;
//     var claims = ParseClaimsFromJwt(request[0]);
//     var count = claims.Count();
//     var result = await _service.GetUserGroupsInfo(Request, id);
//     return Ok(result);
// }
//
