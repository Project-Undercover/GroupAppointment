import i18next from "i18next";
import { initReactI18next } from "react-i18next";
import { I18nManager } from "react-native";
import ar from "../locales/ar";
import he from "../locales/he";

export const languageResources = {
  ar: { translation: ar },
  he: { translation: he },
};

I18nManager.forceRTL(true);
I18nManager.allowRTL(true);
i18next.use(initReactI18next).init({
  compatibilityJSON: "v3",
  //   lng: "ar",
  fallbackLng: "he",
  resources: languageResources,
});

export default i18next;
