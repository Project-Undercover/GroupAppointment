import { StyleSheet, TouchableOpacity, View } from "react-native";
import React from "react";
import globalStyles from "../../../../utils/theme/globalStyles";
import TextComponent from "../../../components/TextComponent";
import { AntDesign } from "@expo/vector-icons";
import theme from "../../../../utils/theme";
import { Mode } from "../../../../utils/Enums";
import { useTranslation } from "react-i18next";
const UsersBar = ({ navigation }) => {
  const { t } = useTranslation();
  const handlePressAddUser = () => {
    navigation.navigate("user-manager", {
      mode: Mode.Add,
    });
  };
  return (
    <View className="flex-row justify-between">
      <View className="w-auto" style={{ alignSelf: "flex-start" }}>
        <TextComponent mediumBold style={styles.titleText}>
          {t("users")}
        </TextComponent>
        <View style={globalStyles.underLine}></View>
      </View>
      <TouchableOpacity activeOpacity={0.5} onPress={handlePressAddUser}>
        <AntDesign name="plussquare" size={26} color={theme.COLORS.primary} />
      </TouchableOpacity>
    </View>
  );
};

export default UsersBar;

const styles = StyleSheet.create({
  titleText: {
    fontSize: 18,
  },
});
