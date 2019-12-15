using System;
using System.Collections.Generic;
using System.Text;

namespace jirachi_core {
    public class MoveModel {
        public MoveModel(int moveId, int currentPP, int pPUps) {
            MoveId = moveId;
            CurrentPP = currentPP;
            PPUps = pPUps;
        }

        /// <summary>
        /// Represents this move's generation-specific move index
        /// </summary>
        public int MoveId { get; set; }
        /// <summary>
        /// Represents the generation this move is linked to
        /// </summary>
        public int CurrentPP { get; set; }
        /// <summary>
        /// Represents the number of PP Ups applied to this move
        /// </summary>
        public int PPUps { get; set; }

        public override bool Equals(object obj) {
            return obj is MoveModel model &&
                   MoveId == model.MoveId &&
                   CurrentPP == model.CurrentPP &&
                   PPUps == model.PPUps;
        }

        public override int GetHashCode() {
            var hashCode = 1407069828;
            hashCode = hashCode * -1521134295 + MoveId.GetHashCode();
            hashCode = hashCode * -1521134295 + CurrentPP.GetHashCode();
            hashCode = hashCode * -1521134295 + PPUps.GetHashCode();
            return hashCode;
        }
    }
}
