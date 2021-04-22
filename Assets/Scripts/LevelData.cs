using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData {

    public bool[] activatedLevels;
    public bool[] activatedSkins;
    public int indexOfActiveSkin;

    public LevelData(LevelBarMenu levelBarMenu) {
        activatedLevels = levelBarMenu.activatedLevels;
        activatedSkins = levelBarMenu.activatedSkins;
        indexOfActiveSkin = levelBarMenu.indexOfActiveSkin;
    }
}
