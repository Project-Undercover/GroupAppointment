import { StyleSheet, Text, View } from "react-native";
import React from "react";
import CustomeStatusBar from "../../components/CustomeStatusBar";
import DefaultHeader from "../../components/DefaultHeader";
import { useTranslation } from "react-i18next";

const History = () => {
  const { t } = useTranslation();
  return (
    <View className="flex-1">
      <CustomeStatusBar />
      <DefaultHeader title={t("history")} />
    </View>
  );
};

export default History;

const styles = StyleSheet.create({});
