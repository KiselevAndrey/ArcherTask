using UnityEngine;

namespace CodeBase.Weapon
{
    public class MeleeWeapon : Weapon
    {
        public override bool AttackEnded { get => true; protected set => throw new System.NotImplementedException(); }
    }
}