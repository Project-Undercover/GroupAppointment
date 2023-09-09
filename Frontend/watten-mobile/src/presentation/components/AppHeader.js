import { StyleSheet, View, Image, TouchableOpacity } from "react-native";
import React from "react";
import TextComponent from "./TextComponent";
import theme from "../../utils/theme";
import { Feather } from "@expo/vector-icons";
import { useTranslation } from "react-i18next";
import { useNavigation } from "@react-navigation/native";
const AppHeader = () => {
  const { t } = useTranslation();
  const navigation = useNavigation();

  const openDrawer = () => {
    navigation.openDrawer();
  };

  return (
    <View style={styles.container}>
      <TouchableOpacity className="px-5" onPress={openDrawer}>
        <Feather name="menu" size={30} color={theme.COLORS.white} />
      </TouchableOpacity>
      <View style={styles.titleContainer}>
        <TextComponent mediumBold style={styles.titleText}>
          {t("watten")}
        </TextComponent>
        <View style={styles.point}></View>
      </View>
    </View>
  );
};

export default AppHeader;

const styles = StyleSheet.create({
  container: {
    backgroundColor: theme.COLORS.primary,
    height: 55,
    flexDirection: "row",
    justifyContent: "space-between",
    alignItems: "center",
    paddingTop: 2,
  },
  titleContainer: {
    // height: 24,
    width: 80,
    borderStartStartRadius: 5,
    borderEndStartRadius: 5,
    flexDirection: "row",
    justifyContent: "space-around",
    alignItems: "center",

    backgroundColor: "rgba(255, 255, 255, 0.83)",
    position: "relative",
    paddingHorizontal: 4,
    paddingVertical: 6,
  },
  titleText: {
    fontSize: 14,
  },
  img: {
    width: 28,
    height: 28,
    position: "absolute",
    transform: [{ rotate: "180deg" }],
    top: -8,
    end: -5,
  },
  point: {
    width: 10,
    height: 10,
    borderRadius: 30,
    backgroundColor: theme.COLORS.secondary,
    ...theme.SHADOW.lightShadow,
  },
});
