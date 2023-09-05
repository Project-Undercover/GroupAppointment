import React, { useState } from "react";
import { View, Text, StyleSheet, TouchableOpacity } from "react-native";
import theme from "../../utils/theme";
const RadioButton = ({ isActive, handleSelectOption, option }) => {
  return (
    <TouchableOpacity
      style={styles.radioButton}
      onPress={() => handleSelectOption(option)}
    >
      <View
        style={[
          styles.radioOuterCircle,
          isActive && styles.radioOuterCircleSelected,
        ]}
      >
        {isActive && <View style={styles.radioInnerCircle} />}
      </View>
    </TouchableOpacity>
  );
};

const styles = StyleSheet.create({
  container: {
    flexDirection: "column",
  },
  radioButton: {
    flexDirection: "row",
    alignItems: "center",
    marginVertical: 5,
  },
  radioOuterCircle: {
    height: 17,
    width: 17,
    borderRadius: 10,
    borderWidth: 2,
    borderColor: theme.COLORS.primary,
    justifyContent: "center",
    alignItems: "center",
    marginRight: 10,
  },
  radioOuterCircleSelected: {
    borderColor: theme.COLORS.primary,
  },
  radioInnerCircle: {
    height: 10,
    width: 10,
    borderRadius: 5,
    backgroundColor: theme.COLORS.primary,
  },
});

export default RadioButton;
