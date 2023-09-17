using Cinemachine;
using UnityEngine;

public class PlayerHealth : PlayerAbstract, IHealth
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    
    private bool isDeath;

    private void Start()
    {
        this.ResetHealth();
    }

    public void ResetHealth()
    {
        if (this.playerCtrl.Character)
        {
            this.maxHealth = CharacterStatsCalculate.MaxHealth(this.playerCtrl.Character.CharacterData);
            this.currentHealth = this.maxHealth;

            if (ListenerManager.HasInstance)
            {
                ListenerManager.Instance.BroadCast(ListenType.UpdatePlayerHealth, this);
                Debug.Log("BroadCast");

            }
        }
    }

    public int GetMaxHealth()
    {
        return this.maxHealth;
    }

    public int GetCurrentHealth()
    {
        return this.currentHealth;
    }

    public bool IsDeath()
    {
        return this.isDeath;
    }

    public void TakeDamage(int damage)
    {
        if (this.isDeath) return;

        Debug.Log("PLAYER -" + damage);
        this.currentHealth -= CharacterStatsCalculate.DamageTake(this.playerCtrl.Character.CharacterData, damage);
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.BroadCast(ListenType.UpdatePlayerHealth, this);
            Debug.Log("BroadCast");
        }

        if (this.currentHealth <= 0)
        {
            this.currentHealth = 0;
            this.Die();
        }
    }

    public void Die()
    {
        this.isDeath = true;
        this.playerCtrl.Character.RagdollCtrl.EnableRagdoll();
        this.playerCtrl.PlayerInput.enabled = false;

        this.playerCtrl.PlayerCamera.IsTPSCamera = true;
        this.playerCtrl.PlayerCamera.TPSCamera.Follow = this.playerCtrl.Character.CenterPoint;
        this.playerCtrl.PlayerCamera.TPSCamera.LookAt = null;
        this.playerCtrl.PlayerCamera.TPSCamera.GetComponent<CinemachineInputProvider>().enabled = false;

        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlayBgm(AUDIO.BGM_LOSE_AMAZEING_HAMSTER_PIANO_VERSION, 1.5f);
        }

        Invoke("ShowLosePanel", 2f);
    }

    private void ShowLosePanel()
    {
        if (UIManager.HasInstance)
        {
            UIManager.Instance.InGamePanel.ShowOther(null);
            UIManager.Instance.InGamePanel.Other.ShowLosePanel();
        }
    }

    public void TakeDamage(int damage, Vector3 force, Vector3 hitPoint, Rigidbody hitRigidbody)
    {
        throw new System.NotImplementedException();
    }
}
