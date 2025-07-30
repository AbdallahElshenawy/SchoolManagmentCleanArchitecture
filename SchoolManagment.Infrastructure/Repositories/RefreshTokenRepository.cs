using SchoolManagment.Data.Entities.Identity;
using SchoolManagment.Infrastructure.Abstracts;
using SchoolManagment.Infrastructure.Data;
using SchoolManagment.Infrastructure.InfrastructureBases;

namespace SchoolManagment.Infrastructure.Repositories
{
    public class RefreshTokenRepository(AppDbContext context) : GenericRepository<UserRefreshToken>(context), IRefreshTokenRepository
    {

    }
}
