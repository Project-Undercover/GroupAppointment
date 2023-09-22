import React from "react";
import { View, Animated, StyleSheet } from "react-native";

const CustomeSkeleton = ({ customeStyle = {} }) => {
  const shimmerAnimation = new Animated.Value(0);

  Animated.loop(
    Animated.sequence([
      Animated.timing(shimmerAnimation, {
        toValue: 1,
        duration: 1000,
        useNativeDriver: false,
      }),
      Animated.timing(shimmerAnimation, {
        toValue: 0,
        duration: 1000,
        useNativeDriver: false,
      }),
    ])
  ).start();

  const interpolatedColor = shimmerAnimation.interpolate({
    inputRange: [0, 1],
    outputRange: ["#e0e0e0", "#f0f0f0"],
  });

  return (
    <Animated.View
      style={[
        styles.shimmer,
        customeStyle,
        {
          backgroundColor: interpolatedColor,
        },
      ]}
    />
  );
};

const styles = StyleSheet.create({
  shimmer: {
    width: "100%",
    height: "100%",
    borderRadius: 5,
    backgroundColor: "#f5f5f5",
    marginVertical: 5,
  },
});

export default CustomeSkeleton;
