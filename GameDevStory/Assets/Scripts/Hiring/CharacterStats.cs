using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public enum Gender { MALE, FEMALE }

    public string name;

    public int age;
    public Gender gender;

    public int communicationStat;
    public int testingStat;
    public int technicalStat;
    public int creativityStat;
    public int designStat;

    public string bio;
    public Sprite sprite;
}
