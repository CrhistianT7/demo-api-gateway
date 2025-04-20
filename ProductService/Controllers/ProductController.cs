using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("products")]
public class ProductController : ControllerBase
{
  [HttpGet]
  [Authorize]
  public IActionResult Get() => Ok(new[] { "Apple", "Banana", "Carrot" });
}
