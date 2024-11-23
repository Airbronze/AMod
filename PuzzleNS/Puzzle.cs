using System;
using System.Collections.Generic;

namespace AMod.PuzzleNS
{
    // Token: 0x02000091 RID: 145
    public class Puzzle : System.IEquatable<Puzzle>
    {
        // Token: 0x170000B3 RID: 179
        // (get) Token: 0x0600031C RID: 796 RVA: 0x00002F62 File Offset: 0x00001162
        // (set) Token: 0x0600031D RID: 797 RVA: 0x00002F6A File Offset: 0x0000116A
        public int[] Board { get; private set; }

        // Token: 0x170000B4 RID: 180
        // (get) Token: 0x0600031E RID: 798 RVA: 0x00002F73 File Offset: 0x00001173
        // (set) Token: 0x0600031F RID: 799 RVA: 0x00002F7B File Offset: 0x0000117B
        public int Size { get; private set; }

        // Token: 0x06000320 RID: 800 RVA: 0x00002F84 File Offset: 0x00001184
        public Puzzle(int[] board)
        {
            if (board.Length != 9)
            {
                throw new System.ArgumentException("Board must have exactly 9 elements.");
            }
            this.Board = board;
            this.Size = 3;
        }

        // Token: 0x06000321 RID: 801 RVA: 0x00018508 File Offset: 0x00016708
        public int FindZeroIndex()
        {
            for (int i = 0; i < this.Board.Length; i++)
            {
                if (this.Board[i] == 0)
                {
                    return i;
                }
            }
            return -1;
        }

        // Token: 0x06000322 RID: 802 RVA: 0x00018538 File Offset: 0x00016738
        public bool IsGoal()
        {
            for (int i = 0; i < this.Board.Length - 1; i++)
            {
                if (this.Board[i] != i + 1)
                {
                    return false;
                }
            }
            return this.Board[this.Board.Length - 1] == 0;
        }

        // Token: 0x06000323 RID: 803 RVA: 0x00018580 File Offset: 0x00016780
        public int ManhattanDistance()
        {
            int num = 0;
            for (int i = 0; i < this.Board.Length; i++)
            {
                int num2 = this.Board[i];
                if (num2 != 0)
                {
                    int num3 = (num2 - 1) / this.Size;
                    int num4 = (num2 - 1) % this.Size;
                    int num5 = i / this.Size;
                    int num6 = i % this.Size;
                    num += System.Math.Abs(num3 - num5) + System.Math.Abs(num4 - num6);
                }
            }
            return num;
        }

        // Token: 0x06000324 RID: 804 RVA: 0x000185F4 File Offset: 0x000167F4
        public System.Collections.Generic.List<Puzzle> GenerateSuccessors()
        {
            System.Collections.Generic.List<Puzzle> list = new System.Collections.Generic.List<Puzzle>();
            int num = this.FindZeroIndex();
            int num2 = num / this.Size;
            int num3 = num % this.Size;
            int[] array = new int[4];
            array[0] = -1;
            array[1] = 1;
            int[] array2 = array;
            int[] array3 = new int[]
            {
                0,
                0,
                -1,
                1
            };
            for (int i = 0; i < 4; i++)
            {
                int num4 = num2 + array2[i];
                int num5 = num3 + array3[i];
                if (num4 >= 0 && num4 < this.Size && num5 >= 0 && num5 < this.Size)
                {
                    int num6 = num4 * this.Size + num5;
                    int[] array4 = (int[])this.Board.Clone();
                    array4[num] = array4[num6];
                    array4[num6] = 0;
                    list.Add(new Puzzle(array4));
                }
            }
            return list;
        }

        // Token: 0x06000325 RID: 805 RVA: 0x00002FAC File Offset: 0x000011AC
        public override bool Equals(object obj)
        {
            return this.Equals(obj as Puzzle);
        }

        // Token: 0x06000326 RID: 806 RVA: 0x000186C0 File Offset: 0x000168C0
        public bool Equals(Puzzle other)
        {
            if (other == null)
            {
                return false;
            }
            for (int i = 0; i < this.Board.Length; i++)
            {
                if (this.Board[i] != other.Board[i])
                {
                    return false;
                }
            }
            return true;
        }

        // Token: 0x06000327 RID: 807 RVA: 0x000186FC File Offset: 0x000168FC
        public override int GetHashCode()
        {
            int num = 17;
            foreach (int num2 in this.Board)
            {
                num = num * 31 + num2.GetHashCode();
            }
            return num;
        }

        // Token: 0x06000328 RID: 808 RVA: 0x00018734 File Offset: 0x00016934
        public override string ToString()
        {
            string text = "";
            for (int i = 0; i < this.Board.Length; i++)
            {
                text = text + this.Board[i] + " ";
                if ((i + 1) % this.Size == 0)
                {
                    text += System.Environment.NewLine;
                }
            }
            return text;
        }
    }
}
