import { StyleSheet, Text, View, SafeAreaView } from "react-native";
import React from "react";
import TextComponent from "../../components/TextComponent";
import { useTranslation } from "react-i18next";

const Splash = () => {
  const { t } = useTranslation();
  return (
    <View>
      <SafeAreaView />
      <TextComponent>{t("greeting")}</TextComponent>
    </View>
  );
};

export default Splash;

const styles = StyleSheet.create({});
