using System;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using MelonLoader;
using Il2Cpp;
using Il2CppSystem.Collections.Generic;

namespace LiterallyPoker;

[RegisterTypeInIl2Cpp]
public class ShugenjaPR : BettingRole
{
    public override ActedInfo bcq(Character charRef)
    {
        string[] dirs = new string[] { "Clockwise", "equidistant", "Counter-Clockwise" };
        int dir = getHighCardPos(charRef);
        string line = string.Format("Highest Card is: {0}", dirs[dir]);
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
        string[] dirs = new string[] { "Clockwise", "equidistant", "Counter-Clockwise" };
        List<int> numDirs = new List<int>();
        numDirs.Add(0);
        numDirs.Add(1);
        numDirs.Add(2);
        int dir = getHighCardPos(charRef);
        numDirs.Remove(dir);
        int notDir = numDirs[UnityEngine.Random.RandomRangeInt(0, numDirs.Count)];
        string line = string.Format("Highest Card is: {0}", dirs[notDir]);
        return new ActedInfo(line);
    }
    public override void bcx(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Day)
        {
            this.onActed.Invoke(this.bcr(charRef));
        }
    }
    public int getHighCardPos(Character charRef)
    {
        int charId = charRef.id;
        List<int> highChars = new List<int>();
        List<Character> characters = Gameplay.CurrentCharacters;
        int charSize = characters.Count;
        int highCard = 0;
        int ccmin = -100;
        int cmin = -100;
        foreach (Character ch in characters)
        {
            Hand hand = this.getHand(ch);
            int hiddenVal = hand.card2.num;
            if (hiddenVal == 1)
            {
                hiddenVal = 14;
            }
            if (hiddenVal > highCard)
            {
                highChars.Clear();
            }
            if (hiddenVal >= highCard)
            {
                highChars.Add(ch.id);
                highCard = hiddenVal;
            }
        }
        foreach (int highPos in highChars)
        {
            int counterClockwise = getCounterClockwise(charId, highPos, charSize);
            int clockwise = getClockwise(charId, highPos, charSize);
            if (ccmin == -100 || ccmin > counterClockwise)
            {
                ccmin = counterClockwise;
            }
            if (cmin == -100 || cmin > clockwise)
            {
                cmin = clockwise;
            }
        }
        if (ccmin > cmin)
        {
            return 0;
        }
        else if (cmin > ccmin)
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }
    public int getCounterClockwise(int id1, int id2, int totalChars)
    {
        int id3 = id2;
        if (id2 > id1)
        {
            id3 -= totalChars;
        }
        return id1 - id3;
    }
    public int getClockwise(int id1, int id2, int totalChars)
    {
        int id3 = id2;
        if (id2 < id1)
        {
            id3 += totalChars;
        }
        return id3 - id1;
    }
    public ShugenjaPR() : base(ClassInjector.DerivedConstructorPointer<ShugenjaPR>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public ShugenjaPR(IntPtr ptr) : base(ptr)
    {

    }
}