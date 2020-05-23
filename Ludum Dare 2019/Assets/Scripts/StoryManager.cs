using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.ChatSystem;
using System;

[RequireComponent(typeof(ChatSystem))]
public class StoryManager : MonoBehaviour
{
    private ChatSystem chatSystem;


    private void Awake()
    {
        chatSystem = GetComponent<ChatSystem>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayIntro()
    {
        Conversation conversation = new Conversation();
        conversation.ConversationFinished += GetCallback("");
    }

    public EventHandler GetCallback(string name)
    {
        throw new NotImplementedException();
    }
}
