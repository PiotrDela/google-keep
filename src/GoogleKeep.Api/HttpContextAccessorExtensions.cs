namespace GoogleKeep.Api
{
    public static class HttpContextAccessorExtensions
    {
        public static Guid? ParseUserId(this IHttpContextAccessor httpContextAccessor)
        {
            var claim = httpContextAccessor.HttpContext.User.FindFirst("UserId");

            if (claim != null && Guid.TryParse(claim.Value, out Guid userId))
            {
                return userId;
            }

            return null;
        }
    }
}
