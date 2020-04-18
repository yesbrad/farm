using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[System.Serializable]
public class Message
{
    public List<string> messages = new List<string>();
}

public class UI_Speech : MonoBehaviour
{
    public static UI_Speech instance;
    [SerializeField] GameObject messageContainer;
    [SerializeField] Text messageText;
    [SerializeField] float typeSpeed = 0.1f;
    [SerializeField] AudioClip messageAudio;
    public List<string> currentMessages = new List<string>();

    [Header("Yes No Container")]
    public GameObject yesNoContainer;
    public GameObject yesIndicator;
    public GameObject noIndicator;
    public Button noButton;
    public bool isYesSelected;

    private AudioSource source;
    private bool showingMessage;
    private List<Action> onMessageFinished = new List<Action>();

    public bool ShowingMessage
    {
        get { return showingMessage; }
    }

    private void Start()
	{
        instance = this;
        source = GetComponent<AudioSource>();
        messageContainer.SetActive  (false);
        EnableYesOrNo(false);
	}

    public void StartMessages ()
    {
        if (!showingMessage)
            StartCoroutine(ShowMessage());
    }

	private void Update()
	{

	}

    bool waitNode;

	IEnumerator ShowMessage ()
    {
        showingMessage = true;

        messageContainer.SetActive(true);

        int curMessageIndex = -1;

        bool first = false;

        while(true)
        {
            if(Input.GetKeyDown(KeyCode.Space) || !first)
            {
				first = true;

                curMessageIndex++;
								
                if(curMessageIndex > currentMessages.Count - 1)
                {
                    showingMessage = false;
                    break;
                }

				UpdateMessage(currentMessages[curMessageIndex]);

                waitNode = true;
            }

            yield return null; 
        }

        showingMessage = false;
        messageContainer.SetActive(false);
        currentMessages.Clear();

        //Debug.Log("NEXT NODE");

        NextNode();
        waitNode = false;
        first = false;

        yield return null;
    }

    private void UpdateMessage (string _message)
    {
        if (prevText != null)
            StopCoroutine(prevText);

        prevText = TypeText(_message);
        StartCoroutine(prevText);
    }

    IEnumerator prevText;

    IEnumerator TypeText (string _message)
    {
        char[] charList = _message.ToCharArray();
        int index = 0;
        string newMessage = "";

        while(true)
        {
            yield return new WaitForSeconds(typeSpeed);

            if (index > charList.Length - 1)
                break;

            newMessage += charList[index];
            UpdateText(newMessage);
            index++;

            yield return null;
        }

        yield return null;
    }

    bool onLastNode;

    private void NextNode ()
    {
        if(onMessageFinished.Count > 0)
        {
            if(yesNoContainer.activeSelf)
                yesNoContainer.SetActive(false);

            if (isYesSelected && onMessageFinished.Count > 1)
            {
                onMessageFinished[1].Invoke();
            } 
            else
            {
                onMessageFinished[0].Invoke();
            }
        }
        else
        {
            Debug.Log("Oh shit onMessageFinished is null");
        }
    }

    private void UpdateText (string _message)
    {
        source.pitch = UnityEngine.Random.Range(0.98f, 1.02f);
        source.Play();
        messageText.text = _message;
    }

    string[] splitMessage;

    public void AddMessage (string _message, Action _onFinished)
    {
        List<Action> actions = new List<Action>();
        actions.Add(_onFinished);
        AddMessage(_message, actions);
    }

    public void AddMessage (string _message , List<Action> _onFinished)
    {
        Message a = new Message();

        splitMessage = _message.Split('\n');

        for (int m = 0; m < splitMessage.Length; m++)
        {
            if(!string.IsNullOrEmpty(splitMessage[m]))
                a.messages.Add(splitMessage[m]);
        }

        for (int i = 0; i < a.messages.Count; i++)
        {
            currentMessages.Add(a.messages[i]);
        }

        onMessageFinished.Clear();

        onMessageFinished = _onFinished;

        if(_onFinished.Count > 1)
        {
            EnableYesOrNo(true);
        }

        StartMessages();
    }

    public void EnableYesOrNo (bool toggle)
    {
        yesNoContainer.SetActive(toggle);
        UIManager.instance.SetCurrentButton(noButton.gameObject);
        SelectYesOrNo(false);
    }

    public void SelectYesOrNo(bool isYes)
    {
        isYesSelected = isYes;
        noIndicator.SetActive(isYes);
        yesIndicator.SetActive(!isYes);
    }
}
