import { Linking, Platform } from "react-native";
import { useAlertsContext } from "../hooks/useAlertsContext";
const ExternalActions = () => {
  const { showError } = useAlertsContext();
  const callPhone = ({ phoneNumber }) => {
    const phoneNumberWithProtocol = `tel:${phoneNumber}`;
    Linking.canOpenURL(phoneNumberWithProtocol)
      .then((supported) => {
        if (!supported) {
          showError(t("phone_number_not_supported"));
        } else {
          return Linking.openURL(phoneNumberWithProtocol);
        }
      })
      .catch((error) => console.error("Error occurred:", error));
  };

  const openWhatsApp = ({ phoneNumber }) => {
    const whatsappURL = `whatsapp://send?phone=972${phoneNumber}`;

    Linking.canOpenURL(whatsappURL)
      .then((supported) => {
        return Linking.openURL(whatsappURL);
      })
      .catch((err) => console.error("An error occurred: ", err));
  };
  return { callPhone, openWhatsApp };
};

export default ExternalActions;
