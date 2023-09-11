import { StyleSheet, I18nManager } from "react-native";
import theme from ".";
const globalStyles = StyleSheet.create({
  underLine: {
    height: 2,
    borderBottomEndRadius: 20,
    borderBottomStartRadius: 20,
    backgroundColor: theme.COLORS.secondaryPrimary,
  },
  input: {
    flex: 1,
    padding: 10,
    paddingLeft: 0,
    backgroundColor: "#fff",
    color: "#424242",
    fontSize: 14,
    fontFamily: theme.FONTS.primaryFontRegular,
    textAlign: I18nManager.isRTL ? "right" : "left",
  },
  noSessionsText: {
    fontSize: 20,
    color: theme.COLORS.primary,
  },
});

export default globalStyles;
