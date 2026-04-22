using UnityEngine;

public class EquipmentSystem : MonoBehaviour
{
    [SerializeField] GameObject weaponHolder;
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject weaponSheath;

    GameObject currentWeaponInHand;
    GameObject currentWeaponInSheath;

    void Start()
    {
        currentWeaponInSheath = Instantiate(weapon);
        currentWeaponInSheath.transform.SetParent(weaponSheath.transform, false);
        currentWeaponInSheath.transform.localPosition = Vector3.zero;
        currentWeaponInSheath.transform.localRotation = Quaternion.identity;
    }

    public void DrawWeapon()
    {
        if (currentWeaponInSheath != null)
        {
            Destroy(currentWeaponInSheath);
        }

        currentWeaponInHand = Instantiate(weapon);
        currentWeaponInHand.transform.SetParent(weaponHolder.transform, false);
        currentWeaponInHand.transform.localPosition = Vector3.zero;
        currentWeaponInHand.transform.localRotation = Quaternion.identity;

        MeshRenderer sheathRenderer = weaponSheath.GetComponent<MeshRenderer>();
        if (sheathRenderer != null)
        {
            sheathRenderer.enabled = false;
        }
    }

    public void SheathWeapon()
    {
        if (currentWeaponInHand != null)
        {
            Destroy(currentWeaponInHand);
        }

        currentWeaponInSheath = Instantiate(weapon);
        currentWeaponInSheath.transform.SetParent(weaponSheath.transform, false);
        currentWeaponInSheath.transform.localPosition = Vector3.zero;
        currentWeaponInSheath.transform.localRotation = Quaternion.identity;

        MeshRenderer sheathRenderer = weaponSheath.GetComponent<MeshRenderer>();
        if (sheathRenderer != null)
        {
            sheathRenderer.enabled = true;
        }
    }
  
    public void StartDealDamage()
    {
        if (currentWeaponInHand != null)
        {
            DamageDealer damageDealer = currentWeaponInHand.GetComponentInChildren<DamageDealer>();
            if (damageDealer != null)
            {
                damageDealer.StartDealDamage();
            }
        }
    }

    public void EndDealDamage()
    {
        if (currentWeaponInHand != null)
        {
            DamageDealer damageDealer = currentWeaponInHand.GetComponentInChildren<DamageDealer>();
            if (damageDealer != null)
            {
                damageDealer.EndDealDamage();
            }
        }
    }
}