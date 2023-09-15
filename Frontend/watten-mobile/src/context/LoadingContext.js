import { createContext, useEffect, useState } from "react";
import LottieView from "lottie-react-native";

import { View, StyleSheet, ActivityIndicator } from "react-native";
export const LoadingContext = createContext(false);

export const LoadingContextProvider = ({ children }) => {
  const [loadingSkelton, setLoadingSkelton] = useState(false);
  const [loading, setLoading] = useState(false);

  return (
    <LoadingContext.Provider
      value={{
        loading,
        setLoading,
        loadingSkelton,
        setLoadingSkelton,
      }}
    >
      {children}
      {loading && (
        <View style={styles.centeredContainer}>
          <ActivityIndicator size="large" color="black" />
        </View>
      )}
    </LoadingContext.Provider>
  );
};
const styles = StyleSheet.create({
  centeredContainer: {
    position: "absolute",
    top: 0,
    bottom: 0,
    left: 0,
    right: 0,
    justifyContent: "center",
    alignItems: "center",
  },
  animation: {
    width: 100,
    height: 100,
  },
});
