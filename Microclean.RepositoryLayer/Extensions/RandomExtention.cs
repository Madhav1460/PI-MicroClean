using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microclean.RepositoryLayer.Extensions
{
    public static class RandomExtention
    {
        public static List<T> ProduceShuffle<T>(this IEnumerable<T> original, Random random)
        {
            return original.OrderBy(x => random.NextDouble()).ToList();
        }

    }
}
