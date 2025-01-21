using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Artheia.CombatUnit
{
    public abstract class CombatUnitBase : MonoBehaviour
    {
        private const float _maxActionGauge = 100f;

        // Stat
        private string _id;
        private int _priority;
        private int _speed;
        private ActionGauge _actionGauge;
        protected CombatUnitData _data;

        public void Initialize()
        {
            _id = _data.Id;
            _priority = _data.Priority;
            _speed = _data.Speed;
            _actionGauge = new ActionGauge(_speed);
            
            //TurnController.Instance.OnTurnStarted += TurnStarted;
        }

        public void StartTurn()
        {
        }

        public void TakeTurn()
        {

        }

        public void EndTurn()
        {
            _actionGauge = _speed;
        }

        private void TurnStarted()
        {
            _actionGauge++;
        }
        
        public int Priority => _priority;
        public int Speed => _speed;
        public ActionGauge ActionGauge => _actionGauge;
    }
}