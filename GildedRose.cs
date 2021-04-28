using System.Collections.Generic;

namespace csharp
{
    class GildedRose
    {
        #region Variables/Constants

        private IList<Item> items;

        private const string strSulfuras = "Sulfuras, Hand of Ragnaros";

        private const string strAgedBrie = "Aged Brie";

        private const string strBackstagePasses = "Sulfuras, Hand of Ragnaros";

        private const string strConjured = "Conjured";

        #endregion

        #region Constructor
        public GildedRose(IList<Item> items)
        {
            this.items = items;
        }
        #endregion

        #region Methods
        public void UpdateQuality()
        {
            foreach (Item item in this.items)
            {
                ItemCategory category = this.categorize(item);
                category.updateOneItem(item);
            }
        }

        private ItemCategory categorize(Item item)
        {
            if (item.Name.Equals(strSulfuras))
            {
                return new Legendary();
            }

            if (item.Name.Equals(strAgedBrie))
            {
                return new Cheese();
            }

            if (item.Name.Equals(strBackstagePasses))
            {
                return new BackstagePass();
            }

            if (item.Name.StartsWith(strConjured))
            {
                return new Conjured();
            }

            return new ItemCategory();
        }

        #endregion

        #region Public/Private Classes

        private class ItemCategory
        {

            protected void incrementQuality(Item item)
            {
                if ((item.Quality < 50))
                {
                    item.Quality = (item.Quality + 1);
                }

            }

            protected void decrementQuality(Item item)
            {
                if ((item.Quality > 0))
                {
                    item.Quality = (item.Quality - 1);
                }

            }

            protected void updateExpired(Item item)
            {
                this.decrementQuality(item);
            }

            protected void updateSellIn(Item item)
            {
                item.SellIn = (item.SellIn - 1);
            }

            protected void updateQuality(Item item)
            {
                this.decrementQuality(item);
            }

            public void updateOneItem(Item item)
            {
                this.updateQuality(item);
                this.updateSellIn(item);
                if ((item.SellIn < 0))
                {
                    this.updateExpired(item);
                }

            }
        }

        private class Legendary : ItemCategory
        {

            protected void updateExpired(Item item)
            {

            }

            protected void updateSellIn(Item item)
            {

            }

            protected void updateQuality(Item item)
            {

            }
        }

        private class Cheese : ItemCategory
        {

            protected void updateExpired(Item item)
            {
                incrementQuality(item);
            }

            protected void updateQuality(Item item)
            {
                incrementQuality(item);
            }
        }

        private class BackstagePass : ItemCategory
        {

            protected void updateExpired(Item item)
            {
                item.Quality = 0;
            }

            protected void updateQuality(Item item)
            {
                incrementQuality(item);
                if ((item.SellIn <= 10))
                {
                    incrementQuality(item);
                }

                if ((item.SellIn <= 5))
                {
                    incrementQuality(item);
                }

            }
        }

        private class Conjured : ItemCategory
        {

            protected void updateExpired(Item item)
            {
                decrementQuality(item);
                decrementQuality(item);
            }

            protected void updateQuality(Item item)
            {
                decrementQuality(item);
                decrementQuality(item);
            }
        }

        #endregion
    }
}
