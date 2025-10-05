using System;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using MelonLoader;
using Il2Cpp;
using Il2CppSystem.Collections.Generic;
using UnityEngine;

namespace LiterallyPoker;

[RegisterTypeInIl2Cpp]
public class EmpathPR : BettingRole
{
    public override ActedInfo bcq(Character charRef)
    {
        List<Character> characters = Gameplay.CurrentCharacters;
        List<Character> list1 = CharactersHelper.tl(characters, charRef);
        Character neighbor1 = list1[1];
        Character neighbor2 = list1[list1.Count - 1];
        Card n1Card = this.getHand(neighbor1).card2;
        Card n2Card = this.getHand(neighbor2).card2;
        int sum = n1Card.num + n2Card.num;
        string line = string.Format("Total of adjacent cards is {0}", sum);
        List<Character> targets = new List<Character>();
        targets.Add(neighbor1);
        targets.Add(neighbor2);
        return new ActedInfo(line, targets);
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
        List<Character> list1 = CharactersHelper.tl(characters, charRef);
        Character neighbor1 = list1[1];
        Character neighbor2 = list1[list1.Count - 1];
        Card n1Card = this.getHand(neighbor1).card2;
        Card n2Card = this.getHand(neighbor2).card2;
        int notN2Card = Calculator.tf(n2Card.num, 1, 14);
        int sum = n1Card.num + notN2Card;
        string line = string.Format("Total of adjacent cards is {0}", sum);
        List<Character> targets = new List<Character>();
        targets.Add(neighbor1);
        targets.Add(neighbor2);
        return new ActedInfo(line, targets);
    }
    public override void bcx(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Day)
        {
            this.onActed.Invoke(this.bcr(charRef));
        }
    }
    public EmpathPR() : base(ClassInjector.DerivedConstructorPointer<EmpathPR>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public EmpathPR(IntPtr ptr) : base(ptr)
    {

    }
}