using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ProgBlog.Options
{
    public class JwtSettings
    {
        public string Secret { get; set; }

        public SymmetricSecurityKey SymmetricSecurityKey
        {
            get => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.Secret));
        }
    }
}
