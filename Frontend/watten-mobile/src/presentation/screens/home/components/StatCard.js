import { StyleSheet, Image, View } from "react-native";
import React from "react";
import TextComponent from "../../../components/TextComponent";
import theme from "../../../../utils/theme";

const StatCard = ({ value, label, iconPath }) => {
  return (
    <View style={styles.container}>
      <View style={styles.valueContainer}>
        <Image style={styles.image} source={iconPath} />

        <TextComponent mediumBold style={styles.valueText}>
          {value}
        </TextComponent>
      </View>
      <TextComponent style={styles.labelText}>{label}</TextComponent>
    </View>
  );
};

export default StatCard;

const styles = StyleSheet.create({
  container: {
    backgroundColor: theme.COLORS.white,
    paddingHorizontal: 10,
    justifyContent: "space-between",
    alignItems: "flex-start",
    // alignItems: "stretch",
    paddingVertical: 20,
    paddingBottom: 0,
    width: "50%",
    borderRadius: 12,
    ...theme.SHADOW.lightShadow,
  },
  valueContainer: {
    flexDirection: "row",
    alignItems: "center",
    gap: 15,
  },
  image: {
    width: 40,
    height: 40,
  },
  valueText: {
    fontSize: 25,
  },
  labelText: {
    fontSize: 16,
    paddingHorizontal: 3,
    paddingVertical: 10,
  },
});
