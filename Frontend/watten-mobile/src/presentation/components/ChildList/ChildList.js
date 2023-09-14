import { StyleSheet, Text, View } from "react-native";
import React from "react";
import { ScrollView } from "react-native-gesture-handler";
import ChildCard from "./components/ChildCard";

const ChildList = ({ data, showCloseChildIcon }) => {
  return (
    <ScrollView
      horizontal={true}
      style={{ flex: 1, width: "100%" }}
      showsHorizontalScrollIndicator={false}
    >
      {data?.map((child) => {
        return (
          <ChildCard
            value={child?.name}
            key={child?.id}
            showCloseChildIcon={showCloseChildIcon}
          />
        );
      })}
    </ScrollView>
  );
};

export default ChildList;
