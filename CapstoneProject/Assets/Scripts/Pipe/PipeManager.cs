using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PipeManager : MonoBehaviour
{
    [SerializeField] private int phase;

    private DimensionManager dimManager;
    [SerializeField] private Transform pipes;

    private float targetZ;
    private List<Vector3> pipesOriginPos = new List<Vector3>();

    private void Start()
    {
        dimManager = GameObject.Find("DimensionManager").GetComponent<DimensionManager>();
        SavePosZ();
    }

    private void Update()
    {
        if (dimManager.Is2D)
        {
            ChangePosZ();
        }
        else
        {
            ResetPosZ();
        }
    }

    private void SavePosZ()
    {
        targetZ = pipes.GetChild(0).position.z;

        for (int i = 1; i < pipes.childCount; i++)
            pipesOriginPos.Add(pipes.GetChild(i).position);
    }

    private void ChangePosZ()
    {
        if (phase == 1)
        {
            for (int i = 1; i < pipes.childCount; i++)
            {
                var pipe = pipes.GetChild(i);
                pipe.position = new Vector3(pipe.position.x, pipe.position.y, targetZ);
            }
        }
        else
        {
            pipes.GetChild(1).position = new Vector3(pipes.GetChild(1).position.x, pipes.GetChild(1).position.y, targetZ);
            pipes.GetChild(2).position = new Vector3(pipes.GetChild(2).position.x, pipes.GetChild(2).position.y, targetZ);
            pipes.GetChild(3).position = new Vector3(pipes.GetChild(3).position.x, pipes.GetChild(3).position.y, targetZ - 0.065f);
            pipes.GetChild(4).position = new Vector3(pipes.GetChild(4).position.x, pipes.GetChild(4).position.y, targetZ - 0.55f);
            pipes.GetChild(5).position = new Vector3(pipes.GetChild(5).position.x, pipes.GetChild(5).position.y, targetZ - 0.59f);
            pipes.GetChild(6).position = new Vector3(pipes.GetChild(6).position.x, pipes.GetChild(6).position.y, targetZ - 0.6f);
            pipes.GetChild(7).position = new Vector3(pipes.GetChild(7).position.x, pipes.GetChild(7).position.y, targetZ - 0.605f);
        }
    }

    private void ResetPosZ()
    {
        for (int i = 1; i < pipes.childCount; i++)
        {
            var pipe = pipes.GetChild(i);
            pipe.position = pipesOriginPos[i - 1];
        }
    }
}
