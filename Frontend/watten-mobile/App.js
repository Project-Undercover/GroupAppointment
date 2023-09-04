import { View, StatusBar, I18nManager } from "react-native";
import { Provider } from "react-redux";
import store from "./src/stores/configureStore";
import Navigation from "./src/navigation";
import { useFonts } from "expo-font";
// import i18next from "./src/utils/i18n";
import { TailwindProvider } from "tailwindcss-react-native";

export default function App() {
  const [fontsLoaded] = useFonts({
    "Changa-Bold": require("./src/assets/fonts/Changa-Bold.ttf"),
    "Changa-Bold": require("./src/assets/fonts/Changa-Bold.ttf"),
    "Rubik-Bold": require("./src/assets/fonts/Rubik-Bold.ttf"),
    "Rubik-Medium": require("./src/assets/fonts/Rubik-Medium.ttf"),
    "Rubik-Regular": require("./src/assets/fonts/Rubik-Regular.ttf"),
    "Rubik-SemiBold": require("./src/assets/fonts/Rubik-SemiBold.ttf"),
  });
  if (!fontsLoaded) {
    return null;
  }
  I18nManager.forceRTL(false);
  return (
    <Provider store={store}>
      <TailwindProvider>
        <View style={{ flex: 1 }}>
          <Navigation />
        </View>
      </TailwindProvider>
    </Provider>
  );
}
