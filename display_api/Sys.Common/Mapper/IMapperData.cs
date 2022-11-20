using System.Collections.Generic;

namespace Sys.Common.Mapper
{
    public interface IMapperData
    {
        IList<T> Transform<U, T>(IList<U> entity);

        T Transform<U, T>(U entity);
    }
}