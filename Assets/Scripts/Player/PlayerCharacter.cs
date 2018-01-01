using UnityEngine;

public class PlayerCharacter : Character
{
    public static PlayerCharacter Me;

    protected override void Awake()
    {
        base.Awake();
        Me = this;
    }

    public override Vector3 FoculPoint
    {
        get
        {
            return Camera.main.transform.position;
        }
    }

    public override Vector3 AimDirection()
    {
        return Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 1)) - Camera.main.transform.position;
    }
}
