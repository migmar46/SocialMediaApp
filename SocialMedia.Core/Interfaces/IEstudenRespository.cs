using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces
{
    public interface IEstudenRespository : IRepository<Estuden>
    {
        Task<IEnumerable<Estuden>> GetEstudenByUser(int IDEstudiante);
    }
}
