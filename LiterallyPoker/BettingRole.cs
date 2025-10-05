using System;
using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem.Collections.Generic;
using MelonLoader;
using UnityEngine;

namespace LiterallyPoker;

[RegisterTypeInIl2Cpp]
public class BettingRole : Role
{
    public virtual int betChips(Character charRef)
    {
        Hand hand = this.getHand(charRef);
        List<int> possibleBets = new List<int>();
        int[] bets = PrSave.bettingChances(hand.quality);
        for (int i = 0; i < bets.Length; i++)
        {
            int weight = bets[i];
            {
                for (int j = 0; j < weight; j++)
                {
                    possibleBets.Add(i);
                }
            }
        }
        int randBet = possibleBets[UnityEngine.Random.RandomRangeInt(0, possibleBets.Count)];
        return randBet;
    }
    public Hand getHand(Character charRef)
    {
        GameObject gameObject = charRef.gameObject.transform.FindChild("Hand_PrMode").gameObject;
        Hand hand = gameObject.GetComponent<Hand>();
        return hand;
    }
    public List<CharacterData> getPokerVillagers()
    {
        System.Collections.Generic.List<CharacterData> listPoker = PrSave.pokerCD;
        List<CharacterData> listVill = new List<CharacterData>();
        foreach (CharacterData data in listPoker)
        {
            if (data.type == ECharacterType.Villager)
            {
                listVill.Add(data);
            }
        }
        return listVill;
    }
    public List<CharacterData> getUnusedPokerVillagers()
    {
        List<CharacterData> villData = getPokerVillagers();
        List<Character> characters = Gameplay.CurrentCharacters;
        foreach (Character ch in characters)
        {
            CharacterData chData = ch.dataRef;
            if (chData.type == ECharacterType.Villager && villData.Contains(chData))
            {
                villData.Remove(chData);
            }
        }
        return villData;
    }
    public BettingRole() : base(ClassInjector.DerivedConstructorPointer<BettingRole>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public BettingRole(IntPtr ptr) : base(ptr)
    {

    }
}
