import { View, Text, TextInput, StyleSheet, I18nManager } from "react-native";
import { useRef } from "react";
import theme from "../../utils/theme";
import TextComponent from "./TextComponent";
import globalStyles from "../../utils/theme/globalStyles";
const DefaultInput = ({
  label,
  value,
  icon,
  placeholder,
  containerStyle,
  wrapperStyle,
  inputStyle,
  component,
  onFocus = () => {},
  handleChange = () => {},
  onChange = () => {},
}) => {
  const ref = useRef();
  return (
    <View style={[styles.wrapper, { ...wrapperStyle }]}>
      <TextComponent style={styles.label}>{label}</TextComponent>
      <View style={[styles.container, { ...containerStyle }]}>
        {icon && <View style={styles.iconContainer}>{icon}</View>}
        {!component ? (
          <TextInput
            ref={ref}
            onFocus={() => onFocus(ref)}
            onChange={onChange}
            style={[globalStyles.input, { ...inputStyle }]}
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
  wrapper: {},
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
