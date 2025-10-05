using System;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using MelonLoader;
using Il2Cpp;
using Il2CppSystem.Collections.Generic;

namespace LiterallyPoker;

[RegisterTypeInIl2Cpp]
public class TrackerPR : BettingRole
{
    public string[] suits = new string[] { "♠","♥","♣","♦" };
    public override ActedInfo bcq(Character charRef)
    {
        List<Character> characters = Gameplay.CurrentCharacters;
        int maxDist = characters.Count / 2;
        int randomDist = UnityEngine.Random.RandomRangeInt(0, maxDist) + 1;
        List<Character> order = CharactersHelper.tl(characters, charRef);
        List<Character> list1 = new List<Character>();
        Character ch1 = order[randomDist];
        Character ch2 = order[order.Count - randomDist];
        list1.Add(ch1);
        if (ch2.id != ch1.id)
        {
            list1.Add(ch2);
        }
        Character randomChar = list1[UnityEngine.Random.RandomRangeInt(0, list1.Count)];
        Card rcHidden = getHand(randomChar).card2;
        int suitVal = (int)rcHidden.suit - 1;
        string line = string.Format("I am {0} cards away from {1}{2}", randomDist, PrSave.cardString(rcHidden.num), suits[suitVal]);
        if (randomDist == 1)
        {
            line = string.Format("I am 1 card away from {0}{1}", PrSave.cardString(rcHidden.num), suits[suitVal]);
        }
        return new ActedInfo(line, list1);
    }
    public override void bcs(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Day)
        {
            this.onActed.Invoke(this.bcq(charRef));
        }
    }
    public override ActedInfo bcr(Character charRef)
    {
        List<Character> characters = Gameplay.CurrentCharacters;
        System.Collections.Generic.List<Card> cards = new System.Collections.Generic.List<Card>();
        foreach (Character ch in characters)
        {
            Hand hand = getHand(ch);
            cards.Add(hand.card2);
        }
        int maxDist = characters.Count / 2;
        int randomDist = UnityEngine.Random.RandomRangeInt(0, maxDist) + 1;
        List<Character> order = CharactersHelper.tl(characters, charRef);
        List<Character> list1 = new List<Character>();
        Character ch1 = order[randomDist];
        Character ch2 = order[order.Count - randomDist];
        Hand hand1 = getHand(ch1);
        Hand hand2 = getHand(ch2);
        cards.Remove(hand1.card2);
        cards.Remove(hand2.card2);
        Card notRcHidden = cards[UnityEngine.Random.RandomRangeInt(0, cards.Count)];
        int suitVal = (int)notRcHidden.suit - 1;
        string line = string.Format("I am {0} cards away from {1}{2}", randomDist, PrSave.cardString(notRcHidden.num), suits[suitVal]);
        if (randomDist == 1)
        {
            line = string.Format("I am 1 card away from {0}{1}", PrSave.cardString(notRcHidden.num), suits[suitVal]);
        }
        List<Character> targets = new List<Character>();
        targets.Add(ch1);
        targets.Add(ch2);
        return new ActedInfo(line, targets);
    }
    public override void bcx(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Day)
        {
            this.onActed.Invoke(this.bcr(charRef));
        }
    }
    public TrackerPR() : base(ClassInjector.DerivedConstructorPointer<TrackerPR>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public TrackerPR(IntPtr ptr) : base(ptr)
    {

    }
}