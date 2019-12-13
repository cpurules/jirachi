using System;
using System.Collections.Generic;
using System.Text;

namespace jirachi_core {
    class ItemModel {
        /// <summary>
        /// Represents this item's generation-specific item index
        /// </summary>
        public int ItemId { get; set; }
        /// <summary>
        /// Represents the generation this item is linked to
        /// </summary>
        public int Generation { get; set; }
        /// <summary>
        /// Represents the pocket that holds this item
        /// </summary>
        public ItemPocket Pocket { get; set; }
        /// <summary>
        /// Represents the quantity of this item
        /// </summary>
        public int Quantity { get; set; }
    }
}
