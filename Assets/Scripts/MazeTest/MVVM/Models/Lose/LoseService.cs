using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace MazeTest.MVVM.Models.Lose
{
    public class LoseService : MonoBehaviour, ILoseService
    {
        private ILoseMutableModel _model;

        private const float LoseTime = 1f;

        private readonly SortedList<int, float> _timers = new(10);

        private int _timerCounter;

        public event Action Lost;

        [Inject]
        public void Construct(ILoseMutableModel model)
        {
            _model = model;
        }

        public int StartLoseTimer()
        {
            _timers.Add(++_timerCounter, LoseTime);
            return _timerCounter;
        }

        private void Update()
        {
            bool lost = false;
            foreach (var timerKey in _timers.Keys.ToArray())
            {
                var value = _timers[timerKey];
                value = Mathf.Max(0f, value - Time.deltaTime);
                _timers[timerKey] = value;
                if (value <= 0)
                {
                    lost = true;
                    break;
                }
            }

            if (lost)
            {
                _timers.Clear();
                UpdateLoseLeftTime();
                Lost?.Invoke();
            }
            else
            {
                UpdateLoseLeftTime();
            }
        }

        public void StopLoseTimer(int timerId)
        {
            _timers.Remove(timerId);
            UpdateLoseLeftTime();
        }

        private void UpdateLoseLeftTime()
        {
            if (_timers.Any())
            {
                _model.SetLoseLeftTime(_timers.First().Value);
            }
            else
            {
                _model.SetLoseLeftTime(null);
            }
        }
    }
}