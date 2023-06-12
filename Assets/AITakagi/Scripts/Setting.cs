using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    //���õ�Api
    [SerializeField] private InputField m_InputAPI;
    //�����Զ���URL
    [SerializeField] private InputField m_InputOtherURL;
    //xx�ֺ��Զ�ϴ��
    [SerializeField] private InputField m_InputNumberOfConversations;
    //ϴ��ģʽ����
    [SerializeField] private GameObject m_BrainwashingButtonONIsChecked;
    [SerializeField] private GameObject m_BrainwashingButtonONIsNoChecked;
    [SerializeField] private GameObject m_BrainwashingButtonOFFIsChecked;
    [SerializeField] private GameObject m_BrainwashingButtonOFFIsNoChecked;
    //�����ļ���ť
    [SerializeField] private GameObject m_SaveButton;
    private string apiKey;
    private string apiKey1 = "��д����ģʽ��key";

    /// <summary>
    /// api��ַ������棩
    /// </summary>
    public string OtherUrl = "";

    //��xx�ֶԻ�ʱ���Զ�ϴ��
    public int AutoBrainwashingNum = 3;
    //�Զ�ϴ��ģʽ�Ƿ���
    public int AutoBrainwashing = 1;

    private string configFile = "";

    public string linkMode = "0";


    private void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            configFile = Application.persistentDataPath + "/Config.txt";
            if (!File.Exists(configFile))
            {
                WWW loadWWW = new WWW(configFile);
                while (!loadWWW.isDone)
                {

                }
                File.WriteAllBytes(configFile, loadWWW.bytes);
                File.WriteAllText(configFile,
                    "APIKey=" + "" +
                    "\nAutoBrainwashing=" + "1" +
                    "\nAutoBrainwashingNum=" + "3" +
                    "\nOtherAPIUrl=" + "https://api.openai-proxy.com/v1/chat/completions");
            }
            GetConfig();
        }
        else
        {
            configFile = Application.dataPath + "\\config.txt";
#if !UNITY_EDITOR
        configFile = System.Environment.CurrentDirectory + "/config.txt";
#endif

            GetConfig();
        }

        //string m = MachineCode.GetMachineCodeString();
        //Debug.Log(m);
        
    }

    void Update()
    {

    }
    private void GetConfig()
    {

        if (File.Exists(configFile))
        {
            string[] strs = File.ReadAllLines(configFile);
            if (strs.Length < 1)
                return;
            for (int i = 0; i < strs.Length; i++)
            {
                strs[i] = strs[i].Replace(" ", "");
            }
            try
            {
                apiKey = strs[0].Replace("APIKey=", "");
                if (apiKey.Length == 51)
                {
                    m_InputAPI.text = "������APIKey������˿�ɸ����µ�APIKey";
                }
                else
                {
                    m_InputAPI.text = "���APIKey���ܲ���ȷ���������APIKey����ʹ�ð���ģʽ";
                }
                
                AutoBrainwashing = int.Parse(strs[1].Replace("AutoBrainwashing=", ""));
                //���ÿ��ش��ߵİ�ť״̬
                setAutoBrainwashingSW();
                AutoBrainwashingNum = getStringToInt(strs[2].Replace("AutoBrainwashingNum=", ""));
                m_InputNumberOfConversations.text = getIntToString(AutoBrainwashingNum);
                OtherUrl = strs[3].Replace("OtherAPIUrl=", "");
                m_InputOtherURL.text = OtherUrl;
                //Debug.Log(apiKey);
            }
            catch (System.Exception)
            {
                Debug.Log("��ȡ�����ļ�����");
                SaveConfigFile();
            }

        }
    }
    //����ֱ��ģʽ��APIKey
    public void setApikey()
    {
        if (!m_InputAPI.text.Equals("������APIKey������˿�ɸ����µ�APIKey"))
        {
            if (m_InputAPI.text.Length == 51)
            {
                apiKey = m_InputAPI.text;
                //SaveConfigFile();
                m_SaveButton.SetActive(true);
                Debug.Log("Apikey���óɹ�������");
            }
            else
            {
                m_InputAPI.text = "���APIKey���ܲ���ȷ���������APIKey����ʹ�ð���ģʽ";
                Debug.Log("Apikey����Ϊ�գ�����");
            }
        }
    }
    //��������ģʽΪֱ��
    public void setLinkModeIsOpenAI()
    {
        linkMode = "0";
    }
    //��������ģʽΪ����
    public void setLinkModeIsPublic()
    {
        linkMode = "1";
    }

    //���ÿ����Զ�����
    public void setAutoBrainwashingIsOn()
    {
        AutoBrainwashing = 1;
        m_SaveButton.SetActive(true);
        //SaveConfigFile();
    }
    //���ùر��Զ�����
    public void setAutoBrainwashingIsOff()
    {
        AutoBrainwashing = 0;
        m_SaveButton.SetActive(true);
        //SaveConfigFile();
    }
    //���ÿ��ش��ߵİ�ť״̬
    private void setAutoBrainwashingSW()
    {
        if (AutoBrainwashing == 1)
        {
            m_BrainwashingButtonONIsChecked.SetActive(true);
            m_BrainwashingButtonONIsNoChecked.SetActive(false);
            m_BrainwashingButtonOFFIsChecked.SetActive(false);
            m_BrainwashingButtonOFFIsNoChecked.SetActive(true);
        }
        else
        {
            m_BrainwashingButtonONIsChecked.SetActive(false);
            m_BrainwashingButtonONIsNoChecked.SetActive(true);
            m_BrainwashingButtonOFFIsChecked.SetActive(true);
            m_BrainwashingButtonOFFIsNoChecked.SetActive(false);
        }
    }
    //����xx�ֺ��Զ�����
    public void setAutoBrainwashingNum()
    {
        try
        {
            AutoBrainwashingNum = int.Parse(m_InputNumberOfConversations.text);
            //SaveConfigFile();
            m_SaveButton.SetActive(true);
            Debug.Log("���������ɹ�����ǰ���õ�����Ϊ��"+ AutoBrainwashingNum);
        }
        catch
        {
            AutoBrainwashingNum = 3;
            m_InputNumberOfConversations.text = "3";
            //SaveConfigFile();
            m_SaveButton.SetActive(true);
            Debug.Log("���������޷�ת�����������Զ�����Ϊ3��");
        }
        
    }

    //�����Զ�������
    public void setOhterUrl()
    {
        if (!m_InputOtherURL.text.Equals(""))
        {
            OtherUrl = m_InputOtherURL.text;
            m_SaveButton.SetActive(true);
            //SaveConfigFile();
            Debug.Log("Api�������óɹ�������");
        }
        else
        {
            Debug.Log("Api���Ӳ���Ϊ�գ�����");
        }


    }
    
    public string getApikey()
    {
        if (linkMode.Equals("0"))
        {
            Debug.Log("key��ֱ��ģʽAPI");
            return apiKey;
        }
        else
        {

            Debug.Log("key�ǰ���ģʽAPI");
            return apiKey1;
        }
    }
    private int getStringToInt(string num)
    {
        try
        {
            int a = int.Parse(num);
            return a;
        }
        catch
        {
            return 0;
        }
    }

    private string getIntToString(int num)
    {
        try
        {
            string a = num.ToString();
            return a;
        }
        catch
        {
            return "";
        }
    }
    //����config�ļ�
    public void SaveConfigFile()
    {
        File.WriteAllText(configFile, 
            "APIKey=" + apiKey + 
            "\nAutoBrainwashing=" + AutoBrainwashing + 
            "\nAutoBrainwashingNum=" + m_InputNumberOfConversations.text + 
            "\nOtherAPIUrl=" + m_InputOtherURL.text);
        GetConfig();
    }

}
