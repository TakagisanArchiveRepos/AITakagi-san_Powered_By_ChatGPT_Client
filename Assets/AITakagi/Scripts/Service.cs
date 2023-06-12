using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Service : MonoBehaviour
{
    //���ֲ�����
    [SerializeField] private MusicPlayer m_MusicPlayer;
    //�ű�
    [SerializeField] private ChatScript m_ChatScript;
    //VITS����
    [SerializeField] private VITS_Speech m_VITS_Player;
    //��Ϣ��֤
    [SerializeField] private Msg_Validate m_Msg_Validate;
    //����
    [SerializeField] private Raw_Image m_Raw_Image;
    //����UI��
    [SerializeField] private GameObject m_PostGUI;
    //���췢�Ͳ�
    [SerializeField] private GameObject m_SendText;
    public int ���Ŵ��� = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (m_VITS_Player.getLan().Equals(""))
        {
            Debug.Log("m_VITS_Player.getLan() Is NullorBlank");
            m_VITS_Player.SetChangeLan("����");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// �������ģʽ
    /// </summary>
    public void StartRandomPlay()
    {
        List<Tuple<string, string>> musicName = m_MusicPlayer.getRandomMusicName();
        m_ChatScript.m_ChatHistory.Add("�����������");
       /* UnloadAsset(m_MusicPlayer.audioClip); 
        m_MusicPlayer.audioClip = Resources.Load<AudioClip>("Music/" + musicName[0].Item1);*/
        m_MusicPlayer.Sts = "0";
        m_SendText.SetActive(false);
        m_PostGUI.SetActive(true);

        //ȥ���ļ�����ͷ��XX.
        string a = musicName[0].Item1.Substring(0, 3);
        string musicNameJP = musicName[0].Item1.Replace(a, "");
        string musicNameCN = musicName[0].Item2.Replace(a, "");
        if (���Ŵ��� == 0)
        {

            
            if (m_VITS_Player.langString.Equals("����"))
            {
                m_ChatScript.CallBack("�õģ�����Ϊ�㳪һ�ס�" + musicNameCN + "����");
                Debug.Log(musicNameCN);
                ���Ŵ��� += 1;
            }
            else if (m_VITS_Player.langString.Equals("����"))
            {
                m_ChatScript.CallBack("���㡢��" + musicNameJP + "����褤�ޤ��礦");
                Debug.Log(musicNameJP);
                ���Ŵ��� += 1;
            }
        }
        else
        {
            if (m_VITS_Player.langString.Equals("����"))
            {
                m_ChatScript.CallBack("����������Ϊ�㳪һ�ס�" + musicNameCN + "����");
                Debug.Log(musicNameCN);
            }
            else if (m_VITS_Player.langString.Equals("����"))
            {
                m_ChatScript.CallBack("���㡢�A���ơ�" + musicNameJP + "����褤�ޤ��礦");
                Debug.Log(musicNameJP);
            }
        }
        m_Msg_Validate.IsOutputTextMsg = true;
        m_Msg_Validate.IsPlayingMusic = true;
    }

    //ж��ָ����Դ
    public void UnloadAsset(UnityEngine.Object obj)
    {
        Resources.UnloadAsset(obj);
    }
}
