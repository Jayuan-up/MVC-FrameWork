using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*****************************************
//创建人：Jay
//功能说明：
//***************************************** 
public class Sound : MonoSingleton<Sound>
{
    AudioSource m_background;
    AudioSource m_effect;
    public string ResourceDir = "";

    protected override void Awake()
    {
        base.Awake();
        
        m_background = gameObject.AddComponent<AudioSource>();
        //循环
        m_background.loop = true;
        //不自动播放
        m_background.playOnAwake = false;

        m_effect = gameObject.AddComponent<AudioSource>();

    }

    //播放背景音乐（切换背景音乐）
    public void PlayBG(string audioName)
    {
        string oldName;
        //获取正在播放的音频名称
        if (m_background.clip == null)
        {
            oldName = "";
        }
        else
        {
            oldName= m_background.clip.name;
        }

        if (audioName != oldName)
        {
            //播放新音频
            string path = ResourceDir + "/" + audioName;
            AudioClip clip = Resources.Load<AudioClip>(path);
            if (clip != null)
            {
                m_background.clip = clip;
                m_background.Play();
            }
        }
    }

    //播放音效
    public void PlayEffect(string audioName)
    {
        string path = ResourceDir + "/" + audioName;
        AudioClip clip= Resources.Load<AudioClip>(path);
        //播放一次音效
        m_effect.PlayOneShot(clip);
    }
}
