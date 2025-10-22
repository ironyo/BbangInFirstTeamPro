using UnityEngine;
using System.Collections.Generic;
using System.CodeDom.Compiler;

public class MapGenerator : MonoBehaviour
{
    [Header("Create Setting")]
    [SerializeField] private DungeonData dungeonData;

    private List<RoomNode> _roomNodeList;

    private GetRooms getRooms;

    private void Start()
    {
        GenerateDungeon();
        getRooms = new GetRooms(dungeonData.Width, dungeonData.Height);
    }

    private void GenerateDungeon()
    {
        var bsp = new BinarySpacePartitioning();
        _roomNodeList = bsp.BSP(dungeonData);

        GetComponent<LineDisplay>().DisplayLine(_roomNodeList[0]); // 0
    }
}
