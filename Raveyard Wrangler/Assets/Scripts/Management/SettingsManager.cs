using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public struct Settings
{
    public Dictionary<string, string> stringSettings;
    public Dictionary<string, float> floatSettings;
    public Dictionary<string, int> intSettings;
    public Dictionary<string, bool> boolSettings;
}

public class SettingsManager
{
    // Reference to itself that can be accessed from anywhere
    private static readonly SettingsManager instance = new SettingsManager();
    public static SettingsManager Instance
    {
        get
        {
            return instance;
        }
    }

    // EDITOR VARIABLES
    [SerializeField]
    string settingsDefineFilePath;
    [SerializeField]
    string userSettingsFilePath;

    // SETTINGS
    Settings settings;

    // Explicit static constructor to tell C# compiler
    // not to mark type as beforefieldinit
    static SettingsManager()
    {
        instance.LoadFile();
        Debug.Log("cursorTexturePath: " + instance.GetSetting<string>("cursorTexturePath"));
        Debug.Log("cursorPixelSize: " + instance.GetSetting<int>("cursorPixelSize"));
    }

    public void SetSetting<T>(string key, T value)
    {
        switch (value)
        {
            case string sValue:
                settings.stringSettings.Add(key, sValue);
                break;

            case float fValue:
                settings.floatSettings.Add(key, fValue);
                break;

            case int iValue:
                settings.intSettings.Add(key, iValue);
                break;

            case bool bValue:
                settings.boolSettings.Add(key, bValue);
                break;

            default:
                Debug.LogError("Type " + value.GetType() + " is not a valid settings type\nValid types are: string, float, int(32), and bool");
                break;
        }
    }

    public dynamic GetSetting<T>(string key)
    {
        Debug.Log("Name of T: " + typeof(T).Name);
        switch (typeof(T).Name)
        {
            case "String":
                string sValue;
                if(settings.stringSettings.TryGetValue(key, out sValue))
                {
                    return sValue;
                }
                else
                {
                    Debug.LogError("Key " + key + "is not associated with any string value");
                    return null;
                }

            case "Float":
                float fValue;
                if (settings.floatSettings.TryGetValue(key, out fValue))
                {
                    return fValue;
                }
                else
                {
                    Debug.LogError("Key " + key + "is not associated with any float value");
                    return null;
                }

            case "Int32":
                int iValue;
                if (settings.intSettings.TryGetValue(key, out iValue))
                {
                    return iValue;
                }
                else
                {
                    Debug.LogError("Key " + key + "is not associated with any integer value");
                    return null;
                }

            case "Bool":
                bool bValue;
                if (settings.boolSettings.TryGetValue(key, out bValue))
                {
                    return bValue;
                }
                else
                {
                    Debug.LogError("Key " + key + "is not associated with any boolean value");
                    return null;
                }

            default:
                Debug.LogError("Type " + typeof(T) + " is not a valid settings type\nValid types are: string, float, int(32), and bool");
                return null;
        }
    }

    public void SaveFile()
    {
        string destination = Application.persistentDataPath + userSettingsFilePath;
        FileStream file;

        if(File.Exists(destination)) { file = File.OpenWrite(destination); }
        else
        {
            file = File.Create(destination);
        }

        BinaryFormatter binFormatter = new BinaryFormatter();
        binFormatter.Serialize(file, settings);
        file.Close();
    }

    public void LoadFile()
    {
        string destination = Application.persistentDataPath + userSettingsFilePath;
        FileStream file;

        if (File.Exists(destination)) { file = File.OpenWrite(destination); }
        else
        {
            Debug.LogWarning("Settings file not found, loading defaults.");
            LeadDefault();
            return;
        }

        BinaryFormatter binFormatter = new BinaryFormatter();
        settings = (Settings)binFormatter.Deserialize(file);
        file.Close();
    }

    void LeadDefault()
    {
        string destination = Application.dataPath + settingsDefineFilePath;

        // Attempt to read the file into jsonString
        string jsonString;
        try
        {
            using (StreamReader streamReader = new StreamReader(destination, Encoding.UTF8))
            {
                jsonString = streamReader.ReadToEnd();
            }
        }
        catch (FileNotFoundException)
        {
            Debug.LogError("Default settings were not loaded.\nFile not found at: " + destination);
            return;
        }

        settings = JsonConvert.DeserializeObject<Settings>(jsonString);
    }
}
