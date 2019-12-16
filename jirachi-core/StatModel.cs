using System;
using System.Collections.Generic;
using System.Text;

namespace jirachi_core {
    public class StatModel {
        public StatType StatType { get; set; }
        public int ActualValue { get; set; }
        public int CalculatedValue { get; set; }
        public int IV { get; set; }
        public int EV { get; set; }
    }
}
