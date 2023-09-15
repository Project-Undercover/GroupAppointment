import {
  ScrollView,
  StyleSheet,
  TouchableOpacity,
  View,
  Image,
} from "react-native";
import React from "react";
import CustomeStatusBar from "./CustomeStatusBar";
import { AntDesign } from "@expo/vector-icons";
import theme from "../../utils/theme";
import TextComponent from "./TextComponent";
import { Ionicons } from "@expo/vector-icons";
import { useTranslation } from "react-i18next";
import DefaultButton from "./DefaultButton";
import { useDispatch } from "react-redux";
import AuthActions from "../../actions/AuthActions";

const DrawerContent = ({ navigation }) => {
  const authActions = AuthActions();
  const dispatch = useDispatch();
  const { t } = useTranslation();
  const closeDrawer = () => {
    navigation.closeDrawer();
  };

  const handleLogout = () => {
    dispatch(authActions.logout());
  };

  const navigateHistory = () => {
    navigation.navigate("history");
  };

  return (
    <ScrollView style={{ flex: 1 }} contentContainerStyle={{ flexGrow: 1 }}>
      <CustomeStatusBar />
      <TouchableOpacity className="px-2 py-4" onPress={closeDrawer}>
        <AntDesign
          name={"left"}
          size={25}
          color={theme.COLORS.secondaryPrimary}
        />
      </TouchableOpacity>
      <View className="flex-1">
        <View className="items-center">
          <Image
            style={styles.image}
            source={require("../../assets/icons/user.png")}
          />
          <View className="items-center mt-3">
            <TextComponent style={styles.nameText} mediumBold>
              Wissam kabha
            </TextComponent>
            <TextComponent style={styles.typeText}>admin</TextComponent>
          </View>
        </View>
        <View className="flex-1 mt-8">
          <TouchableOpacity style={styles.navItem} onPress={navigateHistory}>
            <Ionicons
              name="archive-outline"
              size={20}
              color={theme.COLORS.primary}
            />
            <TextComponent style={styles.navItemText}>
              {t("history")}
            </TextComponent>
          </TouchableOpacity>
        </View>
        <View className="items-center gap-2  mb-10">
          <DefaultButton
            text={t("logout")}
            containerStyle={{ width: "90%" }}
            onPress={handleLogout}
          />
          <View className="flex-row">
            <TextComponent className="mx-2">{t("version")}</TextComponent>
            <TextComponent>{"12.3.4"}</TextComponent>
          </View>
        </View>
      </View>
    </ScrollView>
  );
};

export default DrawerContent;

const styles = StyleSheet.create({
  image: {
    width: 120,
    height: 120,
  },
  nameText: {
    fontSize: 22,
  },
  typeText: {
    fontSize: 15,
    color: theme.COLORS.primary,
  },
  navItem: {
    flexDirection: "row",
    alignItems: "center",
    gap: 4,
    borderBottomWidth: 1,
    borderColor: theme.COLORS.primary,
    padding: 10,
  },
  navItemText: {
    fontSize: 18,
  },
});
