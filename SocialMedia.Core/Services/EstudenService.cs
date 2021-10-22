using Microsoft.Extensions.Options;
using SocialMedia.Core.CustomEntities;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Exceptions;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Services
{
      public class EstudenService : IEstudenService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public EstudenService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public async Task<Estuden> GetEstuden(int id)
        {
            return await _unitOfWork.EstudenRepository.GetById(id);
        }

        public PagedList<Estuden> GetEstudens(EstudenFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;

            var estudens = _unitOfWork.EstudenRepository.GetAll();

            if (filters.IdEstudiante != null)
            {
                estudens = estudens.Where(x => x.IdEstudiante == filters.IdEstudiante);
            }

            if (filters.Nombre != null)
            {
                estudens = estudens.Where(x => x.Nombre.ToLower().Contains(filters.Nombre.ToLower()));
            }

            if (filters.Apellido != null)
            {
                estudens = estudens.Where(x => x.Apellido.ToLower().Contains(filters.Apellido.ToLower()));
            }

            var pagedEstuden = PagedList<Estuden>.Create(estudens, filters.PageNumber, filters.PageSize);
            return pagedEstuden;
        }

        public async Task InsertEstuden(Estuden estuden)
        {
            var user = await _unitOfWork.EstudenRepository.GetById(estuden.IdEstudiante);
            if (user == null)
            {
                throw new BusinessException("User doesn't exist");
            }

            var userEstuden = await _unitOfWork.PostRepository.GetPostsByUser(estuden.IdEstudiante);
            if (userEstuden.Count() < 10)
            {
                var lastEstuden = userEstuden.OrderByDescending(x => x.Date).FirstOrDefault();
                if ((DateTime.Now - lastEstuden.Date).TotalDays < 7)
                {
                    throw new BusinessException("You are not able to publish the post");
                }
            }

            if (estuden.Materia.Contains("Sexo"))
            {
                throw new BusinessException("Content not allowed");
            }

            await _unitOfWork.EstudenRepository.Add(estuden);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateEstuden(Estuden estuden)
        {
            var existingPost = await _unitOfWork.EstudenRepository.GetById(estuden.Id);
            existingPost.Nombre = estuden.Nombre;
            existingPost.Materia = estuden.Materia;

            _unitOfWork.EstudenRepository.Update(existingPost);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteEstuden(int id)
        {
            await _unitOfWork.EstudenRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
