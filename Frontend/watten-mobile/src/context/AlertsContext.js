import { createContext } from "react";
import { useToast } from "react-native-toast-notifications";
import theme from "../utils/theme";
export const AlertsContext = createContext();

export const AlertsContextProvider = ({ children }) => {
  const toast = useToast();

  const showSuccess = (text) => {
    toast.show(text, {
      type: "success",
      dangerColor: theme.COLORS.green2,
      placement: "top",
      duration: 3000,
      offset: 30,
      animationType: "slide-in",
    });
  };

  const showError = (text) => {
    toast.show(text, {
      type: "danger",
      dangerColor: theme.COLORS.red,
      placement: "top",
      duration: 4000,
      offset: 30,
      animationType: "slide-in",
    });
  };
  const showWarning = (text) => {
    toast.show(text, {
      type: "warning",
      dangerColor: "yellow",
      placement: "top",
      duration: 4000,
      offset: 30,
      animationType: "slide-in",
    });
  };

  return (
    <AlertsContext.Provider value={{ showError, showWarning, showSuccess }}>
      {children}
    </AlertsContext.Provider>
  );
};
