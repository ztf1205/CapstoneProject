using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class ScaleBasedTiling_Ground : MonoBehaviour
{
    [Label("활성화")]
    public bool activate = true;

    // 원하는 스케일 조정값
    [Label("머티리얼 스케일")]
    public Vector2 scaleMultiplier = new Vector2(1.0f, 1.0f);


    [Foldout("Info"), SerializeField, OnValueChanged("UpdateMaterial")]
    private Material material;
    [Foldout("Info"), SerializeField]
    private List<Renderer> renderers;

    private Vector2 originalTilingScale;

    private bool lastActivate;
    private Vector3 lastTilingScale;
    private Vector2 lastScaleMultiplier;

    void Start()
    {
        BuildTile();

        UpdateLastVariable();

        ChangeTiling();
    }

    void Update()
    {
        ChangeTilingCheck();
    }

    private void BuildTile()
    {
        // 메테리얼을 복제
        Material materialInstance = new Material(material);
        // 기본 스케일값 저장
        originalTilingScale = materialInstance.mainTextureScale;

        foreach (var renderer in renderers)
        {
            renderer.material = materialInstance;
        }
    }

    private void UpdateLastVariable()
    {
        lastActivate = activate;
        lastTilingScale = transform.localScale;
        lastScaleMultiplier = scaleMultiplier;
    }

    private void ChangeTilingCheck()
    {
        if (IsSameLastVariable())
        {
            return;
        }

        UpdateLastVariable();

        ChangeTiling();
    }

    private bool IsSameLastVariable()
    {
        return lastActivate == activate && lastTilingScale == transform.localScale && lastScaleMultiplier == scaleMultiplier;
    }

    private void ChangeTiling()
    {
        if (activate)
        {
            ScaleBasedTiling();
        }
        else
        {
            OriginalTiling();
        }
    }

    private void ScaleBasedTiling()
    {
        // 현재 스케일 가져오기
        Vector3 currentScale = transform.localScale;

        for (int i = 0; i < renderers.Count; i++)
        {
            Vector2 tiling = Vector2.zero;
            if (i <= 1)
            {
                // 스케일 비율 계산
                tiling = new Vector2(currentScale.x * scaleMultiplier.x, currentScale.z * scaleMultiplier.y);
            }
            else if (i <= 3)
            {
                // 스케일 비율 계산
                tiling = new Vector2(currentScale.x * scaleMultiplier.x, currentScale.y * scaleMultiplier.y);
            }
            else if (i <= 5)
            {
                // 스케일 비율 계산
                tiling = new Vector2(currentScale.z * scaleMultiplier.x, currentScale.y * scaleMultiplier.y);
            }
            // 메테리얼의 타일링 값을 설정
            renderers[i].material.mainTextureScale = tiling;
        }
    }

    private void OriginalTiling()
    {
        foreach (var renderer in renderers)
        {
            renderer.material.mainTextureScale = originalTilingScale;
        }
    }

    [Button]
    private void UpdateMaterial()
    {
        foreach (var renderer in renderers)
        {
            renderer.material = material;
        }
    }
}