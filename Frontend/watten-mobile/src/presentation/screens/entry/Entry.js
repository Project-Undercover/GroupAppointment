import {
  StyleSheet,
  Image,
  View,
  SafeAreaView,
  Keyboard,
  TouchableWithoutFeedback,
  StatusBar,
} from "react-native";
import React from "react";
import Login from "./components/Login";
import theme from "../../../utils/theme";
import Spacer from "../../components/Spacer";
import OTP from "./components/OTP";
import { useSelector } from "react-redux";
import CustomeStatusBar from "../../components/CustomeStatusBar";
const Entry = () => {
  const { isLoggedIn } = useSelector((state) => state.auth);
  return (
    <TouchableWithoutFeedback onPress={Keyboard.dismiss}>
      <View className="flex-1 bg-white">
        <StatusBar barStyle="dark-content" backgroundColor="white" />
        <SafeAreaView />
        <View className="flex-1 items-center">
          <Image
            style={styles.logo}
            source={require("../../../assets/imgs/logo.png")}
          />
          <Spacer space={10} />
          {isLoggedIn ? <OTP /> : <Login />}
        </View>
        <View style={styles.footer}>
          <View style={styles.footerHalfCircle}></View>
        </View>
      </View>
    </TouchableWithoutFeedback>
  );
};

export default Entry;

const styles = StyleSheet.create({
  logo: {
    width: 240,
    height: 140,
  },
  footer: {
    height: 200,
    backgroundColor: theme.COLORS.primary,
    position: "relative",
    overflow: "hidden",
  },
  footerHalfCircle: {
    width: "50%",
    height: 50,
    position: "absolute",
    top: -20,
    // right: 0,
    // left: "50%",
    borderRadius: 20,
    transform: [{ scaleX: 2 }],
    backgroundColor: theme.COLORS.white,
    alignSelf: "center",
  },
});
