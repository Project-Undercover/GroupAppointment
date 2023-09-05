import { Text, TouchableOpacity, StyleSheet } from "react-native";
import React from "react";
import theme from "../../utils/theme";
const DefaultButton = ({ text, onPress, containerStyle, textStyle, icon }) => {
  return (
    <TouchableOpacity
      activeOpacity={0.7}
      onPress={onPress}
      style={{ ...styles.button, ...containerStyle }}
    >
      {icon}
      <Text style={[styles.text, { ...textStyle }]}>{text}</Text>
    </TouchableOpacity>
  );
};

const styles = StyleSheet.create({
  button: {
    width: 301,
    height: 50,
    borderRadius: 13,
    borderWidth: 1,
    borderColor: "white",
    backgroundColor: theme.COLORS.secondaryPrimary,
    justifyContent: "center",
    alignItems: "center",
    flexDirection: "row",
    gap: 4,
  },
  text: {
    fontFamily: theme.FONTS.primaryFontBold,
    fontSize: 16,
    color: theme.COLORS.white,
  },
});

export default DefaultButton;
