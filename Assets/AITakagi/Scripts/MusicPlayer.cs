using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicPlayer : MonoBehaviour
{
    /// <summary>
    /// ��Ƶ���
    /// </summary>
    public AudioSource m_AudioSource;
    //��Ϣ��֤
    [SerializeField] private Msg_Validate m_Msg_Validate;
    //����
    [SerializeField] private Raw_Image m_Raw_Image;

    [SerializeField] private ChatScript m_ChatScript;
    //ֹͣ���Ű�ť
    [SerializeField] private GameObject m_musicStop;
    //������
    [SerializeField] private Animator m_Quad;
    //����UI��
    [SerializeField] private GameObject m_PostGUI;
    //���췢�Ͳ�
    [SerializeField] private GameObject m_SendText;
    //���ص���Ϣ
    [SerializeField] private Text m_TextBack;
    //VITS
    [SerializeField] private VITS_Speech m_VITS_Player;
    //����
    [SerializeField] private Service m_Service;

    /// <summary>
    /// musicName����������
    /// </summary>
    public string musicName;
    /// <summary>
    /// <br>Sts�����Ž���</br>
    /// <br>0����ʼ��</br>
    /// <br>1����ʼ����ǰ��ת��</br>
    /// <br>2��ת����ɲ���ʼ����</br>
    /// <br>3�����Ž�������ʼת���س�ʼ����</br>
    /// </summary>
    public string Sts = "0";

    //
    string lang = "";

    public AudioClip audioClip;
    //��������Ų��ŵ���һ�׺���ĸ�ʱ������
    public List<Tuple<string, string>> musicName1;

    /// <summary>
    /// <br>����Ԫ��</br>
    /// <br>int, string, string, float, float</br>
    /// <br>��Ŀ����</br>
    /// <br>��������</br>
    /// <br>������������Ϣ�����ã�</br>
    /// <br>����ʱ�ı����巬��</br>
    /// <br>����ʱ���������淬��</br>
    /// </summary>
    public List<Tuple<int, string, string, float, float>> MusicAll = new List<Tuple<int, string, string, float, float>>();
    //string path = "";
    // Start is called before the first frame update
    void Start()
    {
        //��ʼ��ʱ������ֹͣ���Ű�ť
        m_musicStop.SetActive(false);
        m_Quad.SetFloat("BackGround", 0.04f);
        //ȫ������
        //��һ��ED�ϼ�
        MusicAll.Add(new Tuple<int, string, string, float, float>(11, "11.�ݤޤ����ޥ�ƥ��å�", "11.��Ѫ����������", 0.01f, 0.011f));
        MusicAll.Add(new Tuple<int, string, string, float, float>(12, "12.AM11��00", "12.����11��", 0.02f, 0.021f));
        MusicAll.Add(new Tuple<int, string, string, float, float>(13, "13.��ܞ܇", "13.���г�", 0.03f, 0.31f));
        MusicAll.Add(new Tuple<int, string, string, float, float>(14, "14.�L��������", "14.���֮��", 0.04f, 0.041f));
        MusicAll.Add(new Tuple<int, string, string, float, float>(15, "15.С�������Τ���", "15.СС����", 0.05f, 0.081f));
        MusicAll.Add(new Tuple<int, string, string, float, float>(16, "16.�ۆh", "16.����", 0.06f, 0.061f));
        MusicAll.Add(new Tuple<int, string, string, float, float>(17, "17.����ä�핤Τ褦��", "17.�������ʱ", 0.07f, 0.071f));

        //�ڶ���ED�ϼ�
        MusicAll.Add(new Tuple<int, string, string, float, float>(21, "21.��(���ʤ�)", "21.��", 0.08f, 0.081f));
        MusicAll.Add(new Tuple<int, string, string, float, float>(22, "22.��ѩ", "22.��ѩ", 0.09f, 0.051f));
        MusicAll.Add(new Tuple<int, string, string, float, float>(23, "23.������", "23.�漣", 0.10f, 0.011f));
        MusicAll.Add(new Tuple<int, string, string, float, float>(24, "24.���꤬�Ȥ�", "24.лл", 0.11f, 0.021f));
        MusicAll.Add(new Tuple<int, string, string, float, float>(25, "25.STARS", "25.STARS", 0.01f, 0.031f));
        MusicAll.Add(new Tuple<int, string, string, float, float>(26, "26.���ʤ���", "26.�׸���", 0.02f, 0.041f));
        MusicAll.Add(new Tuple<int, string, string, float, float>(27, "27.�Ԥ�ʤ����ɤ͡�", "27.��Ȼ����˵���ڡ�", 0.03f, 0.051f));
        MusicAll.Add(new Tuple<int, string, string, float, float>(28, "28.�䤵�����ݳ֤�", "28.���������", 0.04f, 0.061f));
        MusicAll.Add(new Tuple<int, string, string, float, float>(29, "29.100��ؤΡ�I love you��", "29.100���˵�����㡱", 0.05f, 0.071f));

        //������ED�ϼ�
        MusicAll.Add(new Tuple<int, string, string, float, float>(31, "31.���Ƿꤨ����", "31.�����������", 0.06f, 0.031f));
        MusicAll.Add(new Tuple<int, string, string, float, float>(32, "32.Over Drive", "32.Over Drive", 0.07f, 0.081f));
        MusicAll.Add(new Tuple<int, string, string, float, float>(33, "33.�Ҥޤ��μs��", "33.���տ���Լ��", 0.08f, 0.091f));
        MusicAll.Add(new Tuple<int, string, string, float, float>(34, "34.ѧ�@���", "34.ѧ԰���", 0.10f, 0.021f));
        MusicAll.Add(new Tuple<int, string, string, float, float>(35, "35.���礤�դ�", "35.����", 0.09f, 0.091f));
        MusicAll.Add(new Tuple<int, string, string, float, float>(36, "36.���󥿤�ˤ�äƤ���", "36.ʥ�����˽�����", 0.11f, 0.051f));
        MusicAll.Add(new Tuple<int, string, string, float, float>(37, "37.���Ω`�ޥ��å��ե��󥿥��`", "37.ѩ֮ħ������", 0.01f, 0.041f));
        MusicAll.Add(new Tuple<int, string, string, float, float>(38, "38.��", "38.��", 0.02f, 0.081f));

        //�糡��ED�ϼ�
        MusicAll.Add(new Tuple<int, string, string, float, float>(41, "41.���դؤ���", "41.ͨ������֮��", 0.03f, 0.011f));
        MusicAll.Add(new Tuple<int, string, string, float, float>(42, "42.�����Q�y", "42.����۲�", 0.04f, 0.021f));
        MusicAll.Add(new Tuple<int, string, string, float, float>(43, "43.fragile", "43.����", 0.05f, 0.031f));
        MusicAll.Add(new Tuple<int, string, string, float, float>(44, "44.���˽줱", "44.���������", 0.06f, 0.041f));

        //���Σ��Ķ���¼��
        MusicAll.Add(new Tuple<int, string, string, float, float>(51, "51.First Love", "51.����", 0.07f, 0.051f));
        MusicAll.Add(new Tuple<int, string, string, float, float>(52, "52.LOVE�ޥ��`��", "52.��������", 0.08f, 0.061f));
        MusicAll.Add(new Tuple<int, string, string, float, float>(53, "53.Maji��koi����5��ǰ", "53.����������5��ǰ", 0.09f, 0.071f));
        MusicAll.Add(new Tuple<int, string, string, float, float>(54, "54.����Lover", "54.��������", 0.10f, 0.081f));
        MusicAll.Add(new Tuple<int, string, string, float, float>(55, "55.���ȹ�", "55.��͹�", 0.11f, 0.091f));
        MusicAll.Add(new Tuple<int, string, string, float, float>(56, "56.�त�٥��", "56.��ɫ����", 0.01f, 0.011f));
        MusicAll.Add(new Tuple<int, string, string, float, float>(57, "57.���ߤΤ��٤�", "57.�ഺ�޻�", 0.02f, 0.021f));
        MusicAll.Add(new Tuple<int, string, string, float, float>(58, "58.���ꥹ�ޥ�������", "58.ƽ��ҹ", 0.11f, 0.031f));

        //AI����


        //Debug.Log(path);

    }

    // Update is called once per frame
    void Update()
    {
        lang = m_VITS_Player.langString;
        //����Ƿ�ʼ�����������
        if (!m_ChatScript.IsRandomPlaying)
        {
            if (Sts == "0" && !m_AudioSource.isPlaying && !m_Msg_Validate.IsOutputTextMsg && !m_Msg_Validate.IsOutputWAVEncoding && m_Msg_Validate.IsPlayingMusic)
            {
                Sts = "1";
                m_Raw_Image.FadeToClear(2f);
                Debug.Log("Sts=" + Sts);
            }
            else if (Sts == "1" && m_Raw_Image.Sts.Equals("2"))
            {
                Sts = "2";

                //��ʾֹͣ���Ű�ť
                m_musicStop.SetActive(true);
                //������Ŀ��ͬ�л�������
                Tuple<int, string, string, float, float> result = MusicAll.Find(x => x.Item2 == musicName);
                m_Quad.SetFloat("BackGround", result.Item4);
                //������Ŀ��ͬ�л�����
                m_ChatScript.setTakagiIllustration(result.Item5);
                //�������촰��
                m_PostGUI.SetActive(false);

                MusicPlay();
                Debug.Log("Sts=" + Sts);
            }

            if (Sts == "2" && !m_AudioSource.isPlaying)
            {
                Sts = "3";
                m_Raw_Image.FadeToClear(2f);
                Debug.Log("Sts=" + Sts);

            }
            else if (Sts == "3" && m_Raw_Image.Sts.Equals("2"))
            {
                Sts = "0";
                //����ֹͣ���Ű�ť
                m_musicStop.SetActive(false);
                //��ʾ���촰��
                m_PostGUI.SetActive(true);
                m_SendText.SetActive(true);
                //��ԭ������
                m_Quad.SetFloat("BackGround", 0.04f);
                //m_ChatScript.DefaultTakagi();
                //��ԭ����
                m_ChatScript.setTakagiIllustration(0f);
                if (lang.Equals("����"))
                {
                    m_VITS_Player.Speek("���Ž���...�����µ���Ϣ�������Ի���");
                    m_TextBack.text = "���Ž���...�����µ���Ϣ�������Ի��ɡ�";

                }
                else if (lang.Equals("����"))
                {
                    m_VITS_Player.Speek("��������...��å��`���ܥå������������ƻ�Ԓ��A���褦");
                    m_TextBack.text = "��������...��å��`���ܥå������������ƻ�Ԓ��A���褦��";
                }

                Debug.Log("Sts=" + Sts);
            }
        }

        if (m_ChatScript.IsRandomPlaying)
        {
            //����Ƿ�ʼ�����������
            if (Sts == "0" && !m_AudioSource.isPlaying && !m_Msg_Validate.IsOutputTextMsg && !m_Msg_Validate.IsOutputWAVEncoding && m_Msg_Validate.IsPlayingMusic)
            {
                Sts = "1";
                m_Raw_Image.FadeToClear(2f);
                Debug.Log("Sts=" + Sts);
            }
            else if (Sts == "1" && m_Raw_Image.Sts.Equals("2"))
            {
                Sts = "2";

                //m_ChatScript.MusicTakagi();
                //��ʾֹͣ���Ű�ť
                m_musicStop.SetActive(true);
                //������Ŀ��ͬ�л�������
                Tuple<int, string, string, float, float> result = MusicAll.Find(x => x.Item2 == musicName);
                m_Quad.SetFloat("BackGround", result.Item4);
                //������Ŀ��ͬ�л�����
                m_ChatScript.setTakagiIllustration(result.Item5);
                //�������촰��
                m_PostGUI.SetActive(false);

                MusicPlay();
                Debug.Log("Sts=" + Sts);
            }

            if (Sts == "2" && !m_AudioSource.isPlaying)
            {
                /*//��������Ų��ŵ���һ�׺���ĸ�ʱ������
                musicName1 = getRandomMusicName();*/
                Sts = "3";
                m_Raw_Image.FadeToClear(2f);
                Debug.Log("Sts=" + Sts);

            }
            else if (Sts == "3" && m_Raw_Image.Sts.Equals("2") && m_ChatScript.IsRandomPlaying && m_Service.���Ŵ��� != 1)
            {
                Sts = "2";
                //��ʾֹͣ���Ű�ť
                m_musicStop.SetActive(true);
                //������Ŀ��ͬ�л�������
                Tuple<int, string, string, float, float> result = MusicAll.Find(x => x.Item2 == musicName);
                m_Quad.SetFloat("BackGround", result.Item4);
                //������Ŀ��ͬ�л�����
                m_ChatScript.setTakagiIllustration(result.Item5);
                //�������촰��
                m_PostGUI.SetActive(false);

                MusicPlay();

                Debug.Log("Sts=" + Sts);
            }
            else if (Sts == "3" && m_Raw_Image.Sts.Equals("2") && m_ChatScript.IsRandomPlaying && m_Service.���Ŵ��� == 1)
            {

                m_Service.StartRandomPlay();
                Sts = "0";

                Debug.Log("Sts=" + Sts);
            }
            else if (Sts == "3" && m_Raw_Image.Sts.Equals("2") && !m_ChatScript.IsRandomPlaying)
            {
                Sts = "0";
                //����ֹͣ���Ű�ť
                m_musicStop.SetActive(false);
                //��ʾ���촰��
                m_PostGUI.SetActive(true);
                m_SendText.SetActive(true);
                //��ԭ������
                m_Quad.SetFloat("BackGround", 0.04f);
                //m_ChatScript.DefaultTakagi();
                //��ԭ����
                m_ChatScript.setTakagiIllustration(0f);
                if (lang.Equals("����"))
                {
                    m_VITS_Player.Speek("���Ž���...�����µ���Ϣ�������Ի���");
                    m_TextBack.text = "���Ž���...�����µ���Ϣ�������Ի��ɡ�";

                }
                else if (lang.Equals("����"))
                {
                    m_VITS_Player.Speek("��������...��å��`���ܥå������������ƻ�Ԓ��A���褦");
                    m_TextBack.text = "��������...��å��`���ܥå������������ƻ�Ԓ��A���褦��";
                }
                Debug.Log("Sts=" + Sts);
            }
        }
    }


    /// <summary>
    /// <br>��ȡָ������</br>
    /// <br>int num�����</br>
    /// <br>return ����</br>
    /// </summary>
    public List<Tuple<string, string>> getMusicName(int num)
    {
        List<Tuple<string, string>> MusicName = new List<Tuple<string, string>>();
        try
        {
            Tuple<int, string, string, float, float> result = MusicAll.Find(x => x.Item1 == num);
            //��������
            string musicNameJP = result.Item2;
            string musicNameCN = result.Item3;
            m_Service.UnloadAsset(audioClip);
            audioClip = Resources.Load<AudioClip>("Music/" + musicNameJP);
            MusicName.Add(new Tuple<string, string>(musicNameJP, musicNameCN));
            return MusicName;
        }
        catch
        {
            string musicNameJP = MusicAll[4].Item2;
            string musicNameCN = MusicAll[4].Item2;
            m_Service.UnloadAsset(audioClip);
            audioClip = Resources.Load<AudioClip>("Music/" + musicNameJP);
            MusicName.Add(new Tuple<string, string>(musicNameJP, musicNameCN));
            return MusicName;
        }
    }

    /// <summary>
    /// ��ȡ������ŵĸ�����
    /// return ������������ĸ���
    /// </summary>
    public List<Tuple<string, string>> getRandomMusicName()
    {
        List<Tuple<string, string>> MusicName = new List<Tuple<string, string>>();
        try
        {
            System.Random random = new System.Random();
            int rad = MusicAll.Count - 1;
            rad = random.Next(0, rad);
            int rad1 = 0;
            if (rad < 0)
            {
                rad1 = 0;
            }
            else
            {
                rad1 = rad;
            }
            Debug.Log(rad1);
            //��������
            string musicNameJP = MusicAll[rad1].Item2;
            string musicNameCN = MusicAll[rad1].Item3;
            musicName = musicNameJP;
            m_Service.UnloadAsset(audioClip);
            audioClip = Resources.Load<AudioClip>("Music/" + musicNameJP);
            MusicName.Add(new Tuple<string, string>(musicNameJP, musicNameCN));
            return MusicName;
        }
        catch (ArgumentOutOfRangeException)
        {
            string musicNameJP = MusicAll[4].Item2;
            string musicNameCN = MusicAll[4].Item3;
            musicName = musicNameJP;
            m_Service.UnloadAsset(audioClip);
            audioClip = Resources.Load<AudioClip>("Music/" + musicNameJP);
            MusicName.Add(new Tuple<string, string>(musicNameJP, musicNameCN));
            return MusicName;
        }
    }

    /// <summary>
    /// ͨ��������ָ����������
    /// parameter����������
    /// </summary>
    public void MusicPlay()
    {
        //Resources.LoadAssetAtPath();
        //AudioClip audioClip = Resources.Load<AudioClip>("Music/" + musicName);
        m_AudioSource.clip = audioClip;

        m_Msg_Validate.IsPlayingMusic = true;

        m_AudioSource.Play(); // ������Ƶ�ļ�
        m_Msg_Validate.IsPlayingMusic = false;
        Debug.Log(audioClip);
        string a = "";
        if (m_Msg_Validate.IsOutputTextMsg)
        {
            a += "����������������������Ϣ,";
        }
        else
        {
            a += "����������������������Ϣ,";
        }

        if (m_Msg_Validate.IsOutputWAVEncoding)
        {
            a += "�ظ���Ϣ��Ƶ���ںϳ�,";
        }
        else
        {
            a += "�ظ���Ϣ��Ƶ�ϳɽ���,";
        }
        if (m_Msg_Validate.IsPlayingMusic)
        {
            a += "���ڲ�������,";
        }
        else
        {
            a += "���ֲ����ѽ���,";
        }

        Debug.Log(a);

        //���������������״̬����
        /*
        path += "/Music/" + getMusicName() + ".mp3";
        //ʹ��www����ز���
        StartCoroutine(LoadMusicClip(path));
        */
    }
    /// <summary>
    /// ֹͣ��������
    /// </summary>
    public void StopMusic()
    {
        m_AudioSource.Stop();
        m_Service.UnloadAsset(audioClip);
        //�˳��������ģʽ
        m_ChatScript.IsRandomPlaying = false;
        m_Service.���Ŵ��� = 0;
    }


    /*
        /// <summary>
        /// ��ȡ���ļ�����Ƶ������
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private IEnumerator LoadMusicClip(string filePath)
        {
            string url = "file://" + filePath; // ���URL
            UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG); // ��������
            yield return www.SendWebRequest(); // �ȴ�������Ӧ

            if (www.result == UnityWebRequest.Result.Success)
            {
                // ��ȡ��Ƶ�ļ�
                AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
                m_AudioSource.clip = clip;
                m_AudioSource.Play(); // ������Ƶ�ļ�
            }
            else
            {
                Debug.Log(filePath);
                //Debug.LogError("Load audio clip failed. " + www.error);
            }
        }
        */



}
