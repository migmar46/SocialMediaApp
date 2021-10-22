using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SocialMedia.Api.Responses;
using SocialMedia.Core.CustomEntities;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.QueryFilters;
using SocialMedia.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class EstudenController : ControllerBase
    {
        private readonly IEstudenService _estudenService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public EstudenController( IEstudenService estudenService, IMapper mapper, IUriService uriService)
        {
            _estudenService = estudenService;
            _mapper = mapper;
            _uriService = uriService;
        }

        /// <summary>
        /// Retrieve all posts
        /// </summary>
        /// <param name="filters">Filters to apply</param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetEstudens))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<EstudenDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetEstudens([FromQuery] EstudenFilter filters)
        {

            var estudens = _estudenService.GetEstudens(filters);
            var estudensDtos = _mapper.Map<IEnumerable<EstudenDto>>(estudens);

            var metadata = new Metadata
            {
                TotalCount = estudens.TotalCount,
                PageSize = estudens.PageSize,
                CurrentPage = estudens.CurrentPage,
                TotalPages = estudens.TotalPages,
                HasNextPage = estudens.HasNextPage,
                HasPreviousPage = estudens.HasPreviousPage,
                NextPageUrl = _uriService.GetEstudenPaginationUri(filters, Url.RouteUrl(nameof(GetEstudens))).ToString(),
                PreviousPageUrl = _uriService.GetEstudenPaginationUri(filters, Url.RouteUrl(nameof(GetEstudens))).ToString()
            };

            var response = new ApiResponse<IEnumerable<EstudenDto>>(estudensDtos)
            {
                Meta = metadata
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEstuden(int id)
        {
            var estuden = await _estudenService.GetEstuden(id);
            var estudenDto = _mapper.Map<EstudenDto>(estuden);
            var response = new ApiResponse<EstudenDto>(estudenDto);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Estuden(EstudenDto estudenDto)
        {
            var estuden = _mapper.Map<Estuden>(estudenDto);

            await _estudenService.InsertEstuden(estuden);

            estudenDto = _mapper.Map<EstudenDto>(estuden);
            var response = new ApiResponse<EstudenDto>(estudenDto);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, EstudenDto estudenDto)
        {
            var estuden = _mapper.Map<Estuden>(estudenDto);
            estuden.Id = id;

            var result = await _estudenService.UpdateEstuden(estuden);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _estudenService.DeleteEstuden(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
