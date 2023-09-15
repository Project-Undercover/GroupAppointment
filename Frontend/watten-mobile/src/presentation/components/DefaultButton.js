import { Text, TouchableOpacity, StyleSheet } from "react-native";
import React from "react";
import theme from "../../utils/theme";
const DefaultButton = ({
  text,
  onPress,
  disabled,
  containerStyle,
  textStyle,
  icon,
}) => {
  return (
    <TouchableOpacity
      activeOpacity={0.7}
      onPress={onPress}
      disabled={disabled}
      style={{ ...styles.button, ...containerStyle }}
    >
      <Text style={[styles.text, { ...textStyle }]}>{text}</Text>
      {icon}
    </TouchableOpacity>
  );
};

const styles = StyleSheet.create({
  button: {
    width: "100%",
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
    padding: 4,
  },
});

export default DefaultButton;
