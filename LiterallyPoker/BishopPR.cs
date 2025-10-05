using System;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using MelonLoader;
using Il2Cpp;
using Il2CppSystem.Collections.Generic;

namespace LiterallyPoker;

[RegisterTypeInIl2Cpp]
public class BishopPR : BettingRole
{
    public override ActedInfo bcq(Character charRef)
    {
        List<Character> characters = Gameplay.CurrentCharacters;
        List<Character> charactersTemp = new List<Character>();
        foreach (Character character in characters)
        {
            if (character.id != charRef.id)
            {
                charactersTemp.Add(character);
            }
        }
        Character ch1 = charactersTemp[UnityEngine.Random.RandomRangeInt(0, charactersTemp.Count)];
        charactersTemp.Remove(ch1);
        Character ch2 = charactersTemp[UnityEngine.Random.RandomRangeInt(0, charactersTemp.Count)];
        charactersTemp.Remove(ch2);
        Character ch3 = charactersTemp[UnityEngine.Random.RandomRangeInt(0, charactersTemp.Count)];
        charactersTemp.Remove(ch3);
        List<int> hiddens = new List<int>();
        hiddens.Add(this.getHand(ch1).card2.num);
        hiddens.Add(this.getHand(ch2).card2.num);
        hiddens.Add(this.getHand(ch3).card2.num);
        int ran1 = hiddens[UnityEngine.Random.RandomRangeInt(0, hiddens.Count)];
        hiddens.Remove(ran1);
        int ran2 = hiddens[UnityEngine.Random.RandomRangeInt(0, hiddens.Count)];
        hiddens.Remove(ran2);
        int ran3 = hiddens[0];
        string line = string.Format("Between #{0}, #{1}, #{2}, there is {3}, {4} and {5}", ch1.id, ch2.id, ch3.id, PrSave.cardString(ran1), PrSave.cardString(ran2), PrSave.cardString(ran3));
        List<Character> targets = new List<Character>();
        targets.Add(ch1);
        targets.Add(ch2);
        targets.Add(ch3);
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
        List<Character> charactersTemp = new List<Character>();
        foreach (Character character in characters)
        {
            if (character.id != charRef.id)
            {
                charactersTemp.Add(character);
            }
        }
        Character ch1 = charactersTemp[UnityEngine.Random.RandomRangeInt(0, charactersTemp.Count)];
        charactersTemp.Remove(ch1);
        Character ch2 = charactersTemp[UnityEngine.Random.RandomRangeInt(0, charactersTemp.Count)];
        charactersTemp.Remove(ch2);
        Character ch3 = charactersTemp[UnityEngine.Random.RandomRangeInt(0, charactersTemp.Count)];
        charactersTemp.Remove(ch3);
        System.Collections.Generic.List<int> nums = new System.Collections.Generic.List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };
        nums.Remove(getHand(ch1).card2.num);
        nums.Remove(getHand(ch2).card2.num);
        nums.Remove(getHand(ch3).card2.num);
        int rand1 = nums[UnityEngine.Random.RandomRangeInt(0, nums.Count)];
        int rand2 = nums[UnityEngine.Random.RandomRangeInt(0, nums.Count)];
        int rand3 = nums[UnityEngine.Random.RandomRangeInt(0, nums.Count)];
        string line = string.Format("Between #{0}, #{1}, #{2}, there is {3}, {4} and {5}", ch1.id, ch2.id, ch3.id, PrSave.cardString(rand1), PrSave.cardString(rand2), PrSave.cardString(rand3));
        List<Character> targets = new List<Character>();
        targets.Add(ch1);
        targets.Add(ch2);
        targets.Add(ch3);
        return new ActedInfo(line, targets);
    }
    public override void bcx(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Day)
        {
            this.onActed.Invoke(this.bcr(charRef));
        }
    }
    public BishopPR() : base(ClassInjector.DerivedConstructorPointer<BishopPR>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public BishopPR(IntPtr ptr) : base(ptr)
    {

    }
}