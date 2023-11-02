using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PipeManager : MonoBehaviour
{
    private DimensionManager dimManager;

    [SerializeField] private GameObject firstPipe;
    [SerializeField] private List<GameObject> pipes;

    private List<float> pipesOriginZ = new List<float>();
    private float targetZ;

    [SerializeField] private GameObject firstMachine;
    [SerializeField] private List<GameObject> machines;

    [SerializeField] private GameObject parentPipe;

    private void Start()
    {
        dimManager = GameObject.Find("DimensionManager").GetComponent<DimensionManager>();
        SavePosZ();
    }

    // Update is called once per frame
    void Update()
    {
        if (dimManager.Is2D)
        {
            ChangePosZ();
            //DeactivateMachines();
        }
        else
        {
            ResetPosZ();
            //ActivateMachines();
        }
    }

    private void SavePosZ()
    {
        targetZ = firstPipe.transform.position.z;
        foreach (var pipe in pipes)
        {
            pipesOriginZ.Add(pipe.transform.position.z);
        }
    }

    private void ChangePosZ()
    {
        pipes[0].transform.position = new Vector3(pipes[0].transform.position.x, pipes[0].transform.position.y, targetZ);
        pipes[1].transform.position = new Vector3(pipes[1].transform.position.x, pipes[1].transform.position.y, targetZ);
        pipes[2].transform.position = new Vector3(pipes[2].transform.position.x, pipes[2].transform.position.y, targetZ - 0.065f);
        pipes[3].transform.position = new Vector3(pipes[3].transform.position.x, pipes[3].transform.position.y, targetZ - 0.55f);
        pipes[4].transform.position = new Vector3(pipes[4].transform.position.x, pipes[4].transform.position.y, targetZ - 0.59f);
        pipes[5].transform.position = new Vector3(pipes[5].transform.position.x, pipes[5].transform.position.y, targetZ - 0.6f);
        pipes[6].transform.position = new Vector3(pipes[6].transform.position.x, pipes[6].transform.position.y, targetZ - 0.605f);
    }

    private void ResetPosZ()
    {
        for (int i = 0; i < pipes.Count; i++)
        {
            pipes[i].transform.position = new Vector3(pipes[i].transform.position.x, pipes[i].transform.position.y, pipesOriginZ[i]);
        }
    }

    private void ActivateMachines()
    {
        for (int i=0; i< machines.Count; i++)
        {
            machines[i].SetActive(true);
        }
    }

    private void DeactivateMachines()
    {
        for (int i = 0; i < machines.Count; i++)
        {
            machines[i].SetActive(false);
        }
    }
}
