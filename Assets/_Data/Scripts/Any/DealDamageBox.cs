using UnityEngine;

public class DealDamageBox : SaiMonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private bool isPlayerWeapon = true;
    [SerializeField] private Collider col;

    public int Damage { get => this.damage; set => this.damage = value; }
    public Collider Col { get => this.col; }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.col == null)
            this.col = GetComponent<Collider>();
    }

    private void Start()
    {
        this.col.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(this.isPlayerWeapon ? "EnemyCollider" : "PlayerCollider"))
        {
            other.GetComponent<HitBox>().OnHit(this.damage);
        }
    }
}
