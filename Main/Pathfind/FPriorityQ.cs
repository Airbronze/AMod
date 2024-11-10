using System;
using System.Collections;
using System.Collections.Generic;

namespace Priority_Queue
{
    public sealed class FastPriorityQueue<T> : IFixedSizePriorityQueue<T, float>, IPriorityQueue<T, float>, IEnumerable<T>, IEnumerable where T : FastPriorityQueueNode
    {
        public FastPriorityQueue(int maxNodes)
        {
            bool flag = maxNodes <= 0;
            if (flag)
            {
                throw new InvalidOperationException("New queue size cannot be smaller than 1");
            }
            this._numNodes = 0;
            this._nodes = new T[maxNodes + 1];
        }

        public int Count
        {
            get
            {
                return this._numNodes;
            }
        }

        public int MaxSize
        {
            get
            {
                return this._nodes.Length - 1;
            }
        }

        public void Clear()
        {
            Array.Clear(this._nodes, 1, this._numNodes);
            this._numNodes = 0;
        }

        public bool Contains(T node)
        {
            bool flag = node == null;
            if (flag)
            {
                throw new ArgumentNullException("node");
            }
            bool flag2 = node.Queue != null && !this.Equals(node.Queue);
            if (flag2)
            {
                throw new InvalidOperationException("node.Contains was called on a node from another queue.  Please call originalQueue.ResetNode() first");
            }
            bool flag3 = node.QueueIndex < 0 || node.QueueIndex >= this._nodes.Length;
            if (flag3)
            {
                throw new InvalidOperationException("node.QueueIndex has been corrupted. Did you change it manually? Or add this node to another queue?");
            }
            return this._nodes[node.QueueIndex] == node;
        }

        public void Enqueue(T node, float priority)
        {
            bool flag = node == null;
            if (flag)
            {
                throw new ArgumentNullException("node");
            }
            bool flag2 = this._numNodes >= this._nodes.Length - 1;
            if (flag2)
            {
                string str = "Queue is full - node cannot be added: ";
                T t = node;
                throw new InvalidOperationException(str + ((t != null) ? t.ToString() : null));
            }
            bool flag3 = node.Queue != null && !this.Equals(node.Queue);
            if (flag3)
            {
                throw new InvalidOperationException("node.Enqueue was called on a node from another queue.  Please call originalQueue.ResetNode() first");
            }
            bool flag4 = this.Contains(node);
            if (flag4)
            {
                string str2 = "Node is already enqueued: ";
                T t2 = node;
                throw new InvalidOperationException(str2 + ((t2 != null) ? t2.ToString() : null));
            }
            node.Queue = this;
            node.Priority = priority;
            this._numNodes++;
            this._nodes[this._numNodes] = node;
            node.QueueIndex = this._numNodes;
            this.CascadeUp(node);
        }

        private void CascadeUp(T node)
        {
            bool flag = node.QueueIndex > 1;
            if (flag)
            {
                int i = node.QueueIndex >> 1;
                T t = this._nodes[i];
                bool flag2 = this.HasHigherOrEqualPriority(t, node);
                if (!flag2)
                {
                    this._nodes[node.QueueIndex] = t;
                    t.QueueIndex = node.QueueIndex;
                    node.QueueIndex = i;
                    while (i > 1)
                    {
                        i >>= 1;
                        T t2 = this._nodes[i];
                        bool flag3 = this.HasHigherOrEqualPriority(t2, node);
                        if (flag3)
                        {
                            break;
                        }
                        this._nodes[node.QueueIndex] = t2;
                        t2.QueueIndex = node.QueueIndex;
                        node.QueueIndex = i;
                    }
                    this._nodes[node.QueueIndex] = node;
                }
            }
        }

        private void CascadeDown(T node)
        {
            int num = node.QueueIndex;
            int num2 = 2 * num;
            bool flag = num2 > this._numNodes;
            if (!flag)
            {
                int num3 = num2 + 1;
                T t = this._nodes[num2];
                bool flag2 = this.HasHigherPriority(t, node);
                if (flag2)
                {
                    bool flag3 = num3 > this._numNodes;
                    if (flag3)
                    {
                        node.QueueIndex = num2;
                        t.QueueIndex = num;
                        this._nodes[num] = t;
                        this._nodes[num2] = node;
                        return;
                    }
                    T t2 = this._nodes[num3];
                    bool flag4 = this.HasHigherPriority(t, t2);
                    if (flag4)
                    {
                        t.QueueIndex = num;
                        this._nodes[num] = t;
                        num = num2;
                    }
                    else
                    {
                        t2.QueueIndex = num;
                        this._nodes[num] = t2;
                        num = num3;
                    }
                }
                else
                {
                    bool flag5 = num3 > this._numNodes;
                    if (flag5)
                    {
                        return;
                    }
                    T t3 = this._nodes[num3];
                    bool flag6 = this.HasHigherPriority(t3, node);
                    if (!flag6)
                    {
                        return;
                    }
                    t3.QueueIndex = num;
                    this._nodes[num] = t3;
                    num = num3;
                }
                for (; ; )
                {
                    num2 = 2 * num;
                    bool flag7 = num2 > this._numNodes;
                    if (flag7)
                    {
                        break;
                    }
                    num3 = num2 + 1;
                    t = this._nodes[num2];
                    bool flag8 = this.HasHigherPriority(t, node);
                    if (flag8)
                    {
                        bool flag9 = num3 > this._numNodes;
                        if (flag9)
                        {
                            goto Block_9;
                        }
                        T t4 = this._nodes[num3];
                        bool flag10 = this.HasHigherPriority(t, t4);
                        if (flag10)
                        {
                            t.QueueIndex = num;
                            this._nodes[num] = t;
                            num = num2;
                        }
                        else
                        {
                            t4.QueueIndex = num;
                            this._nodes[num] = t4;
                            num = num3;
                        }
                    }
                    else
                    {
                        bool flag11 = num3 > this._numNodes;
                        if (flag11)
                        {
                            goto Block_11;
                        }
                        T t5 = this._nodes[num3];
                        bool flag12 = this.HasHigherPriority(t5, node);
                        if (!flag12)
                        {
                            goto IL_2C2;
                        }
                        t5.QueueIndex = num;
                        this._nodes[num] = t5;
                        num = num3;
                    }
                }
                node.QueueIndex = num;
                this._nodes[num] = node;
                return;
            Block_9:
                node.QueueIndex = num2;
                t.QueueIndex = num;
                this._nodes[num] = t;
                this._nodes[num2] = node;
                return;
            Block_11:
                node.QueueIndex = num;
                this._nodes[num] = node;
                return;
            IL_2C2:
                node.QueueIndex = num;
                this._nodes[num] = node;
            }
        }

        private bool HasHigherPriority(T higher, T lower)
        {
            return higher.Priority < lower.Priority;
        }

        private bool HasHigherOrEqualPriority(T higher, T lower)
        {
            return higher.Priority <= lower.Priority;
        }

        public T Dequeue()
        {
            bool flag = this._numNodes <= 0;
            if (flag)
            {
                throw new InvalidOperationException("Cannot call Dequeue() on an empty queue");
            }
            bool flag2 = !this.IsValidQueue();
            if (flag2)
            {
                throw new InvalidOperationException("Queue has been corrupted (Did you update a node priority manually instead of calling UpdatePriority()?Or add the same node to two different queues?)");
            }
            T t = this._nodes[1];
            bool flag3 = this._numNodes == 1;
            T result;
            if (flag3)
            {
                this._nodes[1] = default(T);
                this._numNodes = 0;
                result = t;
            }
            else
            {
                T t2 = this._nodes[this._numNodes];
                this._nodes[1] = t2;
                t2.QueueIndex = 1;
                this._nodes[this._numNodes] = default(T);
                this._numNodes--;
                this.CascadeDown(t2);
                result = t;
            }
            return result;
        }

        public void Resize(int maxNodes)
        {
            bool flag = maxNodes <= 0;
            if (flag)
            {
                throw new InvalidOperationException("Queue size cannot be smaller than 1");
            }
            bool flag2 = maxNodes < this._numNodes;
            if (flag2)
            {
                throw new InvalidOperationException(string.Concat(new string[]
                {
                    "Called Resize(",
                    maxNodes.ToString(),
                    "), but current queue contains ",
                    this._numNodes.ToString(),
                    " nodes"
                }));
            }
            T[] array = new T[maxNodes + 1];
            int num = Math.Min(maxNodes, this._numNodes);
            Array.Copy(this._nodes, array, num + 1);
            this._nodes = array;
        }

        public T First
        {
            get
            {
                bool flag = this._numNodes <= 0;
                if (flag)
                {
                    throw new InvalidOperationException("Cannot call .First on an empty queue");
                }
                return this._nodes[1];
            }
        }

        public void UpdatePriority(T node, float priority)
        {
            bool flag = node == null;
            if (flag)
            {
                throw new ArgumentNullException("node");
            }
            bool flag2 = node.Queue != null && !this.Equals(node.Queue);
            if (flag2)
            {
                throw new InvalidOperationException("node.UpdatePriority was called on a node from another queue");
            }
            bool flag3 = !this.Contains(node);
            if (flag3)
            {
                string str = "Cannot call UpdatePriority() on a node which is not enqueued: ";
                T t = node;
                throw new InvalidOperationException(str + ((t != null) ? t.ToString() : null));
            }
            node.Priority = priority;
            this.OnNodeUpdated(node);
        }

        private void OnNodeUpdated(T node)
        {
            int num = node.QueueIndex >> 1;
            bool flag = num > 0 && this.HasHigherPriority(node, this._nodes[num]);
            if (flag)
            {
                this.CascadeUp(node);
            }
            else
            {
                this.CascadeDown(node);
            }
        }

        public void Remove(T node)
        {
            bool flag = node == null;
            if (flag)
            {
                throw new ArgumentNullException("node");
            }
            bool flag2 = node.Queue != null && !this.Equals(node.Queue);
            if (flag2)
            {
                throw new InvalidOperationException("node.Remove was called on a node from another queue");
            }
            bool flag3 = !this.Contains(node);
            if (flag3)
            {
                string str = "Cannot call Remove() on a node which is not enqueued: ";
                T t = node;
                throw new InvalidOperationException(str + ((t != null) ? t.ToString() : null));
            }
            bool flag4 = node.QueueIndex == this._numNodes;
            if (flag4)
            {
                this._nodes[this._numNodes] = default(T);
                this._numNodes--;
            }
            else
            {
                T t2 = this._nodes[this._numNodes];
                this._nodes[node.QueueIndex] = t2;
                t2.QueueIndex = node.QueueIndex;
                this._nodes[this._numNodes] = default(T);
                this._numNodes--;
                this.OnNodeUpdated(t2);
            }
        }

        public void ResetNode(T node)
        {
            bool flag = node == null;
            if (flag)
            {
                throw new ArgumentNullException("node");
            }
            bool flag2 = node.Queue != null && !this.Equals(node.Queue);
            if (flag2)
            {
                throw new InvalidOperationException("node.ResetNode was called on a node from another queue");
            }
            bool flag3 = this.Contains(node);
            if (flag3)
            {
                throw new InvalidOperationException("node.ResetNode was called on a node that is still in the queue");
            }
            node.Queue = null;
            node.QueueIndex = 0;
        }

        public IEnumerator<T> GetEnumerator()
        {
            int num;
            for (int i = 1; i <= this._numNodes; i = num + 1)
            {
                yield return this._nodes[i];
                num = i;
            }
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public bool IsValidQueue()
        {
            for (int i = 1; i < this._nodes.Length; i++)
            {
                bool flag = this._nodes[i] != null;
                if (flag)
                {
                    int num = 2 * i;
                    bool flag2 = num < this._nodes.Length && this._nodes[num] != null && this.HasHigherPriority(this._nodes[num], this._nodes[i]);
                    bool result;
                    if (flag2)
                    {
                        result = false;
                    }
                    else
                    {
                        int num2 = num + 1;
                        bool flag3 = num2 < this._nodes.Length && this._nodes[num2] != null && this.HasHigherPriority(this._nodes[num2], this._nodes[i]);
                        if (!flag3)
                        {
                            goto IL_C2;
                        }
                        result = false;
                    }
                    return result;
                }
            IL_C2:;
            }
            return true;
        }

        private int _numNodes;

        private T[] _nodes;
    }
}
