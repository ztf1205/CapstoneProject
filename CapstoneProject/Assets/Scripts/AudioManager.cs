using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    private void Awake()
    {
        EventManager.Subscribe("GainCube", PlayCubeSound);
        EventManager.Subscribe("GainCrystal", PlayCrystalSound);
        EventManager.Subscribe("CubP", PlayCubePedestalSound);
    }

    private void OnDestroy()
    {
        EventManager.Unsubscribe("GainCube", PlayCubeSound);
        EventManager.Unsubscribe("GainCrystal", PlayCrystalSound);
        EventManager.Unsubscribe("CubP", PlayCubePedestalSound);
    }

    private void PlayCubeSound()
    {
        Debug.Log("큐브 획득");
    }

    private void PlayCrystalSound()
    {
        Debug.Log("크리스탈 획득");
    }

    private void PlayCubePedestalSound()
    {
        Debug.Log("큐브 설치");
    }
}
