using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class CharacterManager : MonoBehaviour
{
    public static List<character> allCharacters = new List<character>();
    public static bool isCharacterAvailable { get { return allCharacters.Count > 0; } }

    public static enemy GetClosestEnemyToCharacter(character character)
    {
        enemy nearest = character.enemiesWithinFightingDistance.OrderBy(T => Vector3.Distance(T.transform.position, character.transform.position)).
                FirstOrDefault();
        return nearest;
    }

    public static character GetClosestCharacterToEnemy (enemy enemy)
    {
        character nearest = allCharacters.OrderBy(T => Vector3.Distance(T.transform.position, enemy.transform.position)).
               FirstOrDefault();
        return nearest;
    }
}

