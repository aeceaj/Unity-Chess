using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MusicControl : MonoBehaviour
{
    private AudioSource musicPlayer;
    private Toggle playingToggle;
    private List<string> playList;
    private int ind;

    private void Awake()
    {
        musicPlayer = GetComponent<AudioSource>();
        playList = GetSongList(Application.streamingAssetsPath);
        playingToggle = GameObject.Find("UICanvas/MusicPanel/Play_Pause").GetComponent<Toggle>();
        ind = 0;
        StartCoroutine(LoadAudio(playList[ind]));
    }

    private IEnumerator LoadAudio(string fullPath)
    {
        UnityWebRequest audioRequest = UnityWebRequestMultimedia.GetAudioClip(fullPath, AudioType.OGGVORBIS);
        yield return audioRequest.SendWebRequest();
        if (audioRequest.result == UnityWebRequest.Result.Success)
        {
            musicPlayer.clip = DownloadHandlerAudioClip.GetContent(audioRequest);
        }
    }

    private IEnumerator LoadAndPlay(string fullPath)
    {
        UnityWebRequest audioRequest = UnityWebRequestMultimedia.GetAudioClip(fullPath, AudioType.OGGVORBIS);
        yield return audioRequest.SendWebRequest();
        if (audioRequest.result == UnityWebRequest.Result.Success)
        {
            musicPlayer.clip = DownloadHandlerAudioClip.GetContent(audioRequest);
            if (playingToggle.isOn)
            {
                musicPlayer.Play();
            }
            else
            {
                playingToggle.isOn = true;
            }
        }
    }

    private static List<string> GetSongList(string dirPath)
    {
        if (Directory.Exists(dirPath))
        {
            List<string> songList = new();
            DirectoryInfo directory = new(dirPath);
            FileInfo[] files = directory.GetFiles();
            foreach (FileInfo file in files)
            {
                if (file.Name.EndsWith(".ogg"))
                {
                    songList.Add(file.FullName);
                }
            }
            return songList;
        }
        return null;
    }

    public void MusicOn(bool isOn)
    {
        if (isOn)
        {
            musicPlayer.Play();
        }
        else
        {
            musicPlayer.Pause();
        }
    }

    public void NextSong()
    {
        ind++;
        if (ind >= playList.Count)
        {
            ind = 0;
        }
        StartCoroutine(LoadAndPlay(playList[ind]));
    }

    public void PreviousSong()
    {
        ind--;
        if (ind < 0)
        {
            ind = playList.Count - 1;
        }
        StartCoroutine(LoadAndPlay(playList[ind]));
    }

    public static void OpenDirectory()
    {
        string path = Application.streamingAssetsPath.Replace("/", "\\");
        System.Diagnostics.Process.Start(path);
    }
}
