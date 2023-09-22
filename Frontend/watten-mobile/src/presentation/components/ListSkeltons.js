import { StyleSheet, Text, View } from "react-native";
import React from "react";
import CustomeSkeleton from "./CustomeSkeleton";
const ListSkeltons = ({
  numOfItems,
  dir,
  itemWidth,
  itemHeight,
  itemRadius,
}) => {
  const skeletonItems = Array.from({ length: numOfItems }, (_, index) => (
    <CustomeSkeleton
      key={index}
      customeStyle={{
        width: itemWidth,
        height: itemHeight,
        borderRadius: itemRadius,
      }}
    />
  ));
  return <View style={styles.container}>{skeletonItems}</View>;
};

export default ListSkeltons;

const styles = StyleSheet.create({
  container: {
    padding: 5,
  },
});
