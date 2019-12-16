using System;
using System.Collections.Generic;
using System.Text;

namespace jirachi_core {
    public class StatModel {
        public StatModel(StatType statType, int statValue, int iV, int eV) {
            StatType = statType;
            StatValue = statValue;
            IV = iV;
            EV = eV;
        }

        public StatModel(StatType statType, int iV, int eV) {
            StatType = statType;
            StatValue = 0;
            IV = iV;
            EV = eV;
        }

        public StatType StatType { get; set; }
        public int StatValue { get; set; }
        public int IV { get; set; }
        public int EV { get; set; }
    }
}
