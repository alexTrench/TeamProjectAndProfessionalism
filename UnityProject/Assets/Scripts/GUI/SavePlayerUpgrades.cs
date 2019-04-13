using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class BinaryCharacterSaver : MonoBehaviour
{

    public CharacterData characterData;
    const string folderName = "BinaryCharacterData";
    const string fileExtension = ".dat";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            string folderPath = Path.Combine(Application.persistentDataPath, folderName);
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string dataPath = Path.Combine(folderPath, characterData.characterName + fileExtension);
            SaveCharacter(characterData, dataPath);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            string[] filePaths = GetFilePaths();

            if (filePaths.Length > 0)
                characterData = LoadCharacter(filePaths[0]);
        }
    }

    static void SaveCharacter(CharacterData data, string path)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        using (FileStream fileStream = File.Open(path, FileMode.OpenOrCreate))
        {
            binaryFormatter.Serialize(fileStream, data);
        }
    }

    static CharacterData LoadCharacter(string path)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        using (FileStream fileStream = File.Open(path, FileMode.Open))
        {
            return (CharacterData)binaryFormatter.Deserialize(fileStream);
        }
    }

    static string[] GetFilePaths()
    {
        string folderPath = Path.Combine(Application.persistentDataPath, folderName);

        return Directory.GetFiles(folderPath, fileExtension);
    }
}