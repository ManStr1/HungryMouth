using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {
    public static void SaveLevel(DataProcessController dataProcessController) {
        Debug.Log("Level Saved");
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        LevelData data = new LevelData(dataProcessController);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static bool SaveExist() {
        string path = Application.persistentDataPath + "/player.fun";
        return File.Exists(path);
    }

    public static LevelData LoadLevel() {
        string path = Application.persistentDataPath + "/player.fun";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            LevelData data = formatter.Deserialize(stream) as LevelData;
            stream.Close();

            return data;
        } else {

            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public static void ClearLevelData() {
        string path = Application.persistentDataPath + "/player.fun";
        if (File.Exists(path)) {
            File.Delete(path);
        }
    }

    public static void SaveSound(Music soundData) {
        Debug.Log("Sound Saved");
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/sound.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        SoundData data = new SoundData(soundData);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SoundData LoadSound() {
        string path = Application.persistentDataPath + "/sound.fun";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SoundData data = formatter.Deserialize(stream) as SoundData;
            stream.Close();

            return data;
        } else {

            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
