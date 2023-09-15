import React from "react";
import { Switch, View, Text, StyleSheet } from "react-native";
import TextComponent from "./TextComponent";
import theme from "../../utils/theme";
const CustomSwitch = ({ label, value, onValueChange }) => {
  return (
    <View style={styles.container}>
      <TextComponent mediumBold style={styles.label}>
        {label}
      </TextComponent>
      <Switch
        value={value}
        onValueChange={onValueChange}
        trackColor={{ false: theme.COLORS.green, true: theme.COLORS.green }}
        ios_backgroundColor={theme.COLORS.red}
      />
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flexDirection: "row",
    alignItems: "center",
    justifyContent: "space-between",
    paddingVertical: 8,
    width: "100%",
  },
  label: {
    fontSize: 15,
    color: theme.COLORS.secondaryPrimary,
    paddingStart: 15,
    marginBottom: 5,
  },
});

export default CustomSwitch;
