import {
  View,
  StyleSheet,
  Modal,
  TouchableWithoutFeedback,
  TouchableOpacity,
  ImageBackground,
  SafeAreaView,
} from "react-native";
import theme from "../../../utils/theme";
import { useTranslation } from "react-i18next";
import { AntDesign } from "@expo/vector-icons";
const ImageExpandModal = ({ setShowModal, visible, image }) => {
  const { t } = useTranslation();
  return (
    <Modal
      animationType="fade"
      transparent={true}
      visible={visible}
      onRequestClose={() => setShowModal(false)}
    >
      <SafeAreaView />
      <TouchableWithoutFeedback onPress={() => setShowModal(false)}>
        <View style={styles.modal}>
          <View style={styles.modalContainer}>
            <View style={styles.modalHeader}>
              <TouchableOpacity onPress={() => setShowModal(false)}>
                <AntDesign
                  name="closecircleo"
                  size={22}
                  color={theme.COLORS.primary}
                />
              </TouchableOpacity>
            </View>
            <View style={styles.modalBody}>
              <ImageBackground
                source={{ uri: image }}
                resizeMode="contain"
                style={{
                  flex: 1,
                  width: "100%",
                }}
              />
            </View>
          </View>
        </View>
      </TouchableWithoutFeedback>
    </Modal>
  );
};
export default ImageExpandModal;

const styles = StyleSheet.create({
  container: {
    flex: 1,
  },
  modal: {
    backgroundColor: "rgba(0,0,0,0.2)",
    height: "100%",
    width: "100%",
    justifyContent: "center",
    alignItems: "center",
  },
  modalBody: {
    justifyContent: "center",
    alignItems: "center",
    flex: 1,
  },
  modalHeader: {
    paddingHorizontal: 15,
    paddingVertical: 10,
    alignItems: "flex-start",
  },
  modalContainer: {
    backgroundColor: theme.COLORS.white,
    overflow: "hidden",
    width: "95%",
    height: "50%",
    borderRadius: 7,
  },
});
