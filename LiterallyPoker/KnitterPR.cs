using System;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using MelonLoader;
using Il2Cpp;
using Il2CppSystem.Collections.Generic;

namespace LiterallyPoker;

[RegisterTypeInIl2Cpp]
public class KnitterPR : BettingRole
{
    public override ActedInfo bcq(Character charRef)
    {
        int suited = 0;
        List<Character> characters = Gameplay.CurrentCharacters;
        foreach (Character ch in characters)
        {
            Hand hand = this.getHand(ch);
            if (hand.card1.suit == hand.card2.suit)
            {
                suited++;
            }
        }
        string line = string.Format("There are {0} suited hands.", suited);
        if (suited == 1)
        {
            line = "There is only 1 suited hand.";
        }
        return new ActedInfo(line);
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
        int suited = 0;
        List<Character> characters = Gameplay.CurrentCharacters;
        foreach (Character ch in characters)
        {
            Hand hand = this.getHand(ch);
            if (hand.card1.suit == hand.card2.suit)
            {
                suited++;
            }
        }
        int notSuited = Calculator.tf(suited, 0, (characters.Count + 1) / 2);
        string line = string.Format("There are {0} suited hands.", notSuited);
        if (notSuited == 1)
        {
            line = "There is only 1 suited hand.";
        }
        return new ActedInfo(line);
    }
    public override void bcx(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Day)
        {
            this.onActed.Invoke(this.bcr(charRef));
        }
    }
    public KnitterPR() : base(ClassInjector.DerivedConstructorPointer<KnitterPR>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public KnitterPR(IntPtr ptr) : base(ptr)
    {

    }
}