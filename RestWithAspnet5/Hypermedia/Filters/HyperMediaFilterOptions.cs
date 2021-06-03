using RestWithAspnet5.Hypermedia.Abstract;
using System.Collections.Generic;

namespace RestWithAspnet5.Hypermedia.Filters
{
    public class HyperMediaFilterOptions
    {
        public List<IResponseEnricher> ContentResponseEnricherList { get; set; } = new List<IResponseEnricher>();
    }
}
