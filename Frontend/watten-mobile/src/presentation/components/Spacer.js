import { View } from "react-native";

const Spacer = ({ space, styles }) => {
  return <View style={{ padding: space, ...styles }}></View>;
};

export default Spacer;
