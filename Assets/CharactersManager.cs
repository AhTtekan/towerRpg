using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class CharactersManager : SingletonBase<CharactersManager>
{
    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    private Character[] _chars;
    public Character[] Chars
    {
        get {
            if (_chars == null)
                _chars = LoadChars();
            return _chars;
        }
    }
    
    void Update()
    {
    }

    private Character[] LoadChars()
    {
        var path = Application.persistentDataPath + "/characters.char";
        if (File.Exists(path))
        {
            File.Delete(path);
        }

        CreateTestCharacters();
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);

        var chars = binaryFormatter.Deserialize(stream) as Character[];
        stream.Close();

        return chars;
    }

    private void CreateTestCharacters()
    {
        var path = Application.persistentDataPath + "/characters.char";
        Character v = new Character
        {
            HP_Current = 100,
            HP_Max = 100,
            Name = "Valdun",
            PortraitId = 0
        };
        v.APCore.AP_Max = 10;
        v.APCore.APBuildRateInSeconds = 10;
        v.SpeedCore.Agility = 200;
        Character r = new Character
        {
            HP_Current = 100,
            HP_Max = 100,
            Name = "Rettigar",
            PortraitId = 1
        };
        r.APCore.AP_Max = 10;
        r.APCore.APBuildRateInSeconds = 10;
        r.SpeedCore.Agility = 120;
        Character n = new Character
        {
            HP_Current = 100,
            HP_Max = 100,
            Name = "Noralis",
            PortraitId = 2
        };
        n.APCore.AP_Max = 10;
        n.APCore.APBuildRateInSeconds = 10;
        n.SpeedCore.Agility = 90;
        Character c = new Character
        {
            HP_Current = 100,
            HP_Max = 100,
            Name = "Cardione",
            PortraitId = 3
        };
        c.APCore.AP_Max = 10;
        c.APCore.APBuildRateInSeconds = 10;
        c.SpeedCore.Agility = 35;

        Character[] chars = new Character[] { v, r, n, c };

        System.IO.FileStream stream = new System.IO.FileStream(path, System.IO.FileMode.Create);

        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, chars);
        stream.Close();
    }
}
