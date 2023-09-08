import { StyleSheet, View } from "react-native";
import React from "react";
import TextComponent from "../../../components/TextComponent";
import ReservationCard from "./ReservationCard";
import { useTranslation } from "react-i18next";
const ReservationsList = ({ total }) => {
  const { t } = useTranslation();
  return (
    <View style={styles.container}>
      <TextComponent mediumBold style={styles.textTitle}>
        {t("upcoming_sessions") + " - " + total}
      </TextComponent>

      <ReservationCard instructure={"Sabreen"} />
      <ReservationCard instructure={"Sabreen"} />
      <ReservationCard instructure={"Sabreen"} />
    </View>
  );
};

export default ReservationsList;

const styles = StyleSheet.create({
  container: {
    padding: 16,
    gap: 10,
  },
  textTitle: {
    fontSize: 22,
  },
});
