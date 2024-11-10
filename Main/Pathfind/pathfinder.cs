using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using HarmonyLib;
using Priority_Queue;
using UnityEngine;
using static Il2CppSystem.Xml.Schema.FacetsChecker.FacetsCompiler;
using Random = System.Random;
using UnityEngine.UIElements;
using AMod;
using Il2Cpp;
using Il2CppBasicTypes;
using Il2CppKernys.Bson;
using MelonLoader;

namespace AMod
{
    public class pathfinder
    {
        public static bool[,] GetMap()
        {
            int num = Globals.world.worldSizeX;
            int num2 = Globals.world.worldSizeY;

            if (num <= 0 || num2 <= 0)
            {
                throw new ArgumentException("Invalid world size");
            }

            bool[,] array = new bool[num, num2];
            int num3 = pathfinder.CalculateNumObstacles(num, num2);
            Random random = new Random();

            for (int i = 0; i < num3; i++)
            {
                int num4;
                int num5;
                do
                {
                    num4 = random.Next(0, num);
                    num5 = random.Next(0, num2);
                }
                while (array[num4, num5]);

                array[num4, num5] = true;
            }

            return array;
        }

        private static int CalculateNumObstacles(int worldSizeX, int worldSizeY)
        {
            int minValue = worldSizeX * worldSizeY / 4;
            int num = worldSizeX * worldSizeY / 2;
            Random random = new Random();
            return random.Next(minValue, num + 1);
        }

        public static bool IsTileWalkable(int x, int y, Vector2i cpos, bool diag = false, bool isnether = false)
        {
            pathfinder find = new pathfinder();
            World.BlockType blockType = Globals.world.GetBlockType(new Vector2i(x, y));
            bool flag = !Globals.world.IsMapPointInWorld(new Vector2i(x, y));
            bool result;
            if (flag)
            {
                result = false;
            }
            else
            {
                bool flag2 = find.IsBlockCloud(blockType);
                if (flag2)
                {
                    result = true;
                }
                else
                {
                    bool flag3 = ConfigData.IsBlockPlatform(blockType) || blockType == (World.BlockType)110;
                    if (flag3)
                    {
                        bool flag4 = Globals.lastpos.y <= y;
                        if (flag4)
                        {
                            return true;
                        }
                    }
                    bool flag5 = ConfigData.IsAnyDoor(blockType);
                    if (flag5)
                    {
                        bool flag6 = Globals.WorldController.DoesPlayerHaveRightToGoDoorForCollider(new Vector2i(x, y));
                        if (flag6)
                        {
                            return true;
                        }
                    }
                    bool flag7 = ConfigData.IsBlockBattleBarrier(blockType);
                    if (flag7)
                    {
                        BattleBarrierBasicData battleBarrierBasicData = new BattleBarrierBasicData(1);
                        battleBarrierBasicData.SetViaBSON(Globals.world.GetWorldItemData(new Vector2i(x, y)).GetAsBSON());
                        bool isOpen = battleBarrierBasicData.isOpen;
                        if (isOpen)
                        {
                            return true;
                        }
                    }
                    bool flag8 = ConfigData.IsBlockDisappearingBlock(blockType);
                    if (flag8)
                    {
                        DisappearingBlockData disappearingBlockData = new DisappearingBlockData(1);
                        disappearingBlockData.SetViaBSON(Globals.world.GetWorldItemData(new Vector2i(x, y)).GetAsBSON());
                        bool isOpen2 = disappearingBlockData.isOpen;
                        if (isOpen2)
                        {
                            return true;
                        }
                    }
                    bool flag9 = blockType == (World.BlockType)1420 || blockType == (World.BlockType)4286 || blockType == (World.BlockType)4366 || blockType == (World.BlockType)4372 || blockType == (World.BlockType)4103 || blockType == (World.BlockType)3966;
                    if (flag9)
                    {
                        result = true;
                    }
                    else
                    {
                        bool flag10 = ConfigData.doesBlockHaveCollider[(int)blockType];
                        result = !flag10;
                    }
                }
            }
            return result;
        }

        public bool IsBlockCloud(World.BlockType blockType)
        {
            return blockType == (World.BlockType)656 || blockType == (World.BlockType)956;
        }

        public bool IsBlockCloudOn(int x, int y)
        {
            return this.IsBlockCloud(Globals.world.GetBlockType(x, y));
        }

        private static List<PNode> TracePath(PNode end)
        {
            List<PNode> list = new List<PNode>();
            PNode pnode = end;

            while (pnode != null)
            {
                list.Add(pnode);
                pnode = pnode.parent;
            }

            list.Reverse();
            return list;
        }

        public virtual void ResetSize(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        private void ResetMap()
        {
            ResetSize(Globals.world.worldSizeX, Globals.world.worldSizeY);
        }

        public void Run(Vector2i start, Vector2i end)
        {
            ResetMap();
            (List<PNode> path, PathfindingResult result) = pathfinder.FindPath(start, end, pathfinder.GetMap());

            if (result == PathfindingResult.SUCCESSFUL)
            {
                if (path.Count == 0)
                {
                    ChatUI.SendLogMessage("Error: Path found but path list is empty.");
                    return;
                }
                AMod main = new AMod();
                Globals.TeleportPath = path;
                Globals.CurrentTp = 0;
                Globals.Isteleporting = true;
                Globals.TeleportTimer = 0;

                if (Globals.TeleportPath.Count > 0)
                {
                    Globals.targetIndex = 0;
                    Globals.targetPosition = Globals.WorldController.ConvertMapPointToWorldPoint(new Vector2i(Globals.TeleportPath[Globals.targetIndex].x, Globals.TeleportPath[Globals.targetIndex].y));
                }
                else
                {
                    ChatUI.SendLogMessage("Error: Path found but no valid targets.");
                }
            }
            else
            {
                HandlePathfindingError(result);
            }
        }

        public static void HandlePathfindingError(PathfindingResult result)
        {
            switch (result)
            {
                case PathfindingResult.ERROR_START_OUT_OF_BOUNDS:
                    Globals.DoCustomNotification("Error: Start position is out of bounds.", Globals.CurrentMapPoint);
                    break;
                case PathfindingResult.ERROR_END_OUT_OF_BOUNDS:
                    Globals.DoCustomNotification("Error: End position is out of bounds.", Globals.CurrentMapPoint);
                    break;
                case PathfindingResult.Same_Block:
                    Globals.DoCustomNotification("Error: Start and end positions are the same.", Globals.CurrentMapPoint);
                    break;
                case PathfindingResult.Start_Not_Valid:
                    Globals.DoCustomNotification("Error: Start position is not valid.", Globals.CurrentMapPoint);
                    break;
                case PathfindingResult.Path_Not_Found_Block:
                    Globals.DoCustomNotification("Error: End position is blocked by an obstacle.", Globals.CurrentMapPoint);
                    break;
                case PathfindingResult.Path_Not_Found:
                    Globals.DoCustomNotification("Error: No path could be found.", Globals.CurrentMapPoint);
                    break;
                case PathfindingResult.ERROR_PATH_TOO_LONG:
                    Globals.DoCustomNotification("Error: The path is too long.", Globals.CurrentMapPoint);
                    break;
                case PathfindingResult.Invalid_Ending_Pos:
                    Globals.DoCustomNotification("Error: Ending position is invalid.", Globals.CurrentMapPoint);
                    break;
                default:
                    Globals.DoCustomNotification("Error: Unknown pathfinding error.", Globals.CurrentMapPoint);
                    break;
            }
        }

        public static (List<PNode>, PathfindingResult) FindPath(Vector2i _from, Vector2i _to, bool[,] map)
        {
            PriorityQueue<PNode> openSet = new PriorityQueue<PNode>();
            bool[,] visited = new bool[Globals.world.worldSizeX, Globals.world.worldSizeY];
            PNode startNode = new PNode(_from.x, _from.y, null, 0, Heuristic(_from, _to));
            openSet.Enqueue(startNode);
            visited[_from.x, _from.y] = true;

            while (openSet.Count > 0)
            {
                PNode currentNode = openSet.Dequeue();
                if (currentNode.x == _to.x && currentNode.y == _to.y)
                {
                    return (ReconstructPath(currentNode), PathfindingResult.SUCCESSFUL);
                }

                int[] dx = { -1, 1, 0, 0, -1, 1, 1, -1 };
                int[] dy = { 0, 0, -1, 1, 1, -1, -1, 1 };

                for (int i = 0; i < 8; i++)
                {
                    int newX = currentNode.x + dx[i];
                    int newY = currentNode.y + dy[i];
                    bool isDiagonal = Math.Abs(dx[i]) == 1 && Math.Abs(dy[i]) == 1;

                    if (IsValidPosition(newX, newY, visited) &&
                        pathfinder.IsTileWalkable(newX, newY, Globals.CurrentMapPoint, true, false))
                    {
                        World.BlockType blockType = Globals.world.GetBlockType(newX, newY);

                        bool isPlatform = ConfigData.IsBlockPlatform(blockType);
                        bool isTrapdoor = blockType == (World.BlockType)956 || blockType == (World.BlockType)656;

                        bool isAbove = newY < currentNode.y;
                        bool isBelow = newY > currentNode.y;

                        if (isPlatform)
                        {
                            if (isAbove && !isTrapdoor)
                            {
                                continue;
                            }

                            if (isBelow || isTrapdoor)
                            {
                            }
                        }

                        bool canMoveDiagonally = !isDiagonal ||
                                                 (pathfinder.IsTileWalkable(currentNode.x, currentNode.y + dy[i], Globals.CurrentMapPoint, false, false) &&
                                                  pathfinder.IsTileWalkable(currentNode.x + dx[i], currentNode.y, Globals.CurrentMapPoint, false, false));

                        if (canMoveDiagonally)
                        {
                            int gCost = currentNode.gCost + 1;
                            int hCost = Heuristic(new Vector2i(newX, newY), _to);
                            PNode newNode = new PNode(newX, newY, currentNode, gCost, hCost);

                            if (!visited[newX, newY])
                            {
                                openSet.Enqueue(newNode);
                                visited[newX, newY] = true;
                            }
                        }
                    }
                }
            }

            if (ConfigData.DoesBlockHaveCollider(Globals.world.GetBlockType(_to.x, _to.y)))
            {
                return (new List<PNode>(), PathfindingResult.Path_Not_Found_Block);
            }

            return (new List<PNode>(), DeterminePathfindingError(_from, _to, map));
        }

        private static PathfindingResult DeterminePathfindingError(Vector2i from, Vector2i to, bool[,] map)
        {
            if (!IsValidPosition(from.x, from.y, map))
                return PathfindingResult.ERROR_START_OUT_OF_BOUNDS;

            if (!IsValidPosition(to.x, to.y, map))
                return PathfindingResult.ERROR_END_OUT_OF_BOUNDS;

            if (from.x == to.x && from.y == to.y)
                return PathfindingResult.Same_Block;

            if (map[from.x, from.y] || map[to.x, to.y])
                return PathfindingResult.Start_Not_Valid;

            return PathfindingResult.Path_Not_Found;
        }
        private static bool IsValidPosition(int x, int y, bool[,] map)
        {
            return x >= 0 && x < Globals.world.worldSizeX && y >= 0 && y < Globals.world.worldSizeY && !map[x, y];
        }

        private static int Heuristic(Vector2i from, Vector2i to)
        {
            return Math.Abs(from.x - to.x) + Math.Abs(from.y - to.y);
        }

        private static List<PNode> ReconstructPath(PNode endNode)
        {
            List<PNode> path = new List<PNode>();
            PNode currentNode = endNode;
            while (currentNode != null)
            {
                path.Add(currentNode);
                currentNode = currentNode.parent;
            }
            path.Reverse();
            return path;
        }

        public class PriorityQueue<T> where T : IComparable<T>
        {
            private List<T> data = new List<T>();

            public void Enqueue(T item)
            {
                data.Add(item);
                int ci = data.Count - 1;
                while (ci > 0)
                {
                    int pi = (ci - 1) / 2;
                    if (data[ci].CompareTo(data[pi]) >= 0) break;
                    T tmp = data[ci]; data[ci] = data[pi]; data[pi] = tmp;
                    ci = pi;
                }
            }

            public T Dequeue()
            {
                int li = data.Count - 1;
                T frontItem = data[0];
                data[0] = data[li];
                data.RemoveAt(li);

                --li;
                int pi = 0;
                while (true)
                {
                    int ci = pi * 2 + 1;
                    if (ci > li) break;
                    int rc = ci + 1;
                    if (rc <= li && data[rc].CompareTo(data[ci]) < 0)
                        ci = rc;
                    if (data[pi].CompareTo(data[ci]) <= 0) break;
                    T tmp = data[pi]; data[pi] = data[ci]; data[ci] = tmp;
                    pi = ci;
                }
                return frontItem;
            }

            public int Count => data.Count;
        }

        public static bool IsWithinBounds(int x, int y)
        {
            return x >= 0 && x < Globals.world.worldSizeX && y >= 0 && y < Globals.world.worldSizeY;
        }


        private int[,] gridmap = new int[100, 100];

        private int _width;

        private int _height;

        private Func<PNode, int> _getWeight;

        public PathfindingResult Result { get; private set; }

        public PathfindingResult LastResult { get; private set; }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public BSONObject[][] worldsItemBson;
    }
}
