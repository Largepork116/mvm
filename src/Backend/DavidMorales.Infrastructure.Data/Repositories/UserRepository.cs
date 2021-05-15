using DavidMorales.Domain.Entities;
using DavidMorales.Domain.Interfaces.Repositories;
using DavidMorales.Infrastructure.Data.Repositories.Base;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace DavidMorales.Infrastructure.Data.Repositories
{
    public class UserRepository : Repository<AppUser>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {

        }


        public async Task<IEnumerable<object>> GetUserWithRoleAsync()
        {
            var query =
                "SELECT " +
                "   t01.Id AS UserId, " +
                "	t04.Name, " +
                "	t01.Email, " +
                "	t03.Id AS RoleId, " +
                "	t03.Name As Role " +
                "FROM " +
                "    [identity].users AS t01 INNER JOIN" +
                "    [identity].UsersRoles AS t02 ON t01.id = t02.UserId INNER JOIN" +
                "    [identity].Roles AS t03 ON t02.RoleId = t03.Id INNER JOIN" +
                "    [dbo].People AS t04 ON t01.PersonId = t04.PersonId";

            var conn = _context.Database.GetDbConnection();
            await conn.OpenAsync();

            var users = new List<object>();

            using (var command = conn.CreateCommand())
            {
                command.CommandText = query;
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            var user = new
                            {
                                userId = reader.GetInt64(0),
                                name = reader.GetString(1),
                                email = reader.GetString(2),
                                roleId = reader.GetInt64(3),
                                role = reader.GetString(4)
                            };

                            users.Add(user);
                        }
                    }
                }
            }

            return users;
        }
    }
}
