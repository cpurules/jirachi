using System;
using System.Collections.Generic;
using System.Text;

namespace jirachi_core {
    public class ItemModel {
        public ItemModel(int itemId, int quantity, int generation, ItemPocket pocket) {
            this.ItemId = itemId;
            this.Quantity = quantity;
            this.Generation = generation;
            this.Pocket = pocket;
        }
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

        public override bool Equals(object obj) {
            ItemModel item = obj as ItemModel;

            return (item != null)
                && (this.ItemId == item.ItemId)
                && (this.Quantity == item.Quantity)
                && (this.Generation == item.Generation)
                && (this.Pocket == item.Pocket);
        }
    }
}
