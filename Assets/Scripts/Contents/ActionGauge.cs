using System;

namespace Artheia
{
    public class ActionGauge
    {
        private const int _baseSpeed = 100;
        private const int _baseIncrement = 10;
        private const int _baseGaugeRatio = 50;

        private static float _speedRatio;
        private int _gauge;

        public ActionGauge(int speed)
        {
            Gauge = _CalculateInitialGauge(speed);
        }

        private int _CalculateInitialGauge(int speed)
        {
            _speedRatio = _UpdateSpeedRatio(speed);
            int initialGauge = (int)(_speedRatio * _baseGaugeRatio);
            return initialGauge;
        }

        private float _UpdateSpeedRatio(int speed)
        {
            return _speedRatio = (float)speed / _baseSpeed;
        }

        // Operator overloading
        public static ActionGauge operator ++(ActionGauge gauge)
        {
            int increment = (int)(_speedRatio * _baseIncrement);
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

        public int Gauge
        {
            get => _gauge;
            private set
            {
                if (value > 100) 
                    _gauge = 100;
                else
                    _gauge = value;
            }
        }
    }
}