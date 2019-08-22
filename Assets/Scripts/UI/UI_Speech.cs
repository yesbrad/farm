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

    private AudioSource source;
    private bool showingMessage;
    private Action onMessageFinished;

	private void Start()
	{
        instance = this;
        source = GetComponent<AudioSource>();
        messageContainer.SetActive  (false);
	}

    public void StartMessages ()
    {
        if (!showingMessage)
            StartCoroutine(ShowMessage());
    }

	private void Update()
	{
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(PlayerDetection.instance.currentInterractable != null && PlayerDetection.instance.CurrentCrop == null)
            {
                if(!UI_Speech.instance.showingMessage)
                    PlayerDetection.instance.currentInterractable.Interact();
            }
        }
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
                //if(finishedMessage.Count < 1)
                    //NextNode();

                //if(finishedMessage.Count == 1)
                    //first = false;
                   
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
        if(onMessageFinished != null)
        {
            onMessageFinished.Invoke();
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

    public void AddMessage (string _message , Action _onFinished)
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

        onMessageFinished = null;

        onMessageFinished += _onFinished;

        StartMessages();
    }
}
