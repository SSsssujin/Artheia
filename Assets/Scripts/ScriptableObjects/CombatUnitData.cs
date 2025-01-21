using UnityEngine;

[CreateAssetMenu(fileName = "CombatUnitData", menuName = "Scriptable Objects/CombatEntityData")]
public class CombatUnitData : ScriptableObject
{
    [SerializeField]
    private string _id;
    [SerializeField]
    private int _priority;
    [SerializeField]
    private int _speed;
    [SerializeField] 
    private Sprite _portrait;
    [SerializeField]
    private GameObject _model;

    public string Id => _id;
    public int Priority => _priority;
    public int Speed => _speed;
    public Sprite Portrait => _portrait;
    public GameObject Model => _model;
}
