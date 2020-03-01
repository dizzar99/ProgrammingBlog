namespace ProgBlog.Services.Models.IdentityManagment
{
    public class RegisterUserRequest : LoginUserRequest
    {
        public string Email { get; set; }
    }
}
