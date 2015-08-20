using UnityEngine;
using System.Collections;

namespace Soomla.Store.Example
{
		public class MyAsset : IStoreAssets
		{
				public int GetVersion ()
				{
						return 0;
				}
		
				public VirtualCurrency[] GetCurrencies ()
				{
						return new VirtualCurrency[]{LIVES_CURRENCY};
				}
		
				public VirtualGood[] GetGoods ()
				{
						return new VirtualGood[] {};
				}
		
				public VirtualCurrencyPack[] GetCurrencyPacks ()
				{
						return new VirtualCurrencyPack[] {
								FIVE_LIVES_PACK,
								TWENTY_FIVE_LIVES_PACK,
								FIFTY_LIVES_PACK
						};
				}
		
				public VirtualCategory[] GetCategories ()
				{
						return new VirtualCategory[]{};
				}
				///////////////////////////////////////////////////////////////////
		
				/** Static Final Members **/
		
				public const string CURRENCY_ITEM_ID = "lives";
				public const string FIVE_PACK_PRODUCT_ID = "lives5";
				public const string TWENTY_FIVE_PACK_PRODUCT_ID = "lives25";
				public const string FIFTY_PACK_PRODUCT_ID = "lives50";
		
				/** Virtual Currencies **/
		
				public static VirtualCurrency LIVES_CURRENCY = new VirtualCurrency (
					"Lives",                                        // name
					"",                                            // description
					CURRENCY_ITEM_ID                            // item id
				);
		
		
				/** Virtual Currency Packs **/
		
				public static VirtualCurrencyPack FIVE_LIVES_PACK = new VirtualCurrencyPack (
					"5 Lives",                                   // name
					"Buy 5 lives",                       // description
					"lives_5",                                   // item id
					5,                                                // number of currencies in the pack
					CURRENCY_ITEM_ID,                        // the currency associated with this pack
					new PurchaseWithMarket (FIVE_PACK_PRODUCT_ID, 0.99)
				);
				public static VirtualCurrencyPack TWENTY_FIVE_LIVES_PACK = new VirtualCurrencyPack (
					"25 Lives",                                   // name
					"Buy 25 lives",                       // description
					"lives_25",                                   // item id
					25,                                                // number of currencies in the pack
					CURRENCY_ITEM_ID,                        // the currency associated with this pack
					new PurchaseWithMarket (TWENTY_FIVE_PACK_PRODUCT_ID, 1.99)
				);
				public static VirtualCurrencyPack FIFTY_LIVES_PACK = new VirtualCurrencyPack (
					"50 Lives",                                   // name
					"Buy 50 lives",                       // description
					"lives_50",                                   // item id
					50,                                                // number of currencies in the pack
					CURRENCY_ITEM_ID,                        // the currency associated with this pack
					new PurchaseWithMarket (FIFTY_PACK_PRODUCT_ID, 2.99)
				);
		}

	
}
