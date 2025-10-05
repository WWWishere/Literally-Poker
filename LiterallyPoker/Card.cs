using System;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem.Collections.Generic;
using Il2CppTMPro;
using MelonLoader;
using UnityEngine;
using UnityEngine.UI;

namespace LiterallyPoker;

public class Card
{
    public int num;
    public ECardSuit suit;
    public Card(int num, ECardSuit suit)
    {
        this.num = num;
        this.suit = suit;
    }
}

public enum ECardSuit
{
    Spade = 1,
    Heart = 2,
    Club = 3,
    Diamond = 4
}

[RegisterTypeInIl2Cpp]
public class Hand : MonoBehaviour
{
    public Card card1;
    public Card card2;
    public int quality;
    public int val1 = 0;
    public int val2 = 0;
    public int val3 = 0;
    public int val4 = 0;
    public int val5 = 0;
    public TextMeshProUGUI betText;
    public int betSize = 0;
    public void setHand(Card card1, Card card2)
    {
        this.card1 = card1;
        this.card2 = card2;
    }
    public int getHandValue()
    {
        return this.quality * 100000000 + this.val1 * 1000000 + this.val2 * 10000 + this.val3 * 100 + this.val4;
    }
    public void setObject(GameObject obj)
    {
        betText = obj.AddComponent<TextMeshProUGUI>();
    }
    public Hand() : base(ClassInjector.DerivedConstructorPointer<Hand>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public Hand(IntPtr ptr) : base(ptr)
    {

    }
}

[RegisterTypeInIl2Cpp]
public class PrCard : MonoBehaviour
{
    public Card card;
    public TextMeshProUGUI cardNum;
    public Image cardBg;
    public bool flipped = false;
    public void setTextObject(GameObject obj)
    {
        cardNum = obj.AddComponent<TextMeshProUGUI>();
        cardNum.text = string.Format("<color=#000000>{0}</color>", PrSave.cardString(card.num));
    }
    public void setBgObject(GameObject obj)
    {
        cardBg = obj.GetComponent<Image>();
    }
    public void flip()
    {
        this.flipped = true;
    }
    public void unflip()
    {
        this.flipped = false;
        int suitVal = (int)card.suit;
        displayCard(suitVal);
    }
    public void initCard(int newNum, ECardSuit newSuit)
    {
        card.num = newNum;
        card.suit = newSuit;
        int suitVal = (int)newSuit;
        displayCard(suitVal);
    }
    public void displayCard(int suitVal)
    {
        if (flipped)
        {
            cardNum.text = "";
            cardBg.sprite = PrSave.cardSprites[4];
        }
        else
        {
            if (suitVal % 2 == 0)
            {
                cardNum.text = string.Format("<color=#A00000>{0}</color>", PrSave.cardString(card.num));
            }
            else
            {
                cardNum.text = string.Format("<color=#000000>{0}</color>", PrSave.cardString(card.num));
            }
            if (PrSave.cardSprites.Count >= suitVal)
            {
                Sprite newSprite = PrSave.cardSprites[suitVal - 1];
                cardBg.sprite = newSprite;
            }
        }
    }
    public PrCard() : base(ClassInjector.DerivedConstructorPointer<PrCard>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
        card = new Card(0, ECardSuit.Spade);
    }
    public PrCard(IntPtr ptr) : base(ptr)
    {
        card = new Card(0, ECardSuit.Spade);
    }
}