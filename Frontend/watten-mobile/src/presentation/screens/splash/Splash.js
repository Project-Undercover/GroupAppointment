import React, { useCallback, useRef, useEffect } from "react";
import {
  Platform,
  StyleSheet,
  View,
  Animated,
  Easing,
  Image,
} from "react-native";
import { useFocusEffect, useRoute } from "@react-navigation/native";
import AsyncStorage from "@react-native-async-storage/async-storage";
import theme from "../../../utils/theme";
import LottieView from "lottie-react-native";
import AuthActions from "../../../actions/AuthActions";
import { useDispatch } from "react-redux";
import TextComponent from "../../components/TextComponent";
import { useTranslation } from "react-i18next";
// import { useAuthContext } from "../../hooks/useAuthContext";
const SplashScreen = ({ navigation }) => {
  const backgroundFade = useRef(new Animated.Value(0))?.current;
  const logoFade = useRef(new Animated.Value(0))?.current;
  const logoMovement = useRef(new Animated.Value(0))?.current;
  const authActions = AuthActions();
  const dispatch = useDispatch();
  const { t } = useTranslation();
  useFocusEffect(
    useCallback(() => {
      Animated.timing(backgroundFade, {
        toValue: 1,
        duration: 2000,
        useNativeDriver: false,
      }).start();
      Animated.timing(logoFade, {
        toValue: 1,
        duration: 2000,
        useNativeDriver: false,
      }).start();
      setTimeout(() => {
        Animated.timing(logoMovement, {
          toValue: -50,
          duration: 2000,
          easing: Easing.inOut(Easing.exp),
          useNativeDriver: false,
        }).start(() => {
          dispatch(authActions.checkLanguageInStorage());
          dispatch(authActions.checkUserInStorage());
        });
      }, 1000);
    }, [])
  );

  return (
    <View style={{ flex: 1 }}>
      <Animated.View style={styles.container}>
        <Animated.View
          style={{
            width: "100%",
            alignItems: "center",
            opacity: logoFade,
            transform: [{ translateY: logoMovement }],
          }}
        >
          {/* <Image
            source={require("../../../assets/imgs/logo.png")}
            style={styles.img}
          /> */}
          <TextComponent style={{ color: theme.COLORS.white, fontSize: 35 }}>
            {t("وَتين")}
          </TextComponent>
        </Animated.View>
        <View style={styles.loadersRow}>
          <LottieView
            source={require("../../../assets/loaders/loading_cat.json")}
            style={styles.loaderIcon}
            autoPlay
            loop
          />
        </View>
      </Animated.View>
    </View>
  );
};

export default SplashScreen;

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: theme.COLORS.primary,
    justifyContent: "center",
    alignItems: "center",
  },
  loaderIcon: {
    width: 100,
    height: 100,
    margin: 5,
  },
  loadersRow: {
    flexDirection: "row",
    alignItems: "center",
    justifyContent: "center",
    width: "100%",
  },
  loaderWrapper: {
    ...Platform.select({
      android: {
        height: 150,
      },
      ios: {
        height: 80,
      },
    }),
    width: "100%",
    alignSelf: "center",
    position: "absolute",
    bottom: 0,
    zIndex: 10,
  },
  bottomWrapper: {
    width: "100%",
    height: "100%",
    alignSelf: "flex-start",
    position: "absolute",
    bottom: 0.5,
    left: 0,
  },
  halfCircle: {
    width: "100%",
    backgroundColor: "#EEFBF3",
    height: "100%",
    borderRadius: 12,
  },
  img: {
    width: "60%",
    height: 100,
    resizeMode: "contain",
  },
  versionTxtWrapper: {
    position: "absolute",
    bottom: 0,
    right: 0,
    padding: 10,
    width: "100%",
    flexDirection: "row",
    alignItems: "center",
    justifyContent: "flex-end",
    zIndex: 2,
  },
});
