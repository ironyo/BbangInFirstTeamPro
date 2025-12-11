using UnityEngine;

public class AttackEventRalay : MonoBehaviour
{
    private Customer customer;

    private void Awake()
    {
        customer = GetComponentInParent<Customer>();
    }

    public void OnAttackEvent()
    {
        customer?.PlayHitParticle();
    }
}
