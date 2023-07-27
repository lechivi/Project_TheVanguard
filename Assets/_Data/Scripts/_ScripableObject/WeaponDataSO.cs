#if UNITY_EDITOR
using Cinemachine;
using UnityEditor;
#endif
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "SO/WeaponData")]
public class WeaponDataSO : ScriptableObject
{
    [Header("Common Properties")]
    public WeaponType WeaponType;
    public string WeaponName;
    public GameObject Icon; //UI
    public GameObject Model; 

    // Ranged weapon variables
    [Space(5)]
    [SerializeField] private float rangedDamage;
    [SerializeField] private float fireRate;
    [SerializeField] private float accuracy;
    [SerializeField] private AmmoType ammoType;
    [SerializeField] private int magazineSize;
    [SerializeField] private float reloadTime;
    [SerializeField] private float range;

    // Melee weapon variables
    [Space(5)]
    [SerializeField] private float meleeDamage;
    [SerializeField] private float swingSpeed;

    [Header("Other")]
    [SerializeField] private float value;

    #region Getter
    public float RangedDamage { get => this.rangedDamage; private set => this.rangedDamage = value; }
    public float FireRate { get => this.fireRate; private set => this.fireRate = value; }
    public float Accuracy { get => this.accuracy; private set => this.accuracy = value; }
    public AmmoType AmmoType { get => this.AmmoType; private set => this.AmmoType = value; }
    public int MagazineSize { get => this.magazineSize; private set => this.magazineSize = value; }
    public float ReloadTime { get => this.reloadTime; private set => this.reloadTime = value; }

    public float MeleeDamage { get => this.meleeDamage; private set => this.meleeDamage = value; }
    public float SwingSpeed { get => this.swingSpeed; private set => this.swingSpeed = value; }
    #endregion
}

#if UNITY_EDITOR
[CustomEditor(typeof(WeaponDataSO))]
public class WeaponDataEditor : Editor
{
    SerializedProperty weaponType;
    SerializedProperty weaponName;
    SerializedProperty icon;
    SerializedProperty model;

    SerializedProperty rangedDamage;
    SerializedProperty fireRate;
    SerializedProperty accuracy;
    SerializedProperty ammoType;
    SerializedProperty magazineSize;
    SerializedProperty reloadTime;
    SerializedProperty range;

    SerializedProperty meleeDamage;
    SerializedProperty swingSpeed;

    SerializedProperty value;

    private void OnEnable()
    {
        this.weaponType = serializedObject.FindProperty("WeaponType");
        this.weaponName = serializedObject.FindProperty("WeaponName");
        this.icon = serializedObject.FindProperty("Icon");
        this.model = serializedObject.FindProperty("Model");

        this.rangedDamage = serializedObject.FindProperty("rangedDamage");
        this.fireRate = serializedObject.FindProperty("fireRate");
        this.accuracy = serializedObject.FindProperty("accuracy");
        this.ammoType = serializedObject.FindProperty("ammoType");
        this.magazineSize = serializedObject.FindProperty("magazineSize");
        this.reloadTime = serializedObject.FindProperty("reloadTime");
        this.range = serializedObject.FindProperty("range");

        this.meleeDamage = serializedObject.FindProperty("meleeDamage");
        this.swingSpeed = serializedObject.FindProperty("swingSpeed");

        this.value = serializedObject.FindProperty("value");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(this.weaponType);
        EditorGUILayout.PropertyField(this.weaponName);
        EditorGUILayout.PropertyField(this.icon);
        EditorGUILayout.PropertyField(this.model);

        WeaponType selectedWeaponType = (WeaponType)weaponType.enumValueIndex;

        if (selectedWeaponType != WeaponType.Melee)
        {
            EditorGUILayout.PropertyField(this.rangedDamage);
            EditorGUILayout.PropertyField(this.fireRate);
            EditorGUILayout.PropertyField(this.accuracy);
            EditorGUILayout.PropertyField(this.ammoType);
            EditorGUILayout.PropertyField(this.magazineSize);
            EditorGUILayout.PropertyField(this.reloadTime);
            EditorGUILayout.PropertyField(this.range);
        }
        else
        {
            EditorGUILayout.PropertyField(meleeDamage);
            EditorGUILayout.PropertyField(swingSpeed);
        }

        EditorGUILayout.PropertyField(this.value);

        serializedObject.ApplyModifiedProperties();
    }
}
#endif