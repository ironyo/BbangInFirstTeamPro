using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LineDisplay : MonoBehaviour
{
    // 라인 렌더러 관련 필드 제거 (width, lineMat, Z_OFFSET)

    [Header("Tile Setting")]
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase tile;

    public void DisplayLine(RoomNode rootNode)
    {
        if (tilemap == null || tile == null)
        {
            Debug.LogError("Tilemap 또는 Tile이 Inspector에 할당되지 않았습니다.");
            return;
        }

        var queue = new Queue<RoomNode>();
        queue.Enqueue(rootNode);

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();

            var posArr = node.Pos.GetPositionArray(); // [BL, TL, TR, BR]

            // 룸의 네 경계선을 타일로 그립니다.
            for (var i = 0; i < posArr.Length; ++i)
            {
                Vector2Int startPoint = posArr[i];
                Vector2Int endPoint = posArr[(i + 1) % posArr.Length];

                DrawLine(startPoint, endPoint, tilemap, tile);
            }

            foreach (var child in node.ChildNode)
            {
                queue.Enqueue((RoomNode)child);
            }
        }
    }

    /// <summary>
    /// 축에 정렬된 두 점 사이에 타일로 선을 그립니다. (두께 2)
    /// </summary>
    private void DrawLine(Vector2Int p1, Vector2Int p2, Tilemap map, TileBase lineTile)
    {
        // 수평선 (Y 좌표가 같음): X축으로 길게, Y축으로 두께 2
        if (p1.y == p2.y)
        {
            int startX = Mathf.Min(p1.x, p2.x);
            int endX = Mathf.Max(p1.x, p2.x);
            int y = p1.y;

            for (int x = startX; x <= endX; x++)
            {
                //  두께 2 구현: 현재 Y 좌표와 Y+1 좌표에 모두 타일을 찍습니다.
                map.SetTile(new Vector3Int(x, y, 0), lineTile);
                map.SetTile(new Vector3Int(x, y + 1, 0), lineTile); // 두께 2
            }
        }
        // 수직선 (X 좌표가 같음): Y축으로 길게, X축으로 두께 2
        else if (p1.x == p2.x)
        {
            int startY = Mathf.Min(p1.y, p2.y);
            int endY = Mathf.Max(p1.y, p2.y);
            int x = p1.x;

            for (int y = startY; y <= endY; y++)
            {
                //  두께 2 구현: 현재 X 좌표와 X+1 좌표에 모두 타일을 찍습니다.
                map.SetTile(new Vector3Int(x, y, 0), lineTile);
                map.SetTile(new Vector3Int(x + 1, y, 0), lineTile); // 두께 2
            }
        }
    }
}