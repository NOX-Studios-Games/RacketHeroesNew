using UnityEngine;

[CreateAssetMenu(menuName = "Stage", fileName = "New Stage")]
public class Stage : ScriptableObject {
    public string stageName;
    public GameObject stage;
}