using System;
using MelonLoader;
using LiterallyPoker;
using Il2CppInterop.Runtime.Injection;
using UnityEngine;
using Il2Cpp;
using Il2CppSystem.Collections.Generic;
using Il2CppInterop.Runtime.Runtime.VersionSpecific.Type;
using Il2CppInterop.Runtime;
using System.Runtime.CompilerServices;
using HarmonyLib;
using Il2CppTMPro;
using UnityEngine.UI;
using UnityEngine.Scripting;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using System.IO;
using System.Reflection;

[assembly: MelonInfo(typeof(PrModMain), "LiterallyPoker", "0.1", "SS122")]
[assembly: MelonGame("UmiArt", "Demon Bluff")]
namespace LiterallyPoker;

public class PrModMain : MelonMod
{
    System.Collections.Generic.List<CharacterData> allDefaultDatas = new System.Collections.Generic.List<CharacterData>();
    public override void OnInitializeMelon()
    {
        ClassInjector.RegisterTypeInIl2Cpp<Poker>();
        ClassInjector.RegisterTypeInIl2Cpp<BettingRole>();
        ClassInjector.RegisterTypeInIl2Cpp<Hand>();
        ClassInjector.RegisterTypeInIl2Cpp<PrCard>();

        ClassInjector.RegisterTypeInIl2Cpp<ConfessorPR>();
        ClassInjector.RegisterTypeInIl2Cpp<ArchivistPR>();
        ClassInjector.RegisterTypeInIl2Cpp<EmpathPR>();
        ClassInjector.RegisterTypeInIl2Cpp<BishopPR>();
        ClassInjector.RegisterTypeInIl2Cpp<ShugenjaPR>();
    }
    public override void OnLateInitializeMelon()
    {
        GameObject leftUI = GameObject.Find("Game/Gameplay/Content/Canvas/UI/Objectives_Left");
        PrSave.objScore3 = GameObject.Instantiate(leftUI.transform.FindChild("Objective (13)").gameObject);
        PrSave.objTable = GameObject.Instantiate(leftUI.transform.FindChild("Objective (14) A").gameObject);
        PrSave.objScore3.name = "Objectives (13) Pok";
        PrSave.objTable.name = "Objectives (14) Pok";
        PrSave.objScore3.transform.SetParent(leftUI.transform);
        PrSave.objTable.transform.SetParent(leftUI.transform);
        PrSave.objScore3.GetComponent<EnableOnMode>().mode = PrSave.poker;
        PrSave.objTable.GetComponent<EnableOnMode>().mode = PrSave.poker;
        PrSave.objTable.GetComponent<EnableOnMode>().mode2 = null;
        PrSave.objScore3.transform.localScale = new UnityEngine.Vector3(1f, 1f, 1f);
        PrSave.objTable.transform.localScale = new UnityEngine.Vector3(1f, 1f, 1f);
        PrSave.objScore3.SetActive(true);
        PrSave.objTable.SetActive(true);
        AscensionsData allCharactersAscension = ProjectContext.Instance.gameData.allCharactersAscension;
        foreach (CharacterData data in allCharactersAscension.startingTownsfolks)
        {
            allDefaultDatas.Add(data);
        }
        foreach (CharacterData data in allCharactersAscension.startingOutsiders)
        {
            allDefaultDatas.Add(data);
        }
        foreach (CharacterData data in allCharactersAscension.startingMinions)
        {
            allDefaultDatas.Add(data);
        }
        foreach (CharacterData data in allCharactersAscension.startingDemons)
        {
            allDefaultDatas.Add(data);
        }
        // Define roles here.
        CharacterData confessorPR = copyCharacterData("Confessor", new ConfessorPR());
        confessorPR.description = "Learn the number of my hidden Card.";
        confessorPR.hints = "";
        PrSave.pokerCD.Add(confessorPR);
        CharacterData gemcrafterPR = copyCharacterData("Gemcrafter", new ArchivistPR());
        gemcrafterPR.description = "Learn a hidden Card.";
        PrSave.pokerCD.Add(gemcrafterPR);
        CharacterData loverPR = copyCharacterData("Lover", new EmpathPR());
        loverPR.description = "Learn the total of adjacent hidden cards.";
        PrSave.pokerCD.Add(loverPR);
        CharacterData bishopPR = copyCharacterData("Bishop", new BishopPR());
        bishopPR.description = "Learn 3 Characters.\nLearn the 3 hidden cards that the characters hold.";
        bishopPR.ifLies = "Learn 3 cards not held by the Characters.";
        PrSave.pokerCD.Add(bishopPR);
        CharacterData enlightenedPR = copyCharacterData("Enlightened", new ShugenjaPR());
        enlightenedPR.description = "Learn the position of the highest card in play.";
        enlightenedPR.hints = "If I have the highest card, I will say equidistant.";
        PrSave.pokerCD.Add(enlightenedPR);
        CharacterData hunterPR = copyCharacterData("Hunter", new TrackerPR());
        hunterPR.description = "Learn how far I am from a specific hidden card.";
        PrSave.pokerCD.Add(hunterPR);
        CharacterData knitterPR = copyCharacterData("Knitter", new KnitterPR());
        knitterPR.description = "Learn how many Characters have suited cards.";
        PrSave.pokerCD.Add(knitterPR);
        CharacterData witnessPR = copyCharacterData("Witness", new WitnessPR());
        witnessPR.description = "Learn all of the Lying Characters.";
        witnessPR.ifLies = "Learn truthful Characters.";
        witnessPR.hints = "";
        PrSave.pokerCD.Add(witnessPR);
        CharacterData drunkPR = copyCharacterData("Drunk", new DrunkPR());
        drunkPR.description = "I Disguise as a random not in play Villager.\nI am Corrupted and I Lie.";
        drunkPR.hints = "I always bet high on the last card.";
        PrSave.pokerCD.Add(drunkPR);
        CharacterData poisonerPR = copyCharacterData("Poisoner", new PoisonerPR());
        poisonerPR.description = "<b>Game Start:</b>\nOne adjacent Villager is Corrupted (if possible).\n\nI Lie and I Disguise";
        PrSave.pokerCD.Add(poisonerPR);
        CharacterData pookaPR = copyCharacterData("Pooka", new PookaPR());
        pookaPR.description = "<b>Game Start:</b>\nVillagers adjacent to me are Corrupted (if possible).\n\nI Lie and I Disguise";
        PrSave.pokerCD.Add(pookaPR);
        System.Collections.Generic.List<CharacterData> listData1 = new System.Collections.Generic.List<CharacterData>()
        {
            confessorPR, gemcrafterPR, loverPR, bishopPR, enlightenedPR
        };
        List<CharactersCount> countsData1 = new List<CharactersCount>();
        countsData1.Add(new CharactersCount(5, 5, 0, 0, 0));
        countsData1.Add(new CharactersCount(5, 5, 0, 0, 0));
        countsData1.Add(new CharactersCount(5, 5, 0, 0, 0));
        PrSave.setUpPokerData(PrSave.pokerData, listData1, countsData1);
        System.Collections.Generic.List<CharacterData> listData2 = new System.Collections.Generic.List<CharacterData>()
        {
            confessorPR, gemcrafterPR, loverPR, bishopPR, enlightenedPR, hunterPR, drunkPR
        };
        List<CharactersCount> countsData2 = new List<CharactersCount>();
        countsData2.Add(new CharactersCount(5, 4, 0, 1, 0));
        countsData2.Add(new CharactersCount(5, 4, 0, 1, 0));
        countsData2.Add(new CharactersCount(5, 5, 0, 0, 0));
        PrSave.setUpPokerData(PrSave.pokerData2, listData2, countsData2);
        System.Collections.Generic.List<CharacterData> listData3 = new System.Collections.Generic.List<CharacterData>()
        {
            confessorPR, gemcrafterPR, loverPR, bishopPR, enlightenedPR, hunterPR, drunkPR,
            witnessPR, poisonerPR
        };
        List<CharactersCount> countsData3 = new List<CharactersCount>();
        countsData3.Add(new CharactersCount(5, 4, 0, 1, 0));
        countsData3.Add(new CharactersCount(5, 4, 0, 0, 1));
        countsData3.Add(new CharactersCount(5, 5, 0, 0, 0));
        PrSave.setUpPokerData(PrSave.pokerData3, listData3, countsData3);
        System.Collections.Generic.List<CharacterData> listData4 = new System.Collections.Generic.List<CharacterData>()
        {
            confessorPR, gemcrafterPR, loverPR, bishopPR, enlightenedPR, hunterPR, drunkPR,
            witnessPR, poisonerPR, knitterPR, pookaPR
        };
        List<CharactersCount> countsData4 = new List<CharactersCount>();
        countsData4.Add(new CharactersCount(5, 4, 1, 0, 0));
        countsData4.Add(new CharactersCount(5, 4, 0, 1, 0));
        countsData4.Add(new CharactersCount(5, 4, 0, 0, 1));
        PrSave.setUpPokerData(PrSave.pokerData4, listData4, countsData4);
        List<CharactersCount> countsData8 = new List<CharactersCount>();
        countsData8.Add(new CharactersCount(8, 5, 1, 1, 1));
        countsData8.Add(new CharactersCount(8, 6, 1, 1, 0));
        countsData8.Add(new CharactersCount(8, 6, 0, 1, 1));
        countsData8.Add(new CharactersCount(8, 7, 1, 0, 0));
        countsData8.Add(new CharactersCount(8, 7, 0, 1, 0));
        countsData8.Add(new CharactersCount(8, 7, 0, 0, 1));
        countsData8.Add(new CharactersCount(8, 8, 0, 0, 0));
        PrSave.setUpPokerData(PrSave.pokerData8, listData4, countsData8);

        System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
        Sprite spade = PrSave.getPngSprite(assembly, "CardSpade.png");
        Sprite heart = PrSave.getPngSprite(assembly, "CardHeart.png");
        Sprite club = PrSave.getPngSprite(assembly, "CardClub.png");
        Sprite diamond = PrSave.getPngSprite(assembly, "CardDiamond.png");
        Sprite back = PrSave.getPngSprite(assembly, "CardBack.png");
        spade.name = "Spade";
        heart.name = "Heart";
        club.name = "Club";
        diamond.name = "Diamond";
        back.name = "Card Back";
        PrSave.cardSprites.Add(spade);
        PrSave.cardSprites.Add(heart);
        PrSave.cardSprites.Add(club);
        PrSave.cardSprites.Add(diamond);
        PrSave.cardSprites.Add(back);
        GameObject circles = GameObject.Find("Game/Gameplay/Content/Canvas/Characters");
        GameObject circle_5 = circles.transform.FindChild("Circle_5").gameObject;
        addCircleCards(circle_5, 5);
        PrSave.circle_5 = circle_5;
        GameObject circle_8 = circles.transform.FindChild("Circle_8").gameObject;
        addCircleCards(circle_8, 8);
        PrSave.circle_8 = circle_8;
        GameObject playerHand = PrSave.poker.objPlayerHand;
        playerHand.name = "Player_Hand";
        EnableOnMode enabler = playerHand.AddComponent<EnableOnMode>();
        enabler.mode = PrSave.poker;
        GameObject gameUI = GameObject.Find("Game/Gameplay/Content/Canvas/UI");
        playerHand.transform.SetParent(gameUI.transform);
        playerHand.transform.localScale = new Vector3(1f, 1f, 1f);
        Hand playHand = playerHand.AddComponent<Hand>();
        playHand.setObject(playerHand);
        GameObject playCard1 = new GameObject("Card1");
        playCard1.transform.SetParent(playerHand.transform);
        GameObject playCard1BG = new GameObject("Bg");
        playCard1BG.transform.SetParent(playCard1.transform);
        GameObject playCard1Text = new GameObject("Text (TMP)");
        playCard1Text.transform.SetParent(playCard1.transform);
        playCard1.transform.localScale = new Vector3(1f, 1f, 1f);
        playCard1Text.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        playCard1BG.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        PrCard playCard1PR = playCard1.AddComponent<PrCard>();
        playCard1PR.setTextObject(playCard1Text);
        Image pc1Image = playCard1BG.AddComponent<Image>();
        pc1Image.sprite = PrSave.cardSprites[0];
        GameObject playCard2 = new GameObject("Card2");
        playCard2.transform.SetParent(playerHand.transform);
        GameObject playCard2BG = new GameObject("Bg");
        playCard2BG.transform.SetParent(playCard2.transform);
        GameObject playCard2Text = new GameObject("Text (TMP)");
        playCard2Text.transform.SetParent(playCard2.transform);
        playCard2.transform.localScale = new Vector3(1f, 1f, 1f);
        playCard2Text.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        playCard2BG.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        PrCard playCard2PR = playCard2.AddComponent<PrCard>();
        playCard2PR.setTextObject(playCard2Text);
        Image pc2Image = playCard2BG.AddComponent<Image>();
        pc2Image.sprite = PrSave.cardSprites[0];
        playCard1PR.setBgObject(playCard1BG);
        playCard2PR.setBgObject(playCard2BG);
        playCard1.transform.localPosition = new Vector3(0f, 0f, 0f);
        playCard2.transform.localPosition = new Vector3(46f, 0f, 0f);
        playCard1BG.transform.localPosition = new Vector3(-40f, -34f, 0f);
        playCard2BG.transform.localPosition = new Vector3(-40f, -34f, 0f);
        playHand.setHand(playCard1PR.card, playCard2PR.card);
        playerHand.transform.localPosition = new Vector3(-720f, -110f, 0f);
        GameObject cardBoard = new GameObject("Poker_Board");
        cardBoard.transform.SetParent(gameUI.transform);
        cardBoard.transform.localScale = new Vector3(1f, 1f, 1f);
        cardBoard.transform.localPosition = new Vector3(-65f, 140f, 0f);
        for (int i = 0; i < 5; i++)
        {
            GameObject newCard = PrSave.setBoardCard(i, cardBoard);
            if (i <= 2)
            {
                newCard.transform.localPosition = new Vector3(i * 120f, 0f, 0f);
            }
            else
            {
                newCard.transform.localPosition = new Vector3(i * 120f - 300f, -150f, 0f);
            }
            PrSave.poker.boardPrCards[i] = newCard;
        }
        GameObject deckView = GameObject.Find("Game/Gameplay/Content/DeckIntro/DeckView/Panel/Panel");
        GameObject pokerView = new GameObject("Poker_Cards");
        pokerView.transform.SetParent(deckView.transform);
        pokerView.transform.localScale = new Vector3(1f, 1f, 1f);
        EnableOnMode enablePoker = pokerView.AddComponent<EnableOnMode>();
        enablePoker.mode = PrSave.poker;
        for (int i = 0; i < 54; i++)
        {
            createPokerDV(i, pokerView, back);
        }

        GameObject menuScreen = GameObject.Find("Game/Menu/GameModes/Canvas (1)");
        GameObject cardSelect = GameObject.Find("Game/Menu/GameModes/Canvas (1)/ButtonsLayout");
        GameObject roguelikeCard = GameObject.Find("Game/Menu/GameModes/Canvas (1)/ButtonsLayout/Roguelike");
        GameObject pokerCard = UnityEngine.Object.Instantiate(roguelikeCard, cardSelect.transform);
        pokerCard.name = "Poker Card";
        GameObject pokerGameMode = pokerCard.transform.FindChild("GameModeCard1").gameObject;
        ChangeGameModeButton button = pokerGameMode.GetComponent<ChangeGameModeButton>();
        GameModeCard gameModeCard = pokerGameMode.GetComponent<GameModeCard>();
        button.mode = PrSave.poker;
        gameModeCard.gameMode = PrSave.poker;
        GameObject textTitle = pokerGameMode.transform.FindChild("Text").gameObject;
        GameObject textDesc = pokerGameMode.transform.FindChild("Text (2)").gameObject;
        textTitle.GetComponent<TextMeshProUGUI>().text = "Poker";
        textDesc.GetComponent<TextMeshProUGUI>().text = "Poker based mode.\nPress 'L' to deal cards.";
    }
    public override void OnUpdate()
    {
        if (PrSave.allData.Length == 0)
        {
            var loadedCharList = Resources.FindObjectsOfTypeAll(Il2CppType.Of<CharacterData>());
            if (loadedCharList != null)
            {
                PrSave.allData = new CharacterData[loadedCharList.Length];
                for (int i = 0; i < loadedCharList.Length; i++)
                {
                    PrSave.allData[i] = loadedCharList[i]!.Cast<CharacterData>();
                }
            }
            if (PrSave.allData.Length > 0)
            {

            }
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            GameData.bgk(PrSave.poker);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (GameData.GameMode is Poker)
            {
                Poker pokerMode = (Poker)GameData.GameMode;
                if (pokerMode.boardCards.Count == 0)
                {
                    pokerMode.DealCards(3);
                }
                else
                {
                    pokerMode.DealCards(1);
                }
            }
        }
    }
    public CharacterData copyCharacterData(string charName, BettingRole role)
    {
        CharacterData? originalData = null;
        foreach (CharacterData data in allDefaultDatas)
        {
            if (data.name == charName)
            {
                originalData = data;
                break;
            }
        }
        CharacterData newData = new CharacterData();
        if (originalData != null)
        {
            newData.name = "Betting " + charName;
            newData.art = originalData.art;
            newData.art_cute = originalData.art_cute;
            newData.art_nice = originalData.art_nice;
            newData.abilityUsage = originalData.abilityUsage;
            newData.backgroundArt = originalData.backgroundArt;
            newData.bundledCharacters = originalData.bundledCharacters;
            newData.canAppearIf = new Il2CppSystem.Collections.Generic.List<CharacterData>();
            newData.artBgColor = originalData.artBgColor;
            newData.cardBgColor = originalData.cardBgColor;
            newData.cardBorderColor = originalData.cardBorderColor;
            newData.color = originalData.color;
            newData.descriptionCHN = originalData.descriptionCHN;
            newData.descriptionPL = originalData.descriptionPL;
            newData.hints = originalData.hints;
            newData.ifLies = originalData.ifLies;
            newData.notes = originalData.notes;
            newData.picking = originalData.picking;
            newData.role = role;
            newData.skins = originalData.skins;
            newData.startingAlignment = EAlignment.Good;
            newData.type = originalData.type;
            newData.bluffable = originalData.bluffable;
            newData.flavorText = originalData.flavorText;
            newData.tags = originalData.tags;
            newData.characterId = charName + "_PR";
        }
        return newData;
    }
    public void fixStringData(CharacterData bData, string charName)
    {
        CharacterData? originalData = null;
        foreach (CharacterData data in allDefaultDatas)
        {
            if (data.name == charName)
            {
                originalData = data;
                break;
            }
        }
    }
    public void addCircleCards(GameObject circle, int characterCount)
    {
        for (int i = 0; i < characterCount; i++)
        {
            GameObject card = circle.transform.GetChild(i).gameObject;
            GameObject hand = new GameObject("Hand_PrMode");
            EnableOnMode enabler = hand.AddComponent<EnableOnMode>();
            enabler.mode = PrSave.poker;
            hand.transform.SetParent(card.transform);
            hand.transform.localScale = new Vector3(1f, 1f, 1f);
            Hand cardHand = hand.AddComponent<Hand>();
            cardHand.setObject(hand);
            hand.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
            hand.transform.localPosition = new Vector3(-65f, 405f, 0f);
            // Set the position of the hand
            int xdiff = 0;
            int ydiff = 1;
            GameObject handCard1 = new GameObject("Card1");
            handCard1.transform.SetParent(hand.transform);
            GameObject handCard1Img = new GameObject("Bg");
            handCard1Img.transform.SetParent(handCard1.transform);
            GameObject handCard1Text = new GameObject("Text (TMP)");
            handCard1Text.transform.SetParent(handCard1.transform);
            handCard1.transform.localScale = new Vector3(1f, 1f, 1f);
            handCard1Text.transform.localScale = new Vector3(1f, 1f, 1f);
            handCard1Img.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            PrCard handCard1PR = handCard1.AddComponent<PrCard>();
            handCard1PR.setTextObject(handCard1Text);
            Image hc1Image = handCard1Img.AddComponent<Image>();
            hc1Image.sprite = PrSave.cardSprites[0];


            GameObject handCard2 = new GameObject("Card2");
            handCard2.transform.SetParent(hand.transform);
            GameObject handCard2Img = new GameObject("Bg");
            handCard2Img.transform.SetParent(handCard2.transform);
            GameObject handCard2Text = new GameObject("Text (TMP)");
            handCard2Text.transform.SetParent(handCard2.transform);
            handCard2.transform.localScale = new Vector3(1f, 1f, 1f);
            handCard2Text.transform.localScale = new Vector3(1f, 1f, 1f);
            handCard2Img.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            PrCard handCard2PR = handCard2.AddComponent<PrCard>();
            handCard2PR.setTextObject(handCard2Text);
            Image hc2Image = handCard2Img.AddComponent<Image>();
            hc2Image.sprite = PrSave.cardSprites[0];
            handCard1PR.setBgObject(handCard1Img);
            handCard2PR.setBgObject(handCard2Img);
            handCard1.transform.localPosition = new Vector3(50f, 0f, 0f);
            handCard2.transform.localPosition = new Vector3(96f, 0f, 0f);
            handCard1Img.transform.localPosition = new Vector3(-50f, -34f, 0f);
            handCard2Img.transform.localPosition = new Vector3(-50f, -34f, 0f);
            cardHand.setHand(handCard1PR.card, handCard2PR.card);
        }
    }
    public void createPokerDV(int index, GameObject parent, Sprite backCard)
    {
        string dvCardName = "Card" + index;
        GameObject dvCard = createChild(parent, dvCardName);
        GameObject dvCardBg = createChild(dvCard, "Bg");
        Image dvCardImg = dvCardBg.AddComponent<Image>();
        dvCardBg.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        dvCardImg.sprite = backCard;
        GameObject dvCardText = createChild(dvCard, "Text (TMP)");
        PrCard prCard = dvCard.AddComponent<PrCard>();
        prCard.setBgObject(dvCardBg);
        prCard.setTextObject(dvCardText);
        dvCard.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        int in1 = index % 10;
        int in2 = (index - in1) / 10;
        dvCard.transform.localPosition = new Vector3(in1 * 50f - 910f, -70f * in2 - 100f, 0f);
        dvCardBg.transform.localPosition = new Vector3(-50f, -34f, 0f);
        PrSave.pokerDeckView.Add(prCard);
    }
    public GameObject createChild(GameObject parent, string name)
    {
        GameObject child = new GameObject(name);
        child.transform.SetParent(parent.transform);
        child.transform.localScale = new Vector3(1f, 1f, 1f);
        return child;
    }
}
public static class PrSave
{
    public static Poker poker = new Poker();
    public static GameObject objScore3;
    public static GameObject objTable;
    public static GameObject circle_5;
    public static GameObject circle_8;
    public static System.Collections.Generic.List<PrCard> pokerDeckView = new System.Collections.Generic.List<PrCard>();
    public static AscensionsData pokerData = GameObject.Instantiate(ProjectContext.Instance.gameData.advancedAscension);
    public static AscensionsData pokerData2 = GameObject.Instantiate(ProjectContext.Instance.gameData.advancedAscension);
    public static AscensionsData pokerData3 = GameObject.Instantiate(ProjectContext.Instance.gameData.advancedAscension);
    public static AscensionsData pokerData4 = GameObject.Instantiate(ProjectContext.Instance.gameData.advancedAscension);
    public static AscensionsData pokerData8 = GameObject.Instantiate(ProjectContext.Instance.gameData.advancedAscension);
    public static int[] indexesBigStraight = new int[] { 0, 9, 10, 11, 12 };
    public static CharacterData[] allData = Array.Empty<CharacterData>();
    // 0, 10, 20, 30, 40, or 50 bet
    public static int[] bettingChance1 = new int[] { 4, 2, 1, 0, 0, 0 };
    public static int[] bettingChance2 = new int[] { 1, 4, 2, 1, 0, 0 };
    public static int[] bettingChance3 = new int[] { 1, 2, 4, 2, 1, 0 };
    public static int[] bettingChance4 = new int[] { 0, 1, 2, 4, 2, 1 };
    public static int[] bettingChance5 = new int[] { 0, 0, 2, 4, 3, 1 };
    public static int[] bettingChance6 = new int[] { 0, 0, 1, 4, 4, 1 };
    public static int[] bettingChance7 = new int[] { 0, 0, 1, 3, 4, 2 };
    public static int[] bettingChance8 = new int[] { 0, 0, 0, 1, 3, 6 };
    public static int[] bettingChance9 = new int[] { 0, 0, 0, 0, 2, 8 };
    public static System.Collections.Generic.List<Sprite> cardSprites = new System.Collections.Generic.List<Sprite>();
    public static System.Collections.Generic.List<CharacterData> pokerCD = new System.Collections.Generic.List<CharacterData>();
    public static void setUpPokerData(AscensionsData ascensionsData, System.Collections.Generic.List<CharacterData> datas, List<CharactersCount> counts)
    {
        for (int i = 0; i < ascensionsData.possibleScriptsData.Length; i++)
        {
            CustomScriptData scriptData = new CustomScriptData();
            ScriptInfo script = new ScriptInfo();
            scriptData.scriptInfo = script;
            foreach (CharacterData bData in datas)
            {
                switch (bData.type)
                {
                    case ECharacterType.Villager:
                        script.startingTownsfolks.Add(bData);
                        break;
                    case ECharacterType.Outcast:
                        script.startingOutsiders.Add(bData);
                        break;
                    case ECharacterType.Minion:
                        script.startingMinions.Add(bData);
                        break;
                    case ECharacterType.Demon:
                        script.startingDemons.Add(bData);
                        break;
                }
            }
            script.characterCounts = counts;
            ascensionsData.possibleScriptsData[i] = scriptData;
        }
    }
    public static int[] bettingChances(int handQuality)
    {
        switch (handQuality)
        {
            case 2:
                return bettingChance2;
            case 3:
                return bettingChance3;
            case 4:
                return bettingChance4;
            case 5:
                return bettingChance5;
            case 6:
                return bettingChance6;
            case 7:
                return bettingChance7;
            case 8:
                return bettingChance8;
            case 9:
                return bettingChance9;
            default:
                return bettingChance1;
        }
    }
    public static int analyzeHand(System.Collections.Generic.List<Card> cards, Hand hand)
    {
        hand.val1 = 0;
        hand.val2 = 0;
        hand.val3 = 0;
        hand.val4 = 0;
        hand.val5 = 0;
        bool straight = false;
        bool flush = false;
        bool straightflush = false;
        int[] numCounts = new int[13];
        int[] suitsCount = new int[4];
        int[] singles = new int[] { 0, 0, 0, 0, 0 };
        int valPair = 0;
        int valPair2 = 0;
        int valTriple = 0;
        int valQuad = 0;
        int valStraight = 0;
        int valFlush = 0;
        int valSF = 0;
        int valQSingle = 0;
        foreach (Card card in cards)
        {
            numCounts[card.num - 1]++;
            int suitNum = (int)card.suit - 1;
            suitsCount[suitNum]++;
        }
        if (checkBigStraight(numCounts))
        {
            straight = true;
            valStraight = 14;
        }
        for (int i = 0; i < 9; i++)
        {
            if (checkStraight(i, numCounts))
            {
                straight = true;
                valStraight = i + 5;
            }
        }
        for (int j = 0; j < 4; j++)
        {
            int suitCount = suitsCount[j];
            int suitNum = j + 1;
            ECardSuit suit = (ECardSuit)suitNum;
            if (suitCount >= 5)
            {
                flush = true;
                foreach (Card card in cards)
                {
                    int num = card.num;
                    if (num == 1)
                    {
                        num = 14;
                    }
                    if (card.suit == suit && num > valFlush)
                    {
                        valFlush = num;
                    }
                }
                {
                    int sfVal = checkSF(cards, suit);
                    if (sfVal > 0)
                    {
                        valSF = sfVal;
                        straightflush = true;
                    }
                }
            }
        }
        for (int k = 0; k < numCounts.Length; k++)
        {
            int count = numCounts[k];
            int num = k + 1;
            if (num == 1)
            {
                num = 14;
            }
            if (count == 1)
            {
                for (int i = 0; i < singles.Length; i++)
                {
                    if (num > singles[i])
                    {
                        for (int j = singles.Length - 1; j > i; j--)
                        {
                            singles[j] = singles[j - 1];
                        }
                        singles[i] = num;
                        break;
                    }
                }
            }
            else if (count == 2)
            {
                if (num > valPair)
                {
                    valPair2 = valPair;
                    valPair = num;
                }
                else if (num > valPair2)
                {
                    valPair2 = num;
                }
            }
            else if (count == 3)
            {
                if (num > valTriple)
                {
                    int oldTriple = valTriple;
                    if (oldTriple > valPair)
                    {
                        valPair = oldTriple;
                    }
                    valTriple = num;
                }
                else if (num > valPair)
                {
                    valPair = num;
                }
            }
            else if (count == 4)
            {
                if (num > valQuad)
                {
                    valQuad = num;
                }
            }
            if (count >= 1 && num > valQSingle && num != valQuad)
            {
                valQSingle = num;
            }
        }
        if (straightflush)
        {
            hand.val1 = valSF;
            return 9;
        }
        else if (valQuad > 0)
        {
            hand.val1 = valQuad;
            hand.val2 = valQSingle;
            return 8;
        }
        else if (valTriple > 0 && valPair > 0)
        {
            hand.val1 = valTriple;
            hand.val2 = valPair;
            return 7;
        }
        else if (flush)
        {
            hand.val1 = valFlush;
            return 6;
        }
        else if (straight)
        {
            hand.val1 = valStraight;
            return 5;
        }
        else if (valTriple > 0)
        {
            hand.val1 = valTriple;
            hand.val2 = singles[0];
            hand.val3 = singles[1];
            return 4;
        }
        else if (valPair > 0 && valPair2 > 0)
        {
            hand.val1 = valPair;
            hand.val2 = valPair2;
            hand.val3 = singles[0];
            return 3;
        }
        else if (valPair > 0)
        {
            hand.val1 = valPair;
            hand.val2 = singles[0];
            hand.val3 = singles[1];
            hand.val4 = singles[2];
            return 2;
        }
        else
        {
            hand.val1 = singles[0];
            hand.val2 = singles[1];
            hand.val3 = singles[2];
            hand.val4 = singles[3];
            hand.val5 = singles[4];
            return 1;
        }
    }
    public static string getHandTypeName(int type)
    {
        switch (type)
        {
            case 9:
                return "Straight Flush";
            case 8:
                return "Four of a Kind";
            case 7:
                return "Full House";
            case 6:
                return "Flush";
            case 5:
                return "Straight";
            case 4:
                return "Three of a Kind";
            case 3:
                return "Two Pair";
            case 2:
                return "Pair";
            default:
                return "High Card";
        }
    }
    public static bool checkBigStraight(int[] numCounts)
    {
        foreach (int index in indexesBigStraight)
        {
            if (numCounts[index] < 1)
            {
                return false;
            }
        }
        return true;
    }
    public static bool checkStraight(int start, int[] numCounts)
    {
        for (int j = 0; j < 5; j++)
        {
            if (numCounts[start + j] < 1)
            {
                return false;
            }
        }
        return true;
    }
    public static int checkSF(System.Collections.Generic.List<Card> cards, ECardSuit suit)
    {
        bool[] hasSuitCards = new bool[14];
        foreach (Card card in cards)
        {
            if (card.suit == suit)
            {
                hasSuitCards[card.num - 1] = true;
                if (card.num == 1)
                {
                    hasSuitCards[13] = true;
                }
            }
        }
        for (int i = 0; i < 9; i++)
        {
            if (hasSuitCards[i] && hasSuitCards[i + 1] && hasSuitCards[i + 2] && hasSuitCards[i + 3] && hasSuitCards[i + 4])
            {
                return i + 5;
            }
        }
        return 0;
    }
    public static string cardString(int num)
    {
        if (num == 1)
        {
            return "A";
        }
        else if (num == 11)
        {
            return "J";
        }
        else if (num == 12)
        {
            return "Q";
        }
        else if (num == 13)
        {
            return "K";
        }
        else
        {
            return num.ToString();
        }
    }
    public static CharacterData? getPokerCD(string name)
    {
        foreach (CharacterData data in pokerCD)
        {
            if (data.name == name)
            {
                return data;
            }
        }
        MelonLogger.Msg("Couldn't find Betting Data of Character: " + name + "!");
        return null;
    }
    public static Sprite getPngSprite(Assembly assembly, string name)
    {
        using (Stream stream = assembly.GetManifestResourceStream(assembly.FullName.Split(",")[0] + "." + name) ?? assembly.GetManifestResourceStream(name))
        {
            byte[] data;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                data = memoryStream.ToArray();
            }
            Texture2D texture2D = new Texture2D(2, 2, TextureFormat.RGBA32, false);
            ImageConversion.LoadImage(texture2D, data);
            return Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
        }
    }
    public static GameObject setBoardCard(int index, GameObject parent)
    {
        string cardName = "BoardCard" + (index + 1);
        GameObject card = new GameObject(cardName);
        card.transform.SetParent(parent.transform);
        GameObject cardBG = new GameObject("Bg");
        cardBG.transform.SetParent(card.transform);
        Image cardImg = cardBG.AddComponent<Image>();
        cardImg.sprite = cardSprites[0];
        GameObject cardText = new GameObject("Text (TMP)");
        cardText.transform.SetParent(card.transform);
        card.transform.localScale = new Vector3(1f, 1f, 1f);
        EnableOnMode enabler = card.AddComponent<EnableOnMode>();
        enabler.mode = poker;
        PrCard prCard = card.AddComponent<PrCard>();
        cardBG.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        cardText.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        cardBG.transform.localPosition = new Vector3(-40f, -34f, 0f);
        prCard.setBgObject(cardBG);
        prCard.setTextObject(cardText);
        prCard.initCard(0, ECardSuit.Spade);
        return card;
    }
    public static void checkHand(System.Collections.Generic.List<Card> cards, Hand hand)
    {
        System.Collections.Generic.List<Card> cardsFull = new System.Collections.Generic.List<Card>();
        foreach (Card card in cards)
        {
            cardsFull.Add(card);
        }
        cardsFull.Add(hand.card1);
        cardsFull.Add(hand.card2);
        hand.quality = analyzeHand(cardsFull, hand);
    }
}

[HarmonyPatch(typeof(Character), nameof(Character.el))]
public static class BettingText1
{
    public static void Postfix(Character __instance)
    {
        TextMeshProUGUI charText = __instance.chName;
        string chText = charText.text;
        if (chText.StartsWith("BETTING "))
        {
            charText.text = chText.Substring(8);
        }
    }
}
[HarmonyPatch(typeof(Character), nameof(Character.em))]
public static class BettingText2
{
    public static void Postfix(Character __instance)
    {
        TextMeshProUGUI charText = __instance.chName;
        string chText = charText.text;
        if (chText.StartsWith("BETTING "))
        {
            charText.text = chText.Substring(8);
        }
    }
}
[HarmonyPatch(typeof(Character), nameof(Character.en))]
public static class BettingText3
{
    public static void Postfix(Character __instance)
    {
        TextMeshProUGUI charText = __instance.chName;
        string chText = charText.text;
        if (chText.StartsWith("BETTING "))
        {
            charText.text = chText.Substring(8);
        }
        if (GameData.GameMode is Poker)
        {
            GameObject gameObject = __instance.gameObject.transform.FindChild("Hand_PrMode").gameObject;
            GameObject cardObject = gameObject.transform.GetChild(1).gameObject;
            PrCard prCard = cardObject.GetComponent<PrCard>();
            prCard.unflip();
        }
    }
}
[HarmonyPatch(typeof(Character), nameof(Character.eo))]
public static class BettingText4
{
    public static void Postfix(Character __instance)
    {
        TextMeshProUGUI charText = __instance.chName;
        string chText = charText.text;
        if (chText.StartsWith("BETTING "))
        {
            charText.text = chText.Substring(8);
        }
    }
}
[HarmonyPatch(typeof(CharacterView), nameof(CharacterView.hw))]
public static class BettingText5
{
    public static void Postfix(CharacterView __instance, ref CharacterData data)
    {
        TextMeshProUGUI charText = __instance.chName;
        string chText = charText.text;
        if (chText.StartsWith("BETTING "))
        {
            charText.text = chText.Substring(8);
        }
    }
}