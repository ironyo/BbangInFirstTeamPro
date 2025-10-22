using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BinarySpacePartitioning
{
    private DungeonData _data;
    //  노드에 고유한 Index를 부여하기 위한 카운터
    private int _nodeCounter = 0;

    public List<RoomNode> BSP(DungeonData data)
    {
        _data = data;
        _nodeCounter = 0; // BSP 시작 시 카운터 초기화

        var rootPos =
            new NodePosition(new Vector2Int(0, 0),
            new Vector2Int(_data.Width, _data.Height));

        // 루트 노드에 고유 인덱스 0 할당 후 카운터 증가
        var rootNode = new RoomNode(null, rootPos, _nodeCounter++);

        var result = new List<RoomNode> { rootNode };
        var graph = new Queue<RoomNode>(new[] { rootNode });
        var iter = 0;

        while (iter < _data.Iteration && graph.Count > 0)
        {
            iter++;

            var curNode = graph.Dequeue();
            SplitSpace(curNode, graph, result);
        }

        return result;
    }

    public enum ELine
    {
        None = -1,
        Horizontal = 0,
        Vertical = 1
    }

    private void SplitSpace(RoomNode node, Queue<RoomNode> graph, List<RoomNode> nodeList)
    {
        var widthStatus = node.Width >= _data.RoomMinWidth * 2;
        var heightStatus = node.Height >= _data.RoomMinHeight * 2;

        var splitDir = (widthStatus, heightStatus) switch
        {
            (true, true) => (ELine)Random.Range(0, 2),
            (false, true) => ELine.Horizontal,
            (true, false) => ELine.Vertical,
            _ => ELine.None
        };

        var curPos = node.Pos;
        var newPos = 0;
        NodePosition pos1 = null;
        NodePosition pos2 = null;

        switch (splitDir)
        {
            case ELine.Horizontal:
                newPos = Random.Range(
                    curPos.BL.y + _data.RoomMinHeight,
                    curPos.TL.y - _data.RoomMinHeight
                );

                pos1 = new NodePosition(curPos.BL, new Vector2Int(curPos.BR.x, newPos));
                pos2 = new NodePosition(new Vector2Int(curPos.BL.x, newPos), curPos.TR);
                break;

            case ELine.Vertical:
                newPos = Random.Range(
                    curPos.BL.x + _data.RoomMinWidth,
                    curPos.BR.x - _data.RoomMinWidth
                );

                pos1 = new NodePosition(curPos.BL, new Vector2Int(newPos, curPos.TL.y));
                pos2 = new NodePosition(new Vector2Int(newPos, curPos.BL.y), curPos.TR);
                break;

            case ELine.None:
            default:
                return;
        }

        //  고유한 Index 부여: _nodeCounter를 사용하여 노드마다 다른 Index 할당
        var node1 = new RoomNode(node, pos1, _nodeCounter++);
        var node2 = new RoomNode(node, pos2, _nodeCounter++);

        graph.Enqueue(node1);
        nodeList.Add(node1);

        graph.Enqueue(node2);
        nodeList.Add(node2);
    }
}