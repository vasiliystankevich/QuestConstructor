using System.Collections.Generic;

namespace Quest.Core.Helpers
{
    static class ListHelper
    {        
        public static void MoveElement<T>(IList<T> list, T element, int dir)
        {
            var i = list.IndexOf(element);
            if (i < 0) return;

            if (i == 0 && dir < 0) return; 
            if (i >= list.Count - 1 && dir > 0) return;

            list.RemoveAt(i);
            list.Insert(i + dir, element);
        }
    }
}
