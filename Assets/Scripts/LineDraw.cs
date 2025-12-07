using UnityEngine;
using System.Collections.Generic;

public class LineDraw : MonoBehaviour
{
    public GameObject drawingPrefab;
    public Material material;

    public float width = 1f;
    public float height = 3f;
    public int segmentsPerEdge = 30;

    private LineRenderer lineRenderer;

    private void Start()
    {
        CreateRectangle();
    }

    private void CreateRectangle()
    {
        // 기존 방식 유지: 프리팹 생성 후 라인랜더러 가져오기
        GameObject drawing = Instantiate(drawingPrefab);
        lineRenderer = drawing.GetComponent<LineRenderer>();

        lineRenderer.startWidth = 0.15f;
        lineRenderer.endWidth = 0.15f;

        Randomize();

        var points = new List<Vector3>();
        float halfW = width / 2f;
        float halfH = height / 2f;

        // 깔끔한 4변 구성
        AddEdge(points, new Vector3(-halfW, -halfH), new Vector3(halfW, -halfH)); // 아래
        AddEdge(points, new Vector3(halfW, -halfH), new Vector3(halfW, halfH)); // 오른쪽
        AddEdge(points, new Vector3(halfW, halfH), new Vector3(-halfW, halfH)); // 위
        AddEdge(points, new Vector3(-halfW, halfH), new Vector3(-halfW, -halfH)); // 왼쪽

        // LineRenderer에 정확한 포인트 적용
        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());

        // Material 적용
        lineRenderer.material = material;
    }

    private void AddEdge(List<Vector3> list, Vector3 start, Vector3 end)
    {
        // 하나의 변을 segmentsPerEdge로 잘게 나눠 점선 패턴 유지
        for (int i = 0; i < segmentsPerEdge; i++)
        {
            float t = (float)i / segmentsPerEdge;
            list.Add(Vector3.Lerp(start, end, t));
        }
    }

    private void Randomize()
    {
        material.SetFloat("width", 1f);
        material.SetFloat("heigth", 0.5f);
    }
}
