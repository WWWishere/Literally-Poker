using System;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using MelonLoader;
using Il2Cpp;
using Il2CppSystem.Collections.Generic;

namespace LiterallyPoker;

[RegisterTypeInIl2Cpp]
public class PookaPR : BettingRole
{
    public override ActedInfo bcq(Character charRef)
    {
        return new ActedInfo("");
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
        return new ActedInfo("");
    }
    public override void bcx(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Day)
        {
            this.onActed.Invoke(this.bcr(charRef));
        }
    }
    public override CharacterData bcz(Character charRef)
    {
        List<Character> characters = Gameplay.CurrentCharacters;
        List<Character> list2 = CharactersHelper.tl(characters, charRef);
        List<Character> list3 = new List<Character>();
        list3.Add(list2[1]);
        list3.Add(list2[list2.Count - 1]);
        List<Character> list4 = new List<Character>();
        foreach (Character ch in list3)
        {
            if (ch.dataRef.type == ECharacterType.Villager && !ch.statuses.fo(ECharacterStatus.Corrupted))
            {
                ch.statuses.fm(ECharacterStatus.Corrupted, charRef);
            }
        }
        List<CharacterData> listVillagers = this.getPokerVillagers();
        List<CharacterData> unusedVillagers = this.getUnusedPokerVillagers();
        CharacterData randomUnused = unusedVillagers[UnityEngine.Random.RandomRangeInt(0, unusedVillagers.Count)];
        Gameplay.Instance.mm(randomUnused.type, randomUnused);
        return randomUnused;
    }
    public PookaPR() : base(ClassInjector.DerivedConstructorPointer<PookaPR>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public PookaPR(IntPtr ptr) : base(ptr)
    {

    }
}