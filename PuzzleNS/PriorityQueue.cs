using System;
using System.Collections.Generic;

namespace AMod.PuzzleNS
{
    // Token: 0x02000093 RID: 147
    public class PriorityQueue<T> where T : System.IComparable<T>
    {
        // Token: 0x06000334 RID: 820 RVA: 0x0000303B File Offset: 0x0000123B
        public PriorityQueue()
        {
            this.data = new System.Collections.Generic.List<T>();
        }

        // Token: 0x06000335 RID: 821 RVA: 0x000187C8 File Offset: 0x000169C8
        public void Enqueue(T item)
        {
            this.data.Add(item);
            int num;
            for (int i = this.data.Count - 1; i > 0; i = num)
            {
                num = (i - 1) / 2;
                T t = this.data[i];
                if (t.CompareTo(this.data[num]) >= 0)
                {
                    break;
                }
                T value = this.data[i];
                this.data[i] = this.data[num];
                this.data[num] = value;
            }
        }

        // Token: 0x06000336 RID: 822 RVA: 0x00018858 File Offset: 0x00016A58
        public T Dequeue()
        {
            int num = this.data.Count - 1;
            T result = this.data[0];
            this.data[0] = this.data[num];
            this.data.RemoveAt(num);
            num--;
            int num2 = 0;
            for (; ; )
            {
                int num3 = num2 * 2 + 1;
                if (num3 > num)
                {
                    break;
                }
                int num4 = num3 + 1;
                T t;
                if (num4 <= num)
                {
                    t = this.data[num4];
                    if (t.CompareTo(this.data[num3]) < 0)
                    {
                        num3 = num4;
                    }
                }
                t = this.data[num2];
                if (t.CompareTo(this.data[num3]) <= 0)
                {
                    break;
                }
                T value = this.data[num2];
                this.data[num2] = this.data[num3];
                this.data[num3] = value;
                num2 = num3;
            }
            return result;
        }

        // Token: 0x06000337 RID: 823 RVA: 0x0000304E File Offset: 0x0000124E
        public int Count()
        {
            return this.data.Count;
        }

        // Token: 0x06000338 RID: 824 RVA: 0x0000305B File Offset: 0x0000125B
        public bool IsEmpty()
        {
            return this.data.Count == 0;
        }

        // Token: 0x0400029C RID: 668
        private System.Collections.Generic.List<T> data;
    }
}
