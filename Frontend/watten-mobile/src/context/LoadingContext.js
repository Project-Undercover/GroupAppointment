import { createContext, useEffect, useState } from "react";
import LottieView from "lottie-react-native";

import { View, StyleSheet, ActivityIndicator } from "react-native";
export const LoadingContext = createContext(false);

export const LoadingContextProvider = ({ children }) => {
  const [loadingSkelton, setLoadingSkelton] = useState(false);
  const [loading, setLoading] = useState(false);

  const loaderSource = require(`../assets/loaders/loading_cat.json`);
  return (
    <LoadingContext.Provider
      value={{ loading, setLoading, loadingSkelton, setLoadingSkelton }}
    >
      {children}
      {loading && (
        <View style={styles.centeredContainer}>
          <ActivityIndicator size="large" color="black" />
          {/* <LottieView
            source={loaderSource}
            autoPlay
            loop
            style={styles.animation}
          /> */}
        </View>
      )}
    </LoadingContext.Provider>
  );
};
const styles = StyleSheet.create({
  centeredContainer: {
    position: "absolute",
    // backgroundColor: "rgba(0, 0, 0,0.1)",
    top: 0,
    bottom: 0,
    left: 0,
    right: 0,
    zIndex: 999,
    justifyContent: "center",
    alignItems: "center",
  },
  animation: {
    width: 100,
    height: 100,
  },
});
