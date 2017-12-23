using UnityEngine;

public class PlayerAbilityController : AbilityController
{
    private Transform _owner;

    private void Update()
    {
        HandleCombat();
    }

    public void HandleCombat()
    {
        if (Input.anyKeyDown)
        {
            foreach (var ability in _availableAbilities)
            {
                var casting = ability;
                if (Input.GetKeyDown(casting.HotKey))
                {
                    casting.PerformAbility(transform);
                }
            }
        }
    }
}
