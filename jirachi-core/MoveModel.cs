using System;
using System.Collections.Generic;
using System.Text;

namespace jirachi_core {
    class MoveModel {
        /// <summary>
        /// Represents the generation-specific move index
        /// </summary>
        public int MoveId { get; set; }
        /// <summary>
        /// Represents the generation this move ID is linked to
        /// </summary>
        public int Generation { get; set; }
        /// <summary>
        /// Represents the current PP the Pokemon has for this move
        /// </summary>
        public int CurrentPP { get; set; }
        /// <summary>
        /// Represents the number of PP Ups applied to this move
        /// </summary>
        public int PPUps { get; set; }
    }
}
