using AutoMapper;
using ProgBlog.DataAccess.Models;
using ProgBlog.Services.Models.IdentityManagment;
using ProgBlog.Services.Models.UserManagment;

namespace ProgBlog.Services.Mapper
{
    class UserCredentialsManagmentMapper : Profile
    {
        public UserCredentialsManagmentMapper()
        {
            
            this.CreateMap<DbUser, UserCredentials>();
            this.CreateMap<RegisterUserRequest, DbUser>();
        }
    }
}
