import { StyleSheet, Text, View, Image } from "react-native";
import React from "react";
import theme from "../../../utils/theme";
import * as Progress from "react-native-progress";
import TextComponent from "../TextComponent";
import SessionInfo from "./SessionInfo";

const SessionCard = ({ maxParticipants = 30, totalParticipant = 5 }) => {
  return (
    <View style={styles.container}>
      <View style={styles.imagePartContainer}>
        <View style={styles.imgContainer}>
          <Image
            style={styles.img}
            source={require("../../../assets/icons/image.png")}
          />
        </View>
        <View style={styles.participantsContainer}>
          <TextComponent style={styles.progressText}>
            Participants {totalParticipant + "/" + maxParticipants}{" "}
          </TextComponent>
          <Progress.Bar progress={0.3} width={100} color={theme.COLORS.white} />
        </View>
      </View>
      <SessionInfo />
    </View>
  );
};

export default SessionCard;

const styles = StyleSheet.create({
  container: {
    padding: 5,
    width: "100%",
    backgroundColor: theme.COLORS.white,
    borderRadius: 5,
    flexDirection: "row",
    justifyContent: "flex-start",
    ...theme.SHADOW.lightShadow,
  },
  imagePartContainer: {
    width: 120,
    height: "100%",
    position: "relative",
    borderRadius: 4,
    borderWidth: 1,
    borderColor: theme.COLORS.lightGray2,

    overflow: "hidden",
  },
  participantsContainer: {
    backgroundColor: "rgba(0, 0, 0, 0.15)",
    height: 35,
    blurRadius: 7.5,
    position: "absolute",
    bottom: 0,
    width: "100%",
    alignItems: "center",
    justifyContent: "center",
  },
  imgContainer: {
    flex: 1,
    alignItems: "center",
    justifyContent: "center",
  },
  img: {
    width: 90,
    height: 90,
  },
  progressText: {
    fontSize: 11,
    color: theme.COLORS.white,
  },
  infoTitle: {
    fontSize: 14,
  },
});
