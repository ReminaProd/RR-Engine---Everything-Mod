using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class LoadWavFile : MonoBehaviour {

    AudioSource source;
    Text loadDisplay;
    InputField inputFile;

    // Use this for initialization
    void Start () {
        source = gameObject.GetComponent<AudioSource>();
        loadDisplay = transform.Find("LoadDisplay").Find("Text").GetComponent<Text>();
        inputFile = transform.Find("InputField").GetComponent<InputField>();
    }

    // Update is called once per frame
    void Update () {
	
	}

    // Player interfaces:
    public void Play()
    {
        source.Play();
    }
    public void Pause()
    {
        source.Pause();
    }
    public void Stop()
    {
        source.Stop();
    }

    // File control:
    public void DeleteClip()
    {
        source.clip = null;
        File.Delete(Path.Combine(Application.persistentDataPath, "MyFile.wav"));
    }
    public void SaveClip()
    {
        byte[] wavFile = OpenWavParser.AudioClipToByteArray(source.clip);
        File.WriteAllBytes(Path.Combine(Application.persistentDataPath, "MyFile.wav"), wavFile);
    }

    public void LoadDefaultFile()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "MyFile.wav");
        if (File.Exists(filePath))
        {
            byte[] wavFile = File.ReadAllBytes(filePath);
            source.clip = OpenWavParser.ByteArrayToAudioClip(wavFile);
            loadDisplay.text = "Samples: " + source.clip.samples.ToString();
        }
        else
        {
            loadDisplay.text = "File not found";
        }
    }

    public void LoadCustomFile()
    {
        string filePath = Path.Combine(Application.persistentDataPath, inputFile.text);
        if (File.Exists(filePath))
        {
            byte[] wavFile = File.ReadAllBytes(filePath);
            source.clip = OpenWavParser.ByteArrayToAudioClip(wavFile);
            loadDisplay.text = "Samples: " + source.clip.samples.ToString();
        }
        else
        {
            loadDisplay.text = "File not found";
        }
    }
}
