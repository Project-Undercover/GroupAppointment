import AsyncStorage from "@react-native-async-storage/async-storage";
export const getDataFromStorage = async () => {
  try {
    const userJson = await AsyncStorage.getItem("user");
    const tokenJson = await AsyncStorage.getItem("token");
    return {
      user: userJson ? JSON.parse(userJson) : null,
      token: tokenJson ? JSON.parse(tokenJson) : null,
    };
  } catch (e) {
    // console.log(e);
  }
};

export const getLanguageFromStorage = async () => {
  try {
    const language = await AsyncStorage.getItem("language");
    // return language;
    return language ? language : "he";
  } catch (e) {
    // console.log(e);
  }
};

export const getLocationServiceFromStorage = async () => {
  try {
    const service = await AsyncStorage.getItem("location_service");
    // console.log('storage service', service);
    // return language;
    return service ? service : "waze";
  } catch (e) {
    // console.log(e);
  }
};
