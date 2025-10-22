using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public abstract class BSP_Node
{
    public BSP_Node ParentNode;
    public List<BSP_Node> ChildNode;

    public NodePosition Pos;
    public int Index;

    public BSP_Node(BSP_Node parent)
    {
        ParentNode = parent;
        ChildNode = new List<BSP_Node>();

        // 자식 노드가 생성될 때 부모 노드의 AddChildNode를 호출
        ParentNode?.AddChildNode(this);
    }

    public void AddChildNode(BSP_Node node)
    {
        // 안전하게 null 체크 (생성자에서 초기화되지만, 견고함을 위해 유지)
        if (ChildNode == null)
        {
            ChildNode = new List<BSP_Node>();
        }
        ChildNode.Add(node);
    }
}