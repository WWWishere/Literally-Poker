using System;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using MelonLoader;
using Il2Cpp;

namespace LiterallyPoker;

[RegisterTypeInIl2Cpp]
public class ConfessorPR : BettingRole
{
    public override ActedInfo bcq(Character charRef)
    {
        Hand hand = this.getHand(charRef);
        Card hidden = hand.card2;
        string line = string.Format("I have {0}", PrSave.cardString(hidden.num));
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
        return new ActedInfo("I can't say my hand.");
    }
    public override void bcx(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Day)
        {
            this.onActed.Invoke(this.bcr(charRef));
        }
    }
    public ConfessorPR() : base(ClassInjector.DerivedConstructorPointer<ConfessorPR>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public ConfessorPR(IntPtr ptr) : base(ptr)
    {

    }
}