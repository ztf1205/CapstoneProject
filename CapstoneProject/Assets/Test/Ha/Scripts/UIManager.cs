using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text cubeCountText;
    public TMP_Text crystalCountText;
    public TMP_Text maxCubeText;
    public TMP_Text maxCrystalText;
    public int maxCube;
    public int maxCrystal;

    void Awake()
    {
        maxCubeText.text = "/ " + maxCube.ToString();
        maxCrystalText.text = "/ " + maxCrystal.ToString();
    }

    public void IncreaseCubeCount(int cubeCount)
    {
        cubeCountText.text = cubeCount.ToString();

    }

    public void IncreaseCrystalCount(int crystalCount)
    {
        crystalCountText.text = crystalCount.ToString();

    }

}
