using System;
using System.Collections;
using System.Collections.Generic;

namespace Priority_Queue
{
    internal interface IFixedSizePriorityQueue<TItem, in TPriority> : IPriorityQueue<TItem, TPriority>, IEnumerable<TItem>, IEnumerable
    {
        void Resize(int maxNodes);

        int MaxSize { get; }

        void ResetNode(TItem node);
    }
}