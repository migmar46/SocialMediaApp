using SocialMedia.Core.QueryFilters;
using SocialMedia.Infrastructure.Interfaces;
using System;

namespace SocialMedia.Infrastructure.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;
        private readonly string _baseUriE;

        public UriService(string baseUri)
        {
            _baseUri = baseUri;
            _baseUriE = baseUri;
        }

        public Uri GetEstudenPaginationUri(EstudenFilter filter, string actionUrl)
        {
            string baseUrl = $"{_baseUriE}{actionUrl}";
            return new Uri(baseUrl);
        }
        public Uri GetPostPaginationUri(PostQueryFilter filter, string actionUrl)
        {
            string baseUrl = $"{_baseUri}{actionUrl}";
            return new Uri(baseUrl);
        }
    }
}
