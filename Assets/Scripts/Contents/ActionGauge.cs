using System;

namespace Artheia
{
    public class ActionGauge
    {
        private const int _baseSpeed = 100;
        private const int _baseIncrement = 10;
        private const int _baseGaugeRatio = 4;

        private static float _speedRatio;
        private float _gauge;

        public ActionGauge(int speed)
        {
            Gauge = _CalculateInitialGauge(speed);
        }

        private float _CalculateInitialGauge(int speed)
        {
            _speedRatio = _UpdateSpeedRatio(speed);
            float initialGauge = _speedRatio * _baseGaugeRatio * 0.1f;
            return initialGauge;
        }

        private float _UpdateSpeedRatio(int speed)
        {
            return _speedRatio = (float)speed / _baseSpeed;
        }

        // Operator overloading
        public static ActionGauge operator ++(ActionGauge gauge)
        {
            float increment = _speedRatio * _baseIncrement;
            gauge.Gauge += increment;
            return gauge;
        }
        
        public static bool operator >(ActionGauge x, ActionGauge y)
        {
            return x.Gauge > y.Gauge;
        }

        public static bool operator <(ActionGauge x, ActionGauge y)
        {
            return x.Gauge < y.Gauge;
        }
        
        public static bool operator ==(ActionGauge x, ActionGauge y)
        {
            return x?.Gauge == y?.Gauge;
        }

        public static bool operator !=(ActionGauge x, ActionGauge y)
        {
            return x?.Gauge != y?.Gauge;
        }

        public static implicit operator ActionGauge(int speed)
        {
            return new ActionGauge(speed);
        }

        public float Gauge
        {
            get => _gauge;
            private set
            {
                if (value > 1) 
                    _gauge = 1;
                else
                    _gauge = value;
            }
        }
    }
}