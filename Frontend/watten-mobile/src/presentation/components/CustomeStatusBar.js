import {
  StyleSheet,
  View,
  StatusBar,
  Platform,
  SafeAreaView,
} from "react-native";
import theme from "../../utils/theme";

const STATUSBAR_HEIGHT = StatusBar.currentHeight;
const APPBAR_HEIGHT = Platform.OS === "ios" ? 44 : 56;
const CustomeStatusBar = ({ props }) => {
  return (
    <View style={[styles.statusBar, { backgroundColor: theme.COLORS.primary }]}>
      <SafeAreaView>
        <StatusBar
          translucent
          backgroundColor={theme.COLORS.primary}
          {...props}
        />
      </SafeAreaView>
    </View>
  );
};

export default CustomeStatusBar;

const styles = StyleSheet.create({
  statusBar: {
    height: STATUSBAR_HEIGHT,
  },
  appBar: {
    backgroundColor: "#79B45D",
    height: APPBAR_HEIGHT,
  },
  content: {
    flex: 1,
    backgroundColor: "#33373B",
  },
});
