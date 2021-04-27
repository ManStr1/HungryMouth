using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData {

    public bool[] activatedLevels;
    public bool[] activatedSkins;
    public bool[] rewardedLevels;
    public int indexOfActiveSkin;
    public int money;

    public LevelData(DataProcessController dataProcessController) {
        activatedLevels = dataProcessController.activatedLevels;
        activatedSkins = dataProcessController.activatedSkins;
        indexOfActiveSkin = dataProcessController.indexOfActiveSkin;
        money = dataProcessController.money;
        rewardedLevels = dataProcessController.rewardedLevels;
    }
}
