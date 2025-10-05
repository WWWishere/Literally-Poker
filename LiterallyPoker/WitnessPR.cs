using System;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using MelonLoader;
using Il2Cpp;
using Il2CppSystem.Collections.Generic;

namespace LiterallyPoker;

[RegisterTypeInIl2Cpp]
public class WitnessPR : BettingRole
{
    public override ActedInfo bcq(Character charRef)
    {
        List<Character> characters = Gameplay.CurrentCharacters;
        List<Character> lying = new List<Character>();
        foreach (Character character in characters)
        {
            if (character.id != charRef.id)
            {
                bool confess = character.dataRef.name == "Betting Confessor";
                bool corrupt = character.statuses.fo(ECharacterStatus.Corrupted);
                bool bluff = character.bluff != null && !character.statuses.fo(ECharacterStatus.HealthyBluff);
                if (!confess && (corrupt || bluff))
                {
                    lying.Add(character);
                }
            }
        }
        if (lying.Count == 0)
        {
            return new ActedInfo("NO character is lying");
        }
        string lyingList = "";
        foreach (Character ch in lying)
        {
            if (lyingList == "")
            {
                lyingList = "#" + ch.id;
            }
            else
            {
                lyingList += ", #" + ch.id;
            }
        }
        string line = lyingList + " is lying.";
        return new ActedInfo(line, lying);
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
        List<Character> truthful = new List<Character>();
        foreach (Character character in characters)
        {
            if (character.id != charRef.id)
            {
                bool confess = character.dataRef.name == "Betting Confessor";
                bool corrupt = character.statuses.fo(ECharacterStatus.Corrupted);
                bool bluff = character.bluff != null && !character.statuses.fo(ECharacterStatus.HealthyBluff);
                if (confess || !(corrupt || bluff))
                {
                    truthful.Add(character);
                }
            }
        }
        if (truthful.Count == 0)
        {
            return new ActedInfo("NO character is lying");
        }
        string truthList = "";
        int randomCount = UnityEngine.Random.RandomRangeInt(0, truthful.Count);
        List<Character> targets = new List<Character>();
        for (int i = 0; i < randomCount + 1; i++)
        {
            Character ch = truthful[i];
            if (truthList == "")
            {
                truthList = "#" + ch.id;
            }
            else
            {
                truthList += ", #" + ch.id;
            }
            targets.Add(ch);
        }
        string line = truthList + " is lying.";
        return new ActedInfo(line, targets);
    }
    public override void bcx(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Day)
        {
            this.onActed.Invoke(this.bcr(charRef));
        }
    }
    public WitnessPR() : base(ClassInjector.DerivedConstructorPointer<WitnessPR>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public WitnessPR(IntPtr ptr) : base(ptr)
    {

    }
}