﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace NWaves.Signals.Builders
{
    /// <summary>
    /// Class for the generator of sawtooth waves
    /// </summary>
    public class SawtoothBuilder : SignalBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        private double _low;

        /// <summary>
        /// 
        /// </summary>
        private double _high;

        /// <summary>
        /// 
        /// </summary>
        private double _frequency;

        public SawtoothBuilder()
        {
            ParameterSetters = new Dictionary<string, Action<double>>
            {
                {"low, lo, lower",  param => _low = param},
                {"high, hi, upper", param => _high = param},
                {"frequency, freq", param => _frequency = param},
            };

            _low = -1.0;
            _high = 1.0;
            _frequency = 0.0;
        }

        /// <summary>
        /// Formula:
        /// 
        ///     s[n] = LO + (HI - LO) * frac(i * freq + phi)
        /// 
        /// </summary>
        /// <returns></returns>
        public override DiscreteSignal Generate()
        {
            if (_frequency <= 0)
            {
                throw new FormatException("Frequency must be positive!");
            }

            if (_high < _low)
            {
                throw new FormatException("Upper level must be greater than he lower one!");
            }

            var samples = Enumerable.Range(0, Length)
                                    .Select(i => _low + (_high - _low) * (i*_frequency - Math.Floor(i * _frequency)));

            return new DiscreteSignal(SamplingRate, samples);
        }
    }
}
