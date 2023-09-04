import { StyleSheet, View, Image, TouchableOpacity } from "react-native";
import React from "react";
import TextComponent from "./TextComponent";
import theme from "../../utils/theme";
import { Feather } from "@expo/vector-icons";
const AppHeader = () => {
  return (
    <View style={styles.container}>
      <View style={styles.titleContainer}>
        <TextComponent mediumBold style={styles.titleText}>
          Watten
        </TextComponent>
        <Image
          style={styles.img}
          source={require("../../assets/icons/snake.png")}
        />
      </View>
      <TouchableOpacity className="px-5">
        <Feather name="menu" size={30} color={theme.COLORS.white} />
      </TouchableOpacity>
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
    borderStartEndRadius: 5,
    borderEndEndRadius: 5,

    backgroundColor: "rgba(255, 255, 255, 0.83)",
    position: "relative",
    paddingHorizontal: 4,
    paddingVertical: 6,
  },
  titleText: {
    textAlign: "auto",
    fontSize: 14,
  },
  img: {
    width: 28,
    height: 28,
    position: "absolute",

    top: -8,
    end: -5,
  },
});
