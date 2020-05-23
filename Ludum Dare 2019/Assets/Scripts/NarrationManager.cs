using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.ChatSystem.Depreciated;

public class NarrationManager : MonoBehaviour
{
    private ChatInterface chatInterface;
    private PlayerController player;

    public Text Instructions;

    private static System.Random random = new System.Random();

    [Range(0, 1)]
    public double ConversationChanceOnPickup = 0.25f;

    public bool IsNarrating
    {
        get
        {
            return chatInterface.InConversation;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        chatInterface = FindObjectOfType<ChatInterface>();
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayConversation(Conversations conversation, Action callback = null)
    {
        switch (conversation)
        {
            case Conversations.Introduction:
                Introduction(callback);
                break;
            case Conversations.FirstLight:
                FirstLight(callback);
                break;
            case Conversations.LiftBriefing:
                LiftBriefing(callback);
                break;
            case Conversations.FirstPowerCell:
                FirstPowerCell(callback);
                break;
            case Conversations.ThirdLightIncorrect:
                ThirdLightIncorrect(callback);
                break;
            //case Conversations.PowerBelowThreshold:
            //    PowerBelowThreshold(callback);
            //    break;
            case Conversations.FiveRoomsLit:
                FiveRoomsLit(callback);
                break;
            case Conversations.PitZoneEntry:
                PitZoneEntry(callback);
                break;
            case Conversations.Falling:
                Falling(callback);
                break;
            case Conversations.Respawn:
                Respawn(callback);
                break;
            case Conversations.RespawnAlt:
                RespawnAlt(callback);
                break;
            case Conversations.RuinedKey:
                RuinedKey(callback);
                break;
            case Conversations.WallCodeIntro:
                WallCodeIntro(callback);
                break;
            case Conversations.FirstBitNoDarkness:
                FirstBitNoDarkness(callback);
                break;
            case Conversations.NoPowerZone:
                NoPowerZone(callback);
                break;
            case Conversations.DarkKey:
                DarkKey(callback);
                break;
            case Conversations.ArboretumIntro:
                ArboretumIntro(callback);
                break;
            case Conversations.Mushrooms:
                Mushrooms(callback);
                break;
            case Conversations.FallenTree:
                FallenTree(callback);
                break;
            case Conversations.GreenhouseKey:
                GreenhouseKey(callback);
                break;
            case Conversations.OfficeKey:
                OfficeKey(callback);
                break;
            case Conversations.AllKeysObtained:
                AllKeysObtained(callback);
                break;
            case Conversations.BrokenTV:
                BrokenTV(callback);
                break;
            case Conversations.WorkingScreen:
                WorkingScreen(callback);
                break;
            case Conversations.BlueWire:
                BlueWire(callback);
                break;
            case Conversations.Blood:
                Blood(callback);
                break;
            case Conversations.EmptyRoom:
                EmptyRoom(callback);
                break;
            case Conversations.EmptyRoom2:
                EmptyRoom2(callback);
                break;
            case Conversations.Notepad:
                Notepad(callback);
                break;
            case Conversations.Skidmarks:
                Skidmarks(callback);
                break;
            case Conversations.LiftFinale:
                LiftFinale(callback);
                break;
            //case Conversations.FoundPower:
            //    FoundPower(callback);
            //    break;
            case Conversations.DarkNoPower:
                DarkNoPower(callback);
                break;
            case Conversations.NotEnoughPower:
                NotEnoughPower(callback);
                break;
        }
    }

    #region Conversations

    private void Introduction(Action callback)
    {
        chatInterface.PushMessage("...", Character.FacilityFloor);
        chatInterface.PushMessage("It sure is dark in here. Can you do anything about it from up there?", Character.FacilityFloor);
        chatInterface.PushMessage("There's just enough juice left to route power to your sector. One second...", Character.ControlRoom, callback: callback);
    }

    private void FirstLight(Action callback)
    {
        chatInterface.PushMessage("...", Character.ControlRoom);
        chatInterface.PushMessage("Much better!", Character.FacilityFloor);
        chatInterface.PushMessage("Looks like I overestimated how much power it would take! We should have enough for a few more, at least.", Character.ControlRoom, 2, callback: callback);
    }

    private void LiftBriefing(Action callback)
    {
        chatInterface.PushMessage("This lift... doesn’t look like it's active.", Character.FacilityFloor);
        chatInterface.PushMessage("But that's our ticket to the next floor!", Character.ControlRoom);
        chatInterface.PushMessage("The panel has four slots. Maybe it needs four keys to activate it?", Character.FacilityFloor);
        chatInterface.PushMessage("I'll have a root around - see what I can find.", Character.FacilityFloor);
        chatInterface.PushMessage("Good luck, mother! I'll do my best to light your way!", Character.ControlRoom, callback: callback);
    }

    private void FirstPowerCell(Action callback)
    {
        chatInterface.PushMessage("Looks like this is a power cell. We should be able to route power to a few more sectors now.", Character.FacilityFloor);
        chatInterface.PushMessage("On it!", Character.ControlRoom, callback: callback);
    }

    private void ThirdLightIncorrect(Action callback)
    {
        chatInterface.PushMessage("...?", Character.FacilityFloor);
        chatInterface.PushMessage("Sorry, that was the wrong one...", Character.ControlRoom, callback: callback);
    }

    private void PowerBelowThreshold(Action callback)
    {
        chatInterface.PushMessage("Running low on power up here - see if you can find any more cells!", Character.ControlRoom);
        chatInterface.PushMessage("On it.", Character.FacilityFloor, callback: callback);
    }
       
    private void FiveRoomsLit(Action callback)
    {
        chatInterface.PushMessage("Careful - looks like the more rooms we have lit, the more power we're going to lose over time.", Character.ControlRoom);
        chatInterface.PushMessage("Turn off the power in any rooms we don't need anymore. I trust your judgment.", Character.FacilityFloor);
        chatInterface.PushMessage("I won't let you down!", Character.ControlRoom, callback: callback);
    }

    private void PitZoneEntry(Action callback)
    {
        chatInterface.PushMessage("Looks like the ground's missing in some of these sectors. Hard to tell how deep it goes.", Character.FacilityFloor);
        chatInterface.PushMessage("Tread lightly, mother.", Character.ControlRoom, callback: callback);
    }

    private void Falling(Action callback)
    {
        chatInterface.PushMessage("Mother?! MOTHEEEEEEEEEEEER!!", Character.ControlRoom, callback: callback);
    }

    private void Respawn(Action callback)
    {
        chatInterface.PushMessage("I'm back.", Character.FacilityFloor);
        chatInterface.PushMessage("Oh, thank goodness! We lost a lot of power while you were climbing out, but we should still be okay.", Character.ControlRoom, callback: callback);
    }

    private void RespawnAlt(Action callback)
    {
        switch (random.NextDouble())
        {
            case double roll when roll > 0.5:
                chatInterface.PushMessage("That was a close one...", Character.FacilityFloor);
                chatInterface.PushMessage("Please be more careful, mother!", Character.ControlRoom, callback: callback);
                break;
            default:
                chatInterface.PushMessage("I'm okay.", Character.FacilityFloor);
                chatInterface.PushMessage("Please stop scaring me like that!", Character.ControlRoom, callback: callback);
                break;
        }
    }

    private void RuinedKey(Action callback)
    {
        chatInterface.PushMessage("A key.", Character.FacilityFloor);
        chatInterface.PushMessage("Don't drop it on your way out!", Character.ControlRoom, callback: callback);
    }

    private void WallCodeIntro(Action callback)
    {
        chatInterface.PushMessage("Hey - what does the note say?", Character.ControlRoom);
        chatInterface.PushMessage("'PSA: FREQUENT POWER FAILURES AHEAD. IF YOU FIND YOURSELF IN THE DARK, JUST REMEMBER: WSWN AND REPEAT'", Character.FacilityFloor);
        chatInterface.PushMessage("'WSWN AND REPEAT'... that's not very memorable...", Character.ControlRoom);
        chatInterface.PushMessage("OH! Could those be compass directions, perhaps?", Character.ControlRoom);
        chatInterface.PushMessage("Indeed! I wonder how you know when to switch up the direction though...", Character.FacilityFloor, callback: callback);
    }

    private void FirstBitNoDarkness(Action callback)
    {
        chatInterface.PushMessage("That marking on the ground - I wonder if it's related to that note...", Character.FacilityFloor);
    }

    private void NoPowerZone(Action callback)
    {
        chatInterface.PushMessage("Any chance of some light?", Character.FacilityFloor);
        chatInterface.PushMessage("I'm trying!! Doesn't look like I can route power to any of these sectors...", Character.ControlRoom);
        chatInterface.PushMessage("I guess all we have to go on is 'WSWN', then... lovely.", Character.FacilityFloor);
        chatInterface.PushMessage("Be careful! We don't know what's ahead...", Character.ControlRoom, callback: callback);
    }

    private void DarkKey(Action callback)
    {
        chatInterface.PushMessage("There was a key in the dark. Now to re-trace my steps...", Character.FacilityFloor);
        chatInterface.PushMessage("The pattern was 'WSWN', so you'll need to go SOUTH first, right?", Character.ControlRoom);
        chatInterface.PushMessage("That's right.", Character.FacilityFloor, callback: callback);
    }


    private void ArboretumIntro(Action callback)
    {
        chatInterface.PushMessage("Whoa, look at all this!", Character.ControlRoom);
        chatInterface.PushMessage("Exotic flora all around. We don't have a lot of these back home.", Character.FacilityFloor);
        chatInterface.PushMessage("How can it take root on such forsaken soil?", Character.ControlRoom);
        chatInterface.PushMessage("Nature always finds a way.", Character.FacilityFloor, callback: callback);
    }

    private void Mushrooms(Action callback)
    {
        chatInterface.PushMessage("Mushrooms. Want me to bring you some back for dinner?", Character.FacilityFloor);
        chatInterface.PushMessage("*RETCHES*", Character.ControlRoom, callback: callback);
    }

    private void FallenTree(Action callback)
    {
        chatInterface.PushMessage("Is that a tree? Why's it leaking?", Character.ControlRoom);
        chatInterface.PushMessage("Even nature weeps...", Character.FacilityFloor, callback: callback);
    }

    private void GreenhouseKey(Action callback)
    {
        chatInterface.PushMessage("A key. I wonder why this one was left here...", Character.FacilityFloor);
        chatInterface.PushMessage("It's beautiful! I wish I could be down there with you...", Character.ControlRoom, callback: callback);
    }

    private void OfficeKey(Action callback)
    {
        chatInterface.PushMessage("A key! That one was easy.", Character.ControlRoom);
        chatInterface.PushMessage("Must've been in a real hurry to just leave it lying around...", Character.FacilityFloor, callback: callback);
    }

    private void AllKeysObtained(Action callback)
    {
        chatInterface.PushMessage("Okay, that's all of them. I'll head back to the elevator.", Character.FacilityFloor);
        chatInterface.PushMessage("I'll make sure the path is clear!", Character.ControlRoom, callback: callback);
    }

    private void BrokenTV(Action callback)
    {
        chatInterface.PushMessage("This place has seen better days, that's for sure.", Character.FacilityFloor, callback: callback);
    }

    private void WorkingScreen(Action callback)
    {
        chatInterface.PushMessage("Hey, that screen's working! But it's just blank...", Character.ControlRoom, callback: callback);
    }

    private void BlueWire(Action callback)
    {
        chatInterface.PushMessage("I wonder where that wire leads...", Character.ControlRoom, callback: callback);
    }

    private void Blood(Action callback)
    {
        chatInterface.PushMessage("It's dry. Must be an old wound.", Character.FacilityFloor);
        chatInterface.PushMessage("No corpse, though.", Character.FacilityFloor);
        chatInterface.PushMessage("*SHUDDERS*", Character.ControlRoom, callback: callback);
    }

    private void EmptyRoom(Action callback)
    {
        chatInterface.PushMessage("Hey... why do you think they did it?", Character.ControlRoom);
        chatInterface.PushMessage("Fear? Desparation? We can only guess.", Character.FacilityFloor);
        chatInterface.PushMessage("All for nothing...", Character.ControlRoom);
        chatInterface.PushMessage("We must learn from their mistakes. That's why we're here - to understand.", Character.FacilityFloor, callback: callback);
    }

    private void EmptyRoom2(Action callback)
    {
        chatInterface.PushMessage("I'd never do what they did...", Character.ControlRoom);
        chatInterface.PushMessage("Actions can have unintended consequences.", Character.FacilityFloor, callback: callback);
    }

    private void Notepad(Action callback)
    {
        chatInterface.PushMessage("A notepad? What does it say?", Character.ControlRoom);
        chatInterface.PushMessage("'LEAVE THEM BE'...", Character.FacilityFloor);
        chatInterface.PushMessage("*SHIVERS*", Character.ControlRoom, callback: callback);
    }

    private void Skidmarks(Action callback)
    {
        chatInterface.PushMessage("Those skidmarks... yikes.", Character.ControlRoom, callback:callback);
    }

    private void LiftFinale(Action callback)
    {
        chatInterface.PushMessage("It's working!", Character.ControlRoom);
        chatInterface.PushMessage("You did a great job up there! I'll be counting on you again, I'm sure.", Character.FacilityFloor);
        chatInterface.PushMessage("Here's hoping the next floor has some answers...", Character.ControlRoom, callback: callback);
    }

    private void DarkNoPower(Action callback)
    {
        chatInterface.PushMessage("We're all out of power!", Character.ControlRoom);
        chatInterface.PushMessage("I'll stumble around and see what I can find...", Character.FacilityFloor);
        chatInterface.PushMessage("Wait, what's that sou---", Character.FacilityFloor);
        chatInterface.PushMessage("What's what?", Character.ControlRoom, 0.5f);
        chatInterface.PushMessage("Mother? What's what?", Character.ControlRoom, 0.5f);
        chatInterface.PushMessage("...", Character.ControlRoom, 0.5f);
        chatInterface.PushMessage("MOTHER???", Character.ControlRoom, callback: callback);
    }

    private void NotEnoughPower(Action callback)
    {
        chatInterface.PushMessage("We don't have enough power to do that right now!", Character.ControlRoom, callback: callback);
    }

    private void FoundPower(Action callback)
    {
        if (random.NextDouble() <= ConversationChanceOnPickup)
        {
            switch (random.NextDouble())
            {
                case double roll when roll > 0.75:
                    chatInterface.PushMessage("I've found another power cell.", Character.FacilityFloor, callback: callback);
                    break;
                case double roll when roll > 0.5:
                    chatInterface.PushMessage("Hey, we have more power to work with now!", Character.ControlRoom, callback: callback);
                    break;
                case double roll when roll > 0.25:
                    chatInterface.PushMessage("This should keep us going a bit longer.", Character.FacilityFloor, callback: callback);
                    break;
                default:
                    chatInterface.PushMessage("Power cell acquired.", Character.FacilityFloor, callback: callback);
                    break;
            }
        }
    }

    #endregion
}

public enum Conversations
{
    Introduction,
    FirstLight,
    LiftBriefing,
    FirstPowerCell,
    ThirdLightIncorrect,
    PowerBelowThreshold,
    FiveRoomsLit,
    PitZoneEntry,
    Falling,
    Respawn,
    RespawnAlt,
    RuinedKey,
    WallCodeIntro,
    FirstBitNoDarkness,
    NoPowerZone,
    DarkKey,
    ArboretumIntro,
    Mushrooms,
    FallenTree,
    GreenhouseKey,
    OfficeKey,
    AllKeysObtained,
    BrokenTV,
    WorkingScreen,
    BlueWire,
    Blood,
    EmptyRoom,
    EmptyRoom2,
    Notepad,
    Skidmarks,
    LiftFinale,
    FoundPower,
    DarkNoPower,
    NotEnoughPower
}
