using System;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using MelonLoader;
using Il2Cpp;
using Il2CppSystem.Collections.Generic;

namespace LiterallyPoker;

[RegisterTypeInIl2Cpp]
public class DrunkPR : BettingRole
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
        charRef.statuses.fm(ECharacterStatus.Corrupted, charRef);
        List<CharacterData> unusedVillagers = this.getUnusedPokerVillagers();
        CharacterData randomUnused = unusedVillagers[UnityEngine.Random.RandomRangeInt(0, unusedVillagers.Count)];
        Gameplay.Instance.mm(randomUnused.type, randomUnused);
        return randomUnused;
    }
    public override int betChips(Character charRef)
    {
        if (GameData.GameMode is Poker)
        {
            Poker pokerMode = (Poker)GameData.GameMode;
            if (pokerMode.boardCards.Count == 5)
            {
                int rand = UnityEngine.Random.RandomRangeInt(4, 6);
                return rand;
            }
        }
        return base.betChips(charRef);
    }
    public DrunkPR() : base(ClassInjector.DerivedConstructorPointer<DrunkPR>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public DrunkPR(IntPtr ptr) : base(ptr)
    {

    }
}