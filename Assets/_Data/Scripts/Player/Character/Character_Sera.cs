using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Sera : Character
{
    [Header("SERA")]
    [SerializeField] private float timer1 = 0.1f;
    [SerializeField] private int maxScanTimes = 3;
    [SerializeField] private float scanRange = 3f;
    [SerializeField] private PoolingObject poolingObject;
    [SerializeField] private ParticleSystem lightningStrikeFx;
    [SerializeField] private ScannerEnemy scannerEnemy;
    [SerializeField] private ElectricLine electricLine;

    private Transform enemyHit;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.poolingObject == null)
            this.poolingObject = GetComponent<PoolingObject>();

        if (this.lightningStrikeFx == null)
            this.lightningStrikeFx = transform.Find("FX_LightningStrike").GetComponent<ParticleSystem>();

        if (this.scannerEnemy == null)
            this.scannerEnemy = GetComponentInChildren<ScannerEnemy>();

        if (this.electricLine == null)
            this.electricLine = GetComponentInChildren<ElectricLine>();
    }

    public override void SpecialSkill()
    {
        base.SpecialSkill();
        if (this.isReadySpecialSkill)
        {
            if (this.IsHitEnemy())
            {
                this.isMiss = false;
            }
            else
            {
                this.isMiss = true;
            }

            this.isReadySpecialSkill = false;
            this.isCoolingDownSpecicalSkill = true;
            this.animator.SetTrigger("SpecialSkill");
        }
    }

    public IEnumerator CastSpell()
    {
        if (this.enemyHit == null)
            yield break;

        this.PlayLightningStrikeFx();
        yield return new WaitForSeconds(this.timer1);

        this.scannerEnemy.Scan(this.enemyHit.GetComponent<EnemyCtrl>(), this.maxScanTimes, this.scanRange);
        List<EnemyCtrl> listEnemy = this.scannerEnemy.Enemies;
        List<ParticleSystem> listFx = new List<ParticleSystem>();

        if (listEnemy.Count > 1)
        {
            this.electricLine.SetListPoint(listEnemy);
            this.electricLine.gameObject.SetActive(true);
            StartCoroutine(this.electricLine.AnimateElectricBeam());
        }

        //TODO: Set state Electrocuted/ Dizzy in EnemyCtrl
        for (int i = 0; i < listEnemy.Count; i++)
        {
            ParticleSystem fx = this.poolingObject.GetObject(listEnemy[i].CenterPoint.position, listEnemy[i].CenterPoint.rotation).GetComponent<ParticleSystem>();
            listEnemy[i].Animator.SetTrigger("Electrocuted");
            fx.Play();
            listFx.Add(fx);
        }
        yield return new WaitForSeconds(this.characterData.ExecutionSkillTime - this.timer1);

        if (listEnemy.Count > 1)
        {
            this.electricLine.gameObject.SetActive(false);
        }
        for (int i = 0; i < listEnemy.Count; i++)
        {
            listEnemy[i].Animator.Rebind();
            listFx[i].gameObject.SetActive(false);
        }
    }

    private void PlayLightningStrikeFx()
    {
        if (!this.enemyHit) return;

        this.lightningStrikeFx.transform.position = this.enemyHit.position;
        this.lightningStrikeFx.Play();
    }

    private bool IsHitEnemy()
    {
        Voidspawn_InfoScanner enemyHit = PlayerCtrl.Instance.PlayerInfoScanner.GetInfoScannerObjectByRaycast() as Voidspawn_InfoScanner;
        if (enemyHit != null)
        {
            this.enemyHit = enemyHit.transform;
            return true;
        }

        this.enemyHit = null;
        return false;
    }
}
