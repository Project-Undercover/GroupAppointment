import { StyleSheet, Text, View } from "react-native";
import React from "react";
import { ScrollView } from "react-native-gesture-handler";
import ChildCard from "./components/ChildCard";

const ChildList = () => {
  return (
    <ScrollView
      horizontal={true}
      style={{ flex: 1, width: "100%" }}
      showsHorizontalScrollIndicator={false}
    >
      <ChildCard value={"wissam"} />
      <ChildCard value={"wissam"} />
      <ChildCard value={"wissam"} />
      <ChildCard value={"wissam"} />
      <ChildCard value={"wissam"} />
    </ScrollView>
  );
};

export default ChildList;
