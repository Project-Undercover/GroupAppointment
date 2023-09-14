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
  keyboardType,
  editable = true,
  onFocus = () => {},
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
            value={value}
            ref={ref}
            onFocus={() => onFocus(ref)}
            onChangeText={onChange}
            editable={editable}
            style={[globalStyles.input, { ...inputStyle }]}
            placeholder={placeholder}
            underlineColorAndroid="transparent"
            keyboardType={keyboardType}
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
    justifyContent: "flex-start",
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
