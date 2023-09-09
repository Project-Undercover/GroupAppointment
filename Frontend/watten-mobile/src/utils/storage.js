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

export const getToken = async () => {
  try {
    const token = await AsyncStorage.getItem("token");
    if (token == null) return null;
    return JSON.parse(token);
  } catch (e) {
    // console.log('getToken:', e);
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

export const storeData = async ({ token, user }) => {
  try {
    await AsyncStorage.setItem("token", JSON.stringify(token));
    await AsyncStorage.setItem("user", JSON.stringify(user));
  } catch (e) {
    console.log(e);
  }
};

export const storeLanguage = async (language) => {
  try {
    await AsyncStorage.setItem("language", language);
  } catch (e) {
    console.log(e);
  }
};

export const cleanData = async () => {
  try {
    await AsyncStorage.removeItem("user");
    await AsyncStorage.removeItem("token");
  } catch (e) {
    console.log("Error removing data:", e);
  }
};
