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
        Debug.Log("Attack Event Relay OnAttackEvent");
        customer?.PlayHitParticle();
        customer.InflictDamage();
    }

    public void OnUnkindAttackEvent()
    {
        customer?.PlayHitParticle();
        customer.InflictDamage();
        Destroy(customer.gameObject);
    }
}
