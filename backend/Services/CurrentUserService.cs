using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace backend.Services;

public class CurrentUserService
{
  private readonly IHttpContextAccessor _httpContextAccessor;

  public CurrentUserService(
      IHttpContextAccessor httpContextAccessor)
  {
    _httpContextAccessor = httpContextAccessor;
  }

  private ClaimsPrincipal? User =>
      _httpContextAccessor.HttpContext?.User;

  public int? GetCurrentUserId()
  {
    var id = User?.FindFirstValue(
        ClaimTypes.NameIdentifier
    );

    return int.TryParse(id, out var userId)
        ? userId
        : null;
  }

  public bool IsAdmin()
  {
    return User?.IsInRole("Admin") ?? false;
  }
}