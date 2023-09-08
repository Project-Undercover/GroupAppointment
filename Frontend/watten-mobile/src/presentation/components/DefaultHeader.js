import {
  StyleSheet,
  View,
  Image,
  TouchableOpacity,
  I18nManager,
} from "react-native";
import React from "react";
import TextComponent from "./TextComponent";
import theme from "../../utils/theme";
import { AntDesign } from "@expo/vector-icons";
import { useNavigation } from "@react-navigation/native";
const DefaultHeader = ({ title }) => {
  const navigation = useNavigation();
  return (
    <View style={styles.container}>
      <TextComponent semiBold style={styles.title}>
        {title}
      </TextComponent>
      <TouchableOpacity
        style={styles.iconContainer}
        onPress={() => navigation.goBack()}
      >
        <AntDesign
          name={I18nManager.allowRTL ? "left" : "right"}
          size={24}
          color={theme.COLORS.white}
        />
      </TouchableOpacity>
    </View>
  );
};

export default DefaultHeader;

const styles = StyleSheet.create({
  container: {
    backgroundColor: theme.COLORS.primary,
    height: 55,
    flexDirection: "row",
    justifyContent: "center",
    alignItems: "center",
    paddingTop: 2,
  },
  title: {
    fontSize: 20,
    color: theme.COLORS.white,
  },
  iconContainer: {
    position: "absolute",
    end: 10,
  },
});
