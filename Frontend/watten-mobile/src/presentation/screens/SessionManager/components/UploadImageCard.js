import { StyleSheet, View, Image, TouchableOpacity } from "react-native";
import React, { useState } from "react";
import theme from "../../../../utils/theme";
import * as ImagePicker from "expo-image-picker";
import { manipulateAsync, SaveFormat } from "expo-image-manipulator";
const UploadImageCard = ({ image, handleSelectImage }) => {
  const selectImage = async () => {
    let result = await ImagePicker.launchImageLibraryAsync({
      mediaTypes: ImagePicker.MediaTypeOptions.Images,
      allowsEditing: true,
      aspect: [1, 1],
      quality: 1,
    });

    if (!result.canceled) {
      const manipulatedImage = await manipulateAsync(
        result?.assets[0]?.uri,
        [{ resize: { width: 800, height: 800 } }],
        { compress: 0.8, format: SaveFormat.JPEG }
      );
      handleSelectImage(manipulatedImage?.uri);
    }
  };
  return (
    <TouchableOpacity style={styles.container} onPress={selectImage}>
      <Image
        style={styles.image}
        source={{ uri: image }}
        defaultSource={require("../../../../assets/icons/image.png")}
      />
    </TouchableOpacity>
  );
};

export default UploadImageCard;

const styles = StyleSheet.create({
  container: {
    borderWidth: 1,
    borderColor: theme.COLORS.secondary2,
    borderStyle: "dashed",
    padding: 40,
    justifyContent: "center",
    alignItems: "center",
    borderRadius: 6,
  },
  image: {
    width: 100,
    height: 100,
    borderRadius: 4,
  },
});
