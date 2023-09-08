import { View, StatusBar, I18nManager } from "react-native";
import { Provider } from "react-redux";
import { AlertsContextProvider } from "./src/context/AlertsContext";
import { TailwindProvider } from "tailwindcss-react-native";
import { ToastProvider } from "react-native-toast-notifications";
import { useEffect } from "react";
import store from "./src/stores/configureStore";
import Navigation from "./src/navigation";
import { useFonts } from "expo-font";
import i18next from "./src/utils/i18n";
import "moment/locale/ar";
import "moment/locale/he";
import moment from "moment";
export default function App() {
  const [fontsLoaded] = useFonts({
    "Changa-Bold": require("./src/assets/fonts/Changa-Bold.ttf"),
    "Changa-Bold": require("./src/assets/fonts/Changa-Bold.ttf"),
    "Rubik-Bold": require("./src/assets/fonts/Rubik-Bold.ttf"),
    "Rubik-Medium": require("./src/assets/fonts/Rubik-Medium.ttf"),
    "Rubik-Regular": require("./src/assets/fonts/Rubik-Regular.ttf"),
    "Rubik-SemiBold": require("./src/assets/fonts/Rubik-SemiBold.ttf"),
  });

  useEffect(() => {
    if (i18next.language === "ar") {
      moment.locale("ar");
    } else {
      moment.locale("he");
    }
  }, []);
  if (!fontsLoaded) {
    return null;
  }

  return (
    <Provider store={store}>
      <TailwindProvider>
        <ToastProvider>
          <AlertsContextProvider>
            <View style={{ flex: 1 }}>
              <Navigation />
            </View>
          </AlertsContextProvider>
        </ToastProvider>
      </TailwindProvider>
    </Provider>
  );
}
