using System;
using System.Collections.Generic;
using System.Text;

namespace jirachi_core {
    public class MoveModel {
        /// <summary>
        /// Represents this move's generation-specific move index
        /// </summary>
        public int MoveId { get; set; }
        /// <summary>
        /// Represents the generation this move is linked to
        /// </summary>
        public int Generation { get; set; }
        /// <summary>
        /// Represents the current PP for this move
        /// </summary>
        public int CurrentPP { get; set; }
        /// <summary>
        /// Represents the number of PP Ups applied to this move
        /// </summary>
        public int PPUps { get; set; }
    }
}
