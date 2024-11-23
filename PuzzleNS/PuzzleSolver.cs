using System;
using System.Collections.Generic;

namespace AMod.PuzzleNS
{
    // Token: 0x02000094 RID: 148
    public class PuzzleSolver
    {
        // Token: 0x06000339 RID: 825 RVA: 0x00018958 File Offset: 0x00016B58
        public System.Collections.Generic.List<Puzzle> SolvePuzzle(Puzzle startPuzzle)
        {
            System.Collections.Generic.HashSet<Puzzle> hashSet = new System.Collections.Generic.HashSet<Puzzle>();
            PriorityQueue<Node> priorityQueue = new PriorityQueue<Node>();
            Node item = new Node(startPuzzle, 0, null);
            priorityQueue.Enqueue(item);
            while (!priorityQueue.IsEmpty())
            {
                Node node = priorityQueue.Dequeue();
                if (node.State.IsGoal())
                {
                    return this.ReconstructPath(node);
                }
                hashSet.Add(node.State);
                foreach (Puzzle puzzle in node.State.GenerateSuccessors())
                {
                    if (!hashSet.Contains(puzzle))
                    {
                        Node item2 = new Node(puzzle, node.Cost + 1, node);
                        priorityQueue.Enqueue(item2);
                    }
                }
            }
            return null;
        }

        // Token: 0x0600033A RID: 826 RVA: 0x00018A28 File Offset: 0x00016C28
        private System.Collections.Generic.List<Puzzle> ReconstructPath(Node node)
        {
            System.Collections.Generic.List<Puzzle> list = new System.Collections.Generic.List<Puzzle>();
            while (node != null)
            {
                list.Insert(0, node.State);
                node = node.Parent;
            }
            return list;
        }

        // Token: 0x0600033B RID: 827 RVA: 0x00018A58 File Offset: 0x00016C58
        public void PrintSolutionSteps(System.Collections.Generic.List<Puzzle> path)
        {
            Console.WriteLine("Steps to solve the puzzle:");
            for (int i = 0; i < path.Count; i++)
            {
                Console.WriteLine(string.Format("Step {0}:", i));
                Console.WriteLine(path[i].ToString());
            }
            System.Collections.Generic.List<int> values = this.FindMoves(path);
            Console.WriteLine("Moves: " + string.Join<int>(", ", values));
        }

        // Token: 0x0600033C RID: 828 RVA: 0x00018AC8 File Offset: 0x00016CC8
        public System.Collections.Generic.List<int> FindMoves(System.Collections.Generic.List<Puzzle> boards)
        {
            System.Collections.Generic.List<int> list = new System.Collections.Generic.List<int>();
            for (int i = 1; i < boards.Count; i++)
            {
                int[] board = boards[i - 1].Board;
                int[] board2 = boards[i].Board;
                System.Array.IndexOf<int>(board, 0);
                int num = System.Array.IndexOf<int>(board2, 0);
                int value = board[num];
                int item = System.Array.IndexOf<int>(board, value);
                list.Add(item);
            }
            return list;
        }
    }
}
