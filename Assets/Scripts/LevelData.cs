using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData {

    public bool[] activatedLevels;

    public LevelData(LevelBarMenu levelBarMenu) {
        activatedLevels = levelBarMenu.activatedLevels;
    }
}
