
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;


public class GptTurboScript : MonoBehaviour
{
    /// <summary>
    /// api��ַ���ٷ��棩
    /// </summary>
    public string m_ApiUrl = "https://api.openai.com/v1/chat/completions";
    
    /*
    public string m_ApiUrl_backup1 = "http://127.0.0.1:8000/chat/completions";
    public string m_ApiUrl_backup2 = "https://api.openai-proxy.com/v1/chat/completions";
    public string m_ApiUrl_backup3 = "https://api1.openai-proxy.com/v1/chat/completions";
    public string m_ApiUrl_backup4 = "https://api2.openai-proxy.com/v1/chat/completions";
    public string m_ApiUrl_backup5 = "https://api3.openai-proxy.com/v1/chat/completions";
    public string m_ApiUrl_backup6 = "chatapi.takagi3.top/v1/chat/completions";
    public string m_ApiUrl_backup8 = "openapi.takagi3.top/api/v1/chat/completions";
    public string m_ApiUrl_backup10 = "api.takagi3.top/v1/chat/completions";
    */

    /// <summary>
    /// gpt-3.5-turbo
    /// </summary>
    public string m_gptModel = "gpt-3.5-turbo";
    /// <summary>
    /// ����Ի�
    /// </summary>
    [SerializeField]public List<SendData> m_DataList = new List<SendData>();
    /// <summary>
    /// AI��������1
    /// </summary>
    public string PromptCN;
    /// <summary>
    /// AI��������2
    /// </summary>
    public string PromptCN1;
    /// <summary>
    /// AI��������1
    /// </summary>
    public string PromptJP;
    //����
    [SerializeField] private Setting m_Setting;

    //VITS����
    [SerializeField] private VITS_Speech m_VITS_Player;
    //��Ϣ��֤
    [SerializeField] private Msg_Validate m_Msg_Validate;
    //���ű�
    [SerializeField] private ChatScript m_ChatScript;
    //�ɵ���Ϣ
    private string oldpostWord = "";

    private void Start()
    {
        //����ʱ���������
        //m_DataList.Add(new SendData("system", Prompt));
    }
    /// <summary>
    /// ���ýӿ�
    /// </summary>
    /// <param name="_postWord"></param>
    /// <param name="_openAI_Key"></param>
    /// <param name="_callback"></param>
    /// <returns></returns>
    public IEnumerator GetPostData(string _postWord,string _openAI_Key, System.Action<string> _callback)
    {
 
        //ϴ��ģʽ;
        if (m_ChatScript.IsBrainwashing)
        {
            //��ջ���Ի�
            m_DataList.Clear();
            //ϴ����ɣ��ر�ϴ��״̬
            m_ChatScript.IsBrainwashing = false;
            Debug.Log("ϴ�����");
        }
        //���淢�͵���Ϣ�б�
        m_DataList.Add(new SendData("user", _postWord));
        //�л��ٷ��������url
        string apiUrl = "";
        if (m_Setting.linkMode.Equals("0"))
        {
            apiUrl = m_ApiUrl;
            Debug.Log(apiUrl);
        }
        else
        {
            apiUrl = m_Setting.OtherUrl;
            Debug.Log(apiUrl);
        }
        using (UnityWebRequest request = new UnityWebRequest(apiUrl, "POST"))
        {
            PostData _postData = new PostData
            {
                model = m_gptModel,
                messages = m_DataList
            };

            string _jsonText = JsonUtility.ToJson(_postData);
            byte[] data = System.Text.Encoding.UTF8.GetBytes(_jsonText);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(data);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", string.Format("Bearer {0}", _openAI_Key));

            yield return request.SendWebRequest();

            if (request.responseCode == 200)
            {
                Debug.Log(request.responseCode);
                string _msg = request.downloadHandler.text;
                MessageBack _textback = JsonUtility.FromJson<MessageBack>(_msg);
                if (_textback != null && _textback.choices.Count > 0)
                {
                    string _backMsg = _textback.choices[0].message.content;
                    //����Ƿ񴥷����ģʽ
                    _backMsg = m_Msg_Validate.musicSelectionModeCheck(_backMsg);
                    //��Ӽ�¼
                    m_DataList.Add(new SendData("assistant", _backMsg));
                    _callback(_backMsg);
                }
                Debug.Log(_postWord);

            }
            else
            {
                //����ģʽ�ܾ�����ʱ���Ի�url����
                if (request.responseCode == 403 && m_Setting.linkMode.Equals("1"))
                {
                    //����ģʽ�ط����� += 1;
                    m_ChatScript.IsBrainwashing = true;
                    m_ChatScript.setSendNum(0);
                    _callback(m_Msg_Validate.checkChatGPTError(request.responseCode));
                    //ReGetPostData(_callback);
                }
                else
                {
                    _callback(m_Msg_Validate.checkChatGPTError(request.responseCode));
                }




                /*//����ģʽ�Ǿܾ����ʻ�ֱ��ģʽ���еĴ����ط�
                if (m_Setting.linkMode.Equals("0") && ֱ��ģʽ�ط����� <= 3)
                {
                    m_ChatScript.IsBrainwashing = true;
                    ֱ��ģʽ�ط����� += 1;
                    ReGetPostData(_callback);
                }
                else if(m_Setting.linkMode.Equals("0") && ֱ��ģʽ�ط����� >= 3)
                {
                    _callback(m_Msg_Validate.checkChatGPTError(request.responseCode));
                    ֱ��ģʽ�ط����� = 0;
                }*/


                
                Debug.Log("ChatGPT����" + request.responseCode);
            }
            
        }


    }

    private void ReGetPostData(System.Action<string> _callback)
    {
        //ϴ��ģʽ;
        //��ջ���Ի�
        m_DataList.Clear();
        //ϴ����ɣ��ر�ϴ��״̬
        m_ChatScript.IsBrainwashing = false;
        m_ChatScript.setSendNum(1);
        Debug.Log("ϴ�����,��ʼ���·���");

        if (!m_ChatScript.initializing_Hypnosis_StatementsIsJPN)
        {
            //OpenAIֱ��ģʽ
            if (m_Setting.linkMode.Equals("0"))
            {
                oldpostWord = PromptCN + string.Format("����ȫ��ʹ��{0}�ش�", m_VITS_Player.getLan()) + m_ChatScript.old_msg;
            }
            //��������ģʽ
            else if (m_Setting.linkMode.Equals("1"))
            {
                oldpostWord = PromptCN1 + string.Format("�����������ľ������ģ������������������(��߹涨)\n����{0}�ش�", m_VITS_Player.getLan()) + m_ChatScript.old_msg;
            }
        }
        else if (m_ChatScript.initializing_Hypnosis_StatementsIsJPN)
        {
            oldpostWord = PromptJP + string.Format("��{0}�Ƿ��¤��Ƥ���������", m_ChatScript.getChineseToJapaneseLan(m_VITS_Player.getLan())) + m_ChatScript.old_msg;
        }

        StartCoroutine(GetPostData(oldpostWord, m_Setting.getApikey(), _callback));
    }

    #region ���ݰ�

    [Serializable]public class PostData
    {
        public string model;
        public List<SendData> messages;
    }

    [Serializable]
    public class SendData
    {
        public string role;
        public string content;
        public SendData() { }
        public SendData(string _role,string _content) {
            role = _role;
            content = _content;
        }

    }
    [Serializable]
    public class MessageBack
    {
        public string id;
        public string created;
        public string model;
        public List<MessageBody> choices;
    }
    [Serializable]
    public class MessageBody
    {
        public Message message;
        public string finish_reason;
        public string index;
    }
    [Serializable]
    public class Message
    {
        public string role;
        public string content;
    }

    #endregion


}

