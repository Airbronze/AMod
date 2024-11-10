using System;

namespace AMod.PuzzleNS
{
    // Token: 0x02000092 RID: 146
    public class Node : System.IComparable<Node>
    {
        // Token: 0x170000B5 RID: 181
        // (get) Token: 0x06000329 RID: 809 RVA: 0x00002FBA File Offset: 0x000011BA
        // (set) Token: 0x0600032A RID: 810 RVA: 0x00002FC2 File Offset: 0x000011C2
        public Puzzle State { get; private set; }

        // Token: 0x170000B6 RID: 182
        // (get) Token: 0x0600032B RID: 811 RVA: 0x00002FCB File Offset: 0x000011CB
        // (set) Token: 0x0600032C RID: 812 RVA: 0x00002FD3 File Offset: 0x000011D3
        public int Cost { get; private set; }

        // Token: 0x170000B7 RID: 183
        // (get) Token: 0x0600032D RID: 813 RVA: 0x00002FDC File Offset: 0x000011DC
        // (set) Token: 0x0600032E RID: 814 RVA: 0x00002FE4 File Offset: 0x000011E4
        public int Heuristic { get; private set; }

        // Token: 0x170000B8 RID: 184
        // (get) Token: 0x0600032F RID: 815 RVA: 0x00002FED File Offset: 0x000011ED
        public int TotalCost
        {
            get
            {
                return this.Cost + this.Heuristic;
            }
        }

        // Token: 0x170000B9 RID: 185
        // (get) Token: 0x06000330 RID: 816 RVA: 0x00002FFC File Offset: 0x000011FC
        // (set) Token: 0x06000331 RID: 817 RVA: 0x00003004 File Offset: 0x00001204
        public Node Parent { get; private set; }

        // Token: 0x06000332 RID: 818 RVA: 0x0000300D File Offset: 0x0000120D
        public Node(Puzzle state, int cost, Node parent)
        {
            this.State = state;
            this.Cost = cost;
            this.Parent = parent;
            this.Heuristic = this.State.ManhattanDistance();
        }

        // Token: 0x06000333 RID: 819 RVA: 0x0001878C File Offset: 0x0001698C
        public int CompareTo(Node other)
        {
            int num = this.TotalCost.CompareTo(other.TotalCost);
            if (num == 0)
            {
                num = this.Heuristic.CompareTo(other.Heuristic);
            }
            return num;
        }
    }
}
