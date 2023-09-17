import {
  View,
  StyleSheet,
  Modal,
  TouchableWithoutFeedback,
  TouchableOpacity,
  Text,
} from "react-native";
import theme from "../../../utils/theme";
import TextComponent from "../TextComponent";
import { useTranslation } from "react-i18next";
import LottieView from "lottie-react-native";
import DefaultButton from "../DefaultButton";
const SuccessModal = ({ setShowModal, visible, message }) => {
  const { t } = useTranslation();
  return (
    <Modal
      animationType="fade"
      transparent={true}
      visible={visible}
      onRequestClose={() => setShowModal(false)}
    >
      <TouchableWithoutFeedback onPress={() => setShowModal(!visible)}>
        <View style={styles.modal}>
          <View style={styles.modalContainer}>
            <View style={styles.modalHeader}>
              <View
                style={{
                  marginVertical: 10,
                  alignItems: "center",
                  justifyContent: "center",
                }}
              >
                <TextComponent style={styles.successText} bold>
                  {message}
                </TextComponent>
              </View>
            </View>
            <View style={styles.modalBody}>
              <LottieView
                source={require("../../../assets/loaders/success.json")}
                style={styles.loaderIcon}
                autoPlay
                loop
              />
              <DefaultButton
                onPress={() => setShowModal(false)}
                containerStyle={{ backgroundColor: theme.COLORS.green }}
                textStyle={{ fontSize: 17 }}
                text={t("close")}
              />
            </View>
          </View>
        </View>
      </TouchableWithoutFeedback>
    </Modal>
  );
};
export default SuccessModal;

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
    padding: 10,
    justifyContent: "center",
    alignItems: "center",
  },
  modalHeader: {
    // backgroundColor: theme.COLORS.primary,
    // borderRadius: 10,
    justifyContent: "center",
    alignItems: "center",
    // ...theme.SHADOW.shadowComponent2,
  },
  modalContainer: {
    backgroundColor: theme.COLORS.white,
    width: "80%",
    borderRadius: 10,
  },
  langBtn: {
    width: 180,
    height: 45,
    ...theme.SHADOW.shadowComponent,
    backgroundColor: theme.COLORS.white,
    borderRadius: 15,
    marginVertical: 10,
    flexDirection: "row",
    alignItems: "center",
    justifyContent: "space-around",
  },
  successText: {
    fontSize: 16,
  },
  loaderIcon: {
    width: 100,
    height: 100,
  },
});
