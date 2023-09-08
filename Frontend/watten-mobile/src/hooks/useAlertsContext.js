import { AlertsContext } from "../context/AlertsContext";
import { useContext } from "react";

export const useAlertsContext = () => {
  const context = useContext(AlertsContext);

  if (!context) {
    throw Error("useAlertsContext must be used inside an AlertsProvider");
  }

  return context;
};
