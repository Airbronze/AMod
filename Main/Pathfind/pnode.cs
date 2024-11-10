using Il2Cpp;
using Il2CppBasicTypes;
using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PNode : FastPriorityQueueNode, IComparable<PNode>
{
    public static PNode Create(int x, int y)
    {
        return new PNode(x, y);
    }

    public int X { get; private set; }
    public int Y { get; private set; }

    public int x;
    public int y;
    public float g; // cost from start node
    public float h; // heuristic (estimated cost to goal)
    public float f; // total estimated cost (f = g + h)
    public PNode parent;
    public float Priority;
    public float G; // Cost from start node to this node
    public float H;
    public int blockType;
    public int gCost; // Cost from start to current node
    public int hCost; // Heuristic cost from current node to goal

    public PNode Parent { get; private set; }

    public PNode(int x, int y, PNode parent, int gCost, int hCost)
    {
        this.x = x;
        this.y = y;
        this.parent = parent;
        this.gCost = gCost;
        this.hCost = hCost;
        this.f = gCost + hCost;
    }

    public PNode(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public static explicit operator Vector2i(PNode pn)
    {
        return new Vector2i(pn.X, pn.Y);
    }

    public Vector2i ToVector2i()
    {
        return new Vector2i(x, y);
    }

    public Vector2 ToVector2()
    {
        return ControllerHelper.worldController.ConvertMapPointToWorldPoint(new Vector2i(this.x, this.y));
    }

    public Vector2 GetVector2()
    {
        return new Vector2(x, y);
    }

    public override bool Equals(object obj)
    {
        PNode pnode = (PNode)obj;
        return this.X == pnode.X && this.Y == pnode.Y;
    }

    public override int GetHashCode()
    {
        return this.X + this.Y * 7;
    }

    public override string ToString()
    {
        return string.Concat(new string[]
        {
            "(",
            this.X.ToString(),
            ", ",
            this.Y.ToString(),
            ")"
        });
    }

    public int CompareTo(PNode other)
    {
        return (this.gCost + this.hCost).CompareTo(other.gCost + other.hCost);
    }
}