import { StyleSheet, Text, View } from "react-native";
import React from "react";
import UserSkelton from "./UserSkelton";

const UsersSkelton = ({ numOfItems }) => {
  const skeletonItems = Array.from({ length: numOfItems }, (_, index) => (
    <UserSkelton key={index} />
  ));
  return <View style={styles.container}>{skeletonItems}</View>;
};

export default UsersSkelton;

const styles = StyleSheet.create({
  container: {
    padding: 5,
  },
});
