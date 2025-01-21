using System.Collections.Generic;
using Artheia.CombatUnit;
using UnityEngine;

namespace Artheia
{
    public class PartyController : MonoBehaviour
    {
        [SerializeField] 
        private List<PlayerUnit> _tempParty;
    }
}