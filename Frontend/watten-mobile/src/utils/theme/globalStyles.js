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
  errorText: {
    fontSize: 12,
    textAlign: "left",
    color: theme.COLORS.red,
    paddingStart: 5,
  },

  dropDownInput: {
    height: 48,
    flexDirection: "row",
    justifyContent: "center",
    alignItems: "center",
    backgroundColor: "#fff",
    borderWidth: 1,
    borderColor: theme.COLORS.secondary2,
    borderRadius: 5,
    overflow: "hidden",
  },
});

export default globalStyles;
