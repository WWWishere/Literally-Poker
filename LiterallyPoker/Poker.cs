using Il2Cpp;
using MelonLoader;
using UnityEngine;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppTMPro;
using System;
using System.Collections.Generic;

namespace LiterallyPoker;

[RegisterTypeInIl2Cpp]
public class Poker : AdvancedMode
{
    public int score;
    public int currentLevel;
    public new int bestScore;
    public int bestTable = 0;
    public Il2CppSystem.Action action;
    public Il2CppSystem.Action action2;
    public Il2CppSystem.Action<Character> action3;
    public Il2CppSystem.Action action4;
    public Il2CppSystem.Action action5;
    public Il2CppSystem.Action<Character> defaultOnCharKilled;
    public List<Card> boardCards = new List<Card>();
    public GameObject objPlayerHand = new GameObject();
    public GameObject[] boardPrCards = new GameObject[5];
    public Stack<Card> deck = new Stack<Card>();
    public List<int> targets = new List<int>();
    public WinConditions winConditions;
    public GameObject objEvils;
    public List<Card> dvCards = new List<Card>();
    // Deck view should include all poker cards that are in play.
    public override EGameMode bha()
    {
        return EGameMode.Standard;
    }
    public override void bhb()
    {
        defaultOnCharKilled = new Action<Character>(empty);
        defaultOnCharKilled += GameplayEvents.OnCharacterKilled;
        GameplayEvents.OnRoundWon += action;
        GameplayEvents.OnDied += action2;
        GameplayEvents.OnCharacterKilled = action3;
        UIEvents.OnUIUpdate += action4;
        // GameplayEvents.OnGameStart += action5;
        // GameplayEvents.OnStartNewLevel += action5;
        GameplayEvents.OnDeckShuffled += action5;

        GameData.CurrentVillage = this.currentLevel;
    }

    public override GameMode bhc()
    {
        return PrSave.poker;
    }

    public override void bhe()
    {
        GameplayEvents.OnRoundWon -= action;
        GameplayEvents.OnDied -= action2;
        GameplayEvents.OnCharacterKilled = defaultOnCharKilled;
        UIEvents.OnUIUpdate -= action4;
        // GameplayEvents.OnGameStart -= action5;
        // GameplayEvents.OnStartNewLevel -= action5;
        GameplayEvents.OnDeckShuffled -= action5;
    }

    public override int bhf()
    {
        return this.currentLevel;
    }
    public override int bhg()
    {
        return this.currentLevel;
    }

    public override void bhl()
    {
        this.bhe();
        score = 0;
        currentLevel = 1;
        PrSave.poker = this;

        UIEvents.OnUIUpdate.Invoke();
    }

    public override void bhm()
    {
        getBonusPoints();
        UIEvents.OnUIUpdate.Invoke();
        PrSave.poker = this;
    }

    public override void bhn(int score, int level)
    {
        if (this.currentLevel < level)
        {
            this.currentLevel = level;
            if (level > bestTable)
            {
                bestTable = level;
            }
        }
    }

    public override int bho()
    {
        return this.score;
    }

    public override int bhq()
    {
        return this.currentLevel;
    }

    public override bool bhr()
    {
        return false;
    }

    public override string bhs()
    {
        int bestScore = this.bestScore;
        int bestTable = this.bestTable;
        string str1 = string.Format("<size=24>Highest Score: <color=green>{0} </color>\nHighest Table: <color=green>{1} </color></size>", bestScore, bestTable);
        return str1;
    }

    public override string bht()
    {
        int currentScore = this.score;
        string result = string.Format("<color=grey><size=20>Score: </color><color=green><size=24>{0}</color>", currentScore);
        return result;
    }

    public void bhx()
    {
        score = 0;
        currentLevel = 1;
        GameData.CurrentVillage = 0;

        PrSave.poker = this;
    }

    public bool bhy(int mod = 0)
    {
        int currentLevel = this.currentLevel;
        return currentLevel <= mod + 5;
    }

    public void editUI()
    {
        GameObject objScore3 = PrSave.objScore3;
        GameObject objTable = PrSave.objTable;
        if (objScore3 == null)
        {
            return;
        }
        TMP_Text textScore = objScore3.transform.FindChild("Bg/Text (TMP)").gameObject.GetComponent<TMP_Text>();
        TMP_Text textVillage = objTable.transform.FindChild("Bg/Text (TMP)").gameObject.GetComponent<TMP_Text>();
        if (textScore != null)
        {
            textScore.text = this.bht();
            textVillage.text = string.Format("<color=grey><size=20>Current Table: </color><color=white><size=24>{0}</color>", this.currentLevel);
        }
        TMP_Text textEvils = objEvils.GetComponent<TMP_Text>();
        if (boardCards.Count < 5)
        {
            textEvils.text = string.Format("<color=#a0b1ff><size=22>Cards dealt: <size=24>{0}</size> / 5</size></color>", boardCards.Count);
        }
        else
        {
            textEvils.text = string.Format("<color=#a0b1ff><size=20>Winning Hands Left: <size=24>{0}</size></size></color>", targets.Count);
        }
    }
    public void getBonusPoints()
    {
        Il2CppSystem.Collections.Generic.List<Character> characters = Gameplay.CurrentCharacters;
        Hand playerHand = objPlayerHand.GetComponent<Hand>();
        int playerHandVal = playerHand.getHandValue();
        foreach (Character ch in characters)
        {
            Hand hand = getChHand(ch);
            if (ch.state != ECharacterState.Dead && hand.getHandValue() < playerHandVal)
            {
                addScore(10 * hand.betSize);
            }
        }
    }
    private void addScore(int amt)
    {
        score += amt;
        if (score > bestScore)
        {
            bestScore = score;
        }
    }

    public override AscensionsData bhh()
    {
        if (this.score > 2500)
        {
            return PrSave.pokerData8;
        }
        else if (this.score > 1500)
        {
            return PrSave.pokerData4;
        }
        else if (this.score > 1000)
        {
            return PrSave.pokerData3;
        }
        else if (this.score > 500)
        {
            return PrSave.pokerData2;
        }
        return PrSave.pokerData;
    }

    public override AscensionsData bhi()
    {
        if (this.score > 2500)
        {
            return PrSave.pokerData8;
        }
        else if (this.score > 1500)
        {
            return PrSave.pokerData4;
        }
        else if (this.score > 1000)
        {
            return PrSave.pokerData3;
        }
        else if (this.score > 500)
        {
            return PrSave.pokerData2;
        }
        return PrSave.pokerData;
    }

    public override bool bhj()
    {
        return true;
    }

    public override int bhp()
    {
        int savedMaxStandardAscension = SavesGame.SavedMaxStandardAscension;
        return savedMaxStandardAscension;
    }

    public void bia(Character ch)
    {
        if (targets.Contains(ch.id))
        {
            targets.Remove(ch.id);
        }
        Health health = PlayerController.PlayerInfo.health;
        int penalty = 2 * (5 - boardCards.Count);
        health.jl(penalty);
        Hand playerHand = objPlayerHand.GetComponent<Hand>();
        int playerHandVal = playerHand.getHandValue();
        Hand chHand = getChHand(ch);
        if (chHand.getHandValue() > playerHandVal)
        {
            addScore(chHand.betSize * 10);
        }
        else if (chHand.getHandValue() < playerHandVal)
        {
            health.jl(chHand.betSize);
        }
        if (health.value.jw() <= 0)
        {
            winConditions.sa();
        }
        else if (boardCards.Count >= 5 && targets.Count == 0)
        {
            winConditions.ry();
        }
        UIEvents.OnUIUpdate.Invoke();
    }

    public void DealCard()
    {
        if (boardCards.Count >= 5 || deck.Count == 0)
        {
            return;
        }
        boardCards.Add(deck.Pop());
        int cardPos = 1;
        foreach (GameObject boardPrCard in boardPrCards)
        {
            boardPrCard.SetActive(false);
            if (cardPos <= boardCards.Count)
            {
                boardPrCard.SetActive(true);
                Card card = boardCards[cardPos - 1];
                PrCard prCard = boardPrCard.GetComponent<PrCard>();
                prCard.initCard(card.num, card.suit);
            }
            cardPos++;
        }
    }
    public void DealCards(int amount)
    {
        if (boardCards.Count >= 5 || deck.Count == 0)
        {
            return;
        }
        for (int i = 0; i < amount; i++)
        {
            DealCard();
        }
        targets.Clear();
        Hand playerHand = objPlayerHand.GetComponent<Hand>();
        PrSave.checkHand(boardCards, playerHand);
        int playerHandValue = playerHand.getHandValue();
        Il2CppSystem.Collections.Generic.List<Character> characters = Gameplay.CurrentCharacters;
        foreach (Character bettingChar in characters)
        {
            Hand hand = getChHand(bettingChar);
            PrSave.checkHand(boardCards, hand);
            if (hand.getHandValue() > playerHandValue && bettingChar.state != ECharacterState.Dead)
            {
                targets.Add(bettingChar.id);
            }
            if (bettingChar.dataRef.role is BettingRole)
            {
                BettingRole bettingRole = (BettingRole)bettingChar.dataRef.role;
                int newBet = bettingRole.betChips(bettingChar);
                hand.betSize += newBet;
                hand.betText.text = string.Format("<color=yellow>{0}</color>", hand.betSize);
            }
        }
        if (boardCards.Count == 5 && targets.Count == 0)
        {
            winConditions.ry();
        }
        UIEvents.OnUIUpdate.Invoke();
    }

    public void startPoker()
    {
        MelonLogger.Msg("Poker Start!");
        boardCards.Clear();
        dvCards.Clear();
        deck = getShuffledCards();
        AscensionsData ascensionsData = this.bhh();
        ScriptInfo script = ascensionsData.currentPickedScript;
        CharactersCount count1 = script.characterCounts[0];
        int circleSize = count1.allCharCount;
        GameObject circle = getCircleSize(circleSize);
        for (int i = 0; i < circleSize; i++)
        {
            GameObject ch = circle.transform.GetChild(i).gameObject;
            Character character = ch.GetComponent<Character>();
            Card card1 = deck.Pop();
            Card card2 = deck.Pop();
            SetChCard(character, 1, card1);
            SetChCard(character, 2, card2, true);
            dvCards.Add(card2);
            GameObject prHand = ch.transform.FindChild("Hand_PrMode").gameObject;
            Hand hand = prHand.GetComponent<Hand>();
            hand.betSize = 2;
            hand.betText.text = string.Format("<color=yellow>{0}</color>", hand.betSize);
        }
        SetPlCard(1, deck.Pop());
        SetPlCard(2, deck.Pop());
        foreach (GameObject boardPrCard in boardPrCards)
        {
            boardPrCard.SetActive(false);
        }
        foreach (PrCard pr in PrSave.pokerDeckView)
        {
            pr.gameObject.SetActive(false);
        }
        for (int i = 0; i < dvCards.Count; i++)
        {
            Card c = dvCards[i];
            PrCard prCard = PrSave.pokerDeckView[i];
            prCard.initCard(c.num, c.suit);
            prCard.gameObject.SetActive(true);    
        }
        UIEvents.OnUIUpdate.Invoke();
    }
    public List<Card> getAllCardsList()
    {
        List<Card> list = new List<Card>();
        for (int i = 1; i <= 4; i++)
        {
            ECardSuit suit = (ECardSuit)i;
            for (int j = 1; j <= 13; j++)
            {
                list.Add(new Card(j, suit));
            }
        }
        return list;
    }
    public Hand getChHand(Character ch)
    {
        GameObject gameObject = ch.gameObject.transform.FindChild("Hand_PrMode").gameObject;
        Hand hand = gameObject.GetComponent<Hand>();
        return hand;
    }
    public void empty(Character ch)
    {

    }
    public System.Collections.Generic.Stack<Card> getShuffledCards()
    {
        List<Card> list = getAllCardsList();
        List<Card> list2 = new List<Card>();
        foreach (Card card in list)
        {
            list2.Add(card);
        }
        System.Collections.Generic.Stack<Card> stack = new System.Collections.Generic.Stack<Card>();
        int size = list2.Count;
        for (int i = 0; i < size; i++)
        {
            int randIndex = UnityEngine.Random.RandomRangeInt(0, list2.Count);
            Card randomCard = list2[randIndex];
            list2.RemoveAt(randIndex);
            stack.Push(randomCard);
        }
        return stack;
    }
    public void SetChCard(Character charRef, int index, Card card, bool flip = false)
    {
        GameObject gameObject = charRef.gameObject.transform.FindChild("Hand_PrMode").gameObject;
        GameObject cardObject = gameObject.transform.GetChild(index - 1).gameObject;
        PrCard prCard = cardObject.GetComponent<PrCard>();
        if (flip)
        {
            prCard.flip();
        }
        prCard.initCard(card.num, card.suit);
    }
    public void SetPlCard(int index, Card card)
    {
        GameObject handCard1 = objPlayerHand.transform.GetChild(index - 1).gameObject;
        PrCard prCard = handCard1.GetComponent<PrCard>();
        prCard.initCard(card.num, card.suit);
    }
    public GameObject getCircleSize(int size)
    {
        if (size == 8)
        {
            return PrSave.circle_8;
        }
        return PrSave.circle_5;
    }
    public Poker() : base(ClassInjector.DerivedConstructorPointer<Poker>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
        action = new Action(bhm);
        action2 = new Action(bhx);
        action3 = new Action<Character>(bia);
        action4 = new Action(editUI);
        action5 = new Action(startPoker);
        defaultOnCharKilled = new Action<Character>(empty);
        defaultOnCharKilled += GameplayEvents.OnCharacterKilled;
        GameObject winCon = GameObject.Find("Game/Gameplay/Content/WinConditions");
        winConditions = winCon.GetComponent<WinConditions>();
        objEvils = GameObject.Find("Game/Gameplay/Content/Canvas/UI/Objectives_Left/Objective (7)/Bg/Text (TMP)");
    }
    public Poker(IntPtr ptr) : base(ptr)
    {
        action = new Action(bhm);
        action2 = new Action(bhx);
        action3 = new Action<Character>(bia);
        action4 = new Action(editUI);
        action5 = new Action(startPoker);
        defaultOnCharKilled = new Action<Character>(empty);
        defaultOnCharKilled += GameplayEvents.OnCharacterKilled;
        GameObject winCon = GameObject.Find("Game/Gameplay/Content/WinConditions");
        winConditions = winCon.GetComponent<WinConditions>();
        objEvils = GameObject.Find("Game/Gameplay/Content/Canvas/UI/Objectives_Left/Objective (7)/Bg/Text (TMP)");
    }
}