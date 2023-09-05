import { StyleSheet, View } from "react-native";
import React from "react";
import TextComponent from "../../../components/TextComponent";
import ReservationCard from "./ReservationCard";
const ReservationsList = ({ total }) => {
  return (
    <View style={styles.container}>
      <TextComponent mediumBold style={styles.textTitle}>
        Upcoming Reservation - {total}
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
