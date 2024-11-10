using System;

namespace Priority_Queue
{
    public class FastPriorityQueueNode
    {
        public float Priority { get; protected internal set; }

        public int QueueIndex { get; internal set; }

        public object Queue { get; internal set; }
    }
}