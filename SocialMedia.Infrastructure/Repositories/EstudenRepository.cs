using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Repositories
{
    public class EstudenRepository : BaseRepository<Estuden>, IEstudenRespository
    {
        public EstudenRepository(SocialMediaContext context) : base(context) { }

        public async Task<IEnumerable<Estuden>> GetEstudenByUser(int IDEstudiante)
        {
            return await _entities.Where(x => x.IdEstudiante == IDEstudiante).ToListAsync();
        }
    }
}
