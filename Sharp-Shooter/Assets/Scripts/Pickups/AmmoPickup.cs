using UnityEngine;

public class AmmoPickup : Pickup {

    [SerializeField] int minAmmoAmount;
    [SerializeField] int maxAmmoAmount;

    protected override void OnPickup(ActiveWeapon activeWeapon) {

        activeWeapon.AdjustAmmo(Random.Range(minAmmoAmount, maxAmmoAmount + 1));
    }
}
