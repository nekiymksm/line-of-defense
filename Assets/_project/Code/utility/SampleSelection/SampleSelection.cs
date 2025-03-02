using System.Collections.Generic;

namespace _project.Code.utility.SampleSelection
{
    public class SampleSelection
    {
        public static bool HasSampleByType<T,TK>(IEnumerable<TK> collection, out T sample) 
            where T : class, TK
        {
            foreach (var item in collection)
            {
                if (item.GetType() == typeof(T))
                {
                    sample = item as T;
                    return true;
                }
            }
            
            sample = null;
            return false;
        }
        
        public static bool HasSampleByIndex<T>(IEnumerable<T> collection, int index, out T sample) 
            where T : class, ISampleSelectable
        {
            foreach (var item in collection)
            {
                if (item.Index == index)
                {
                    sample = item;
                    return true;
                }
            }

            sample = null;
            return false;
        }
        
        public static bool ContainsByType<T,TK>(IEnumerable<TK> collection) 
            where T : class, TK
        {
            foreach (var item in collection)
            {
                if (item.GetType() == typeof(T))
                {
                    return true;
                }
            }
            
            return false;
        }
        
        public static bool ContainsByIndex(IEnumerable<ISampleSelectable> collection, int index)
        {
            foreach (var item in collection)
            {
                if (item.Index == index)
                {
                    return true;
                }
            }
            
            return false;
        }
    }
}