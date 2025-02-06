using System;
using System.Collections.Generic;

namespace _project.Code.utility
{
    public class SampleSelection
    {
        public static T Select<T, TK>(IEnumerable<TK> collection) where T : class, TK
        {
            foreach (var sample in collection)
            {
                if (sample.GetType() == typeof(T))
                {
                    return sample as T;
                }
            }

            throw new InvalidOperationException();
        }
    }
}