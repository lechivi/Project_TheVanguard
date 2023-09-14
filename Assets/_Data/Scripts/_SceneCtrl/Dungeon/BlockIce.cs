using UnityEngine;

public class BlockIce : SaiMonoBehaviour
{
    [SerializeField] private EnemyCtrl enemyIce;
    [SerializeField] private Transform blockIce;
    [SerializeField] private Transform blockIceDefrost;

    private bool check = true;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.blockIce == null)
            this.blockIce = transform.Find("BlockIce");    
        
        if (this.blockIceDefrost == null)
            this.blockIceDefrost = transform.Find("BlockIceDefrost");
    }

    private void Start()
    {
        this.blockIce.gameObject.SetActive(true);
        this.blockIceDefrost.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (this.enemyIce == null) return;

        if (this.enemyIce.EnemyHealth.IsDeath() && this.check)
        {
            this.check = false;
            Invoke("DefrostBlockIce", 1f);
        }
    }

    private void DefrostBlockIce()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_DEFROST_ICE_BUFF_05);
        }
        blockIce.gameObject.SetActive(false);
        blockIceDefrost.gameObject.SetActive(true);
    }
}
