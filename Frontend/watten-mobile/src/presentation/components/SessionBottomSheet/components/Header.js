import { StyleSheet, Text, TouchableOpacity, View } from "react-native";
import React from "react";
import TextComponent from "../../TextComponent";
import { AntDesign } from "@expo/vector-icons";
import theme from "../../../../utils/theme";
const Header = ({ title, onClose }) => {
  return (
    <View className="flex-row items-center justify-between w-full mb-5">
      <TextComponent mediumBold style={styles.titleText}>
        {title}
      </TextComponent>
      <TouchableOpacity onPress={onClose}>
        <AntDesign name="closecircleo" size={22} color={theme.COLORS.primary} />
      </TouchableOpacity>
    </View>
  );
};

export default Header;

const styles = StyleSheet.create({
  titleText: {
    fontSize: 15,
  },
});
