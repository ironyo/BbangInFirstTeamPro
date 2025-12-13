using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic; // 파티클 데이터 저장을 위해 필요

// ICanvasRaycastFilter는 UI 레이캐스트를 무시하고 싶을 때 사용
// 이 예시에서는 필요 없으므로 주석 처리
// public class UIParticleSystem : Graphic, ICanvasRaycastFilter
public class UIParticleSystem : Graphic
{
    // 파티클 시스템 컴포넌트 참조
    [SerializeField] private ParticleSystem targetParticleSystem;

    // 파티클 시스템의 재질 (Material)
    // 파티클 시스템의 Renderer 모듈에서 사용되는 Material과 일치해야 합니다.
    // 보통 Default-Particle 또는 Sprite-Default 같은 Material을 사용합니다.
    [SerializeField] private Material particleMaterial;

    // 현재 활성화된 파티클 데이터를 저장할 리스트
    private ParticleSystem.Particle[] particles;

    //-----------------------------------------------------------------------------------
    // Graphic 클래스 오버라이드 (렌더링 관련)
    //-----------------------------------------------------------------------------------

    protected override void Awake()
    {
        base.Awake();
        if (targetParticleSystem == null)
        {
            // 자식 Particle System 컴포넌트를 자동으로 찾습니다.
            targetParticleSystem = GetComponentInChildren<ParticleSystem>();
        }
        if (targetParticleSystem == null)
        {
            Debug.LogError("UIParticleSystem: No ParticleSystem found on this GameObject or its children.", this);
            enabled = false; // 파티클 시스템이 없으면 스크립트 비활성화
            return;
        }

        // 파티클 시스템의 Material을 Graphic 컴포넌트의 material과 동기화
        // 이렇게 해야 캔버스 렌더러가 올바른 Material을 사용합니다.
        if (particleMaterial != null)
        {
            material = particleMaterial;
        }
        else if (targetParticleSystem.GetComponent<ParticleSystemRenderer>() != null)
        {
            material = targetParticleSystem.GetComponent<ParticleSystemRenderer>().material;
        }

        if (material == null)
        {
            Debug.LogError("UIParticleSystem: No Material assigned or found on ParticleSystemRenderer.", this);
            enabled = false;
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        // 필요하다면 파티클 시스템 재생
        if (targetParticleSystem != null && !targetParticleSystem.isPlaying)
        {
            targetParticleSystem.Play();
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        // 필요하다면 파티클 시스템 정지
        if (targetParticleSystem != null && targetParticleSystem.isPlaying)
        {
            targetParticleSystem.Stop();
        }
    }

    // UI 레이아웃 변경 시 호출되어 메시를 다시 생성하도록 요청
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear(); // 이전 메시 정보 지우기

        if (targetParticleSystem == null || !targetParticleSystem.isPlaying || targetParticleSystem.particleCount == 0)
        {
            return; // 파티클 시스템이 없거나 재생 중이 아니거나 파티클이 없으면 그릴 것이 없음
        }

        // 현재 활성화된 모든 파티클 데이터 가져오기
        targetParticleSystem.GetParticles(particles);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform, // 이 UI 요소의 RectTransform
            new Vector2(0, 0), // 화면상의 기준점 (여기서는 사용되지 않음)
            canvas.worldCamera, // 캔버스가 Screen Space - Camera 모드일 때 필요
            out Vector2 localPoint // 결과 로컬 좌표
        );

        Camera renderCam = (canvas.worldCamera != null) ? canvas.worldCamera : Camera.main;
        // 파티클 개수만큼 메시 생성
        for (int i = 0; i < targetParticleSystem.particleCount; i++)
        {
            ParticleSystem.Particle p = particles[i];

            // 파티클의 로컬 좌표를 월드 좌표로 변환
            Vector3 particleWorldPos = targetParticleSystem.transform.TransformPoint(p.position);

            // 월드 좌표를 렌더링 카메라의 화면 좌표로 변환
            Vector3 screenPos = renderCam.WorldToScreenPoint(particleWorldPos);

            // 2. 화면 좌표를 이 UI 요소의 로컬 좌표로 변환
            Vector2 particleLocalPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rectTransform, // 이 UI 요소의 RectTransform
                screenPos,     // 화면 좌표 (Screen Point)
                canvas.worldCamera, // EventCamera: Overlay 모드일 경우 null이 전달되도록 함
                out particleLocalPos
            );

            // 파티클의 크기를 UI 스케일에 맞게 조정
            float size = p.GetCurrentSize(targetParticleSystem) * canvas.scaleFactor;
            Color32 color = p.GetCurrentColor(targetParticleSystem);

            // 파티클 하나를 Quad (사각형) 메시로 만듭니다.
            // 정점 4개와 삼각형 2개로 구성됩니다.

            // 현재 vh에 추가될 정점의 시작 인덱스
            int vertCount = vh.currentVertCount;

            // 파티클을 중심으로 하는 Quad의 4개 코너 좌표
            Vector2 halfSize = new Vector2(size * 0.5f, size * 0.5f);

            vh.AddVert(particleLocalPos + new Vector2(-halfSize.x, -halfSize.y), color, new Vector2(0, 0)); // Bottom-Left
            vh.AddVert(particleLocalPos + new Vector2(-halfSize.x, halfSize.y), color, new Vector2(0, 1));  // Top-Left
            vh.AddVert(particleLocalPos + new Vector2(halfSize.x, halfSize.y), color, new Vector2(1, 1));   // Top-Right
            vh.AddVert(particleLocalPos + new Vector2(halfSize.x, -halfSize.y), color, new Vector2(1, 0)); // Bottom-Right

            // 삼각형 인덱스 추가 (두 개의 삼각형으로 사각형 만듦)
            vh.AddTriangle(vertCount + 0, vertCount + 1, vertCount + 2);
            vh.AddTriangle(vertCount + 2, vertCount + 3, vertCount + 0);
        }
    }

    //-----------------------------------------------------------------------------------
    // 업데이트 (메시 재구축 요청)
    //-----------------------------------------------------------------------------------

    private void LateUpdate()
    {
        // 매 프레임 파티클 시스템이 업데이트되므로, 메시를 다시 그릴 필요가 있다고 알립니다.
        // 이것이 없으면 파티클이 움직이지 않고 고정된 위치에 그려집니다.
        // SetVerticesDirty() 대신 SetAllDirty()를 사용해도 되지만, SetVerticesDirty()가 더 효율적입니다.
        if (targetParticleSystem != null && targetParticleSystem.isPlaying)
        {
            SetVerticesDirty();
        }
    }

    // 마우스 이벤트 등을 받지 않도록 RaycastFilter 인터페이스를 구현할 경우
    // public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    // {
    //     return false; // 항상 false를 반환하여 이 UI 요소는 레이캐스트를 막지 않도록 함
    // }
}