using System;
using System.Collections;
using System.Collections.Generic;

namespace Priority_Queue
{
    public interface IPriorityQueue<TItem, in TPriority> : IEnumerable<TItem>, IEnumerable
    {
        void Enqueue(TItem node, TPriority priority);

        TItem Dequeue();

        void Clear();

        bool Contains(TItem node);

        void Remove(TItem node);

        void UpdatePriority(TItem node, TPriority priority);

        TItem First { get; }

        int Count { get; }
    }
}