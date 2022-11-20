using AutoMapper;
using Sys.Common.Helper;
using System.Collections.Generic;

namespace Sys.Common.Mapper
{
    public class MapperData : IMapperData
    {
        private readonly IMapper _mapper;

        public MapperData(IMapper mapper)
        {
            _mapper = mapper;
        }

        public T Transform<U, T>(U entity)
        {
            return _mapper.Map<T>(entity);
        }

        public IList<T> Transform<U, T>(IList<U> entity)
        {
            IList<T> res = new List<T>();
            if (entity.IsNotEmpty())
            {
                foreach (U u in entity)
                {
                    res.Add(_mapper.Map<T>(u));
                }
            }
            return res;
        }
    }
}