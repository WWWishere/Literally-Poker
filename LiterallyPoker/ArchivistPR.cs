using System;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using MelonLoader;
using Il2Cpp;
using Il2CppSystem.Collections.Generic;

namespace LiterallyPoker;

[RegisterTypeInIl2Cpp]
public class ArchivistPR : BettingRole
{
    public override ActedInfo bcq(Character charRef)
    {
        List<Character> characters = Gameplay.CurrentCharacters;
        List<Character> pickChars = new List<Character>();
        foreach (Character character in characters)
        {
            if (character.id != charRef.id)
            {
                pickChars.Add(character);
            }
        }
        Character randomCharacter = pickChars[UnityEngine.Random.RandomRangeInt(0, pickChars.Count)];
        Hand chHand = this.getHand(randomCharacter);
        Card randomHidden = chHand.card2;
        string line = string.Format("#{0} has {1}", randomCharacter.id, PrSave.cardString(randomHidden.num));
        List<Character> target = new List<Character>();
        target.Add(randomCharacter);
        return new ActedInfo(line, target);
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
        List<Character> pickChars = new List<Character>();
        foreach (Character character in characters)
        {
            if (character.id != charRef.id)
            {
                pickChars.Add(character);
            }
        }
        Character randomCharacter = pickChars[UnityEngine.Random.RandomRangeInt(0, pickChars.Count)];
        Hand hand = getHand(randomCharacter);
        int num = Calculator.tf(hand.card2.num, 0, 14);
        string line = string.Format("#{0} has {1}", randomCharacter.id, PrSave.cardString(num));
        List<Character> target = new List<Character>();
        target.Add(randomCharacter);
        return new ActedInfo(line, target);
    }
    public override void bcx(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Day)
        {
            this.onActed.Invoke(this.bcr(charRef));
        }
    }
    public ArchivistPR() : base(ClassInjector.DerivedConstructorPointer<ArchivistPR>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public ArchivistPR(IntPtr ptr) : base(ptr)
    {

    }
}