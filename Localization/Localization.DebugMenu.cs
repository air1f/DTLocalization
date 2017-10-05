using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

using DTLocalization.Internal;
using GDataDB;

#if DT_DEBUG_MENU
using DTDebugMenu;
#endif

namespace DTLocalization {
	public static partial class Localization {
		#if DT_DEBUG_MENU
		// PRAGMA MARK - Internal
		[RuntimeInitializeOnLoadMethod]
		private static void InitializeDebugMenu() {
			RefreshDebugMenu();
			OnLocalizationsUpdated += RefreshDebugMenu;
		}

		private static void RefreshDebugMenu() {
			var inspector = DTDebugMenu.GenericInspectorRegistry.Get("Localization");
			inspector.ResetFields();

			foreach (var kvp in localizationTableMap_) {
				string tableKey = kvp.Key;
				LocalizationTable table = kvp.Value;

				bool fromCached = cachedTableKeys_.Contains(tableKey);
				inspector.RegisterHeader(string.Format("{0}{1}", tableKey, fromCached ? " (cached)" : ""));

				foreach (var culture in table.AllCultures) {
					Dictionary<string, string> keyLocalizedTextMap = table.GetTextMapFor(culture);
					foreach (var kvp3 in keyLocalizedTextMap) {
						string key = kvp3.Key;
						string localizedText = kvp3.Value;

						inspector.RegisterLabel(string.Format("{1} - {0}: {2}", culture.Name, key, localizedText));
					}
				}
			}
		}
		#endif
	}
}
