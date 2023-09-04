import { View, Text, TextInput, StyleSheet, I18nManager } from "react-native";
import { useRef } from "react";
import theme from "../../utils/theme";
import TextComponent from "./TextComponent";

const DefaultInput = ({
  label,
  value,
  icon,
  placeholder,
  containerStyle,
  inputStyle,
  component,
  onFocus,
  handleChange,
}) => {
  const ref = useRef();
  return (
    <View style={[styles.wrapper]}>
      <TextComponent style={styles.label}>{label}</TextComponent>
      <View style={[styles.container, { ...containerStyle }]}>
        {icon && <View style={styles.iconContainer}>{icon}</View>}
        {!component ? (
          <TextInput
            ref={ref}
            onFocus={() => onFocus(ref)}
            // onChange={() => handleChange()}
            style={[styles.input, { ...inputStyle }]}
            placeholder={placeholder}
            underlineColorAndroid="transparent"
          />
        ) : (
          component
        )}
      </View>
    </View>
  );
};

export default DefaultInput;
const styles = StyleSheet.create({
  container: {
    height: 45,
    flexDirection: "row",
    justifyContent: "center",
    alignItems: "center",
    backgroundColor: "#fff",
    borderWidth: 1,
    borderColor: theme.COLORS.secondary2,
    borderRadius: 5,
    overflow: "hidden",
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
  iconContainer: {
    padding: 10,
  },
  label: {
    fontSize: 15,
    color: theme.COLORS.secondaryPrimary,
    paddingStart: 15,
    marginBottom: 5,
  },
});
