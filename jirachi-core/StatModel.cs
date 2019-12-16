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

        public override bool Equals(object obj) {
            return obj is StatModel model &&
                   StatType == model.StatType &&
                   StatValue == model.StatValue &&
                   IV == model.IV &&
                   EV == model.EV;
        }

        public override int GetHashCode() {
            var hashCode = 2043926891;
            hashCode = hashCode * -1521134295 + StatType.GetHashCode();
            hashCode = hashCode * -1521134295 + StatValue.GetHashCode();
            hashCode = hashCode * -1521134295 + IV.GetHashCode();
            hashCode = hashCode * -1521134295 + EV.GetHashCode();
            return hashCode;
        }
    }
}
