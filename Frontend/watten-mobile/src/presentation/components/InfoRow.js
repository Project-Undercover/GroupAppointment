import { StyleSheet, Text, View } from "react-native";
import React from "react";
import TextComponent from "./TextComponent";
import theme from "../../utils/theme";

const InfoRow = ({ icon, value }) => {
  return (
    <View style={styles.container}>
      <View style={styles.iconContainer}>{icon}</View>
      <TextComponent style={styles.valueText}>{value}</TextComponent>
    </View>
  );
};

export default InfoRow;

const styles = StyleSheet.create({
  container: {
    flexDirection: "row",
    justifyContent: "center",
    alignItems: "center",
    gap: 4,
  },
  valueText: {
    fontSize: 13,
    color: theme.COLORS.secondaryPrimary,
  },
  iconContainer: {
    justifyContent: "center",
    alignItems: "center",
  },
});
