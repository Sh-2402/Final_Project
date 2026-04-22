using UnityEngine;

[RequireComponent(typeof(characterStats))]
public class Enemy : Interactable
{
    PlayerManager playerManager;
    characterStats myStats;

    protected override void Start()
    {
        base.Start();

        playerManager = PlayerManager.instance;
        myStats = GetComponent<characterStats>();
    }

    public override void Interact()
    {
        base.Interact();
        CharacterCombat playerCombat = playerManager.player.GetComponent<CharacterCombat>();
       
        if (playerCombat != null) 
        {
            playerCombat.Attack(myStats);
        }
    }
}
