import { View, Image, StyleSheet } from "react-native";
import React from "react";
import TextComponent from "../../../components/TextComponent";
import { useTranslation } from "react-i18next";
const WelcomeBanner = ({ username, date }) => {
  const { t } = useTranslation();
  return (
    <View className="p-5 mt-5 items-start">
      <View className="flex-row items-center gap-4">
        <TextComponent mediumBold style={styles.textMain}>
          {t("greeting") + ", " + username}
        </TextComponent>
        <Image
          className="w-10 h-10"
          source={require("../../../../assets/icons/wave.png")}
        />
      </View>
      <TextComponent mediumBold style={styles.textDate}>
        {date}
      </TextComponent>
    </View>
  );
};

const styles = StyleSheet.create({
  textMain: {
    fontSize: 27,
  },
  textDate: {
    fontSize: 15,
  },
});

export default WelcomeBanner;
