using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Msg_Validate : MonoBehaviour
{
    //VITS����
    [SerializeField] private VITS_Speech m_VITS_Player;
    //���ֲ�����
    [SerializeField] private MusicPlayer m_MusicPlayer;
    //���ֲ�����
    [SerializeField] private ChatScript m_ChatScript;
    //���ص���Ϣ
    [SerializeField] private Text m_TextBack;
    //����
    [SerializeField] private Service m_Service;

    public string inputMsgs;
    public string outputMsgs;
    /// <summary>
    ///�������������Ϣ��Ƶ
    /// </summary>
    public bool IsPlayingMsg = false;
    /// <summary>
    ///����������������������Ϣ
    /// </summary>
    public bool IsOutputTextMsg = false;
    /// <summary>
    ///�ظ���Ϣ��Ƶ���ںϳ�
    /// </summary>
    public bool IsOutputWAVEncoding = false;
    /// <summary>
    ///���ڲ�������
    /// </summary>
    public bool IsPlayingMusic = false;
/*    /// <summary>
    ///���ڲ���ָ������
    /// </summary>
    public bool IsSelectPlaying = false;*/
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// ChatGPT��������ظ���Ϣ����
    /// return ������Ϣ
    /// </summary>
    public string checkChatGPTError(long responseCode)
    {
        string lang = m_VITS_Player.langString;
        string _callback = "";
        if (responseCode == 401)
        {
            Debug.Log(responseCode);
            if (lang.Equals("����"))
            {
                _callback = "���Api������������" + "    ���ش���:" + responseCode;
            }
            else if (lang.Equals("����"))
            {
                _callback = "���ʤ���API�����}�����ꤽ���ʸФ������" + "    �������`��:" + responseCode;
            }
        }
        else if (responseCode == 400)
        {
            Debug.Log(responseCode);
            if (lang.Equals("����"))
            {
                _callback = "���ʵ�̫���ˣ���ͷ�е��Σ��밴���Ͻǵ�����ͼ����ջỰ������" + "    ���ش���:" + responseCode;
            }
            else if (lang.Equals("����"))
            {
                _callback = "�|�����ह���ơ�����ä��^��ʹ�������ϤΥ���`�󥢥������Ѻ���ƻỰ�򥯥ꥢ���Ƥ�����" + "    �������`��:" + responseCode;
            }
        }
        else if (responseCode == 404)
        {
            Debug.Log(responseCode);
            if (lang.Equals("����"))
            {
                _callback = "����������û������ChatGPT��" + "    ���ش���:" + responseCode;
            }
            else if (lang.Equals("����"))
            {
                _callback = "���ʤ��Υͥåȭh��������å�GPT�˽ӾA�Ǥ��ʤ��ä�" + "    �������`��:" + responseCode;
            }

        }
        else if (responseCode == 500)
        {
            Debug.Log(responseCode);
            if (lang.Equals("����"))
            {
                _callback = "ChatGPT�Ǹ����������Լ��������ˣ���û�������������һ�������Ұ�" + "    ���ش���:" + responseCode;
            }
            else if (lang.Equals("����"))
            {
                _callback = "���Υ���å�GPT�ΥХ��᤬���}���𤳤ä��������ʤ��������Ф餯��������������" + "    �������`��:" + responseCode;
            }
        }
        else if (responseCode == 403)
        {
            Debug.Log(responseCode);
            if (lang.Equals("����"))
            {
                _callback = "�ҵķ��ʱ�ChatGPT�ܾ���" + "    ���ش���:" + responseCode;
            }
            else if (lang.Equals("����"))
            {
                _callback = "����å�GPT��˽�Υ���������ܷ񤷤ޤ�����" + "    �������`��:" + responseCode;
            }
        }
        else
        {
            Debug.Log(responseCode);
            if (lang.Equals("����"))
            {
                _callback = "����������Ҳ���Ͳ���������⣬�����ʱ���������Ұ�" + "    ���ش���:" + responseCode;
            }
            else if (lang.Equals("����"))
            {
                _callback = "���Ά��}��˽�ˤ�Ϥ����Ҋ�Ƥ��顢��Q�Ǥ��ʤ��Τǡ����Ф餯���Ƥ���ޤ����Ƥ�" + "    �������`��:" + responseCode;
            }

        }
        return _callback;
    }

    /// <summary>
    /// �Ƿ񴥷����ģʽ���
    /// return �ظ���Ϣ
    /// </summary>
    public string musicSelectionModeCheck(string msg)
    {
        string lang = m_VITS_Player.langString;
        Debug.Log(msg);

        string returnMsg = "";
        if (msg.IndexOf("#������ģʽ") != -1 
            || msg.IndexOf("������`��") != -1 
            || msg.IndexOf("#�M����") != -1 
            || msg.IndexOf("���M����") != -1 
            || msg.IndexOf("#���`�ɤ����") != -1 
            || msg.IndexOf("�褦���ȤǤ��ޤ�") != -1 
            || msg.IndexOf("�褦���Ȥ��Ǥ��ޤ�") != -1 
            || msg.IndexOf("�褦���ȤϤǤ��ޤ�") != -1 
            || msg.IndexOf("�褦�����Ϥ���ޤ�") != -1 
            || msg.IndexOf("�褦����������ޤ�") != -1
            || msg.IndexOf("˽�ϸ��֤ǤϤ���ޤ�") != -1
            || msg.IndexOf("˽�ϸ��֤����") != -1
            || msg.IndexOf("�褦�C�ܤϳ֤äƤ��ʤ�") != -1
            || msg.IndexOf("�褦�C�ܤϤ���ޤ�") != -1)
        {
            List<Tuple<string, string>> musicName = m_MusicPlayer.getRandomMusicName();
            //ȥ���ļ�����ͷ��XX.
            string a = musicName[0].Item1.Substring(0, 3);
            string musicNameJP = musicName[0].Item1.Replace(a, "");
            string musicNameCN = musicName[0].Item2.Replace(a, "");
            m_Service.UnloadAsset(m_MusicPlayer.audioClip);
            m_MusicPlayer.audioClip = Resources.Load<AudioClip>("Music/" + musicName[0].Item1);
            if (lang.Equals("����"))
            {
                returnMsg = "�õģ�����Ϊ�㳪һ�ס�"+ musicNameCN + "����";
            }
            else if (lang.Equals("����"))
            {
                returnMsg = "���㡢��" + musicNameJP + "����褤�ޤ��礦";
            }
            IsOutputTextMsg = true;
            IsPlayingMusic = true;
            return returnMsg;
        }
        else
        {
            return msg;
        }
    }

    public void InputMsgValidate(string msg)
    {
        string lang = m_VITS_Player.langString;
        if (msg.IndexOf("#�л�����") != -1 || msg.IndexOf("#�����椨") != -1 || msg.IndexOf("�������椨") != -1)
        {
            try
            {
                float f = float.Parse(msg.Replace(msg.Substring(0, 5), ""));
                m_ChatScript.setBackGround(f);
                msg = "";
            }
            catch
            {
                msg = "";
            }
        }
        if (msg.IndexOf("#�л�����") != -1 || msg.IndexOf("#�����}��") != -1 || msg.IndexOf("�������}��") != -1)
        {
            try
            {
                float f = float.Parse(msg.Replace(msg.Substring(0, 5), ""));
                m_ChatScript.setTakagiIllustration(f);
                msg = "";
            }
            catch
            {
                msg = "";
            }
        }
        if (msg.IndexOf("#��������") != -1 || msg.IndexOf("#���S����") != -1 || msg.IndexOf("�����S����") != -1)
        {
            try
            {
                m_ChatScript.m_ChatHistory.Add(msg);
                int i = int.Parse(msg.Replace(msg.Substring(0, 5), ""));
                List<Tuple<string, string>> musicName = m_MusicPlayer.getMusicName(i);
                //ȥ���ļ�����ͷ��XX.
                string a = musicName[0].Item1.Substring(0, 3);
                string musicNameJP = musicName[0].Item1.Replace(a, "");
                string musicNameCN = musicName[0].Item2.Replace(a, "");
                m_Service.UnloadAsset(m_MusicPlayer.audioClip);
                m_MusicPlayer.audioClip = Resources.Load<AudioClip>("Music/" + musicName[0].Item1);
                if (lang.Equals("����"))
                {
                    m_ChatScript.CallBack("�õģ�����Ϊ�㳪һ�ס�" + musicNameCN + "����");
                    Debug.Log(musicNameCN);
                }
                else if (lang.Equals("����"))
                {
                    m_ChatScript.CallBack("���㡢��" + musicNameJP + "����褤�ޤ��礦");
                    Debug.Log(musicNameJP);
                }
                IsOutputTextMsg = true;
                IsPlayingMusic = true;
                msg = "";
            }
            catch
            {
                msg = "";
            }
        }
        if (msg.IndexOf("#����") != -1 || msg.IndexOf("#h") != -1 || msg.IndexOf("��h") != -1)
        {
            try
            {
                m_ChatScript.m_ChatHistory.Add(msg);
                if (lang.Equals("����"))
                {
                    m_ChatScript.CallBack("���룺��#�л�����0.XX���������л�����ͼƬ\n���룺��#�л�����0.XX���������л�����ͼƬ\n���룺��#��������XX�������ڲ���ָ������");
                }
                else if (lang.Equals("����"))
                {
                    m_ChatScript.CallBack("����������Ф��椨�ϡ�#�����椨0.XX����\n����饯���`�����}���Ф��椨�ϡ�#�����}��0.XX����\n ָ���������S�������ϡ�#���S����XX����");
                }
                msg = "";
            }
            catch
            {
                msg = "";
            }
        }
        inputMsgs = msg;
    }
    
    //���룺string
    //Ҫƥ���Դ����string[]
    //string[] can = { "����", "Ϊ��" };
    //string[] canCmd = FindSimilarStrings(msg, can);
    //�������Ƶ��ַ���
    public static string[] FindSimilarStrings(string input, string[] strings)
    {
        int threshold = 2; // ���������Ե���ֵ����Levenshtein�����������̴���

        // �洢�����ַ������б�
        List<string> similarStrings = new List<string>();

        foreach (string str in strings)
        {
            int distance = ComputeLevenshteinDistance(input, str);
            if (distance <= threshold)
            {
                similarStrings.Add(str);
            }
        }

        return similarStrings.ToArray();
    }

    public static int ComputeLevenshteinDistance(string str1, string str2)
    {
        int[,] dp = new int[str1.Length + 1, str2.Length + 1];

        for (int i = 0; i <= str1.Length; i++)
        {
            dp[i, 0] = i;
        }

        for (int j = 0; j <= str2.Length; j++)
        {
            dp[0, j] = j;
        }

        for (int i = 1; i <= str1.Length; i++)
        {
            for (int j = 1; j <= str2.Length; j++)
            {
                int cost = (str1[i - 1] == str2[j - 1]) ? 0 : 1;
                dp[i, j] = Math.Min(Math.Min(dp[i - 1, j] + 1, dp[i, j - 1] + 1), dp[i - 1, j - 1] + cost);
            }
        }

        return dp[str1.Length, str2.Length];
    }
    //��ȡ������������Ϣ
    public string getInputMsgs()
    {
        return inputMsgs;
    }
    //��ȡ�����������Ϣ
    public string getOututMsgs()
    {
        return outputMsgs;
    }
}
