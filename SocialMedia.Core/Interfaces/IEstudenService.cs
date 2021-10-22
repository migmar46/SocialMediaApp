using SocialMedia.Core.CustomEntities;
using SocialMedia.Core.Entities;
using SocialMedia.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces
{
    public interface IEstudenService
    {
        PagedList<Estuden> GetEstudens(EstudenFilter filters);

        Task<Estuden> GetEstuden(int id);

        Task InsertEstuden(Estuden post);

        Task<bool> UpdateEstuden(Estuden post);

        Task<bool> DeleteEstuden(int id);
    }
}
